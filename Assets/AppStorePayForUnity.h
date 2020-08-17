//
//  AppStorePayForUnity.h
//  Test
//
//  Created by WarZhan on 14-10-9.
//  Copyright (c) 2014年 Kingjoy. All rights reserved.
//

#ifndef _AppStorePayForUnity_h
#define _AppStorePayForUnity_h

#import <StoreKit/StoreKit.h>
#import "WXApi.h"

@interface AppStorePayForUnity : NSObject <SKProductsRequestDelegate, SKPaymentTransactionObserver,WXApiDelegate>
{
}

@property (nonatomic, retain) NSString *mCallBackObjectName;
@property (nonatomic, retain) NSString *mServerId;
@property (nonatomic, retain) NSString *mOrderId;
@property (nonatomic, retain) NSString *mExInfo;

+ (AppStorePayForUnity*) instance;

//初始化 设置回调的对象
- (void) initAppStorePay:(NSString*)callBackName;

//开始购买
- (void) startBuyProduct:(NSString*)serverId orderId:(NSString*)orderId exInfo:(NSString*)exInfo productId:(NSString*)productId;

//是否允许内购
- (BOOL) canMakePay;

//通过product ID 获取详细信息
- (void) getProductInfoById:(NSString*)productID;

// 支付成功
- (void) completeTransaction:(SKPaymentTransaction*)transaction;

// 支付失败
- (void) failedTransaction:(SKPaymentTransaction*)transaction;

// 对于已购商品，处理恢复购买的逻辑
- (void) restoreTransaction:(SKPaymentTransaction*)transaction;

// 支付成功后 发送给服务器
- (void) checkReceiptToKingjoy:(SKPaymentTransaction*)transaction;

@end

#endif
