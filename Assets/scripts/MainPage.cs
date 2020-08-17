using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using LitJson;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System;
using System.Text;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using LitJson;
using System.Threading;

public class MainPage : MonoBehaviour {
  
  List<Button> numButtons = new List<Button>();
  List<Button> inputNumButtons = new List<Button>();
  public Button numButton0;
  public Button numButton1;
  public Button numButton2;
  public Button numButton3;
  public Button numButton4;
  public Button numButton5;
  public Button numButton6;
  public Button numButton7;
  public Button numButton8;
  public Button numButton9;
  public Button inputNumButton0;
  public Button inputNumButton1;
  public Button inputNumButton2;
  public Button inputNumButton3;
  public Button numDelButton;
  public Button numReButton;

  public GameObject createRoomPanel;
  public Button createRoomCloseButton;
  public Button createRoomButton;
  public GameObject ruleRoomPanel;
  public Button ruleRoomCloseButton;
  public Button ruleRoomButton;

  public GameObject zhuangpanPanel;
  public Button zhuangpanCloseButton;
  public Button zhuangpanBuyButton;
  public Button zhuangpanButton;

  public GameObject modelContent;

  public GameObject giftPanel;
  public Button giftCloseButton;
  public Button giftButton;

  public Button giftMenuButton;


  public GameObject goldImage;

  public GameObject sharePanel;
  public Button wxShareButton;
  public Button wxShareCloseButton;

  public Button shareButton;

  public int wxShareType=0;// 0:分享不获取豆子，1:分享获取豆子 2:分享抽奖

  public bool wxSharGetGoldJump;


  public GameObject majiangTest;
  public Button sendButton;

  public string recieveVCode;

  public GameObject mobilePanel;
  public InputField mobileInput;
  public InputField vCodeInput;

  public Button mobileLoginButton;
  public Button mobileCancelButton;
  int cutdown = 60;


  AudioSource backAudioSource;


  // private IStoreController controller;
  public GameObject searchPanel;

  public GameObject person;

  public GameObject zhuangpanPerson;


  public GameObject worldPanel;

  public Toggle musicToggle;

  public Toggle audioToggle;

  public Toggle testToggle;


  public Button exitButton;

  public Button searchCloseButton;

  public Button searchTabButton;
  public Button nearyTabButton;

  public InputField searchField;

  public Button searchButton;

  public GameObject searchResultContent;

  public GameObject searchUserPrefab;

  public GameObject rankPanel;

  public GameObject rankResultContent;

  public GameObject rankUserPrefab;



  public GameObject mainPanel;

  public GameObject errorToastPrefab;
  public GameObject confirmPrefab;

  public GameObject customPanel;

  public Button customCloseButton;

  public Image customWxImage;
  public Button saveWxButton;

  public GameObject settingPanel;

  public Button settingCloseButton;

  public Button basicTabButton;
  public Button basicRuleTabButton;
  public Button huRuleTabButton;

  public GameObject basicTabPanel;
  public GameObject basicRuleTabPanel;
  public GameObject huRuleTabPanel;

  public Image settingTabImage;

  List<Button> settingTabs = new List<Button>();
  List<GameObject> settingTabPanels = new List<GameObject>();


  public GameObject messagePanel;
  public Button messageCloseButton;

  public bool wxShareChoujiang = false;

  public GameObject messageRowPanel;

  public GameObject messageRowContentList;


  public GameObject zhanjiListContent;

  public GameObject userPanel;
  public Button userPanelCloseButton;

  public Image infoTabImage;

  public Image infoUserImage;
  public Text infoNicknameText;

  public Button infoTabButton;
  public Button zhanjiTabButton;
  public Button rongyuTabButton;


  public GameObject infoTabPanel;
  public GameObject zhanjiTabPanel;
  public GameObject zhanjiTabPanelContent;

  public GameObject rongyuTabPanel;

  List<Button> infoTabButtons = new List<Button>();
  List<GameObject> infoTabPanels = new List<GameObject>();
  public GameObject zhanjiPanel;
  public Button zhanjiPanelCloseButton;
  public GameObject zhanjiRowPanel;

  public AudioClip clickSound;
  public AudioClip backSound;
	private AudioSource audioSource;

  public Button addFriendButton;

  public Button messageButton;

  public Button settingButton;



  public Button headimgButton;

  public Button zhanjiButton;
  public Button customButton;


  public Button tabGoldButton;
  public Button tabDiamondButton;

  public GameObject goldPanel;
  public GameObject diamondPanel;

  public GameObject roomTypePanel;

  public Button payPanelCloseButton;

  public GameObject payPanel;

  public Button payGoldButton1;
  public Button payGoldButton2;
  public Button payGoldButton3;
  public Button payGoldButton4;
  public Button payGoldButton5;
  public Button payGoldButton6;
  public Button payGoldButton7;

  public Button payGoldButton8;


  public Button payDiamondButton1;
  public Button payDiamondButton2;
  public Button payDiamondButton3;
  public Button payDiamondButton4;
  public Button payDiamondButton5;
  public Button payDiamondButton6;
  public Button payDiamondButton7;
  public Button payDiamondButton8;


  public Button signPanelCloseButton;

  public GameObject signPanel;

  public Button signButton1;
  public Button signButton2;
  public Button signButton3;
  public Button signButton4;
  public Button signButton5;
  public Button signButton6;
  public Button signButton7;

  public Button startButton;
  public Button closeRoomTypeButton;


  public GameObject roomTypeButton;


  public Button goldButton;
  public Button diamondButton;

  public Text goldText;
  public Text diamondText;

  
  public Button pipeiButton;
  public Button yuejuButton;
  public Button joinButton;

  public InputField roomNoInput;


  public Image headimg;
  public Text nickname;
  public Text gold;
  public Text diamond;

  DiamondConfig selectDiamondConfig;

  User selectUser;

  int selectRoomType = 1;

  Button selectRoomTypeButton;

  public Sprite selectRoomTypeBgSprite;

  private float effect_sound_volume = 0.6f;



  List<SceneConfig> sceneConfigs = new List<SceneConfig>();

  bool pipei = true;

  List<Button> signButtons = new List<Button>();
  List<Button> payGoldButtons = new List<Button>();
  List<Button> payDiamondButtons = new List<Button>();

  List<GoldConfig> goldConfigs= new List<GoldConfig>();
  List<DiamondConfig> diamondConfigs= new List<DiamondConfig>();

  public GameObject joinPanel;

  public Button closeJoinPanel;

  public Button joinRoomButton;

  SocketClient socketClient;

  private Color sendButtonColor ;
  private Color sendButtonDisableColor ;

  User zhuangbanUser;

  private string inputRoomNo="";


