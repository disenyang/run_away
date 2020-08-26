using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using LitJson;
using UnityEngine.SceneManagement;
public class Pipei : MonoBehaviour {
  public GameObject fastBuyPanel;
  public Button customShareButton;

  public Text title;
  public Text fastBuyText;
  public Button fastBuyButton;
  public Button fastCancelButton;
  AudioSource backAudioSource;
  public GameObject loadImag;
  public GameObject loadPanel;
  public Button shareButton;
  public Button readyButton;
  public Button exitButton;

  public Button startButton;

  public AudioClip clickSound;
  public AudioClip backSound;
	private AudioSource audioSource;

  public GameObject mainPanel;
  float effect_sound_volume = 0.6f;

  public GameObject noUser1;
  public GameObject noUser2;
  public GameObject noUser3;
  public GameObject noUser4;

  public GameObject effect1;
  public GameObject effect2;
  public GameObject effect3;
  public GameObject effect4;
  public GameObject user1;
  public GameObject user2;
  public GameObject user3;
  public GameObject user4;

  public GameObject statusText1;
  public GameObject statusText2;
  public GameObject statusText3;
  public GameObject statusText4;

  public GameObject statusPanel1;
  public GameObject statusPanel2;
  public GameObject statusPanel3;
  public GameObject statusPanel4;


  public GameObject customPanel;

  public Button customCloseButton;

  public Image customWxImage;
  public Button saveWxButton;

  public GameObject nicknameText1;
  public GameObject nicknameText2;
  public GameObject nicknameText3;
  public GameObject nicknameText4;

  List<GameObject> noUserModels = new List<GameObject>();
  List<GameObject> statusTexts = new List<GameObject>();
  List<GameObject> statusPanels = new List<GameObject>();

  List<GameObject> nicknameTexts = new List<GameObject>();

  List<GameObject> effects = new List<GameObject>();

  Room room;

  public GameObject friendPanel;

  public GameObject friendResultContent;

  public GameObject friendUserPrefab;

  SocketClient socketClient;
  
