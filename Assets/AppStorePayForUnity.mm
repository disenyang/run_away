//
//  AppStorePayForUnity.mm
//  Test
//
//  Created by WarZhan on 14-10-9.
//  Copyright (c) 2014年 Kingjoy. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "AppStorePayForUnity.h"
#import "WXApi.h"

#ifndef __APPSTORE_IN_UNITY__
#define __APPSTORE_IN_UNITY__
#endif



#if __IPHONE_OS_VERSION_MAX_ALLOWED >= 70000
#define IOS7_SDK_AVAILABLE 1
#endif

#if defined(__cplusplus)
extern "C" {
#endif
    extern void       UnitySendMessage(const char* obj, const char* method, const char* msg);
    extern NSString*  AppStoreCreateNSString (const char* string);
#if defined(__cplusplus)
}
#endif


static AppStorePayForUnity* _instance = nil;

//@implementation AppStorePayForUnity : NSObject <SKProductsRequestDelegate> // , SKPaymentTransactionObserver>
//{
//    SKProduct *proUpgradeProduct;
//    SKProductsRequest *productsRequest;
//}
@implementation AppStorePayForUnity

@synthesize mCallBackObjectName;
@synthesize mServerId;
@synthesize mOrderId;
@synthesize mExInfo;    

//使用同步创建 保证多线程下也只有一个实例
+ (AppStorePayForUnity *)instance
{
    @synchronized(self)
    {
        if (_instance == nil)
        {
            _instance = [[AppStorePayForUnity alloc] init];
        }
    }
    return _instance;
}

- (id)init
{
    self = [super init];
    
    if (self)
    {
        // 监听购买结果
        [[SKPaymentQueue defaultQueue] addTransactionObserver:self];
    }
    return self;
}




//初始化 设置回调的对象
- (void)initAppStorePay:(NSString*)callBackName
{
    NSLog(@"mCallBackObjectName=%@",mCallBackObjectName);
    self.mCallBackObjectName = callBackName;
}

//是否有购买权限
- (BOOL)canMakePay
{
    return [SKPaymentQueue canMakePayments];
}

// 开始购买商品
- (void)startBuyProduct:(NSString *)serverId orderId:(NSString *)orderId exInfo:(NSString *)exInfo productId:(NSString*)productId
{
    
    if (self.canMakePay)
    {
        NSLog(@"-------------- 开始购买 --------------");
        self.mServerId = serverId;
        self.mOrderId = orderId;
        self.mExInfo = exInfo;
        [self getProductInfoById:productId];
    }
    else
    {
        NSLog(@"------------ App不用允许内购 --------------");
    }
}

// 下面的ProductId应该是事先在itunesConnect中添加好的，已存在的付费项目。否则查询会失败。
-(void)getProductInfoById:(NSString*)productID
{
    NSLog(@"----getProductInfoById---------id: %@", productID);
    NSArray *product = nil;
    product = [[NSArray alloc] initWithObjects:productID, nil];
    NSSet *set = [NSSet setWithArray:product];
    SKProductsRequest *request = [[SKProductsRequest alloc] initWithProductIdentifiers:set];
    //设置并启动监听
    request.delegate = self;
    [request start];
    //[product rele];
}


// 以上查询的回调函数
- (void)productsRequest:(SKProductsRequest *)request didReceiveResponse:(SKProductsResponse *)response
{
    NSLog(@"收到商品反馈");
    NSArray *myProduct = response.products;
    if (myProduct.count == 0)
    {
        NSLog(@"无法获取产品信息，购买失败。");
        return;
    }
    
    // test
    for(SKProduct *temp in myProduct)
    {
        NSLog(@"ProductInfo");
        NSLog(@"SKProduct 描述信息%@", [temp description]);
        NSLog(@"Product id: %@ 价格%@", temp.productIdentifier, temp.price);
    }
    
    NSLog(@"发送购买请求");
    SKPayment * payment = [SKPayment paymentWithProduct:myProduct[0]];
    [[SKPaymentQueue defaultQueue] addPayment:payment];
    NSLog(@"-------------------%@", payment);
}

- (void) paymentQueue:(SKPaymentQueue *)queue updatedTransactions:(NSArray *)transactions
{
    NSLog(@"------- payment Queue -----");
    for (SKPaymentTransaction *transaction in transactions)
    {
        switch (transaction.transactionState)
        {
            case SKPaymentTransactionStatePurchased://交易完成
                NSLog(@"transactionIdentifier = %@", transaction.transactionIdentifier);
                [self completeTransaction:transaction];
                break;
            case SKPaymentTransactionStateFailed://交易失败
                [self failedTransaction:transaction];
                break;
            case SKPaymentTransactionStateRestored://已经购买过该商品
                [self restoreTransaction:transaction];
                break;
            case SKPaymentTransactionStatePurchasing://商品添加进列表
                NSLog(@"商品添加进列表");
                break;
            default:
                break;
        }
    }
}