  void Awake () {
    AppUtil.mainPageEnterCount = AppUtil.mainPageEnterCount+1;
    sendButtonDisableColor = new Color(200/255.0f,200/255.0f,200/255.0f);
    sendButtonColor = new Color(255/255.0f,0/255.0f,0/255.0f);

    GUI.FocusControl("RoomNoInputField");
    backAudioSource =  (AudioSource)this.gameObject.GetComponents(typeof(AudioSource))[1];
    AppUtil.audioControl(backAudioSource,musicToggle,audioToggle);
    goldConfigs.Add(new GoldConfig(1,2400,20));
    goldConfigs.Add(new GoldConfig(2,6000,50));
    goldConfigs.Add(new GoldConfig(3,18500,150));
    goldConfigs.Add(new GoldConfig(4,38000,300));
    goldConfigs.Add(new GoldConfig(5,65000,500));
    goldConfigs.Add(new GoldConfig(6,132000,1000));
    goldConfigs.Add(new GoldConfig(7,405000,3000));
    goldConfigs.Add(new GoldConfig(8,700000,5000)); 

    numButtons.Add(numButton0);
    numButtons.Add(numButton1);
    numButtons.Add(numButton2);
    numButtons.Add(numButton3);
    numButtons.Add(numButton4);
    numButtons.Add(numButton5);
    numButtons.Add(numButton6);
    numButtons.Add(numButton7);
    numButtons.Add(numButton8);
    numButtons.Add(numButton9);

    inputNumButtons.Add(inputNumButton0);
    inputNumButtons.Add(inputNumButton1);
    inputNumButtons.Add(inputNumButton2);
    inputNumButtons.Add(inputNumButton3);
    

    diamondConfigs.Add(new DiamondConfig("com.putao.majiang6",1,120,6));
    diamondConfigs.Add(new DiamondConfig("com.putao.majiang18",2,360,18));
    diamondConfigs.Add(new DiamondConfig("com.putao.majiang30",3,600,30));
    diamondConfigs.Add(new DiamondConfig("com.putao.majiang68",4,1360,68));
    diamondConfigs.Add(new DiamondConfig("com.putao.majiang118",5,2360,118));
    diamondConfigs.Add(new DiamondConfig("com.putao.majiang168",6,1900,168));
    diamondConfigs.Add(new DiamondConfig("com.putao.majiang298",7,3400,298));
    diamondConfigs.Add(new DiamondConfig("com.putao.majiang618",8,7200,618));

    AppUtil.getUser();

    if(AppUtil.user==null){
      if(SocketClient.socketClient!=null){
        socketClient.onRecieveMessage = null;
      }
      
      SceneManager.LoadScene("Login");
      Invoke("DestroyScene",1.5f);
      return;
    }
    
    LoadImag.load(AppUtil.user.headimg,headimg,this);
    setUserInfo();
    if(SocketClient.socketClient==null){
      SocketClient.socketClient = new SocketClient();
      SocketClient.socketClient.monoBehaviour = this;
      SocketClient.socketClient.connect();
    }
    socketClient = SocketClient.socketClient;
    socketClient.monoBehaviour = this;
    socketClient.transform = mainPanel.transform;
    socketClient.onRecieveMessage = (JsonData data)=>{
      string cmd = (string)data["cmd"];
      JsonData content = data["con"];
      if(cmd=="invite"){
        int inviteRoomNo = (int)content["roomNo"];
        string inviteNickname = (string)content["nickname"];
        Confirm.show(inviteNickname+"邀请你加入房间:"+inviteRoomNo,mainPanel.transform,()=>{
          playClick();
          inRoom(inviteRoomNo);
        },()=>{
          playClick();
        });
      }
    };
    
    sendButton.onClick.AddListener(SendVCode);

    mobileLoginButton.onClick.AddListener(ConfirmBind);


    mobileCancelButton.onClick.AddListener(()=>{
      playClick();
      mobilePanel.active = false;
    });
    
    pipeiButton.onClick.AddListener(()=>{
      playClick();
      // if(AppUtil.user.mobile==null || AppUtil.user.mobile==""){
      //   mobilePanel.active = true;
      // }else{
      pipei = true;
      showRoomTypeAnimate();
      roomTypePanel.active = true;
      createRoomTypes();
      // }
      
    });
    mobilePanel.active = false;
    // JsonData map = JsonMapper.ToObject("[[{\"gang\":false,\"tianyingused\":false,\"tianying\":false,\"id\":10,\"type\":1,\"value\":3,\"status\":0},{\"gang\":false,\"tianyingused\":false,\"tianying\":false,\"id\":9,\"type\":1,\"value\":3,\"status\":0},{\"gang\":false,\"tianyingused\":false,\"tianying\":false,\"id\":12,\"type\":1,\"value\":3,\"status\":0}]]");
    audioSource = GetComponent<AudioSource> ();
    yuejuButton.onClick.AddListener(()=>{
      pipei = false;
      playClick();
      createRoomPanel.active = true;
    });
    joinButton.onClick.AddListener(()=>{
      joinPanel.active = true;
      playClick();
    });


    closeJoinPanel.onClick.AddListener(()=>{
      joinPanel.active = false;
      playClick();
    });

    joinPanel.active = false;


    goldButton.onClick.AddListener(()=>{
      playClick();
      payPanel.active = true;
      goldPanel.active = true;
      diamondPanel.active = false;
      tabGoldButton.GetComponent<Image>().color = AppUtil.GetColor("#EEEEEE");
      tabDiamondButton.GetComponent<Image>().color = AppUtil.GetColor("#AAA064");
      queryGoldConfigs();
    });
    diamondButton.onClick.AddListener(()=>{
      playClick();
      payPanel.active = true;
      goldPanel.active = false;
      diamondPanel.active = true;
      tabGoldButton.GetComponent<Image>().color = AppUtil.GetColor("#AAA064");
      tabDiamondButton.GetComponent<Image>().color = AppUtil.GetColor("#EEEEEE");
      queryDiamondConfigs();
    });

    messageButton.onClick.AddListener(()=>{
      playClick();
    });
    settingButton.onClick.AddListener(()=>{
      playClick();
    });

   

    closeRoomTypeButton.onClick.AddListener(()=>{
      playClick();
      roomTypePanel.active = false;
      hideRoomTypeAnimate();
    });

    roomTypePanel.active = false;

    zhuangbanUser = new User();
    zhuangbanUser.upper = AppUtil.user.upper;
    zhuangbanUser.trousers = AppUtil.user.trousers;
    zhuangbanUser.shoes = AppUtil.user.shoes;
    zhuangbanUser.hair = AppUtil.user.hair;
    zhuangbanUser.hat = AppUtil.user.hat;



    sceneConfigs.Add(new SceneConfig("青铜场",1,20,400,1000,0));
    sceneConfigs.Add(new SceneConfig("青铜场",2,40,1000,10000,0));
    sceneConfigs.Add(new SceneConfig("黄金场",3,80,5000,50000,0));
    sceneConfigs.Add(new SceneConfig("黄金场",4,150,10000,100000,0));
    sceneConfigs.Add(new SceneConfig("钻石场",5,300,20000,200000,0));
    sceneConfigs.Add(new SceneConfig("钻石场",6,1000,50000,500000,0));
    sceneConfigs.Add(new SceneConfig("王者场",7,3000,100000,1000000,0));
    sceneConfigs.Add(new SceneConfig("王者场",8,7200,200000,2000000,0));

    signButtons.Add(signButton1);
    signButtons.Add(signButton2);
    signButtons.Add(signButton3);
    signButtons.Add(signButton4);
    signButtons.Add(signButton5);
    signButtons.Add(signButton6);
    signButtons.Add(signButton7);
    for(int i=0;i<signButtons.Count;i++){
      Button signButton = signButtons[i];
      signButton.enabled = false;
      int type = i+1;
      signButton.onClick.AddListener(()=>{
        playClick();
        lingqu(type,signButton);
      });
      signButton.GetComponent<Transform>().Find("LingquImage").gameObject.active = false;
    }

    for(int i=0;i<numButtons.Count;i++){
      Button numButton = numButtons[i];
      int num = i;
      numButton.onClick.AddListener(()=>{
        playClick();
        int len = inputRoomNo.Length;
        if(len>=4){
          return;
        }
        inputRoomNo = inputRoomNo +num;
        inputNumButtons[len].GetComponent<Transform>().Find("Text").GetComponent<Text>().text = num+"";
        Debug.Log("inputRoomNo==="+inputRoomNo);

        if(inputRoomNo.Length>=4){
          inRoom(int.Parse(inputRoomNo));
        }
      });
    }

    numDelButton.onClick.AddListener(()=>{
        playClick();
        int len = inputRoomNo.Length;
        if(len==0){
          return;
        }

        inputNumButtons[len-1].GetComponent<Transform>().Find("Text").GetComponent<Text>().text = "";
        inputRoomNo = inputRoomNo.Substring(0,len-1);
        Debug.Log("inputRoomNo==="+inputRoomNo);

    });

    numReButton.onClick.AddListener(()=>{
        playClick();
        inputRoomNo = "";
        for(int i=0;i<inputNumButtons.Count;i++){
          inputNumButtons[i].GetComponent<Transform>().Find("Text").GetComponent<Text>().text = "";
        }
    });

    signPanelCloseButton.onClick.AddListener(()=>{
      playClick();
      signPanel.active = false;
    });

    signPanel.active = false;

    payGoldButtons.Add(payGoldButton1);
    payGoldButtons.Add(payGoldButton2);
    payGoldButtons.Add(payGoldButton3);
    payGoldButtons.Add(payGoldButton4);
    payGoldButtons.Add(payGoldButton5);
    payGoldButtons.Add(payGoldButton6);
    payGoldButtons.Add(payGoldButton7);
    payGoldButtons.Add(payGoldButton8);

    for(int i=0;i<payGoldButtons.Count;i++){
      Button button = payGoldButtons[i];
      int type = i+1;
      button.onClick.AddListener(()=>{
        playClick();
        payGold(type,button);
      });
    }

    shareButton.onClick.AddListener(()=>{
      wxShareType = 0;
      string title = "我邀请您畅玩旌德中心五麻将，正宗的旌德麻将";
      string url = "https://www.xiaowanwu.cn/h5_majiang_zxw_static/index.html#/index";
      string desc = "旌德麻将";

      #if UNITY_IPHONE
      shareToWechatFriend(title,url,desc);
      #elif UNITY_ANDROID
      AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
      AndroidJavaObject  currentActivity = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
      currentActivity.Call("shareToWechat",title,url,desc,true);
      #endif
    });

    if(!(AppUtil.user.mobile=="18868413960" || AppUtil.user.nickname=="不是天才")){
      testToggle.GetComponent<Transform>().gameObject.active = false;
    }

    testToggle.onValueChanged.AddListener((bool value)=>{
      AppUtil.test = value;
      socketClient.close();
      socketClient.connect();
    });

    payPanelCloseButton.onClick.AddListener(()=>{
      playClick();
      payPanel.active = false;
    });

    
    payDiamondButtons.Add(payDiamondButton1);
    payDiamondButtons.Add(payDiamondButton2);
    payDiamondButtons.Add(payDiamondButton3);
    payDiamondButtons.Add(payDiamondButton4);
    payDiamondButtons.Add(payDiamondButton5);
    payDiamondButtons.Add(payDiamondButton6);
    payDiamondButtons.Add(payDiamondButton7);
    payDiamondButtons.Add(payDiamondButton8);

    for(int i=0;i<payDiamondButtons.Count;i++){
      Button button = payDiamondButtons[i];
      int type = i+1;
      DiamondConfig diamondConfig = diamondConfigs[i];
      button.onClick.AddListener(()=>{
        playClick();
        payDiamond(diamondConfig);
      });
    }

    

    tabGoldButton.onClick.AddListener(()=>{
      playClick();
      goldPanel.active = true;
      diamondPanel.active = false;
      tabGoldButton.GetComponent<Image>().color = AppUtil.GetColor("#EEEEEE");
      tabDiamondButton.GetComponent<Image>().color = AppUtil.GetColor("#AAA064");
      queryGoldConfigs();
    });

    tabDiamondButton.onClick.AddListener(()=>{
      playClick();
      goldPanel.active = false;
      diamondPanel.active = true;
      tabGoldButton.GetComponent<Image>().color = AppUtil.GetColor("#AAA064");
      tabDiamondButton.GetComponent<Image>().color = AppUtil.GetColor("#EEEEEE");
      queryDiamondConfigs();
    });

    goldPanel.active = false;
    diamondPanel.active = false;

    payPanel.active = false;
    Debug.Log("zhanjiPanel=="+(zhanjiPanel==null));

    zhanjiPanel.active = false;
    zhanjiButton.onClick.AddListener(()=>{
      playClick();
      selectUser = AppUtil.user;
      zhanjiPanel.active = true;
      AppUtil.queryRecords(zhanjiListContent,transform,zhanjiRowPanel,this,selectUser.id);
    });
    zhanjiPanelCloseButton.onClick.AddListener(()=>{
      playClick();
      zhanjiPanel.active = false;
    });

    exitButton.onClick.AddListener(()=>{
      playClick();
      AppUtil.deleteUser();
      socketClient.onRecieveMessage = null;
      SocketClient.socketClient.close();
      SocketClient.socketClient = null;
      SceneManager.LoadScene("Login");
      Invoke("DestroyScene",1.5f);
    });

    userPanel.active = false;
    headimgButton.onClick.AddListener(()=>{
      playClick();
      selectUser = AppUtil.user;
      userPanel.active = true;
      LoadImag.load(AppUtil.user.headimg,infoUserImage,this);
      infoNicknameText.text = AppUtil.user.nickname;
    });
    userPanelCloseButton.onClick.AddListener(()=>{
      playClick();
      userPanel.active = false;
      resetUserPanel();
    });

    infoTabButtons.Add(infoTabButton);
    infoTabButtons.Add(zhanjiTabButton);
    infoTabButtons.Add(rongyuTabButton);

    infoTabPanels.Add(infoTabPanel);
    infoTabPanels.Add(zhanjiTabPanel);
    infoTabPanels.Add(rongyuTabPanel);

    for(int i=0;i<infoTabButtons.Count;i++){
      Button button = infoTabButtons[i];
      GameObject panel = infoTabPanels[i];
      int type = i+1;
      if(i!=0){
        panel.active = false;
      }
      button.onClick.AddListener(()=>{
        for(int j=0;j<infoTabButtons.Count;j++){
          Button b = infoTabButtons[j];
          GameObject p = infoTabPanels[j];
          if(p==panel){
            p.active = true;
            infoTabImage.transform.DOLocalMoveY(-j*150-15f,0.3f);
            if(j==1){
              AppUtil.queryRecords(zhanjiTabPanelContent,transform,zhanjiRowPanel,this,selectUser.id);
            }
          }else{
            p.active = false;
          }
        }
      });
    }

    settingTabs.Add(basicTabButton);
    settingTabs.Add(basicRuleTabButton);
    settingTabs.Add(huRuleTabButton);

    settingTabPanels.Add(basicTabPanel);
    settingTabPanels.Add(basicRuleTabPanel);
    settingTabPanels.Add(huRuleTabPanel);

    for(int i=0;i<settingTabs.Count;i++){
      Button button = settingTabs[i];
      GameObject panel = settingTabPanels[i];
      int type = i+1;
      if(i!=0){
        panel.active = false;
      }
      button.onClick.AddListener(()=>{
        playClick();
        for(int j=0;j<settingTabs.Count;j++){
          Button b = settingTabs[j];
          GameObject p = settingTabPanels[j];
          if(p==panel){
            p.active = true;
            settingTabImage.transform.DOLocalMoveY(-j*150-15f,0.3f);
          }else{
            p.active = false;
          }
        }
      });
    }

    settingButton.onClick.AddListener(()=>{
      playClick();
      settingPanel.active = true;
    });
    settingCloseButton.onClick.AddListener(()=>{
      playClick();
      settingPanel.active = false;
    });

    settingPanel.active = false;



    messageButton.onClick.AddListener(()=>{
      playClick();
      messagePanel.active = true;
      queryMessages();
    });
    messageCloseButton.onClick.AddListener(()=>{
      playClick();
      messagePanel.active = false;
    });

    messagePanel.active = false;

    customButton.onClick.AddListener(()=>{
      playClick();
      customPanel.active = true;
      queryMessages();
      LoadImag.load("http://u.xiaowanwu.cn/majiang_weixin.jpg",customWxImage,this);
    });

    saveWxButton.onClick.AddListener(()=>{
      playClick();
      StartCoroutine(saveWxImage());
    });

    customCloseButton.onClick.AddListener(()=>{
      playClick();
      customPanel.active = false;
      
    });

    customPanel.active = true;
    LoadImag.load("http://u.xiaowanwu.cn/majiang_weixin.jpg",customWxImage,this);



    addFriendButton.onClick.AddListener(()=>{
      playClick();
      searchPanel.active = true;
      queryUsers(false);
    });

    searchCloseButton.onClick.AddListener(()=>{
      playClick();
      searchPanel.active = false;
    });

    searchTabButton.onClick.AddListener(()=>{
      playClick();
      queryUsers(false);
      searchTabButton.GetComponent<Image>().color = AppUtil.GetColor("#EEEEEE");
      nearyTabButton.GetComponent<Image>().color = AppUtil.GetColor("#AAA064");
    });

    nearyTabButton.onClick.AddListener(()=>{
      playClick();
      queryUsers(true);
      searchTabButton.GetComponent<Image>().color = AppUtil.GetColor("#AAA064");
      nearyTabButton.GetComponent<Image>().color = AppUtil.GetColor("#EEEEEE");
    });
    searchPanel.active = false;
    
    searchButton.onClick.AddListener(()=>{
      playClick();
      queryUsers(false);
    });

    wxShareButton.onClick.AddListener(()=>{
      playClick();
      wxSharGetGoldJump = false;
      shareGetGoldFunc();
      sharePanel.active = false;
    });
    wxShareCloseButton.onClick.AddListener(()=>{
      playClick();
      sharePanel.active = false;
    });

    sharePanel.active = false;

    giftButton.onClick.AddListener(()=>{
      playClick();

      JsonData param = new JsonData();
      param["userid"] = AppUtil.user.id;
      WebService.post("/user/giftBeforeRandom", param,mainPanel.transform,(res)=>{
        int type = (int)res["type"];
        string message = (string)res["message"];
        Confirm.show(message,mainPanel.transform,()=>{
          if(type==1){
            //需要分享
            playClick();
            wxShareType = 2;
            string title = "旌德中心五麻将，完美还原旌德麻将打法，快来体验吧";
            string url = "https://www.xiaowanwu.cn/h5_majiang_zxw_static/index.html#/index";
            string desc = "旌德中心五麻将";
            #if UNITY_IPHONE
            shareToWechatTimeline(title,url,desc);
            #elif UNITY_ANDROID
            AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject  currentActivity = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
            currentActivity.Call("shareToWechat",title,url,desc,false);
            #endif
          }else{
            choujiang();
          }
          
        },()=>{

        });
        
      },(res)=>{
        Toast.error("豆子不足",mainPanel.transform);
      },this);


      
    });

    giftButton.GetComponent<Transform>().DOScale(new Vector3(1.1f,1.1f,1.1f), 1f).SetLoops(-1);


    giftCloseButton.onClick.AddListener(()=>{
      playClick();
      giftPanel.active = false;
    });

    giftMenuButton.onClick.AddListener(()=>{
      playClick();
      giftPanel.active = true;
    });

    zhuangpanCloseButton.onClick.AddListener(()=>{
      playClick();
      updateUserModel();
      zhuangpanPanel.active = false;
    });

    zhuangpanButton.onClick.AddListener(()=>{
      playClick();
      zhuangpanPanel.active = true;

      queryModels();
    });

    zhuangpanBuyButton.onClick.AddListener(()=>{
      playClick();
      zhuangpanPanel.active = false;
    });

    createRoomCloseButton.onClick.AddListener(()=>{
      playClick();
      createRoomPanel.active = false;
    });

    ruleRoomCloseButton.onClick.AddListener(()=>{
      playClick();
      ruleRoomPanel.active = false;
    });

    ruleRoomButton.onClick.AddListener(()=>{
      playClick();
      JsonData param = new JsonData();
      param["userid"] = AppUtil.user.id;
      param["inTimesType"] = selectRoomType;

      WebService.post("/user/getFreeGetGold", param,mainPanel.transform,(res)=>{
        bool showFreeGet = (bool)res["showFreeGet"];
        int maxFreeCount = (int)res["maxCount"];
        int getFreeCount = (int)res["getCount"];
        int perFreeGold = (int)res["perGold"];
        if(showFreeGet){
          if(getFreeCount>=maxFreeCount){
            Toast.info("豆子不够了,请去充值",mainPanel.transform);
            return;
          }else{
            Confirm.show("今日第"+(getFreeCount+1)+"次赠送"+perFreeGold+"个豆子，是否领取("+(getFreeCount+1)+"/"+maxFreeCount+")",mainPanel.transform,()=>{
              JsonData ps = new JsonData();
              ps["userid"] = AppUtil.user.id;
              WebService.post("/user/freeGetGold", ps,mainPanel.transform,(res2)=>{
                refreshUser();
                GoToRoom();
              },(res3)=>{
                Toast.error("系统错误",mainPanel.transform);
              },this);
            },()=>{

            });

            // Confirm.show("豆子不足，分享到朋友圈可以获得"+perFreeGold+"个豆子，是否现在分享",mainPanel.transform,()=>{
            //   wxSharGetGoldJump = true;
            //   shareGetGoldFunc();
            // },()=>{
            //   playClick();
            // });
          }
          return;
        }else{
          GoToRoom();
        }
      },(res)=>{
        Toast.error("系统错误",mainPanel.transform);
      },this);
      ruleRoomPanel.active = false;
    });

    startButton.onClick.AddListener(()=>{
      ruleRoomPanel.active = true;
    });
    ruleRoomPanel.active = false;
    createRoomButton.onClick.AddListener(()=>{
      playClick();
      JsonData param = new JsonData();
      param["userid"] = AppUtil.user.id;
      Confirm.show("确认花费24000颗豆子或者20颗钻石开一个房间(优先使用豆子)?",mainPanel.transform,()=>{
        playClick();
        WebService.post("/user/createRoom", param,mainPanel.transform,(res)=>{
          GoToRoom();
        },(res)=>{
          Toast.error("钻石不够啦！，请去充值~",mainPanel.transform);
        },this);
      },()=>{
        playClick();
      });

      
    });

    createRoomPanel.active = false;

    zhuangpanPanel.active = false;

    if(AppUtil.mainPageEnterCount==1){
      querySign();
      queryShareGetGold();
      queryConfig();
      giftPanel.active = true;
    }else{
      giftPanel.active = false;
    }
    refreshUser();
    updateLoginTime();
    queryRankUsers();

    
    createUserModel();
    hasRoom();

    animate();
    
  }