	void Start () {
    if(AppUtil.user==null){
      
      SceneManager.LoadScene("Login");
      Invoke("DestroyScene",1.5f);
      return;
    }
    backAudioSource = (AudioSource)this.gameObject.GetComponents(typeof(AudioSource))[1];

    if(AppUtil.getMusicConfig()){
      backAudioSource.Play();
    }else{
      backAudioSource.Pause();
    }

    noUserModels.Add(noUser1);
    noUserModels.Add(noUser2);
    noUserModels.Add(noUser3);
    noUserModels.Add(noUser4);

    effects.Add(effect1);
    effects.Add(effect2);
    effects.Add(effect3);
    effects.Add(effect4);
    // userModels.Add(user1);
    // userModels.Add(user2);
    // userModels.Add(user3);
    // userModels.Add(user4);

    statusTexts.Add(statusText1);
    statusTexts.Add(statusText2);
    statusTexts.Add(statusText3);
    statusTexts.Add(statusText4);

    statusPanels.Add(statusPanel1);
    statusPanels.Add(statusPanel2);
    statusPanels.Add(statusPanel3);
    statusPanels.Add(statusPanel4);

    nicknameTexts.Add(nicknameText1);
    nicknameTexts.Add(nicknameText2);
    nicknameTexts.Add(nicknameText3);
    nicknameTexts.Add(nicknameText4);
    
    for(int i=0;i<nicknameTexts.Count;i++){
      GameObject noUserModel = noUserModels[i];
      GameObject statusText = statusTexts[i];
      GameObject statusPanel = statusPanels[i];
      GameObject nicknameText = nicknameTexts[i];
      GameObject effect = effects[i];
      effect.active = true;
      noUserModel.active = true;
      statusPanel.active = false;
      statusText.GetComponentInChildren<TextMesh>().text = "";
      nicknameText.GetComponentInChildren<TextMesh>().text = "";
    }

    audioSource = GetComponent<AudioSource> ();
    socketClient = SocketClient.socketClient;
    socketClient.monoBehaviour = this;
    socketClient.transform = mainPanel.transform;
    socketClient.onRecieveMessage = (JsonData data)=>{
      string cmd = (string)data["cmd"];
      JsonData content = data["con"];
      if(cmd=="in"){
        room = Room.toRoom(content,new List<Majiang>());
        Toast.info(room.extMessage+"加入",mainPanel.transform,true);
        dealRoom();
      }else if(cmd=="ready"){
        string userId = (string)content["user"];
        int status = (int)content["status"];
        int roomStatus = (int)content["roomStatus"];
        User user =  room.getUser(userId);
        user.status = status;
        room.status = roomStatus;
        dealRoom();
      }else if(cmd=="start"){
        // room = Room.toRoom(content,allmajiangs);
        // dealRoom();
        Invoke("goRoom",0);
      }else if(cmd=="room"){
        room = Room.toRoom(content,new List<Majiang>());
        dealRoom();
      }
    };
    friendPanel.active = false;
    getRoom();

    readyButton.onClick.AddListener(()=>{
      playClick();
      Debug.Log("你好...");
      socketClient.sendReady(AppUtil.roomNo,(JsonData res)=>{
        int errno = (int)res["errno"];
        if(errno!=0){
          if(errno==1){
            //豆子不足
            JsonData param = new JsonData();
            param["userid"] = AppUtil.user.id;
            param["inTimesType"] = AppUtil.selectRoomType;
            WebService.post("/user/getFreeGetGold", param,mainPanel.transform,(resp)=>{
              bool showFreeGet = (bool)resp["showFreeGet"];
              int maxFreeCount = (int)resp["maxCount"];
              int getFreeCount = (int)resp["getCount"];
              int perFreeGold = (int)resp["perGold"];
              if(showFreeGet){
                if(getFreeCount>=maxFreeCount){
                  fastBuyPanel.active = true;
                  return;
                }else{
                  // Confirm.show("今日第"+(getFreeCount+1)+"次赠送"+perFreeGold+"个豆子，是否领取("+(getFreeCount+1)+"/"+maxFreeCount+")",mainPanel.transform,()=>{
                  //   JsonData ps = new JsonData();
                  //   ps["userid"] = AppUtil.user.id;
                  //   WebService.post("/user/freeGetGold", ps,mainPanel.transform,(res2)=>{
                  //     refreshUser();
                  //     GoToRoom();
                  //   },(res3)=>{
                  //     Toast.error("系统错误",mainPanel.transform);
                  //   },this);
                  // },()=>{

                  // });

                  Confirm.show("豆子不足，分享到朋友圈可以获得"+perFreeGold+"个豆子，是否现在分享",mainPanel.transform,()=>{
                    playClick();
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
                  },()=>{
                    playClick();
                  });
                }
                return;
              }else{
                
              }
            },(resp)=>{
              Toast.error("系统错误",mainPanel.transform);
            },this);
          }else{
            string message = (string)res["message"];
            Toast.error(message,mainPanel.transform);
          }
          
        }
      });
      getRoom();
    });

    shareButton.onClick.AddListener(()=>{
      playClick();
      share();
    });

    customShareButton.onClick.AddListener(()=>{
      playClick();
      share();
    });

    exitButton.onClick.AddListener(()=>{
      playClick();
      socketClient.sendExitRoom(AppUtil.roomNo,(res)=>{
        
      });
      //开始加载场景
      backAudioSource.Pause();
      socketClient.onRecieveMessage = null;
      
      SceneManager.LoadScene("MainPage");
      Invoke("DestroyScene",1.5f);
    });

		startButton.onClick.AddListener(()=>{
      playClick();
      if(room.status==-1){
        backAudioSource.Pause();
        socketClient.onRecieveMessage = null;
        
        SceneManager.LoadScene("game1");
        Invoke("DestroyScene",1.5f);
      }else{
        if(room.userBottom.status!=-1){
          Toast.info("请准备",mainPanel.transform,false);
        }else{
          Toast.info("好友未准备",mainPanel.transform,false);
        }
      }
      getRoom();      
    });
    queryFriends();


    saveWxButton.onClick.AddListener(()=>{
      playClick();
      StartCoroutine(saveWxImage());
    });

    customCloseButton.onClick.AddListener(()=>{
      playClick();
      customPanel.active = false;
      
    });

    customPanel.active = false;
    

    loadImag.GetComponent<Transform>().DORotate(new Vector3(0f,0f,-360f), 0.8f,RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);

    fastBuyButton.onClick.AddListener(()=>{
      playClick();
      JsonData ps = new JsonData();
      ps["userid"]=AppUtil.user.id;
      ps["fast"]=true;
      ps["price"]=1;
      ps["title"] = "快速充值买钻石";
      WebService.post("/weixin/prePayApp", ps,transform,(JsonData res)=>{
        string appId = (string)res["appId"];
        string partnerId = (string)res["partnerId"];
        string prepay_id = (string)res["prepay_id"];
        string package = (string)res["package"];
        string nonceStr = (string)res["nonceStr"];
        string timestamp = (string)res["timestamp"];
        string sign = (string)res["sign"];
        #if UNITY_IPHONE  
        // _StartBuyProduct(null,null,null,config.id);
        wechatget(appId,partnerId,prepay_id,package,nonceStr,timestamp,sign);
        #elif UNITY_ANDROID
        AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject  currentActivity = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
        currentActivity.Call("wechatPay",appId,partnerId,prepay_id,package,nonceStr,timestamp,sign);
        Toast.hideLoading();
        #endif
        
      },(JsonData res)=>{
        Toast.hideLoading();
      },this);
      
      fastBuyPanel.active = false;
    });
    fastCancelButton.onClick.AddListener(()=>{
      playClick();
      fastBuyPanel.active = false;
    });
    getOneYuanGold();
    fastBuyPanel.active = false;
	}