// 支付成功
- (void) completeTransaction:(SKPaymentTransaction*)transaction
{
    NSLog(@"--------------completeTransaction--------------");
    // Remove the transaction from the payment queue.
    [[SKPaymentQueue defaultQueue] finishTransaction: transaction];
    
    // Your application should implement these two methods.
    NSString * productIdentifier = transaction.payment.productIdentifier;
    
    if([productIdentifier length] > 0)
    {
        NSLog(@"productIdentifier : %@", productIdentifier);
    }
    
    // 向自己的服务器发送购买凭证
    [self checkReceiptToServer:transaction];
#ifdef __APPSTORE_IN_UNITY__
    // 通知 unity 购买成功
    UnitySendMessage(self.mCallBackObjectName.UTF8String,
                     "DebugUnityMessage", "BuySuccess");
    UnitySendMessage(self.mCallBackObjectName.UTF8String,
                     "BuyProductSuccess", productIdentifier.UTF8String);
#endif
}


// 支付失败
- (void) failedTransaction:(SKPaymentTransaction*)transaction
{
    if(transaction.error.code != SKErrorPaymentCancelled)
    {
        NSLog(@"购买失败");
#ifdef __APPSTORE_IN_UNITY__
        UnitySendMessage(self.mCallBackObjectName.UTF8String,
                         "DebugUnityMessage", "购买失败");
#endif
    }
    else
    {
        NSLog(@"用户取消交易");
#ifdef __APPSTORE_IN_UNITY__
        UnitySendMessage(self.mCallBackObjectName.UTF8String,
                         "DebugUnityMessage", "用户取消交易");
#endif
    }
    [[SKPaymentQueue defaultQueue] finishTransaction: transaction];
#ifdef __APPSTORE_IN_UNITY__
    UnitySendMessage(self.mCallBackObjectName.UTF8String,
                     "BuyProudctFailed", "购买失败");
#endif
}

// 对于已购商品，处理恢复购买的逻辑
- (void) restoreTransaction:(SKPaymentTransaction*)transaction
{
    [[SKPaymentQueue defaultQueue] finishTransaction: transaction];
#ifdef __APPSTORE_IN_UNITY__
    UnitySendMessage(self.mCallBackObjectName.UTF8String,
                     "DebugUnityMessage", "恢复已购商品");
#endif
}

- (void) checkReceiptToServer:(SKPaymentTransaction*)transaction
{
    NSString *transactionId = transaction.transactionIdentifier;
    NSLog(@"-----transactionId--------- %@", transactionId);
    
    NSData *receipt = nil;
    NSString *strVersion = nil;

#if IOS7_SDK_AVAILABLE
    NSLog(@"---------------------SDK 7.1 ---------------");
    NSURL *receiptURL = [[NSBundle mainBundle] appStoreReceiptURL];
    receipt = [NSData dataWithContentsOfURL:receiptURL];
    strVersion = @"iOS7";
#else
    NSLog(@"---------------------SDK 6.1 ---------------");
    receipt = transaction.transactionReceipt;
    strVersion = @"iOS6";
#endif
    
    
    if(!receipt)
    {
        //no local receipt -- handle the error
        
    }
    
    //NSLog(@"receipt data is :%@",receipt);
    NSError *error;
    //这个是发给appstore服务端的
    NSDictionary *requestContents = @{
                                      @"receipt-data": [receipt base64EncodedStringWithOptions:0],
                                      };
    
    NSData *requestData = [NSJSONSerialization dataWithJSONObject:requestContents options:0 error:&error];
    
    //TODO:服务端验证
    //…….
}