  void choujiang(){
    JsonData param = new JsonData();
    param["userid"] = AppUtil.user.id;
    WebService.post("/user/giftRandom", param,mainPanel.transform,(res)=>{
        int type = (int)res["type"];
        string message = (string)res["message"];
        giftButton.GetComponent<Transform>().DOLocalRotate(new Vector3(0,0f,1080f+(type-1)*30), 3f,RotateMode.FastBeyond360).OnComplete(()=>{
          Toast.info(message,mainPanel.transform);
          Invoke("recoverGiftButton",3);
          refreshUser();
        });
        AppUtil.uploadData(7,"抽奖:"+message,mainPanel.transform,this);
      },(res)=>{
        Toast.error("豆子不足",mainPanel.transform);
    },this);
  }

  //复原
  void recoverGiftButton(){
    giftButton.GetComponent<Transform>().DOLocalRotate(new Vector3(0,0,0), 0.2f,RotateMode.FastBeyond360);
  }

  // 查询分享金币
  void queryShareGetGold(){
    JsonData param = new JsonData();
    param["userid"] = AppUtil.user.id;
    Debug.Log("queryShareGetGold==============");
    WebService.post("/user/queryShareGetGold", param,mainPanel.transform,(res)=>{
      bool show = (bool)res["show"];
      Debug.Log("show=============="+show);
      sharePanel.active = show;
      if(sharePanel.active){
        goldImage.transform.DOBlendableMoveBy(new Vector3(0, 30, 0), 0.8f).SetEase(Ease.OutElastic);
      }
    },(res)=>{
      Toast.error("系统错误",mainPanel.transform);
    },this);
  }