  void getOneYuanGold(){
    JsonData ps = new JsonData();
    ps["userid"] = AppUtil.user.id;
    WebService.post("/user/getOneYuanGold", ps,mainPanel.transform,(JsonData res)=>{
      int oneYuanGold = (int)res;
      fastBuyText.text = "充值1元获得"+oneYuanGold+"豆子";
    },(res3)=>{
      
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
        Toast.info("保存成功,微信扫码进群~",mainPanel.transform,false);
    }


  void share(){
    string title = "正在邀请您加入旌德中心五麻将房间，房间号："+AppUtil.roomNo;
    string url = "https://www.xiaowanwu.cn/h5_majiang_zxw_static/index.html#/room/"+AppUtil.roomNo;
    string desc = "旌德麻将";

    #if UNITY_IPHONE
    shareToWechatFriend(title,url,desc);
    #elif UNITY_ANDROID
    AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    AndroidJavaObject  currentActivity = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
    currentActivity.Call("shareToWechat",title,url,desc,true);
    #endif
  }

  void waitTooLongRandom(){
    for(int i=0;i<room.users.Count;i++){
      User user = room.users[i];
      if(user.id==null){
        customPanel.active = true;
        LoadImag.load("http://u.xiaowanwu.cn/majiang_weixin.jpg",customWxImage,this);
        // Confirm.show("当前在线用户较少【大哭..】。你可以建立房间，邀请好友来玩～",mainPanel.transform,()=>{
        //   playClick();
        // },()=>{
        //   playClick();
        // });
        break;
      }
    }
    Invoke("waitTooLongRandom",5);
  }

  void waitTooLong(){
    for(int i=0;i<room.users.Count;i++){
      User user = room.users[i];
      if(user.id==null){
        Confirm.show("您可以邀请您的微信好友一起来玩~",mainPanel.transform,()=>{
          playClick();
          share();
        },()=>{
          playClick();
        });
        break;
      }
    }
  }

  void initUserModel(User user,int i){
    float x = 0;
    float y = 0;
    if(i==0){
      x = -17.36f;
      if(user1!=null){
        return;
      }
    }else if(i==1){
      x = -5.69f;
      if(user2!=null){
        return;
      }
    }else if(i==2){
      x = 3.19f;
      if(user3!=null){
        return;
      }
    }else if(i==3){
      x = 14.45f;
      if(user4!=null){
        return;
      }
    }
    user.figure = "female_main";
    GameObject obj = AppUtil.createUserModel(user,"AnimatorControllers/MainPageController",transform,this,new Vector3(x,y,10f),new Vector3(10,10,10),new Vector3(0,180,0));
    if(i==0){
      user1 = obj;
    }else if(i==1){
      user2 = obj;
    }else if(i==2){
      user3 = obj;
    }else if(i==3){
      user4 = obj;
    }
  }

  void goRoom(){
    backAudioSource.Pause();
    socketClient.onRecieveMessage = null;
    
    SceneManager.LoadScene("game1");
    Invoke("DestroyScene",1.5f);
  }

  void getRoom(){
    socketClient.sendGetRoom(AppUtil.roomNo,(JsonData resData)=>{
      JsonData roomData = (JsonData)resData["room"];
      room = Room.toRoom(roomData,new List<Majiang>());
      if(socketClient.monoBehaviour==this){
        dealRoom();
      }
      if(room.random){
        friendPanel.active = true;
        title.text = "匹配麻友";
        Invoke("waitTooLongRandom",5);
      }else{
        friendPanel.active = true;
        title.text = "房间号："+room.no;
        loadPanel.active = false;
        Invoke("waitTooLong",8);
      }
    });
  }


  //分享到朋友圈成功
  void WXShareSceneTimelineCallBack(string code){
    // Toast.info("分享成功",mainPanel.transform);
    JsonData ps = new JsonData();
    ps["userid"] = AppUtil.user.id;
    WebService.post("/user/shareGetGold", ps,mainPanel.transform,(res2)=>{
      Toast.info("现在可以准备了",mainPanel.transform);
    },(res3)=>{
      Toast.error("今日分享已达上限",mainPanel.transform);
    },this);

    AppUtil.uploadData(1,"分享获取豆子",mainPanel.transform,this);
  }

  void DestroyScene(){
    Destroy(gameObject);
  }

  void dealRoom(){
    bool full = true;
    for(int i=0;i<room.users.Count;i++){
      User user = room.users[i];
      GameObject noUserModel = noUserModels[i];
      GameObject statusText = statusTexts[i];
      GameObject statusPanel = statusPanels[i];
      GameObject nicknameText = nicknameTexts[i];
      GameObject effect = effects[i];
      if(user.id!=null){
        initUserModel(user,i);
        effect.active = false;
        noUserModel.active = false;
        statusPanel.active = true;
        Debug.Log("用户状态="+user.status+"//"+(statusText==null));
        statusText.GetComponentInChildren<TextMesh>().text = User.getStatusText(user.status);
        nicknameText.GetComponentInChildren<TextMesh>().text = user.nickname;
      }else{
        full = false;
        effect.active = true;
        noUserModel.active = true;
        statusPanel.active = false;
        statusText.GetComponentInChildren<TextMesh>().text = "";
        nicknameText.GetComponentInChildren<TextMesh>().text = "";
      }
    }

    if(full){
      loadPanel.active = false;
    }else{
      loadPanel.active = true;
    }
  }

  void queryFriends(){
    JsonData ps = new JsonData();
    ps["userid"]=AppUtil.user.id;
    ps["offset"]=0;
    ps["pageSize"]=100;
    WebService.post("/friend/query", ps,transform,(JsonData res)=>{
      JsonData data = res["data"];
      int top =0;
      RectTransform zhanjiListContentTransform = friendResultContent.transform.GetComponent<RectTransform>();
      zhanjiListContentTransform.sizeDelta = new Vector2(0, data.Count*180);
      AppUtil.RemoveAllChildren(friendResultContent);
      for(int i=0;i<data.Count;i++){
        JsonData d = data[i];
        if(d["headimg"]!=null && d["nickname"]!=null){
          top = -i*180;
          string friendid = (string)d["friendid"];
          string headimg = (string)d["headimg"];
          string nickname = (string)d["nickname"];
          bool online = (bool)d["online"];
          GameObject prefab = Instantiate(friendUserPrefab);    // 对象初始化
          prefab.transform.parent = friendResultContent.transform;
          prefab.transform.localScale = Vector3.one;
          prefab.transform.localPosition = new Vector3(0,top,0);
          RectTransform rectTransform = prefab.transform.GetComponent<RectTransform>();
          Text text = prefab.transform.Find("Text").GetComponent<Text>();
          Text statusText = prefab.transform.Find("StatusText").GetComponent<Text>();

          Image image = prefab.transform.Find("Image").GetComponent<Image>();
          text.text = nickname;
          statusText.text = online?"在线":"离线";
          LoadImag.load(headimg,image,this);
          prefab.transform.Find("Button").GetComponent<Button>().onClick.AddListener(()=>{
            playClick();

            socketClient.sendInviteFriend(friendid,AppUtil.roomNo,(JsonData resData)=>{
              Toast.info("成功发送邀请",mainPanel.transform,true);
            });
          });
        }
        
      }
    },(JsonData res)=>{
      
    },this);
  }
  void IsConnect(){
    SocketClient.socketClient.IsConnect();
  }
	
	void Update () {
		
	}


  void playClick(){
    if(AppUtil.getAudioConfig()){
      audioSource.PlayOneShot (clickSound,effect_sound_volume);
    }
  }

  #if UNITY_IPHONE  
  [System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern void wechatget(string appId,string partnerId,string prepayId,string packageValue,string nonceStr,string timeStamp,string sign);
  //AppStorePay
	[System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern void shareToWechatFriend(string title, string url, string desc);
  [System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern void shareToWechatTimeline(string title, string url, string desc);
  #endif
}