#if defined(__cplusplus)
extern "C" {
#endif
    
    NSString* AppStoreCreateNSString (const char* string)
    {
        return [NSString stringWithUTF8String:(string ? string : "")];
    }
    
    // 设置Unity回调的对象名
    void _InitAppStorePay(const char* callBackObjectName)
    {
        [[AppStorePayForUnity instance] initAppStorePay:AppStoreCreateNSString(callBackObjectName)];
    }
    
    // 开始购买商品
    void _StartBuyProduct(const char* serverId, const char* orderId, const char* exInfo, const char* productId)
    {
        [[AppStorePayForUnity instance] startBuyProduct:AppStoreCreateNSString(serverId)
                                               orderId:AppStoreCreateNSString(orderId)
                                                exInfo:AppStoreCreateNSString(exInfo)
                                             productId:AppStoreCreateNSString(productId)];
    }
    
    // 购买商品
    void _BuyProduct(const char* productId)
    {
        [[AppStorePayForUnity instance] getProductInfoById:AppStoreCreateNSString(productId)];
#ifdef __APPSTORE_IN_UNITY__
        UnitySendMessage([AppStorePayForUnity instance].mCallBackObjectName.UTF8String,
                         "DebugUnityMessage", "BuyProduct");
#endif
    }
    
    
    
void shareToWechatFriend(char *title, char *url, char *description){
  NSLog(@"执行到分享");
  //微信好友
  WXMediaMessage *message = [WXMediaMessage message];
  message.title=[[NSString alloc] initWithUTF8String:title];

  message.description = [[NSString alloc] initWithUTF8String:description];
  [message setThumbImage:[UIImage imageNamed:@"logo.png"]];
  WXWebpageObject *webpageObject =[WXWebpageObject object];
  webpageObject.webpageUrl = [[NSString alloc] initWithUTF8String:url];
  message.mediaObject = webpageObject;
  SendMessageToWXReq *req =[[SendMessageToWXReq alloc]init];
  req.bText = NO;
  req.message = message;
  req.scene=WXSceneSession;
    NSLog(@"分享内容:%@,%@,%@",message.title,webpageObject.webpageUrl, message.description);
  [WXApi sendReq:req completion:^(BOOL success){
      NSLog(@"分享结果:%@",success?@"成功":@"失败");
  }];
}
    

        
void shareToWechatTimeline(char *title, char *url, char *description){
      NSLog(@"执行到分享");
      //微信好友
      WXMediaMessage *message = [WXMediaMessage message];
      message.title=[[NSString alloc] initWithUTF8String:title];

      message.description = [[NSString alloc] initWithUTF8String:description];
      [message setThumbImage:[UIImage imageNamed:@"logo.png"]];
      WXWebpageObject *webpageObject =[WXWebpageObject object];
      webpageObject.webpageUrl = [[NSString alloc] initWithUTF8String:url];
      message.mediaObject = webpageObject;
      SendMessageToWXReq *req =[[SendMessageToWXReq alloc]init];
      req.bText = NO;
      req.message = message;
      req.scene=WXSceneTimeline;
        NSLog(@"分享内容:%@,%@,%@",message.title,webpageObject.webpageUrl, message.description);
      [WXApi sendReq:req completion:^(BOOL success){
          NSLog(@"分享结果:%@",success?@"成功":@"失败");
      }];
    }

    void wechatLogin(){
        //构造SendAuthReq结构体
        SendAuthReq* req =[[SendAuthReq alloc]init];
        req.scope = @"snsapi_userinfo";
        req.state = @"123";
        //第三方向微信终端发送一个SendAuthReq消息结构
    //    [WXApi sendAuthReq:req viewController:nil delegate:[AppStorePayForUnity instance] completion:^(BOOL success){
    //        NSLog(@"请求微信登录结果:%@",success?@"成功":@"失败");
    //    }];
        [WXApi sendReq:req completion:^(BOOL success){
                NSLog(@"请求微信登录结果:%@",success?@"成功":@"失败");
        }];
    }
    
    void wechatget(char *appId,char *partnerId,char *prepayId,char *packageValue,char *nonceStr,char *timeStamp,char *sign) {
        PayReq* req = [[PayReq alloc] init];
        req.partnerId = [[NSString alloc] initWithUTF8String:partnerId];;
        req.prepayId = [[NSString alloc] initWithUTF8String:prepayId];;
        req.nonceStr = [[NSString alloc] initWithUTF8String:nonceStr];;
        req.timeStamp = [[[NSString alloc] initWithUTF8String:timeStamp] intValue];
        req.package  = [[NSString alloc] initWithUTF8String:packageValue];;
        req.sign   = [[NSString alloc] initWithUTF8String:sign];;
        [WXApi sendReq:req completion:^(BOOL success){
                NSLog(@"请求微信登录结果:%@",success?@"成功":@"失败");
        }];
        
    }
    
    bool isWechatInstalled(){
        NSLog(@"isWechatInstalled=========%@",[WXApi isWXAppInstalled]?@"true":@"false");
        return [WXApi isWXAppInstalled];
    }

    
    
#if defined(__cplusplus)
}
#endif

@end