  //分享获取金币
  void shareGetGoldFunc(){
    playClick();
    wxShareType= 1;
    string title = "旌德中心五麻将，完美还原旌德麻将打法，快来体验吧";
    string url = "https://www.xiaowanwu.cn/h5_majiang_zxw_static/index.html#/index";
    string desc = "旌德中心五麻将";
    #if UNITY_IPHONE
    shareToWechatTimeline(title,url,desc);
    #elif UNITY_ANDROID
    AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    AndroidJavaObject  currentActivity = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
    currentActivity.Call("shareToWechat",title,url,desc,false);
    #endif
  }

  void hasRoom(){
    socketClient.sendHasRoom(AppUtil.selectRoomType, (JsonData res)=>{
      bool has = (bool)res["has"];
      if(has){
        AppUtil.roomNo = (int)res["roomNo"];
        Confirm.show("您还有未完成的对局[房间号="+AppUtil.roomNo+"]，是否现在进入?",mainPanel.transform,()=>{
          playClick();
          AppUtil.roomNo = (int)res["roomNo"];
          int inTimesType = (int)res["inTimesType"];
          socketClient.onRecieveMessage = null;
          SceneManager.LoadScene("game");
          Invoke("DestroyScene",1.5f);
        },()=>{
          playClick();
        });
      }
    });
  }

  //分享到朋友圈成功
  void WXShareSceneTimelineCallBack(string code){
    Toast.info("分享成功",mainPanel.transform);
    if(wxShareType==2){
      choujiang();
      return;
    }
    if(wxShareType==1){
      JsonData ps = new JsonData();
      ps["userid"] = AppUtil.user.id;
      WebService.post("/user/shareGetGold", ps,mainPanel.transform,(res2)=>{
        refreshUser();
        if(wxSharGetGoldJump){
          GoToRoom();
        }
      },(res3)=>{
        Toast.error("今日分享已达上限",mainPanel.transform);
      },this);
      AppUtil.uploadData(1,"分享获取豆子",mainPanel.transform,this);
    }else if(wxShareType==1){
      AppUtil.uploadData(4,"分享首页",mainPanel.transform,this);
    }
  }

  void majiangPlayTest(){
    //majiangTest.transform.DOBlendableMoveBy(new Vector3(0f, 2f, 0f), 0.8f).SetLoops(-1).SetEase(Ease.OutElastic);
  }

  void animate(){

  }

  void createUserModel(){ 
    if(person!=null){
      Destroy(person);
    }
    clearUserModel();
    AppUtil.user.figure = "female_main";
    person = AppUtil.createUserModel(AppUtil.user,"AnimatorControllers/MainPageController",transform,this,new Vector3(Screen.height*1.0f/Screen.width>0.7f?-4.5f:-9.32f,8.81f,-11.59f),new Vector3(10f,10f,10f),new Vector3(0,180,0));
  }
  void GoToRoom(){
    Action<JsonData> action = (JsonData res)=>{
        int errno = (int)res["errno"];
        Debug.Log("errno================="+errno);
        if(errno==0){
          int roomStatus = (int)res["roomStatus"];
          int no = (int)res["roomNo"];
          Debug.Log("执行回调,房间号:"+no);
          AppUtil.roomNo = no;
          //开始加载场景
          //DontDestroyOnLoad(this);//切换场景不销毁clone
          backAudioSource.Pause();
          if(roomStatus>0){
            socketClient.onRecieveMessage = null;
            
            SceneManager.LoadScene("game");
            
          }else{
            socketClient.onRecieveMessage = null;
            SceneManager.LoadScene("Pipei");
            Invoke("DestroyScene",1.5f);
          }
        }else{
          if(errno==1){
            // 豆子不够
          }
          string message = (string)res["message"];
          Debug.Log("errno================="+message);
          Toast.info(message,mainPanel.transform);
        }
        
        //StartCoroutine("GoRoom");
      };
    if(pipei){
      socketClient.sendRandom(selectRoomType,action);
    }else{
      socketClient.sendNewRoom(selectRoomType,action);
    }
  }

  void DestroyScene(){
    Destroy(gameObject);
  }

  //确认绑定  
  void ConfirmBind(){
    playClick();
    string mobile = mobileInput.text;
    string code = vCodeInput.text;
    if(code==""){
      Toast.error("请输验证码",mainPanel.transform,errorToastPrefab);
      return;
    }else if(code!=recieveVCode){
      Toast.error("验证码不正确",mainPanel.transform,errorToastPrefab);
      return;
    }
    JsonData param = new JsonData();
    param["mobile"] =mobile;
    param["vcode"] = code;
    param["id"] = AppUtil.user.id;

    WebService.post("/user/confirmMobile", param,transform,(res)=>{
      Debug.Log("onSuccess....");
      AppUtil.user.mobile = mobile;
      AppUtil.saveUser(AppUtil.user);
      Toast.info("绑定成功",mainPanel.transform);
      mobilePanel.active = false;
    },(res)=>{
      Toast.error("请输入正确的验证码",mainPanel.transform);
    },this);

  }

  //发送验证码
  void SendVCode(){
    playClick();
    string mobile = mobileInput.text;
    if(mobile==""){
      Toast.error("请输入手机号",mainPanel.transform,errorToastPrefab);
      return;
    }else if(mobile.Length!=11){
      Toast.error("手机号格式不对",mainPanel.transform,errorToastPrefab);
      return;
    }
    sendButton.enabled = false;
    cutdown = 60;
    Timer();
    Invoke("Timer", 1.0f);
    JsonData param = new JsonData();
    param["mobile"] = mobile;
    WebService.post("/user/sendVcode", param,transform,(res)=>{
      Debug.Log("onSuccess22...."+res);
      recieveVCode = (string)res;
    },(res)=>{
      sendButton.enabled = true;
      Toast.error("手机号不正确",mainPanel.transform,errorToastPrefab);
      cutdown = 0;
    },this);
  }

  void Timer() {
    cutdown--;
    Text text = sendButton.transform.Find("Text").GetComponent<Text>();
    text.text = "倒计时"+cutdown+"秒";
    if(cutdown<=0){
      sendButton.enabled = true;
      text.text = "发送验证码";
      sendButton.GetComponent<Image>().color = sendButtonColor;
    }else{
      sendButton.GetComponent<Image>().color = sendButtonDisableColor;
    }
    Invoke("Timer", 1.0f);
    
  }

  // 加入房间
  void inRoom(int no){
    socketClient.sendInRoom(no, (res)=>{
      int errno = (int)res["errno"];
      if(errno==0){
        AppUtil.roomNo = (int)res["roomNo"];
        backAudioSource.Pause();
        socketClient.onRecieveMessage = null;
        
        SceneManager.LoadScene("Pipei");
        Invoke("DestroyScene",1.5f);
      }else{
        string message = (string)res["message"];
        Toast.info(message,mainPanel.transform);
      }
    });
  }

  void showRoomTypeAnimate(){
    headimgButton.transform.gameObject.active = false;
    exitButton.transform.gameObject.active = false;
    nickname.transform.gameObject.active = false;
    rankPanel.transform.gameObject.active = false;
    pipeiButton.transform.gameObject.active = false;
    yuejuButton.transform.gameObject.active = false;
    joinButton.transform.gameObject.active = false;
    worldPanel.transform.gameObject.active = false;
    person.active = false;
    majiangTest.active = false;
    Debug.Log("AppUtil.user.upperObject=="+(AppUtil.user.upperObject==null));

    if(AppUtil.user.upperObject!=null){
      AppUtil.user.upperObject.active = false;
    }
    if(AppUtil.user.trousersObject!=null){
      AppUtil.user.trousersObject.active = false;
    }
    if(AppUtil.user.shoesObject!=null){
      AppUtil.user.shoesObject.active = false;
    }
    if(AppUtil.user.hairObject!=null){
      AppUtil.user.hairObject.active = false;
    }
    if(AppUtil.user.hatObject!=null){
      AppUtil.user.hatObject.active = false;
    }
  }

  void hideRoomTypeAnimate(){
    headimgButton.transform.gameObject.active = true;
    exitButton.transform.gameObject.active = true;
    nickname.transform.gameObject.active = true;
    rankPanel.transform.gameObject.active = true;
    pipeiButton.transform.gameObject.active = true;
    yuejuButton.transform.gameObject.active = true;
    joinButton.transform.gameObject.active = true;
    worldPanel.transform.gameObject.active = true;
    person.active = true;
    majiangTest.active = true;
    if(AppUtil.user.upperObject!=null){
      AppUtil.user.upperObject.active = true;
    }
    if(AppUtil.user.trousersObject!=null){
      AppUtil.user.trousersObject.active = true;
    }
    if(AppUtil.user.shoesObject!=null){
      AppUtil.user.shoesObject.active = true;
    }
    if(AppUtil.user.hairObject!=null){
      AppUtil.user.hairObject.active = true;
    }
    if(AppUtil.user.hatObject!=null){
      AppUtil.user.hatObject.active = true;
    }
    animateHomePipeis();
  }

  void animateHomePipeis(){
    pipeiButton.transform.Find("PipeiImage").GetComponent<RectTransform>().anchoredPosition =new Vector3(0, 0, 0) ;
    yuejuButton.transform.Find("YuejuImage").GetComponent<RectTransform>().anchoredPosition =new Vector3(0, 0, 0) ;
    joinButton.transform.Find("JoinImage").GetComponent<RectTransform>().anchoredPosition =new Vector3(0, 0, 0) ;
    pipeiButton.transform.Find("PipeiImage").DOBlendableMoveBy(new Vector3(0, 30, 0), 0.8f).SetEase(Ease.OutElastic);
    yuejuButton.transform.Find("YuejuImage").DOBlendableMoveBy(new Vector3(0, 30, 0), 0.8f).SetEase(Ease.OutElastic);
    joinButton.transform.Find("JoinImage").DOBlendableMoveBy(new Vector3(0, 30, 0), 0.8f).SetEase(Ease.OutElastic);
  }

  void animateHomePipeisX(){
    pipeiButton.transform.DOBlendableMoveBy(new Vector3(30, 0, 0), 0.8f).SetEase(Ease.OutElastic);
    yuejuButton.transform.DOBlendableMoveBy(new Vector3(30, 0, 0), 0.8f).SetEase(Ease.OutElastic);
    joinButton.transform.DOBlendableMoveBy(new Vector3(30, 0, 0), 0.8f).SetEase(Ease.OutElastic);
  }

  void resetUserPanel(){
    AppUtil.RemoveAllChildren(zhanjiTabPanelContent);
    infoTabImage.transform.DOLocalMoveY(-15f,0.3f);
    for(int i=0;i<infoTabButtons.Count;i++){
      Button button = infoTabButtons[i];
      GameObject panel = infoTabPanels[i];
      int type = i+1;
      if(i!=0){
        panel.active = false;
      }else{
        panel.active = true;
      }
    }
  }

  
  void queryConfig(){
    JsonData ps = new JsonData();
    ps["userid"]=AppUtil.user.id;
    WebService.post("/user/getConfig", ps,transform,(JsonData res)=>{
      #if UNITY_IOS
      string version = (string)res["ios_version"];
      Debug.Log("unityVersion=="+Application.version);
      if(version!=Application.version){
        Confirm.show("有新版本更新，当前版本可能存在未知错误，请去更新",mainPanel.transform,()=>{
          playClick();
          Application.OpenURL("itms-apps://apps.apple.com/cn/app/%E6%97%8C%E5%BE%B7%E4%B8%AD%E5%BF%83%E4%BA%94%E9%BA%BB%E5%B0%86/id1507658298");
        },()=>{
          playClick();
        });
      }
      #elif UNITY_ANDROID
      string version = (string)res["android_version"];
      Debug.Log("unityVersion=="+Application.version);
      if(version!=Application.version){
        Confirm.show("有新版本更新，当前版本可能存在未知错误，请去更新",mainPanel.transform,()=>{
          playClick();
          Application.OpenURL("http://u.xiaowanwu.cn/app-release.apk");
        },()=>{
          playClick();
        });
      }
      #endif
    },(JsonData res)=>{
      
    },this);
  }


  void queryGoldConfigs(){
    JsonData ps = new JsonData();
    ps["userid"]=AppUtil.user.id;
    WebService.post("/user/getPayGoldConfigs", ps,transform,(JsonData data)=>{
      goldConfigs = GoldConfig.toList(data);
      for(int i=0;i<goldConfigs.Count;i++){
        GoldConfig config = goldConfigs[i];
        Button button = payDiamondButtons[i];
        Text titleText = button.transform.Find("TitleText").GetComponent<Text>();
        Text diamondText = button.transform.Find("DiamondText").GetComponent<Text>();
        titleText.text = config.gold+"豆子";
        diamondText.text = config.diamond+"";
      }
    },(JsonData res)=>{
      
    },this);
  }


  void queryDiamondConfigs(){
    JsonData ps = new JsonData();
    ps["userid"]=AppUtil.user.id;
    WebService.post("/user/getPayDiamondConfigs", ps,transform,(JsonData data)=>{
      diamondConfigs = DiamondConfig.toList(data);
      for(int i=0;i<diamondConfigs.Count;i++){
        DiamondConfig config = diamondConfigs[i];
        Button button = payDiamondButtons[i];
        Text titleText = button.transform.Find("TitleText").GetComponent<Text>();
        Text diamondText = button.transform.Find("DiamondText").GetComponent<Text>();
        titleText.text = config.diamond+"";
        diamondText.text = "¥"+config.price;
      }
    },(JsonData res)=>{
      
    },this);
  }

  void createZhuangpanUserModel(){
    if(zhuangpanPerson!=null){
      Destroy(zhuangpanPerson);
    }
    User user = AppUtil.user;
    zhuangbanUser.figure = "female_main";
    zhuangpanPerson = AppUtil.createUserModel(zhuangbanUser,"AnimatorControllers/MainPageController",zhuangpanPanel.transform,this,new Vector3(380f,-348f,35f),new Vector3(450,450,450),new Vector3(0,180,0));

    // GameObject obj = Resources.Load("Models/"+user.figure, typeof(GameObject)) as GameObject;
    // zhuangpanPerson = Instantiate(obj);
    // Animator animator = zhuangpanPerson.AddComponent<Animator>();
    // // animator.controller = animatorController;
    // RuntimeAnimatorController anim = (RuntimeAnimatorController)Resources.Load("AnimatorControllers/MainPageController");//加载资源
    // zhuangpanPerson.GetComponent<Animator>().runtimeAnimatorController = anim;//赋值
    // zhuangpanPerson.transform.parent = zhuangpanPanel.transform;
    // zhuangpanPerson.transform.localPosition = new Vector3(380f,-348f,35f);
    // zhuangpanPerson.transform.localEulerAngles = new Vector3(0,180,0);
    // zhuangpanPerson.transform.localScale = new Vector3(450,450,450);
  }


  void queryModels(){
    
    createZhuangpanUserModel();

    JsonData ps = new JsonData();
    ps["userid"]=AppUtil.user.id;
    ps["offset"]=0;
    ps["pageSize"]=100;
    WebService.post("/model/query", ps,transform,(JsonData res)=>{
      JsonData data = res["data"];
      Debug.Log("/model/query"+data.Count);

      int top =0;
      int left =0;
      RectTransform modelContentTransform = modelContent.transform.GetComponent<RectTransform>();
      modelContentTransform.sizeDelta = new Vector2(0, data.Count*180);
      AppUtil.RemoveAllChildren(modelContent);
      for(int i=0;i<data.Count;i++){
        top = (-i/3)*620;
        left = (i%3)*370;
        JsonData d = data[i];
        string name = (string)d["name"];
        string icon = (string)d["icon"];
        string url = (string)d["url"];
        int price = (int)d["price"];
        int type = (int)d["type"];
        int priceType = (int)d["priceType"];
        GameObject prefab_p = Resources.Load("Prefab/ModelButton") as GameObject;
        GameObject prefab = Instantiate(prefab_p);    // 对象初始化

        // GameObject prefab = prefabButton.transform.GetComponent<Transform>().gameObject;

        prefab.transform.parent = modelContent.transform;
        prefab.transform.localScale = Vector3.one;
        prefab.transform.localPosition = new Vector3(left,top,0);
        RectTransform rectTransform = prefab.transform.GetComponent<RectTransform>();
        Text text = prefab.transform.Find("Text").GetComponent<Text>();
        Text moneyText = prefab.transform.Find("MoneyText").GetComponent<Text>();
        Image GoldImage = prefab.transform.Find("GoldImage").GetComponent<Image>();
        Image image = prefab.transform.Find("Image").GetComponent<Image>();
        text.text = name;
        moneyText.text = price+"";
        LoadImag.load(icon,image,this);
        prefab.GetComponent<Button>().onClick.AddListener(()=>{
          // clearZhuangbanUserModel(type);
          playClick();
          // AnimatorController animatorController = AnimatorController.CreateAnimatorControllerAtPath("Assets/AnimatorControllers/MainPageController.controller");
          Debug.Log("url=="+url+",type="+type);
          GameObject modelRessource = Resources.Load(url) as GameObject;
          GameObject modelObject = Instantiate(modelRessource);    // 对象初始化
          Animator animator = modelObject.AddComponent<Animator>();
          // animator.controller = animatorController;
          // RuntimeAnimatorController anim = (RuntimeAnimatorController)Resources.Load("AnimatorControllers/MainPageController");//加载资源
          // modelObject.GetComponent<Animator>().runtimeAnimatorController = anim;//赋值
          // modelObject.transform.parent = zhuangpanPanel.transform;
          // modelObject.transform.localPosition = zhuangpanPerson.transform.localPosition;
          // modelObject.transform.localEulerAngles = zhuangpanPerson.transform.localEulerAngles;
          // modelObject.transform.localScale = zhuangpanPerson.transform.localScale;
          if(type==1){
            zhuangbanUser.upper = url;
          }else if(type==2){
            zhuangbanUser.trousers = url;
          }else if(type==3){
            zhuangbanUser.shoes = url;
          }else if(type==4){
            zhuangbanUser.hair = url;
          }else if(type==5){
            zhuangbanUser.hat = url;
          }
          createZhuangpanUserModel();
        });
      }
    },(JsonData res)=>{
      
    },this);
  }

  // 清除用户模型
  void clearZhuangbanUserModel(int type){
    if(type==1 && zhuangbanUser.upperObject!=null){
      Destroy(zhuangbanUser.upperObject);
    }else if(type==2 && zhuangbanUser.trousersObject!=null){
      Destroy(zhuangbanUser.trousersObject);
    }else if(type==3 && zhuangbanUser.shoesObject!=null){
      Destroy(zhuangbanUser.shoesObject);
    }else if(type==4 && zhuangbanUser.hairObject!=null){
      Destroy(zhuangbanUser.hairObject);
    }else if(type==5 && zhuangbanUser.hatObject!=null){
      Destroy(zhuangbanUser.hatObject);
    }
  }


  // 清除用户模型
  void clearUserModel(){
    if(AppUtil.user.upperObject!=null){
      Destroy( AppUtil.user.upperObject);
    }
    if(AppUtil.user.trousersObject!=null){
      Destroy( AppUtil.user.trousersObject);
    }
    if(AppUtil.user.shoesObject!=null){
      Destroy( AppUtil.user.shoesObject);
    }
    if(AppUtil.user.hairObject!=null){
      Destroy( AppUtil.user.hairObject);
    }
    if(AppUtil.user.hatObject!=null){
      Destroy( AppUtil.user.hatObject);
    }
  }

  void updateUserModel(){
    JsonData ps = new JsonData();
    ps["id"]=AppUtil.user.id;
    ps["upper"]=zhuangbanUser.upper;
    ps["trousers"]=zhuangbanUser.trousers;
    ps["shoes"]=zhuangbanUser.shoes;
    ps["hair"]=zhuangbanUser.hair;
    ps["hat"]=zhuangbanUser.hat;
    
    WebService.post("/user/update", ps,transform,(JsonData res)=>{
      refreshUser();
      Debug.Log("updateUserMode2l===");

    },(JsonData res)=>{
      
    },this);
  }


  void queryRankUsers(){
    JsonData ps = new JsonData();
    ps["userid"]=AppUtil.user.id;
    ps["offset"]=0;
    ps["pageSize"]=100;
    WebService.post("/friend/query", ps,transform,(JsonData res)=>{
      JsonData data = res["data"];
      int top =0;
      RectTransform zhanjiListContentTransform = rankResultContent.transform.GetComponent<RectTransform>();
      zhanjiListContentTransform.sizeDelta = new Vector2(0, data.Count*180);
      AppUtil.RemoveAllChildren(rankResultContent);
      for(int i=0;i<data.Count;i++){
        top = -i*180;
        JsonData d = data[i];
        
        
        
        User user = new User();
        user.id = (string)d["friendid"];
        
        if(d["nickname"]!=null){
          string nickname = (string)d["nickname"];
          user.nickname = nickname; 
        }

        if(d["headimg"]!=null){
          string headimg = (string)d["headimg"];
          user.headimg = headimg;
        }

        GameObject prefab = Instantiate(rankUserPrefab);    // 对象初始化
        prefab.transform.parent = rankResultContent.transform;
        prefab.transform.localScale = Vector3.one;
        prefab.transform.localPosition = new Vector3(0,top,0);
        RectTransform rectTransform = prefab.transform.GetComponent<RectTransform>();
        Text text = prefab.transform.Find("Text").GetComponent<Text>();
        Image image = prefab.transform.Find("Image").GetComponent<Image>();
        text.text = user.nickname;
        LoadImag.load(user.headimg,image,this);
        prefab.GetComponent<Button>().onClick.AddListener(()=>{
          selectUser = user;
          playClick();
          userPanel.active = true;
          LoadImag.load(user.headimg,infoUserImage,this);
          infoNicknameText.text = user.nickname;
        });
      }
    },(JsonData res)=>{
      
    },this);
  }

  //保存微信图片
  private IEnumerator saveWxImage()
    {
        yield return new WaitForEndOfFrame();
        // Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        // ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        // ss.Apply();
        // Save the screenshot to Gallery/Photos
        Texture2D ss = AppUtil.spriteToTexture2D(customWxImage.sprite);
        NativeGallery.SaveImageToGallery(ss, "GalleryTest", "My img {0}.png");
        // To avoid memory leaks
        Destroy(ss);
        Toast.info("保存成功",mainPanel.transform,false);
    }


  void queryUsers(bool neary){
    JsonData ps = new JsonData();
    ps["userid"]=AppUtil.user.id;
    ps["offset"]=0;
    ps["pageSize"]=50;
    ps["key"] = searchField.text;
    RectTransform contentPanelTransform = searchPanel.transform.Find("Panel").GetComponent<RectTransform>();
    float panelWidth = Screen.width*0.7f;
    contentPanelTransform.sizeDelta = new Vector2(panelWidth,Screen.height-240);
    WebService.post("/user/queryByKey", ps,transform,(JsonData res)=>{
      JsonData data = res["data"];
      int top =0;
      RectTransform zhanjiListContentTransform = searchResultContent.transform.GetComponent<RectTransform>();
      zhanjiListContentTransform.sizeDelta = new Vector2(0, data.Count*200);
      AppUtil.RemoveAllChildren(searchResultContent);
      float width = (panelWidth-60)/2;
      for(int i=0;i<data.Count;i++){
        JsonData d = data[i];
        User user = User.toUserOnly(d);
        if(d["nickname"]!=null){
          string headimg = (string)d["headimg"];
          string nickname = (string)d["nickname"];
          top = -i/2*200;
          float left = i%2==0?0:(width+20);
          Debug.Log("Screen.width=="+Screen.width+","+width+",");
          GameObject prefab = Instantiate(searchUserPrefab);    // 对象初始化
          prefab.transform.parent = searchResultContent.transform;
          prefab.transform.localScale = Vector3.one;
          prefab.transform.localPosition = new Vector3(left,top*1.0f,0f);
          RectTransform rectTransform = prefab.transform.GetComponent<RectTransform>();
          rectTransform.sizeDelta = new Vector2(width,180f);
          Text text = prefab.transform.Find("TitleText").GetComponent<Text>();
          Image image = prefab.transform.Find("Image").GetComponent<Image>();
          text.text = nickname;
          LoadImag.load(headimg,image,this);
          prefab.GetComponent<Button>().onClick.AddListener(()=>{
            selectUser = user;
            playClick();
            userPanel.active = true;
            LoadImag.load(headimg,infoUserImage,this);
            infoNicknameText.text = nickname;
          });
          prefab.transform.Find("Button").gameObject.GetComponent<Button>().onClick.AddListener(()=>{
            playClick();
            AddFriend(user);
          });
        }
      }
    },(JsonData res)=>{
      
    },this);
  }

  void AddFriend(User user){
     JsonData ps = new JsonData();
    ps["userid"]=AppUtil.user.id;
    ps["friendid"]=user.id;
    WebService.post("/friend/save", ps,transform,(JsonData res)=>{
      Toast.info("成功发送请求",mainPanel.transform,false);
    },(JsonData res)=>{
      Toast.error("发送请求失败",mainPanel.transform);
    },this);
  }

  //查询消息
  void queryMessages(){
    JsonData ps = new JsonData();
    ps["revUser"]=AppUtil.user.id;
    ps["offset"]=0;
    ps["pageSize"]=50;
    WebService.post("/message/query", ps,transform,(JsonData res)=>{
      JsonData data = res["data"];
      int top =0;
      RectTransform zhanjiListContentTransform = messageRowContentList.transform.GetComponent<RectTransform>();
      zhanjiListContentTransform.sizeDelta = new Vector2(0, data.Count*130);
      AppUtil.RemoveAllChildren(messageRowContentList);
      for(int i=0;i<data.Count;i++){
        top = -i*130;
        JsonData d = data[i];
        string sendUserHeadimg = (string)d["sendUserHeadimg"];
        string msgTitle = (string)d["msgTitle"];
        int msgType = (int)d["msgType"];
        string sendUser = (string)d["sendUser"];

        string createTime = (string)d["createTime"];
        GameObject prefab = Instantiate(messageRowPanel);    // 对象初始化
        prefab.transform.parent = messageRowContentList.transform;
        prefab.transform.localScale = Vector3.one;
        prefab.transform.localPosition = new Vector3(0,top,0);
        RectTransform rectTransform = prefab.transform.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(1250f,129f);
        Text text1 = prefab.transform.Find("Text1").GetComponent<Text>();
        Text text2 = prefab.transform.Find("Text2").GetComponent<Text>();
        Image image = prefab.transform.Find("Image").GetComponent<Image>();
        text1.text = msgTitle;
        text2.text = createTime;
        LoadImag.load(sendUserHeadimg,image,this);
        prefab.GetComponent<Button>().onClick.AddListener(()=>{
          playClick();
          if(msgType==1){
            Confirm.show(msgTitle,mainPanel.transform,()=>{
              playClick();
              agreeFriend(sendUser);
            },()=>{
              playClick();
            });
          }
        });
      }
    },(JsonData res)=>{
      
    },this);
  }

  //接受好友请求
  void agreeFriend(string sendUser){
    JsonData ps = new JsonData();
    ps["userid"]=AppUtil.user.id;
    ps["friendid"]=sendUser;
    WebService.post("/friend/agree", ps,transform,(JsonData res)=>{
      Toast.info("添加好友成功",mainPanel.transform);
      queryRankUsers();
    },(JsonData res)=>{
      Toast.info("添加好友失败",mainPanel.transform);
    },this);
  }


  void setUserInfo(){
    nickname.text = AppUtil.user.nickname;
    goldText.text = AppUtil.castNumToStr(AppUtil.user.gold);
    diamondText.text = AppUtil.castNumToStr(AppUtil.user.diamond);
  }

  //查询签到记录
  void querySign(){
    JsonData ps = new JsonData();
    ps["userid"]=AppUtil.user.id;
    WebService.post("/user/getDaySign", ps,transform,(JsonData res)=>{
      bool showGoldDay = (bool)res["showSign"];
      if(showGoldDay){
        int continueCount = (int)res["continueCount"];

        for(int i=0;i<signButtons.Count;i++){
          Button signButton = signButtons[i];
          signButton.enabled = i==continueCount;
          signButton.GetComponent<Transform>().Find("LingquImage").gameObject.active = i<continueCount;
          int gold = (int)res["day"+(i+1)];
          signButton.GetComponent<Transform>().Find("GoldText").GetComponent<Text>().text = gold+"";
        }
        signPanel.active = true;
      }
    },(JsonData res)=>{
      
    },this);
    
  }

  void updateLoginTime(){
    JsonData ps = new JsonData();
    ps["id"]=AppUtil.user.id;

    #if UNITY_IOS
    ps["system"] = "ios";
    #elif UNITY_ANDROID
    ps["system"] = "android";
    #endif
    
    ps["appVersion"] = Application.version;

    WebService.post("/user/updateLoginTime", ps,transform,(JsonData res)=>{
      
    },(JsonData res)=>{

    },this);
    
  }

  void refreshUser(){
    JsonData ps = new JsonData();
    ps["id"]=AppUtil.user.id;
    WebService.post("/user/get", ps,transform,(JsonData res)=>{
      clearUserModel();
      User u = User.toUserOnly(res);
      AppUtil.saveUser(u);
      AppUtil.user = u;
      setUserInfo();
      createUserModel();

    },(JsonData res)=>{

    },this);
    
  }


  void payGold(int type,Button signButton){
    JsonData ps = new JsonData();
    ps["userid"]=AppUtil.user.id;
    ps["type"]=type;
    WebService.post("/user/payGold", ps,transform,(JsonData res)=>{
      refreshUser();
      Toast.info("购买成功",mainPanel.transform);
    },(JsonData res)=>{
      Toast.info("钻石不够",mainPanel.transform);
    },this);
  }

  void payDiamond(DiamondConfig config){
    
    selectDiamondConfig = config;
    Toast.loading(mainPanel.transform);
    JsonData ps = new JsonData();
    ps["userid"]=AppUtil.user.id;
    ps["fast"]=false;
    ps["price"]=config.price;
    ps["title"] = "充值买钻石";
    WebService.post("/weixin/prePayApp", ps,transform,(JsonData res)=>{
      string appId = (string)res["appId"];
      string partnerId = (string)res["partnerId"];
      string prepay_id = (string)res["prepay_id"];
      string package = (string)res["package"];
      string nonceStr = (string)res["nonceStr"];
      string timestamp = (string)res["timestamp"];
      string sign = (string)res["sign"];
      #if UNITY_IPHONE  
      if(isWechatInstalled()){
        wechatget(appId,partnerId,prepay_id,package,nonceStr,timestamp,sign);
      }else{
        _StartBuyProduct(null,null,null,config.id);
      }
      #elif UNITY_ANDROID
      AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
      AndroidJavaObject  currentActivity = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
      currentActivity.Call("wechatPay",appId,partnerId,prepay_id,package,nonceStr,timestamp,sign);
      Toast.hideLoading();
      #endif
      
    },(JsonData res)=>{
      Toast.hideLoading();
    },this);
    
    
  }

  void lingqu(int type,Button signButton){
    JsonData ps = new JsonData();
    ps["userid"]=AppUtil.user.id;
    ps["type"]=type;
    WebService.post("/user/signGold", ps,transform,(JsonData res)=>{
      signButton.GetComponent<Transform>().Find("LingquImage").gameObject.active = true;
    },(JsonData res)=>{

    },this);
  }

  // 创建房间类型
  void createRoomTypes(){
    float width = 400;
    float height = 230;
    float space = 50;
    int column = 4;
    float left = Screen.width/2-(width+space)*column/2;
    float top = Screen.height/2-(height+space)*2/2;
    

    for(int i=0;i<sceneConfigs.Count;i++){
      SceneConfig  sceneConfig = sceneConfigs[i];
      GameObject roomType = Instantiate(roomTypeButton);    // 对象初始化
      roomType.transform.parent = roomTypePanel.transform;
      roomType.transform.localScale = Vector3.one;
      roomType.transform.localPosition = Vector3.zero;
      Text text = roomType.transform.Find("Text").GetComponent<Text>();
      Text beiText = roomType.transform.Find("BeiText").GetComponent<Text>();
      Text goldText = roomType.transform.Find("GoldText").GetComponent<Text>();
      RectTransform roomTypeTransform = roomType.transform.GetComponent<RectTransform>();
      roomTypeTransform.sizeDelta = new Vector2(width, height);
      float t = top+height*(i/column)+(i/column)*space;
      t = Screen.height-t-height;
      Debug.Log("top22==="+t);
      float endx = left+width*(i%column)+(i%column)*space;
      roomType.transform.position = new Vector3(endx+100,t,0);
      text.text = sceneConfig.name;
      beiText.text = sceneConfig.times+"";
      goldText.text = sceneConfig.minGold+"~"+sceneConfig.maxGold;
      if(i==0){
        roomType.GetComponent<Button>().image.sprite = selectRoomTypeBgSprite;
        selectRoomTypeButton = roomType.GetComponent<Button>();
      }
      roomType.GetComponent<Button>().onClick.AddListener(()=>{
        playClick();
        selectRoomType = sceneConfig.type;
        AppUtil.selectRoomType = selectRoomType;
        if(selectRoomTypeButton!=null){
          selectRoomTypeButton.image.sprite = null;
        }
        roomType.GetComponent<Button>().image.sprite = selectRoomTypeBgSprite;
        selectRoomTypeButton = roomType.GetComponent<Button>();
        hasRoom();
      });
      roomType.transform.DOMoveX(endx,0.3f).OnComplete(()=>{
        roomType.transform.DOBlendableMoveBy(new Vector3(30, 0, 0), 1).SetEase(Ease.OutElastic);
      });
    }
  }

  void playClick(){
    if(audioToggle.isOn){
      audioSource.PlayOneShot (clickSound,effect_sound_volume);
    }
  }

  IEnumerator GoRoom(){
    AsyncOperation async = Application.LoadLevelAsync("game");
    yield return async;
    Debug.Log("Loading complete");
  }

	// Use this for initialization
	void Start () {
    try{
      #if UNITY_IOS
        _InitAppStorePay (gameObject.name);
      #endif
    }
    catch (Exception ex)
    {
        Debug.Log(ex);
    }
    
	}

	void Update () {
		
	}

  void WechatgetCallback(string productInfo)
	{
    WechatPayCallback(productInfo);
	}

  void WechatPayCallback(string productInfo)
	{
    Toast.hideLoading();
    Toast.info("购买成功",mainPanel.transform);
    refreshUser();
    AppUtil.uploadData(2,"购买钻石成功",mainPanel.transform,this);
	}

  void WechatgetFaildCallback(string code)
	{
    WechatPayFaildCallback(code);
	}

  void WechatPayFaildCallback(string code)
	{
    Toast.hideLoading();
    Toast.info("购买失败",mainPanel.transform);
    AppUtil.uploadData(3,"购买钻石失败",mainPanel.transform,this);
	}

  void BuyProductSuccess(string productInfo)
	{
    Toast.hideLoading();
    
    JsonData ps = new JsonData();
    ps["userid"]=AppUtil.user.id;
    ps["productId"]=selectDiamondConfig.id;
    WebService.post("/user/payDiamond", ps,transform,(JsonData res)=>{
      refreshUser();
      Toast.info("购买成功",mainPanel.transform);
    },(JsonData res)=>{ 
      Toast.info("交易失败",mainPanel.transform);
    },this);

		Debug.Log ("--------------buy success ---------" + productInfo);
	}

	void BuyProudctFailed(string productInfo)
	{
    Toast.hideLoading();
    Toast.error("支付异常",mainPanel.transform);
		Debug.Log ("------------ buy failed ----------" + productInfo);
	}

	void DebugUnityMessage(string logMessage)
	{
    // Toast.hideLoading();
		Debug.Log ("----------------Debug : " + logMessage +  "-------------------");
	}
  void IsConnect(){
    SocketClient.socketClient.IsConnect();
  }

  #if UNITY_IPHONE  
  //AppStorePay
	[System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern void _BuyProduct(string productId);
	[System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern void _InitAppStorePay (string callBackObjectName);
	[System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern void _StartBuyProduct(string serverId, string orderId, string exInfo, string productId);
	[System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern void shareToWechatTimeline(string title, string url, string desc);
  [System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern void wechatget(string appId,string partnerId,string prepayId,string packageValue,string nonceStr,string timeStamp,string sign);
  [System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern void shareToWechatFriend(string title, string url, string desc);
  [System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern bool isWechatInstalled ();

  #endif


}
