using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using LitJson;
using UnityEngine.SceneManagement;
using agora_gaming_rtc;
using System.IO;

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

#if(UNITY_2018_3_OR_NEWER)
using UnityEngine.Android;
#endif
public class Home : MonoBehaviour {
  public GameObject personBottom;
  public GameObject personRight;
  public GameObject personTop;
  public GameObject personLeft;

  public GameObject nowMajiangAlert;

  public GameObject voicePanel;
  public Button colseVoicePanelButton;
  public Toggle voiceToggle;
  public GameObject voiceUser1Button;
  public GameObject voiceUser2Button;
  public GameObject voiceUser3Button;
  public GameObject voiceUser4Button;
  public List<GameObject> voiceUserButtons;


  public GameObject socialAnimatorPanel;
  public Button colseSocialAnimatorButton;
  public Button socialAnimate1Button;
  public Button socialAnimate2Button;
  public Button socialAnimate3Button;
  public Button socialAnimate4Button;
  public Button socialAnimate5Button;
  public Button socialAnimate6Button;
  public Button socialAnimate7Button;
  public Button socialAnimate8Button;
  public List<Button> socialAnimateButtons = new List<Button>();


  public Button socialButton;

  public GameObject socialPanel;
  public GameObject socialContentPanel;

  public Button colseSocialButton;
  public GameObject chatPrefab;



  


  public GameObject roomScoreRowCircle;
  public ParticleSystem pengParticleSystem;
  public ParticleSystem huParticleSystem;

  public ParticleSystem gangParticleSystem;

  AudioSource backAudioSource;

  public Toggle musicToggle;

  public Toggle audioToggle;

  public GameObject totalScore1;
  public GameObject totalScore2;
  public GameObject totalScore3;
  public GameObject totalScore4;

  List<GameObject> totalScores = new List<GameObject>();

  public GameObject roomRecordsContentList;
  public GameObject roomRecordsTopPanel;

  public GameObject roomRecordsPanel;
  public Button roomRecordsPanelCloseButton;

  public GameObject roomScoreRowPanel;

  public GameObject userScoreInfoPanel;


  public GameObject zhanjiRowPanel;

  public GameObject userPanel;
  public Button userPanelCloseButton;
  public Button nowTabButton;
  bool nowTabButtonSelected = true;
  public Button historyTabButton;

  List<Button> scoreTabButtons = new List<Button>();


  

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


  public GameObject majiangButtonPrefab;

  public Image tianyingAlertImage;

  public GameObject tianyingAlert;

  public GameObject optionPanel;

  public Button gang1Button;
  public Button gang2Button;
  public Button gang3Button;


  public Button pengButton;

  public Button guoButton;
  public Button huButton;

  public Text huDescText;


  public Button recordButton;


  public Button settingButton;

  public Button voiceButton;
  public Button shejiaoButton;

  public AudioClip pengSound;
  public AudioClip gangSound;
  public AudioClip lossSound;
  public AudioClip huSound;

  public AudioClip guoSound;

  public AudioClip clickSound;
  public AudioClip backSound;
	private AudioSource audioSource;
  public GameObject userPostionAlert;

  public GameObject userPostionAlertText;

  public Text roomNoText;

  public Text leaveText;

  public Text ziValueText;

  public Text jiaoValueText;

  public Button userTopHeadimgButton;
  public Button userLeftHeadimgButton;
  public Button userRightHeadimgButton;
  public Button userBottomHeadimgButton;


  public Button exitButton;
  public Button zhishaiziButton;
  public Image userTopHeadimg;
  public Image userLeftHeadimg;
  public Image userRightHeadimg;
  public Image userBottomHeadimg;


  public Text userTopNickname;
  public Text userLeftNickname;
  public Text userRightNickname;
  public Text userBottomNickname;

  public Text userTopZi;
  public Text userLeftZi;
  public Text userRightZi;
  public Text userBottomZi;


  public Text userTopStatus;
  public Text userLeftStatus;
  public Text userRightStatus;
  public Text userBottomStatus;

  public GameObject userTopStatusPanel;
  public GameObject userLeftStatusPanel;
  public GameObject userRightStatusPanel;
  public GameObject userBottomStatusPanel;

  public Text userTopJiao;
  public Text userLeftJiao;
  public Text userRightJiao;
  public Text userBottomJiao;
  public GameObject myMajiangsPanel;

  public Text userTopGold;
  public Text userLeftGold;
  public Text userRightGold;
  public Text userBottomGold;

  public Image userTopVoice;
  public Image userLeftVoice;
  public Image userRightVoice;
  public Image userBottomVoice;

  private int rotateY=0;
  public Camera mainCamera;
  public GameObject majiangPrefab;

  public GameObject bottomMajiangCao;
  public GameObject leftMajiangCao;
  public GameObject rightMajiangCao;
  public GameObject topMajiangCao;

  public GameObject mainPanel;
  public GameObject errorToastPrefab;
  public GameObject confirmPrefab;


  public GameObject shaizi1GameObject;
  public GameObject shaizi2GameObject;

  public AudioSource cutdownAudioSource;


  private int totalNumber=17*8;
  private int deng=17;

  private float deskPositionY = 8.502f;//桌面的y坐标(高度)

  private float majiangWidth = 0.17f*9/6;//麻将宽度
  private float majiangHeight = 0.12f*9/6;//麻将高度
  private float majiangLength = 0.28f*9/6;//麻将高度
  private float xipaiMoveDisY = 0.25f*9/6;
  
  private float majiangLength2D;//麻将高度
  private float majiangWidth2D;//麻将高度


  private Vector3 bottomLeaveComputeStart;//底部计算开始的位置
  private Vector3 leftLeaveComputeStart;//左侧计算开始的位置
  private Vector3 rightLeaveComputeStart;//右侧计算开始的位置
  private Vector3 topLeaveComputeStart;//顶部计算开始的位置

  private Vector3 bottomPaisComputeStart;//底部抓起的牌计算开始的位置
  private Vector3 leftPaisComputeStart;//左侧抓起的牌计算开始的位置
  private Vector3 rightPaisComputeStart;//右侧抓起的牌计算开始的位置
  private Vector3 topPaisComputeStart;//顶部抓起的牌计算开始的位置

  private Vector3 bottomPGActionPosition;//底部抓起的牌计算开始的位置
  private Vector3 leftPGActionPosition;//左侧抓起的牌计算开始的位置
  private Vector3 rightPGActionPosition;//右侧抓起的牌计算开始的位置
  private Vector3 topPGActionPosition;//顶部抓起的牌计算开始的位置

  private Vector3 bottomChuPaisComputeStart;//底部出的牌计算开始的位置
  private Vector3 rightChuPaisComputeStart;//右边出的牌计算开始的位置
  private Vector3 topChuPaisComputeStart;//顶部出的牌计算开始的位置
  private Vector3 leftChuPaisComputeStart;//左边出的牌计算开始的位置


  private Vector3 bottomPengPaisComputeStart;//底部出的牌计算开始的位置
  private Vector3 rightPengPaisComputeStart;//右边出的牌计算开始的位置
  private Vector3 topPengPaisComputeStart;//顶部出的牌计算开始的位置
  private Vector3 leftPengPaisComputeStart;//左边出的牌计算开始的位置



  private float leftLeaveComputeX=0;//底部结束的位置

  private Room room;

  private Majiang animateMajiang;

  
  private Vector3 majiangAnimateStepPostion;
  private Vector3 majiangAnimateStepRotation;

  private int animateTimes=0;
  private int totalAnimateTimes=100;

  private List<MajiangAnimate> majiangAnimates;


  Majiang readyChuMajiang;

  private Vector3 shaiziInitRotate;//底部抓

  private int cutdown = 9;

  private float myMajiangsLeft;

  Ray ray;
  RaycastHit hit;
  GameObject obj;

  SocketClient socketClient;

  List<Majiang> allmajiangs;

  List<Chat> chats = new List<Chat>(); 

  private float effect_sound_volume = 0.6f;

  private Vector3 bottomZhuapaiRotate = new Vector3(270,180,0);
  private Vector3 rightZhuapaiRotate = new Vector3(270,90,0);
  private Vector3 topZhuapaiRotate = new Vector3(270,0,0);
  private Vector3 leftZhuapaiRotate = new Vector3(270,270,0);

  Vector3 cameraRotation;
  Vector3 cameraPosition;

  User openInfoUser;

  private List<User> roomUsers;

  

  public static string agoraAppId = "afee5a5da5244c8aa723c6788accb821";

  static IRtcEngine mRtcEngine;

  //加入语音聊天
  private void joinChannel()
  {
    if(room!=null){
      mRtcEngine.SetEnableSpeakerphone(true);
      mRtcEngine.EnableLocalAudio(true);
      mRtcEngine.JoinChannel(room.id, "extra", 0); // join the channel with given match name
      Debug.Log("joining channel:" + room.id);
    }
  }

  //离开频道
  private void leaveChannel()
  {
    if(mRtcEngine!=null){
      mRtcEngine.LeaveChannel(); 
      mRtcEngine.DisableVideoObserver();  
      //UnLoadEngineCallbacks();
      //IRtcEngine.Destroy();
      mRtcEngine = null;
    }
  }


  public static void MuteAllRemoteAudioStreams(bool mute)
  {
    if (mRtcEngine == null) return;
    mRtcEngine.MuteAllRemoteAudioStreams(mute);
  }
  //静音指定的远端音效
  public static void MuteRemoteAudioStream(int uid, bool mute)
  {
    if (mRtcEngine == null) return;
      mRtcEngine.MuteRemoteAudioStream((uint)uid, mute);
  }

  void Awake () {

    if(AppUtil.user==null){
      Invoke("DestroyScene",1.5f);
      SceneManager.LoadScene("Login");
      return;
    }

    chats.Add(new Chat("别磨磨唧唧了","biemomojiji"));
    chats.Add(new Chat("你们好啊!","nimenhaoa"));
    chats.Add(new Chat("太晚了，我要睡觉了。","taiwanlewoyaoshuijiaole"));
    chats.Add(new Chat("快点啊，母猪都要上树了！","kuaidianamzssle"));
    chats.Add(new Chat("开语音交流！","kaiyuyin"));

    nowMajiangAlert.transform.DOBlendableMoveBy(new Vector3(0f, 0.05f, 0f), 0.8f).SetLoops(-1).SetEase(Ease.OutElastic);
    nowMajiangAlert.active = false;
    socialAnimateButtons.Add(socialAnimate1Button);
    socialAnimateButtons.Add(socialAnimate2Button);
    socialAnimateButtons.Add(socialAnimate3Button);
    socialAnimateButtons.Add(socialAnimate4Button);
    socialAnimateButtons.Add(socialAnimate5Button);
    socialAnimateButtons.Add(socialAnimate6Button);
    socialAnimateButtons.Add(socialAnimate7Button);
    socialAnimateButtons.Add(socialAnimate8Button);
    for(int i=0;i<socialAnimateButtons.Count;i++){
      Button button = socialAnimateButtons[i];
      button.onClick.AddListener(()=>{
        Toast.info("暂未开放",mainPanel.transform);
        //userTopAnimator.Play("Samba Dancing");
      });
    }
    colseSocialAnimatorButton.onClick.AddListener(()=>{
      playClick();
      socialAnimatorPanel.active = false;
    });


    voiceToggle.isOn = false;

    voiceUserButtons.Add(voiceUser1Button);
    voiceUserButtons.Add(voiceUser2Button);
    voiceUserButtons.Add(voiceUser3Button);
    voiceUserButtons.Add(voiceUser4Button);
    voiceToggle.onValueChanged.AddListener((bool value)=>{
      playClick();
      socketClient.sendVoiceEnabled(value,(JsonData res)=>{

      });
      if(value){
        joinChannel();
      }else{
        mRtcEngine.EnableLocalAudio(false);
      }
      setUserVoices();
    });

    colseVoicePanelButton.onClick.AddListener(()=>{
      playClick();
      voicePanel.active = false;
    });

    socialButton.onClick.AddListener(()=>{
      playClick();
      socialPanel.active = true;
    });
    colseSocialButton.onClick.AddListener(()=>{
      playClick();
      socialPanel.active = false;
    });
    createChats();
    socialPanel.active = false;

    voicePanel.active = false;

    backAudioSource = (AudioSource)this.gameObject.GetComponents(typeof(AudioSource))[1];
    AppUtil.audioControl(backAudioSource,musicToggle,audioToggle);

    cutdownAudioSource =  (AudioSource)this.gameObject.GetComponents(typeof(AudioSource))[2];
    cutdownAudioSource.Pause();
    majiangWidth2D = Screen.width*0.05f;
    majiangLength2D = majiangWidth2D*170f/120;

    // Screen.height*1.0f/Screen.width>0.7f?-4.5f:-9.32f

    QualitySettings.vSyncCount = 0;
    Application.targetFrameRate = 30;
    cameraRotation = new Vector3(54.45f,0f,0f);
    cameraPosition = new Vector3(0.13f,12.15f,-3.913f);
    myMajiangsLeft = Screen.width/2-majiangWidth2D*14/2;
    audioSource = GetComponent<AudioSource> ();
    tianyingAlert.active = false;

    
    gang1Button.onClick.AddListener(()=>{
      playClick();
      if(room.userBottom.gangOptions!=null && room.userBottom.gangOptions.Count>0){
        gang1Button.GetComponent<Transform>().gameObject.active = false;
        Majiang majiang = room.userBottom.gangOptions[0];
        socketClient.sendSelfGang(AppUtil.roomNo,majiang.type,majiang.value,(JsonData res)=>{

        });
      }else{
        gang1Button.GetComponent<Transform>().gameObject.active = false;
        socketClient.sendGang(AppUtil.roomNo,(JsonData res)=>{

        });
      }
    });
    

    gang2Button.onClick.AddListener(()=>{
      playClick();
      gang2Button.GetComponent<Transform>().gameObject.active = false;
      Majiang majiang = room.userBottom.gangOptions[1];
      socketClient.sendSelfGang(AppUtil.roomNo,majiang.type,majiang.value,(JsonData res)=>{

      });
    });

    gang3Button.onClick.AddListener(()=>{
      playClick();
      gang3Button.GetComponent<Transform>().gameObject.active = false;
      Majiang majiang = room.userBottom.gangOptions[2];
      socketClient.sendSelfGang(AppUtil.roomNo,majiang.type,majiang.value,(JsonData res)=>{

      });
    });

    pengButton.onClick.AddListener(()=>{
      playClick();
      pengButton.GetComponent<Transform>().gameObject.active = false;
      socketClient.sendPeng(AppUtil.roomNo,(JsonData res)=>{

      });
      
    });

    guoButton.onClick.AddListener(()=>{
      playClick();
      guoButton.GetComponent<Transform>().gameObject.active = false;
      room.userBottom.status = 0;
      setOptions();
      socketClient.sendGuo(AppUtil.roomNo,(JsonData res)=>{
        
      });
      
    });

    huButton.onClick.AddListener(()=>{
      playClick();
      huButton.GetComponent<Transform>().gameObject.active = false;
      socketClient.sendHu(AppUtil.roomNo,(JsonData res)=>{

      });
    });

    optionPanel.active = false;



    userPostionAlert.active = false;

    voiceButton.onClick.AddListener(()=>{
      playClick();
      //userAnimator.Play("Samba Dancing");
      voicePanel.active = true;
      voiceToggle.isOn = room.userBottom.voice;
    });

    shejiaoButton.onClick.AddListener(()=>{
      playClick();
      socialAnimatorPanel.active = true;
    });

    socialAnimatorPanel.active = false;

    roomNoText.text = AppUtil.roomNo+"";
    shaiziInitRotate = new Vector3(-90.00001f,0,13.596f);

    zhishaiziButton.onClick.AddListener(()=>{
      playClick();
      zhishaiziButton.GetComponent<Transform>().gameObject.active = false;
      socketClient.sendShaizi(AppUtil.roomNo,(JsonData data)=>{
        
      });
    });

    zhishaiziButton.GetComponent<Transform>().gameObject.active = false;
    exitButton.onClick.AddListener(()=>{
      playClick();
      Confirm.show("游戏已经开始，退出将会由傻傻的机器人代打~",mainPanel.transform,()=>{
        playClick();
        socketClient.sendExitRoom(AppUtil.roomNo,(res)=>{
          
        });
        //开始加载场景
        backAudioSource.Pause();
        Invoke("DestroyScene",1.5f);
        SceneManager.LoadScene("MainPage");
        
      },()=>{
        playClick();
      });
    });
    socketClient = SocketClient.socketClient;
    socketClient.monoBehaviour = this;
    socketClient.transform = mainPanel.transform;
    socketClient.onRecieveMessage = onRecieveMessage;
    room = new Room();
    allmajiangs = initMajiangs();
    
    for(int i=0;i<allmajiangs.Count;i++){
      Majiang majiang = allmajiangs[i];
      majiang.gameObject = Instantiate(majiangPrefab);
      Material mat = Resources.Load("Materials/"+majiang.getMaterialName(), typeof(Material)) as Material;
      majiang.gameObject.transform.Find("Plane").gameObject.GetComponent<MeshRenderer>().material = mat;
      majiang.gameObject.transform.Find("PlaneTianying").gameObject.active = false;
      majiang.gameObject.transform.position = new Vector3(0f,-10f,0f);
    }

    majiangAnimates = new List<MajiangAnimate>();    
    bottomLeaveComputeStart=new Vector3(1.889f, deskPositionY, -2.326f);
    leftLeaveComputeStart=new Vector3(-2.22f, deskPositionY, -1.842f);
    rightLeaveComputeStart=new Vector3(2.35f, deskPositionY, 1.856f);
    topLeaveComputeStart=new Vector3(-1.76f, deskPositionY, 2.26f);

    bottomPaisComputeStart = new Vector3(-1.78f,8.589f, -3.239f);
    leftPaisComputeStart = new Vector3(-2.91f, 8.589f, 1.51f);
    rightPaisComputeStart = new Vector3(3.192f, 8.589f, -1.52f);
    topPaisComputeStart = new Vector3(1.75f, 8.589f, 2.987f);

    bottomPGActionPosition = new Vector3(-0.18f,9.76f, -2.0f);
    leftPGActionPosition = new Vector3(-1.86f, 9.76f, -0.241f);
    rightPGActionPosition = new Vector3(2.22f, 9.76f, -0.79f);
    topPGActionPosition = new Vector3(0.887f, 9.76f, 0.61f);

    bottomChuPaisComputeStart = new Vector3(-0.772f, deskPositionY, -1.018f);
    rightChuPaisComputeStart = new Vector3(0.829f, deskPositionY, -0.828f);
    topChuPaisComputeStart = new Vector3(0.83f, deskPositionY, 0.928f);
    leftChuPaisComputeStart = new Vector3(-0.84f, deskPositionY, 0.712f);

    bottomPengPaisComputeStart = new Vector3(2.71f, deskPositionY, -3.286f);
    rightPengPaisComputeStart = new Vector3(3.3f, deskPositionY, 2.02f);
    topPengPaisComputeStart = new Vector3(-2.36f, deskPositionY, 2.94f);
    leftPengPaisComputeStart = new Vector3(-2.819f, deskPositionY, -2.494f);

    
    userPanel.active = false;
    userTopHeadimgButton.onClick.AddListener(()=>{
      playClick();
      if(room!=null){
        openUserInfo(room.userTop);
      }
    });

    userLeftHeadimgButton.onClick.AddListener(()=>{
      playClick();
      if(room!=null){
        openUserInfo(room.userLeft);
      }
    });
    userBottomHeadimgButton.onClick.AddListener(()=>{
      playClick();
      if(room!=null){
        openUserInfo(room.userBottom);
      }
    });
    userRightHeadimgButton.onClick.AddListener(()=>{
      playClick();
      if(room!=null){
        openUserInfo(room.userRight);
      }
    });
    userPanelCloseButton.onClick.AddListener(()=>{
      playClick();
      resetUserPanel();
      
      userPanel.active = false;
      
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
              AppUtil.queryRecords(zhanjiTabPanelContent,transform,zhanjiRowPanel,this,openInfoUser.id);
            }
          }else{
            p.active = false;
          }
        }
      });
    }

    scoreTabButtons.Add(nowTabButton);
    scoreTabButtons.Add(historyTabButton);

    for(int i=0;i<scoreTabButtons.Count;i++){
      Button button = scoreTabButtons[i];
      button.onClick.AddListener(()=>{
        if(button == nowTabButton){
          nowTabButtonSelected = true;
          nowTabButton.GetComponent<Image>().color = AppUtil.GetColor("#EEEEEE");
          historyTabButton.GetComponent<Image>().color = AppUtil.GetColor("#AAA064");
        }else{
          nowTabButtonSelected = false;
          historyTabButton.GetComponent<Image>().color = AppUtil.GetColor("#EEEEEE");
          nowTabButton.GetComponent<Image>().color = AppUtil.GetColor("#AAA064");
        }
        queryRoomRecords();
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


    recordButton.onClick.AddListener(()=>{
      playClick();
      roomRecordsPanel.active = true;
      queryRoomRecords();
    });
    roomRecordsPanelCloseButton.onClick.AddListener(()=>{
      playClick();
      roomRecordsPanel.active = false;
    });

    roomRecordsPanel.active = false;

    totalScores.Add(totalScore1);
    totalScores.Add(totalScore2);
    totalScores.Add(totalScore3);
    totalScores.Add(totalScore4);


    socketClient.sendGetRoom(AppUtil.roomNo,(JsonData resData)=>{
      JsonData roomData = (JsonData)resData["room"];
      room = Room.toRoom(roomData,allmajiangs);
      dealRoom();
      roomUsers = room.users;
      for(int i=0;i<roomUsers.Count;i++){
        User user = roomUsers[i];
        initUserModel(user);
      }
      if(room.status>0){
        cutdownLeaveSeconds();
      }
    });


   
  }

  // 创建chats
  void createChats(){
    RectTransform socialContentPanelTransform = socialContentPanel.transform.GetComponent<RectTransform>();
    socialContentPanelTransform.sizeDelta = new Vector2(600,chats.Count*150);
    for(int i=0;i<chats.Count;i++){
        Chat chat = chats[i];
        GameObject obj = Instantiate(chatPrefab);
        obj.transform.parent = socialContentPanel.transform;
        Text text = obj.transform.Find("Text").GetComponent<Text>();
        text.text = chat.msg;
        obj.GetComponent<Button>().onClick.AddListener(()=>{
          playClick();
          socketClient.sendChat(chat,(res)=>{

          });
          socialPanel.active = false;
        });
        RectTransform contentPanelTransform = obj.transform.GetComponent<RectTransform>();
        contentPanelTransform.anchoredPosition = new Vector3(0,-i*150,0);
        contentPanelTransform.sizeDelta = new Vector2(600,130);

      }
  }


  void initUserModel(User user){
    float x = 0;
    float y = 0;
    float z = 0;
    float ry = 0;
    float rx = 0;
    if(user.id==room.userBottom.id){
      if(personBottom!=null){
        return;
      }
      x = 0f;
      y = 0.78f;
      z = -4.92f;
      ry = 0;
    }else if(user.id==room.userRight.id){
      if(personRight!=null){
        return;
      }
      x = 4.84f;
      y = 0.76f;
      z = 0f;
      ry = 270;
    }else if(user.id==room.userTop.id){
      if(personTop!=null){
        return;
      }
      x = 0.38f;
      y = 0.9f;
      z = 4.66f;
      ry = 180;
    }else if(user.id==room.userLeft.id){
      if(personLeft!=null){
        return;
      }
      x = -4.89f;
      y = 0.76f;
      z = 0f;
      ry = 90;
    }
    user.figure = "female_majiang";
    GameObject obj = AppUtil.createUserModel(user,"AnimatorControllers/PlayMajiangController",transform,this,new Vector3(x,y,z),new Vector3(10,10,10),new Vector3(rx,ry,0));
    if(user.id==room.userBottom.id){
      personBottom = obj;
    }else if(user.id==room.userRight.id){
      personRight = obj;
    }else if(user.id==room.userTop.id){
      personTop = obj;
    }else if(user.id==room.userLeft.id){
      personLeft = obj;
    }
  }


  // 设置用户语音
  void setUserVoices(){
    if(room!=null){
      for(int i=0;i<room.users.Count;i++){
        User user = room.users[i];
        GameObject voiceUserButton = voiceUserButtons[i];
        Image image = voiceUserButton.transform.Find("Image").GetComponent<Image>();
        Text text = voiceUserButton.transform.Find("Text").GetComponent<Text>();
        Image voiceImage = voiceUserButton.transform.Find("VoiceImage").GetComponent<Image>();
        LoadImag.load(user.headimg,image,this);
        text.text = user.nickname;
        Sprite quietSprite = Resources.Load("Images/quiet", typeof(Sprite)) as Sprite;
        Sprite voiceSprite = Resources.Load("Images/voice", typeof(Sprite)) as Sprite;
        if(user.voice){
          voiceImage.sprite = voiceSprite;
        }else{
          voiceImage.sprite = quietSprite;
        }
        
      }
    }
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

  void queryRoomRecords(){
    AppUtil.RemoveAllChildren(roomRecordsTopPanel);

    RectTransform contentPanelTransform = roomRecordsPanel.transform.Find("Panel").GetComponent<RectTransform>();
    float panelWidth = Screen.width*0.85f;
    contentPanelTransform.sizeDelta = new Vector2(panelWidth,Screen.height*0.8f);
    float perWidth = panelWidth/4;

    for(int i=0;i<room.users.Count;i++){
      User user = room.users[i];
      float left = perWidth*i;
      GameObject prefab = Instantiate(userScoreInfoPanel);    // 对象初始化
      prefab.transform.parent = roomRecordsTopPanel.transform;
      prefab.transform.localScale = Vector3.one;
      prefab.transform.localPosition = new Vector3(left,0,0);
      RectTransform rectTransform = prefab.transform.GetComponent<RectTransform>();
      Image image = prefab.transform.Find("Image").GetComponent<Image>();
      Text text = prefab.transform.Find("Text").GetComponent<Text>();
      LoadImag.load(user.headimg,image,this);
      text.text = user.nickname;
      GameObject gb = totalScores[i];
      gb.transform.localPosition = new Vector3(left,0f,0f);
      gb.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(perWidth,130f);
    }

    JsonData ps = new JsonData();
    ps["roomid"]=room.id;
    // ps["roomid"]="BA8C0794FFAE437C61D11229F3F0BA07";
    // room.circleRound= 3;
    AppUtil.RemoveAllChildren(roomRecordsContentList);

    WebService.post("/majiangrecord/query", ps,transform,(JsonData res)=>{
      JsonData data = res["data"];
      RectTransform roomRecordsContentListTransform = roomRecordsContentList.transform.GetComponent<RectTransform>();
      int row = 0;
      int[] totals = {0,0,0,0};
      for(int k=(nowTabButtonSelected?(room.circleRound):1);k<=room.circleRound;k++){
        GameObject prefabCircle = Instantiate(roomScoreRowCircle);    // 对象初始化
        prefabCircle.transform.parent = roomRecordsContentList.transform;
        prefabCircle.transform.localScale = Vector3.one;
        prefabCircle.transform.localPosition = new Vector3(10,-row*100,0);
        RectTransform rectTransformCircle = prefabCircle.transform.GetComponent<RectTransform>();
        Text text = prefabCircle.transform.Find("Text").GetComponent<Text>();
        text.text = "第"+k+"局";
        
        //局的数据
        List<JsonData> circleData = new List<JsonData>();
        for(int j=0;j<data.Count;j++){
          JsonData d = data[j];
          int circle = (int)d["circle"];
          if(circle==k){
            circleData.Add(d);
          }
        }
        for(int j=0;j<circleData.Count/4;j++){
          //行
          row = row+1;
          for(int i=0;i<room.users.Count;i++){
            User user = room.users[i];
            float left = perWidth*i;
            for(int m=0;m<4;m++){
              JsonData d = circleData[j*4+m];
              string userid = (string)d["userid"];
              if(user.id!=userid){
                continue;
              }
              int top = -row*100;
              string title = (string)d["winmethod"];
              int zi = (int)d["zi"];
              int jiao = (int)d["jiao"];
              string desc = zi+"子,"+jiao+"角";
              totals[i] = totals[i]+zi+jiao*20;
              GameObject prefab = Instantiate(roomScoreRowPanel);    // 对象初始化
              prefab.transform.parent = roomRecordsContentList.transform;
              prefab.transform.localScale = Vector3.one;
              prefab.transform.localPosition = new Vector3(left+10,top,0);
              RectTransform rectTransform = prefab.transform.GetComponent<RectTransform>();
              rectTransform.sizeDelta = new Vector2(perWidth-20,129f);
              Text text1 = prefab.transform.Find("WinMethodText").GetComponent<Text>();
              Text text2 = prefab.transform.Find("NumText").GetComponent<Text>();
              text1.text = title;
              text2.text = desc;
            }
            
          }
        }
        row = row+1;
      }
      for(int i=0;i<room.users.Count;i++){
        GameObject gb = totalScores[i];
        gb.transform.Find("Text").GetComponent<Text>().text = "总成绩="+totals[i]+"子";
        gb.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(perWidth, 130);
      }
      roomRecordsContentListTransform.sizeDelta = new Vector2(0, row*100);

    },(JsonData res)=>{
      
    },this);
  }

  void openUserInfo(User user){
    openInfoUser = user;
    if(user!=null && user.id!=null){
      userPanel.active = true;
      LoadImag.load(user.headimg,infoUserImage,this);
      infoNicknameText.text = user.nickname;
    }
  }


  void playClick(){
    if(audioToggle.isOn){
      audioSource.PlayOneShot (clickSound,effect_sound_volume);
    }
  }

  void setUserPositionAlert(){
    if(room==null || room.userBottom==null){
      return;
    }
    cutdown = 9;
    userPostionAlert.active = true;
    float time = 0.3f;
    if(room.nowUserid==room.userBottom.id){
      userPostionAlert.transform.DOLocalRotate(new Vector3(0f,180f,0f),time);
    }else if(room.nowUserid==room.userRight.id){
      userPostionAlert.transform.DOLocalRotate(new Vector3(0f,90f,0f),time);
    }else if(room.nowUserid==room.userTop.id){
      userPostionAlert.transform.DOLocalRotate(new Vector3(0f,0f,0f),time);
    }else if(room.nowUserid==room.userLeft.id){
      userPostionAlert.transform.DOLocalRotate(new Vector3(0f,270f,0f),time);
    }
  }

  void cutdownLeaveSeconds(){
    userPostionAlertText.GetComponentInChildren<TextMesh>().text = "0"+cutdown;
    Invoke("cutdownLeaveSeconds",1);
    cutdown = cutdown -1;
    if(cutdown<0){
      cutdown = 0;
      if(room.nowUserid==room.userBottom.id){
        cutdownAudioSource.Play();
      }
    }else{
      cutdownAudioSource.Pause();
    }

  }

  IEnumerator back(){
      AsyncOperation async = Application.LoadLevelAsync("MainPage");
      yield return async;
  }

  void destoryMajiangs(){
    List<Majiang> majiangs = allmajiangs;
    for(int i=0;i<majiangs.Count;i++){
      Majiang majiang = majiangs[i];
      if(majiang.gameObject2D!=null){
        GameObject.Destroy(majiang.gameObject2D);
        majiang.gameObject2D = null;
      }
    }
  }

  void createMajiangs(bool animate){
    List<Majiang> majiangs = room.userBottom.majiangs;
    int count = room.userBottom.majiangs.Count;
    float width = majiangWidth2D;
    float height = majiangLength2D;
    float space = 0;
    for(int i=0;i<majiangs.Count;i++){
      Majiang majiang = majiangs[i];
      float endX =  myMajiangsLeft+width*i;
      if(majiang.gameObject2D==null){
        GameObject majiangButton = Instantiate(majiangButtonPrefab);    // 对象初始化
        majiang.gameObject2D = majiangButton;
        majiangButton.transform.parent = myMajiangsPanel.transform;
        majiangButton.transform.localScale = Vector3.one;
        majiangButton.transform.localPosition = Vector3.zero;
        Image majiangImage = majiangButton.transform.Find("MajiangImage").GetComponent<Image>();
        Image tianyingImage = majiangButton.transform.Find("TianyingImage").GetComponent<Image>();
        Sprite sprite = Resources.Load("Images/"+majiang.getImageName(), typeof(Sprite)) as Sprite;
        majiangImage.sprite = sprite;
        if(!majiang.tianying){
          tianyingImage.gameObject.active= false;
        }
        
        majiangButton.transform.localPosition = new Vector3(Screen.width,0,0);
        majiangButton.GetComponent<RectTransform>().sizeDelta = new Vector2(majiangWidth2D,majiangLength2D);
        majiangButton.GetComponent<Button>().onClick.AddListener(()=>{
          playClick();
          readyChuPai2D(majiang);
        });
        int timeCount  = 5;
        
        if(animate){
          DOTween.To(() => timeCount, a => timeCount = a, 1, 0.1f*i).OnComplete(() => {
            majiangButton.transform.DOLocalMoveX(endX, 0.5f);
          });
        }else{
          majiangButton.transform.localPosition = new Vector3(endX,0f,0f);
        }
      }else{
        majiang.gameObject2D.transform.localPosition = new Vector3(endX,0f,0f);
      }
    }
  }

  void fapai2D(Majiang majiang){
    if(majiang.gameObject2D==null){
      List<Majiang> majiangs = room.userBottom.majiangs;
      int count = room.userBottom.majiangs.Count;
      float width = majiangWidth2D;
      float height = majiangLength2D;
      float space = 0;
      float endX =  myMajiangsLeft+width*majiangs.Count;
      GameObject majiangButton = Instantiate(majiangButtonPrefab);    // 对象初始化
      majiang.gameObject2D = majiangButton;
      majiangButton.transform.parent = myMajiangsPanel.transform;
      majiangButton.transform.localScale = Vector3.one;
      majiangButton.transform.localPosition = Vector3.zero;
      majiangButton.GetComponent<RectTransform>().sizeDelta = new Vector2(majiangWidth2D,majiangLength2D);

      Image majiangImage = majiangButton.transform.Find("MajiangImage").GetComponent<Image>();
      Image tianyingImage = majiangButton.transform.Find("TianyingImage").GetComponent<Image>();
      Sprite sprite = Resources.Load("Images/"+majiang.getImageName(), typeof(Sprite)) as Sprite;
      majiangImage.sprite = sprite;
      if(!majiang.tianying){
        tianyingImage.gameObject.active= false;
      }
      majiangButton.transform.localPosition = new Vector3(endX+majiangWidth2D/2,0,0);
      majiangButton.GetComponent<Button>().onClick.AddListener(()=>{
        playClick();
        readyChuPai2D(majiang);
      });
    }

  }

  void resetChuPai2D(Majiang majiang){
    if(majiang!=null){
      try{
        Transform transform = majiang.gameObject2D.GetComponent<Transform>();
        transform.DOLocalMoveY(0, 0.3f);
      } catch (Exception ex)
      {
      }
    }
  }

  void readyChuPai2D(Majiang majiang){
    playClick();
    Transform transform = majiang.gameObject2D.GetComponent<Transform>();
    if(readyChuMajiang!=null && readyChuMajiang.id==majiang.id){
      if(room.nowUserid==AppUtil.user.id){
        chupai(majiang);
      }else{
        resetChuPai2D(readyChuMajiang);
      }
      readyChuMajiang = null;
    }else{
      resetChuPai2D(readyChuMajiang);
      transform.DOLocalMoveY(85, 0.3f);
      readyChuMajiang = majiang;
    }
        
  }

  //映射麻将
  public void mappingMajiang(){
    
  }

  public void setStatus(User user,GameObject panel,Text statusText){
    string str ="";
    if(user.id==null){
      str = "未加入";
    }else if(user.status==-1){
      str =  "已准备";
    }else if(user.status==-2){
      str = "未准备";
    }
    if(str==""){
      panel.active = false;
    }else{
      panel.active = true;
      statusText.text = str;
    }
  }

  public void setUserInfo(){
    setStatus(room.userBottom,userBottomStatusPanel,userBottomStatus);
    setStatus(room.userTop,userTopStatusPanel,userTopStatus);
    setStatus(room.userRight,userRightStatusPanel,userRightStatus);
    setStatus(room.userLeft,userLeftStatusPanel,userLeftStatus);

    if(room.userBottom.id!=null){
      LoadImag.load(room.userBottom.headimg,userBottomHeadimg,this);
      userBottomNickname.text = room.userBottom.nickname;
      userBottomZi.text = room.userBottom.zi+"";
      userBottomJiao.text = room.userBottom.jiao+"";
      userBottomGold.text = AppUtil.castNumToStr(room.userBottom.gold);
    }
    if(room.userLeft.id!=null){
      LoadImag.load(room.userLeft.headimg,userLeftHeadimg,this);
      userLeftNickname.text = room.userLeft.nickname;
      userLeftZi.text = room.userLeft.zi+"";
      userLeftJiao.text = room.userLeft.jiao+"";
      userLeftGold.text = AppUtil.castNumToStr(room.userLeft.gold);
    }
    if(room.userRight.id!=null){
      LoadImag.load(room.userRight.headimg,userRightHeadimg,this);
      userRightNickname.text = room.userRight.nickname;
      userRightZi.text = room.userRight.zi+"";
      userRightJiao.text = room.userRight.jiao+"";
      userRightGold.text =  AppUtil.castNumToStr(room.userRight.gold);
    }
    if(room.userTop.id!=null){
      LoadImag.load(room.userTop.headimg,userTopHeadimg,this);
      userTopNickname.text = room.userTop.nickname;
      userTopZi.text = room.userTop.zi+"";
      userTopJiao.text = room.userTop.jiao+"";
      userTopGold.text = AppUtil.castNumToStr(room.userTop.gold);
    }
  }

  // 房间处理
  public void dealRoom(){
    
    if(room.status==0){
      //用户还未准备
      SceneManager.LoadScene("Pipei");
      Invoke("DestroyScene",1.5f);
      return;
    }else if(room.status==-1){
      //掷骰子状态
      xipai(true);
      if(room.shaiziUser == AppUtil.user.id){
        Invoke("setZhishaiziStatus",2);
      }
    }else if(room.status==1 || room.status==2 || room.status==3){
      //出牌中
      xipai(false);
      fanNoAnimate();
      setGamePais();
      setUserPositionAlert();
    }else{
      //房间结束
      SceneManager.LoadScene("RoundResult");
      Invoke("DestroyScene",1.5f);
      return;
    }
    setTianyingSprite();
    
    if(room.leavemajiangs!=null){
      leaveText.text = room.getLeavesNum()+"";
    }

    ziValueText.text = room.userBottom.zi+"";
    jiaoValueText.text = room.userBottom.jiao+"";

    setUserInfo();
    setOptions();
  }

  void setTianyingSprite(){
    if(room.tianyan!=null){
      tianyingAlert.active = true;
      Sprite sprite = Resources.Load("Images/"+room.tianyan.getImageName(), typeof(Sprite)) as Sprite;
      tianyingAlertImage.sprite = sprite;
    }
  }

  

  public void setZhishaiziStatus(){
    zhishaiziButton.GetComponent<Transform>().gameObject.active = true;
  }

  Vector3 getShaiziEnd(int shaizi){
    Vector3 end = new Vector3(0f,0f,0f);
    
    if(shaizi==1){
        end  = new Vector3(0f+shaiziInitRotate.x,0f,90f+shaiziInitRotate.z);
    }else if(shaizi==2){
        end  = new Vector3(90f+shaiziInitRotate.x,0f,0f+shaiziInitRotate.z);
    }else if(shaizi==3){
        end  = new Vector3(180f+shaiziInitRotate.x,0f,0f+shaiziInitRotate.z);
    }else if(shaizi==4){
        end  = new Vector3(360f+shaiziInitRotate.x,0f,0f+shaiziInitRotate.z);
    }else if(shaizi==5){
        end  = new Vector3(-90f+shaiziInitRotate.x,0f,0f+shaiziInitRotate.z);
    }else if(shaizi==6){
        end  = new Vector3(0f+shaiziInitRotate.x,0f,90f+shaiziInitRotate.z);
    }
    return end;
  }

  //掷骰子动画
  void shaziAnimate(){
    shaizi1GameObject.transform.DOLocalRotate(new Vector3(1080f+shaiziInitRotate.x,0f,shaiziInitRotate.z), 0.2f,RotateMode.FastBeyond360).OnComplete(()=>{
      shaizi1GameObject.transform.DOLocalRotate(new Vector3(shaiziInitRotate.x,1080f,shaiziInitRotate.z), 0.2f,RotateMode.FastBeyond360).OnComplete(()=>{
        Vector3 end  = getShaiziEnd(room.shaizi1);
        shaizi1GameObject.transform.DOLocalRotate(end, 0.5f,RotateMode.FastBeyond360);
      });
    });

    shaizi2GameObject.transform.DOLocalRotate(new Vector3(0f+shaiziInitRotate.x,1080f,shaiziInitRotate.z), 0.2f,RotateMode.FastBeyond360).OnComplete(()=>{
      shaizi2GameObject.transform.DOLocalRotate(new Vector3(1080f+shaiziInitRotate.x,0f,shaiziInitRotate.z), 0.2f,RotateMode.FastBeyond360).OnComplete(()=>{
        Vector3 end  = getShaiziEnd(room.shaizi2);
        shaizi2GameObject.transform.DOLocalRotate(end, 0.5f,RotateMode.FastBeyond360);
      });
    });

    
    Invoke("fanAnimate", 1.1f);
    Invoke("animateZhuapais", 3.1f);
    Invoke("setUserPositionAlert", 3.1f);
    Invoke("cutdownLeaveSeconds", 3.1f);

    
  }

  

  void fanNoAnimate(){
    if(room==null || room.fan == null){
      return;
    }
    if(room.fan.direction==0){
      room.fan.gameObject.transform.localEulerAngles = new Vector3(180f,180f,0f);
    }else if(room.fan.direction==1){
      room.fan.gameObject.transform.localEulerAngles = new Vector3(180f,270f,0f);
    }else if(room.fan.direction==2){
      room.fan.gameObject.transform.localEulerAngles = new Vector3(360f,180f,-180f);
    }else if(room.fan.direction==3){
      room.fan.gameObject.transform.localEulerAngles = new Vector3(180f,90f,0f);
    }
  }

  void recoverCamera(){
    transform.DOLocalMove(cameraPosition,0.2f);
    transform.DOLocalRotate(cameraRotation,0.2f);
    setTianyingSprite();
  }

  public void setUserValues(JsonData userList,bool sync){
    if(userList==null){
      return;
    }
   for(int i=0;i<userList.Count;i++){
      JsonData userMap = userList[i];
      string id = (string)userMap["id"];
      User user = room.getUser(id);
      if(user==null){
        continue;
      }
      if(userMap["status"]!=null){
        user.status = (int)userMap["status"];
      }
      if(userMap["desc"]!=null){
        user.desc = (string)userMap["desc"];
      }
      if(userMap["goldDesc"]!=null){
        user.goldDesc = (string)userMap["goldDesc"];
      }
      user.gold = (int)userMap["gold"];
      user.gangOptions = null;
      if(userMap["gangOptions"]!=null){
        JsonData gangOptions = userMap["gangOptions"];
        List<Majiang> gangOptionList = new List<Majiang>();
        if(gangOptions!=null){
          for(int j=0;j<gangOptions.Count;j++){
            JsonData majiangMap = gangOptions[j];
            gangOptionList.Add(Majiang.toMajiang(majiangMap));
          }
          user.gangOptions = gangOptionList;
        }
      }
      if(userMap["majiangIds"]!=null && sync){
        JsonData majiangIds = userMap["majiangIds"];
        if(majiangIds.Count!=user.majiangs.Count){
          syncMajiangs(user);
        }else{
          for(int j=0;j<majiangIds.Count;j++){
            int mid = (int)majiangIds[j];
            bool find = false;
            for(int k=0;k<user.majiangs.Count;k++){
              Majiang majiang = user.majiangs[k];
              if(majiang.id==mid){
                find = true;
                break;
              }
            }
            if(!find){
              syncMajiangs(user);
              break;
            }
          }
        }
      }
    }
  }

  void showNowmajiangAlert(){
    nowMajiangAlert.active = true;
    Vector3 p = room.nowmajiang.gameObject.transform.localPosition;
    nowMajiangAlert.transform.localPosition = new Vector3(p.x,p.y+0.3f,p.z);
  }

  public void onRecieveMessage(JsonData data){
    string cmd = (string)data["cmd"];
    JsonData content = data["con"];

    if(cmd=="start"){
      room = Room.toRoom(content,allmajiangs);
      leaveText.text = room.getLeavesNum()+"";
      dealRoom();
    }else if(cmd=="in"){
      room = Room.toRoom(content,allmajiangs);
      Toast.info(room.extMessage+"加入",mainPanel.transform,true);
      dealRoom();
    }else if(cmd=="shaizi"){
      room = Room.toRoom(content,allmajiangs);
      leaveText.text = room.getLeavesNum()+"";
      shaziAnimate();
    }else if(cmd=="ready"){
      string userId = (string)content["user"];
      int status = (int)content["status"];
      int roomStatus = (int)content["roomStatus"];
      User user =  room.getUser(userId);
      user.status = status;
      room.status = roomStatus;
      dealRoom();
    }else if(cmd=="room"){
      room = Room.toRoom(content,allmajiangs);
      dealRoom();
    }else if(cmd=="guo"){
      string userId = (string)content["user"];
      string nowUserid = (string)content["nowUserid"];
      User user =  room.getUser(userId);
      room.nowUserid = nowUserid;
      User nextUser =  room.getUser(nowUserid);
      if(nextUser!=null){
        if(content["fapai"]!=null){
          JsonData fapaiMajiang = content["fapai"];
          Majiang majiang = getMajiang(Majiang.toMajiang(fapaiMajiang).id);
          zhuapaiAnimate(majiang,nextUser);
          if(nextUser.id == room.userBottom.id){
            fapai2D(majiang);
          }
          
          nextUser.majiangs.Add(majiang);
          nextUser.zhuapai = majiang;
          if(room.leavemajiangs.Count>0){
            room.leavemajiangs.RemoveAt(0);
          }

          leaveText.text = room.getLeavesNum()+"";
        }
      }
      JsonData userList = content["users"];
      setUserValues(userList,true);
      setOptions();
      setUserPositionAlert();
      
    }else if(cmd=="voice"){
      string userId = (string)content["user"];
      bool voice =  (bool)content["voice"];
      User user =  room.getUser(userId);
      user.voice = voice;
      setUserVoices();
    }else if(cmd=="chupai"){
      cutdownAudioSource.Pause();
      
      int id = (int) content["id"];
      string userId = (string) content["user"];
      string nowUserid = (string)content["nowUserid"];
      
      room.nowUserid = nowUserid;
      room.status = (int)content["roomStatus"];
      int nowMajiangId = (int) content["nowId"];
      room.nowmajiang = GetMajiang(nowMajiangId);
      setUserPositionAlert();
      User nextUser =  room.getUser(nowUserid);
      if(nextUser!=null){
        if(content["fapai"]!=null){
          JsonData fapaiMajiang = content["fapai"];
          Majiang majiang = getMajiang(Majiang.toMajiang(fapaiMajiang).id);
          zhuapaiAnimate(majiang,nextUser);
          if(nextUser.id == room.userBottom.id){
            fapai2D(majiang);
          }
          
          nextUser.majiangs.Add(majiang);
          nextUser.zhuapai = majiang;
          if(room.leavemajiangs.Count>0){
            room.leavemajiangs.RemoveAt(0);
          }

          leaveText.text = room.getLeavesNum()+"";
        }
      }
      
      Majiang mj = GetMajiang(id);
      playMajiangVoice(mj);
      User user =  room.getUser(userId);
      setChupaiPosition(mj,userId,true,user.chumajiangs.Count);
      Invoke("showNowmajiangAlert",0.5f);
      playUserAnimator(user,"MJ_Chu");

      if(user.id == room.userBottom.id){
        if(mj!=null && mj.gameObject2D!=null){
          GameObject.Destroy(mj.gameObject2D,0);
        }
      }
      user.chumajiangs.Add(mj);
      user.removeMajiang(id);
      if(user.id == room.userBottom.id){
        sortZhuapais2D(user);
      }

      JsonData userList = content["users"];
      setUserValues(userList,true);

      sortZhuapais(user);
      setOptions();

      if(user.majiangs.Count%3!=1 && user.id==room.userBottom.id){
        // 牌有问题 同步牌
        syncMajiangs(user);
      }

      // 重新设置peng和杠的牌
      for(int i=0;i<room.users.Count;i++){
        User u = room.users[i];
        setPengGang(u);
      }
    }else if(cmd=="peng"){
      if(audioToggle.isOn){
        audioSource.PlayOneShot(pengSound,effect_sound_volume);
      }
      
      JsonData majiangMap = content["majiang"];
      string userid = (string)content["user"];
      string lastUserId = (string)content["lastUser"];
      Majiang mj = GetMajiang(Majiang.toMajiang(majiangMap).id);
      User user =  room.getUser(userid);
      user.status = 0;
      user.gangOptions = null;
      
      
      playPengAction(user);
      List<Majiang> majiangs = user.peng(mj);
      User lastUser =  room.getUser(lastUserId);
      lastUser.removeChuMajiang(mj.id);
      room.nowUserid = userid;
      setUserPositionAlert();
      sortZhuapais(user);
      
      for(int i=0;i<majiangs.Count;i++){
        Majiang majiang = majiangs[i];
        GameObject.Destroy(majiang.gameObject2D,0f);
        //setPengGangPosition(majiang,user.pengmajiangs.Count+user.gangmajiangs.Count-majiangs.Count+i,user.id,true);
      }

      setPengGang(user);

      JsonData userList = content["users"];
      setUserValues(userList,true);

      setOptions();
      animateOptionNotice();
    }else if(cmd=="gang"){
      if(audioToggle.isOn){
        audioSource.PlayOneShot(gangSound,effect_sound_volume);
      }
      JsonData majiangMap = content["majiang"];
      Majiang gangMajiang = GetMajiang(Majiang.toMajiang(majiangMap).id);
      string userid = (string)content["user"];
      User user =  room.getUser(userid);
      user.status = 0;
      user.gangOptions = null;
      
      playGangAction(user);
      if(content["fanId"]!=null){
        int fanId = (int)content["fanId"];
        Majiang fanMajiang = getMajiang(fanId);
        if(fanMajiang!=null){
          fanMajiang.gang = true;
          if(user.id == room.userBottom.id){
            fapai2D(fanMajiang);
          }
          user.majiangs.Add(fanMajiang);
          user.zhuapai = fanMajiang;
          zhuapaiAnimate(fanMajiang,user);
        }
      }

      room.nowUserid = userid;   
      setUserPositionAlert();   
      List<Majiang> majiangs = user.gang(gangMajiang);

      setPengGang(user);
      for(int i=0;i<majiangs.Count;i++){
        Majiang majiang = majiangs[i];
        GameObject.Destroy(majiang.gameObject2D,0f);
        //setPengGangPosition(majiang,user.pengmajiangs.Count+user.gangmajiangs.Count-majiangs.Count+i,user.id,true);
      }
      for(int i=0;i<room.users.Count;i++){
        User u = room.users[i];
        u.removeChuMajiang(gangMajiang.id);
      }
      if(content["users"]!=null){
        JsonData userList = content["users"];
        setUserValues(userList,true);
      }
      setOptions();
      animateOptionNotice();
    }else if(cmd=="liuju"){
      int roomStatus = (int)content["status"];
      room.status = roomStatus;
      JsonData userList = content["users"];
      setUserValues(userList,false);
      Toast.info("流局",mainPanel.transform);
      if(audioToggle.isOn){
        audioSource.PlayOneShot(lossSound,effect_sound_volume);
      }
      userPostionAlert.active = false;      
      Invoke("goRoundResultPage",3f);
    }else if(cmd=="hu"){
      if(audioToggle.isOn){
        audioSource.PlayOneShot(huSound,effect_sound_volume);
      }
      string userid = (string)content["user"];
      User user = room.getUser(userid);
      room.huUserId = userid;
      int roomStatus = (int)content["status"];
      room.status = roomStatus;

      userPostionAlert.active = false;
      animateOptionNotice();
      JsonData userList = content["users"];
      setUserValues(userList,false);
      huAnimate();
      Invoke("goRoundResultPage",4f);
      setOptions();
      playUserAnimator(user,"MJ_Hu");
      playHuAction(user);
    }else if(cmd=="exit"){
      Toast.info("用户退出",mainPanel.transform);
    }else if(cmd=="chat"){
      string userid = (string)content["user"];
      User user = room.getUser(userid);
      Chat chat = Chat.toChat(content);
      Toast.info(user.nickname+":"+chat.msg,mainPanel.transform,true);
      AudioClip a = Resources.Load("Audio/"+chat.voice, typeof(AudioClip)) as AudioClip;
      if(audioToggle.isOn){
        audioSource.PlayOneShot (a,effect_sound_volume);
      }
    }
  }

  void IsConnect(){
    SocketClient.socketClient.IsConnect();
  }

  void syncMajiangs(User user){
    // if(user.id==AppUtil.user.id){
      // Toast.info("麻将有问题，正在同步麻将",mainPanel.transform);
      socketClient.sendSyncMajiangs(AppUtil.roomNo,user.id,(JsonData res)=>{
        JsonData majiangList = res["majiangs"];
        if(majiangList!=null){
          List<Majiang> majiangs = new List<Majiang>();;
          for(int i=0;i<majiangList.Count;i++){
            JsonData majiangMap = majiangList[i];
            Majiang majiang = Majiang.toMajiang(majiangMap);
            majiangs.Add(GetMajiang(majiang.id));
          }

          user.majiangs = majiangs;
          setGamePais();

          if(user.id==AppUtil.user.id){
            destoryMajiangs();
            user.majiangs = majiangs;
            user.sortMajiangs();
            createMajiangs(false);
          }
        }
      });
    // }
  }
  public User getRoomUser(string id) {
		for(int i=0;i<roomUsers.Count;i++){
			User user = roomUsers[i];
			if(user.id==id){
				return user;
			}
		}
		return null;
	}

  // 播放麻将出牌动作
  void playUserAnimator(User u,string name){
    User user = getRoomUser(u.id);
    Debug.Log("user.bodyObject===="+name+","+(user.bodyObject==null));
    Animator userAnimatorBody  = (Animator)user.bodyObject.GetComponent(typeof(Animator));
    userAnimatorBody.Play(name);
    if(user.upperObject!=null){
      Animator animator  = (Animator)user.upperObject.GetComponent(typeof(Animator));
      animator.Play(name);
    }
    if(user.trousersObject!=null){
      Animator animator  = (Animator)user.trousersObject.GetComponent(typeof(Animator));
      animator.Play(name);
    }
    if(user.shoesObject!=null){
      Animator animator  = (Animator)user.shoesObject.GetComponent(typeof(Animator));
      animator.Play(name);
    }
    if(user.hairObject!=null){
      Animator animator  = (Animator)user.hairObject.GetComponent(typeof(Animator));
      animator.Play(name);
    }
    if(user.hatObject!=null){
      Animator animator  = (Animator)user.hatObject.GetComponent(typeof(Animator));
      animator.Play(name);
    }
    
  }

  

  void huAnimate(){

    float delayUnit = 0.06f;
    for (int i = 0; i < room.userBottom.majiangs.Count; i++) {
      Majiang majiang = room.userBottom.majiangs[i];
      majiang.gameObject.transform.DOLocalRotate(new Vector3(180f,180f,0f),0.5f);
    }

    for (int i = 0; i < room.userRight.majiangs.Count; i++) {
      Majiang majiang = room.userRight.majiangs[i];
      majiang.gameObject.transform.DOLocalRotate(new Vector3(180f,90f,0f),0.5f);
    }

    for (int i = 0; i < room.userTop.majiangs.Count; i++) {
      Majiang majiang = room.userTop.majiangs[i];
      majiang.gameObject.transform.DOLocalRotate(new Vector3(180f,0f,0f),0.5f);
    }

    for (int i = 0; i < room.userLeft.majiangs.Count; i++) {
      Majiang majiang = room.userLeft.majiangs[i];
      majiang.gameObject.transform.DOLocalRotate(new Vector3(180f,270f,0f),0.5f);
    }
    List<Majiang> majiangs = room.userBottom.majiangs;
    for(int i=0;i<majiangs.Count;i++){
      Majiang majiang = majiangs[i];
      if(majiang.gameObject2D!=null){
        GameObject.Destroy(majiang.gameObject2D);
      }
    }

  }

  void playPengAction(User user){
    if(user.id == room.userTop.id){
      pengParticleSystem.transform.position = topPGActionPosition;
    }else if(user.id == room.userLeft.id){
      pengParticleSystem.transform.position = leftPGActionPosition;
    }else if(user.id == room.userRight.id){
      pengParticleSystem.transform.position = rightPGActionPosition;
    }else if(user.id == room.userBottom.id){
      pengParticleSystem.transform.position = bottomPGActionPosition;
    }
    pengParticleSystem.Play();
  }

  

  void playGangAction(User user){
    if(user.id == room.userTop.id){
      gangParticleSystem.transform.position = topPGActionPosition;
    }else if(user.id == room.userLeft.id){
      gangParticleSystem.transform.position = leftPGActionPosition;
    }else if(user.id == room.userRight.id){
      gangParticleSystem.transform.position = rightPGActionPosition;
    }else if(user.id == room.userBottom.id){
      gangParticleSystem.transform.position = bottomPGActionPosition;
    }
    gangParticleSystem.Play();
  }
  void playHuAction(User user){
    if(user.id == room.userTop.id){
      huParticleSystem.transform.position = topPGActionPosition;
    }else if(user.id == room.userLeft.id){
      huParticleSystem.transform.position = leftPGActionPosition;
    }else if(user.id == room.userRight.id){
      huParticleSystem.transform.position = rightPGActionPosition;
    }else if(user.id == room.userBottom.id){
      huParticleSystem.transform.position = bottomPGActionPosition;
    }
    huParticleSystem.Play();
  }

  void goRoundResultPage(){
    AppUtil.room = room;
    socketClient.onRecieveMessage = null;
    backAudioSource.Pause();
    Invoke("DestroyScene",1.5f);
    SceneManager.LoadScene("RoundResult");
  }

  // 动画操作通知
  void animateOptionNotice(){

  }

  // 设置操作
  void setOptions(){
    if(room.userBottom.gangOptions!=null && room.userBottom.gangOptions.Count>0 && room.nowUserid==room.userBottom.id){
      optionPanel.active = true;
    }else if(room.userBottom.status==1 || room.userBottom.status==2|| room.userBottom.status==3 ){
      optionPanel.active = true;
    }else{
      optionPanel.active = false;
    }
    int totalWidth =0 ;
    if(room.userBottom.status==1){
      //碰
      pengButton.GetComponent<Transform>().gameObject.active = true;
      gang1Button.GetComponent<Transform>().gameObject.active = false;
      gang2Button.GetComponent<Transform>().gameObject.active = false;
      gang3Button.GetComponent<Transform>().gameObject.active = false;
      huButton.GetComponent<Transform>().gameObject.active = false;
      guoButton.GetComponent<Transform>().gameObject.active = true;
      totalWidth = 200*2;
    }else if(room.userBottom.status==2){
      //杠
      pengButton.GetComponent<Transform>().gameObject.active = true;
      gang1Button.GetComponent<Transform>().gameObject.active = true;
      gang2Button.GetComponent<Transform>().gameObject.active = false;
      gang3Button.GetComponent<Transform>().gameObject.active = false;
      huButton.GetComponent<Transform>().gameObject.active = false;
      guoButton.GetComponent<Transform>().gameObject.active = true;
      totalWidth = 200*2+250;
      Sprite sprite = Resources.Load("Images/"+room.nowmajiang.getImageName(), typeof(Sprite)) as Sprite;
      gang1Button.GetComponent<Transform>().Find("GangeImage").GetComponent<Image>().sprite = sprite ;
    }else if(room.userBottom.status==3){
      //胡
      pengButton.GetComponent<Transform>().gameObject.active = false;
      gang1Button.GetComponent<Transform>().gameObject.active = false;
      gang2Button.GetComponent<Transform>().gameObject.active = false;
      gang3Button.GetComponent<Transform>().gameObject.active = false;
      huButton.GetComponent<Transform>().gameObject.active = true;
      guoButton.GetComponent<Transform>().gameObject.active = true;
      huDescText.text=room.userBottom.desc;
      totalWidth = 200*2;
      if(room.userBottom.gangOptions!=null && room.userBottom.gangOptions.Count>0){
        pengButton.GetComponent<Transform>().gameObject.active = false;
        gang1Button.GetComponent<Transform>().gameObject.active = true;
        Majiang majiang = room.userBottom.gangOptions[0];
        Sprite sprite = Resources.Load("Images/"+majiang.getImageName(), typeof(Sprite)) as Sprite;
        gang1Button.GetComponent<Transform>().Find("GangeImage").GetComponent<Image>().sprite = sprite ;

        totalWidth = totalWidth + 250;
        if(room.userBottom.gangOptions.Count>1){
          gang2Button.GetComponent<Transform>().gameObject.active = true;
          totalWidth = totalWidth + 250;
          Majiang mj = room.userBottom.gangOptions[1];
          Sprite s = Resources.Load("Images/"+mj.getImageName(), typeof(Sprite)) as Sprite;
          gang2Button.GetComponent<Transform>().Find("GangeImage").GetComponent<Image>().sprite = s ;
        }else{
          gang2Button.GetComponent<Transform>().gameObject.active = false;
        }
        if(room.userBottom.gangOptions.Count>2){
          gang3Button.GetComponent<Transform>().gameObject.active = true;
          totalWidth = totalWidth + 250;
          Majiang mj = room.userBottom.gangOptions[2];
          Sprite s = Resources.Load("Images/"+mj.getImageName(), typeof(Sprite)) as Sprite;
          gang3Button.GetComponent<Transform>().Find("GangeImage").GetComponent<Image>().sprite = s ;
        }else{
          gang3Button.GetComponent<Transform>().gameObject.active = false;
        }
      }
    }else if(room.userBottom.gangOptions!=null && room.userBottom.gangOptions.Count>0){
      pengButton.GetComponent<Transform>().gameObject.active = false;
      gang1Button.GetComponent<Transform>().gameObject.active = true;
      totalWidth = 250;

      Majiang mj = room.userBottom.gangOptions[0];
      Sprite sprite = Resources.Load("Images/"+mj.getImageName(), typeof(Sprite)) as Sprite;
      gang1Button.GetComponent<Transform>().Find("GangeImage").GetComponent<Image>().sprite = sprite ;

      if(room.userBottom.gangOptions.Count>1){
        gang2Button.GetComponent<Transform>().gameObject.active = true;
        totalWidth = totalWidth+250;
        Majiang mj2 = room.userBottom.gangOptions[1];
        Sprite s = Resources.Load("Images/"+mj2.getImageName(), typeof(Sprite)) as Sprite;
        gang2Button.GetComponent<Transform>().Find("GangeImage").GetComponent<Image>().sprite = s ;
      }else{
        gang2Button.GetComponent<Transform>().gameObject.active = false;
      }

      if(room.userBottom.gangOptions.Count>2){
        gang3Button.GetComponent<Transform>().gameObject.active = true;
        totalWidth = totalWidth+250;
        Majiang mj2 = room.userBottom.gangOptions[2];
        Sprite s = Resources.Load("Images/"+mj2.getImageName(), typeof(Sprite)) as Sprite;
        gang3Button.GetComponent<Transform>().Find("GangeImage").GetComponent<Image>().sprite = s ;
      }else{
        gang3Button.GetComponent<Transform>().gameObject.active = false;
      }

      huButton.GetComponent<Transform>().gameObject.active = false;
      guoButton.GetComponent<Transform>().gameObject.active = true;

      totalWidth = totalWidth+200;
    }

    float left  =Screen.width/2-totalWidth/2+75f;
    if(gang1Button.GetComponent<Transform>().gameObject.active){
      RectTransform transform = gang1Button.GetComponent<RectTransform>();
      transform.anchoredPosition = new Vector3(left,90f,0f); 
      left = left+250;
    }

    if(gang2Button.GetComponent<Transform>().gameObject.active){
      RectTransform transform = gang2Button.GetComponent<RectTransform>();
      transform.anchoredPosition = new Vector3(left,90f,0f); 
      left = left+250;
    }

    if(gang3Button.GetComponent<Transform>().gameObject.active){
      RectTransform transform = gang3Button.GetComponent<RectTransform>();
      transform.anchoredPosition = new Vector3(left,90f,0f);  
      left = left+250;
    }

    if(pengButton.GetComponent<Transform>().gameObject.active){
      RectTransform transform = pengButton.GetComponent<RectTransform>();
      transform.anchoredPosition = new Vector3(left,90f,0f);
      left = left+200;
    }

    if(huButton.GetComponent<Transform>().gameObject.active){
      RectTransform transform = huButton.GetComponent<RectTransform>();
      transform.anchoredPosition = new Vector3(left,90f,0f); 
      left = left+200;
    }

    if(guoButton.GetComponent<Transform>().gameObject.active){
      RectTransform transform = guoButton.GetComponent<RectTransform>();
      transform.anchoredPosition = new Vector3(left,90f,0f); 
      left = left+200;
    }
    
  }

  void playMajiangVoice(Majiang majiang){
    int type = majiang.type;
    int value = majiang.value;
    string name = "";
    if(type<4){
      name = "g_"+(value+((type==1?"tiao":(type==2?"tong":"wan")))+"");
    }else if(type==4){
      name = "g_dongfeng";
    }else if(type==5){
      name = "g_nanfeng";
    }else if(type==6){
      name = "g_xifeng";
    }else if(type==7){
      name = "g_beifeng";
    }else if(type==8){
      name = "g_zhong";
    }else if(type==9){
      name = "g_fa";
    }else if(type==10){
      name = "g_bai";
    }
    AudioClip a = Resources.Load("Audio/"+name, typeof(AudioClip)) as AudioClip;
    if(audioToggle.isOn){
      audioSource.PlayOneShot (a,effect_sound_volume);
    }
  }


  // 重排麻将
  void resortMajiang(User user){
    float delayUnit = 0.06f;
    
  }
  

  Majiang getMajiang(int id){
    for(int i=0;i<=allmajiangs.Count;i++){
      Majiang majiang =allmajiangs[i];
      if(majiang.id==id){
        return majiang;
      }
    }
    return null;
  }
  
  //初始化麻将
  List<Majiang> initMajiangs(){

		allmajiangs = new List<Majiang>();
		
		//条子
		int type =1;
		for(int i=1;i<=9;i++){
			for(int j=1;j<=4;j++){
				Majiang majiang = new Majiang();
				majiang.type = type;
				majiang.value = i;
				allmajiangs.Add(majiang);
			}
		}
		
		//筒子
		type =2;
		for(int i=1;i<=9;i++){
			for(int j=1;j<=4;j++){
				Majiang majiang = new Majiang();
				majiang.type = type;
				majiang.value = i;
				allmajiangs.Add(majiang);
			}
		}
		
		//万
		type =3;
		for(int i=1;i<=9;i++){
			for(int j=1;j<=4;j++){
				Majiang majiang = new Majiang();
				majiang.type = type;
				majiang.value = i;
				allmajiangs.Add(majiang);
			}
		}
		
		//东
		type =4;
		for(int i=1;i<=4;i++){
			Majiang majiang = new Majiang();
			majiang.type = type;
			majiang.value = 0;
			allmajiangs.Add(majiang);
		}
		//南
		type =5;
		for(int i=1;i<=4;i++){
			Majiang majiang = new Majiang();
			majiang.type = type;
			majiang.value=0;
			allmajiangs.Add(majiang);
		}
		
		//西
		type =6;
		for(int i=1;i<=4;i++){
			Majiang majiang = new Majiang();
			majiang.type = type;
			majiang.value=0;
			allmajiangs.Add(majiang);
		}
		
		//北
		type =7;
		for(int i=1;i<=4;i++){
			Majiang majiang = new Majiang();
			majiang.type = type;
			majiang.value=0;
			allmajiangs.Add(majiang);
		}
		//中
		type =8;
		for(int i=1;i<=4;i++){
			Majiang majiang = new Majiang();
			majiang.type = type;
			majiang.value=0;
			allmajiangs.Add(majiang);
		}
		
		//发财
		type =9;
		for(int i=1;i<=4;i++){
			Majiang majiang = new Majiang();
			majiang.type = type;
			majiang.value=0;
			allmajiangs.Add(majiang);
		}
		
		//白板
		type =10;
		for(int i=1;i<=4;i++){
			Majiang majiang = new Majiang();
			majiang.type = type;
			majiang.value=0;
			allmajiangs.Add(majiang);
		}

		//设置id
		for(int i=0;i<allmajiangs.Count;i++){
			Majiang majiang = allmajiangs[i];
			majiang.id =i+1;
		}
		return allmajiangs;
    
  }

  Majiang GetMajiang(int id){
    for(int i=0;i<allmajiangs.Count;i++){
			Majiang majiang = allmajiangs[i];
			if(majiang.id==id){
        return majiang;
      }
		}
    return null;
  }

  //抓牌动作 
  void zhupai(int id){
    Majiang majiang = GetMajiang(id);
    animateMajiang = majiang;
    animateTimes = 0;

    Vector3 majiangAnimateEndPostion = new Vector3(-1.34f,8.573f,-3.14f);
    Vector3 majiangAnimateEndRotation = new Vector3(90f,0f,0f);
    Transform transform  = majiang.gameObject.GetComponent<Transform>();
    majiangAnimateStepPostion = new Vector3((majiangAnimateEndPostion.x-transform.position.x)/totalAnimateTimes,(majiangAnimateEndPostion.y-transform.position.y)/totalAnimateTimes,(majiangAnimateEndPostion.z-transform.position.z)/totalAnimateTimes);
    majiangAnimateStepRotation = new Vector3((majiangAnimateEndRotation.x-transform.rotation.x)/totalAnimateTimes,(majiangAnimateEndRotation.y-transform.rotation.y)/totalAnimateTimes,(majiangAnimateEndRotation.z-transform.rotation.z)/totalAnimateTimes);
    print(majiangAnimateStepPostion.x+","+majiangAnimateStepPostion.y+","+majiangAnimateStepPostion.z);

  }


	// Use this for initialization
	void Start () {
    #if (UNITY_2018_3_OR_NEWER)
      if (Permission.HasUserAuthorizedPermission(Permission.Microphone))
      {

      }
      else
      {
          Permission.RequestUserPermission(Permission.Microphone);
      }
    #endif

    mRtcEngine = IRtcEngine.GetEngine(agoraAppId);

    mRtcEngine.OnJoinChannelSuccess += (string channelName, uint uid, int elapsed) => {
        string joinSuccessMessage = string.Format("joinChannel callback uid: {0}, channel: {1}, version: {2}", uid, channelName, IRtcEngine.GetSdkVersion());
        Debug.Log(joinSuccessMessage);
    };

    mRtcEngine.OnLeaveChannel += (RtcStats stats) => {
        string leaveChannelMessage = string.Format("onLeaveChannel callback duration {0}, tx: {1}, rx: {2}, tx kbps: {3}, rx kbps: {4}", stats.duration, stats.txBytes, stats.rxBytes, stats.txKBitRate, stats.rxKBitRate);
        Debug.Log(leaveChannelMessage);
    };

    mRtcEngine.OnUserJoined += (uint uid, int elapsed) => {
        string userJoinedMessage = string.Format("onUserJoined callback uid {0} {1}", uid, elapsed);
        Debug.Log(userJoinedMessage);
    };

    mRtcEngine.OnUserOffline += (uint uid, USER_OFFLINE_REASON reason) => {
        string userOfflineMessage = string.Format("onUserOffline callback uid {0} {1}", uid, reason);
        Debug.Log(userOfflineMessage);
    };

    mRtcEngine.OnVolumeIndication += (AudioVolumeInfo[] speakers, int speakerNumber, int totalVolume) => {
        if (speakerNumber == 0 || speakers == null)
        {
            Debug.Log(string.Format("onVolumeIndication only local {0}", totalVolume));
        }

        for (int idx = 0; idx < speakerNumber; idx++)
        {
            string volumeIndicationMessage = string.Format("{0} onVolumeIndication {1} {2}", speakerNumber, speakers[idx].uid, speakers[idx].volume);
            Debug.Log(volumeIndicationMessage);
        }
    };

    // mRtcEngine.OnUserMuted += (uint uid, bool muted) => {
    //     string userMutedMessage = string.Format("onUserMuted callback uid {0} {1}", uid, muted);
    //     Debug.Log(userMutedMessage);
    // };

    mRtcEngine.OnWarning += (int warn, string msg) => {
        string description = IRtcEngine.GetErrorDescription(warn);
        string warningMessage = string.Format("onWarning callback {0} {1} {2}", warn, msg, description);
        Debug.Log(warningMessage);
    };

    mRtcEngine.OnError += (int error, string msg) => {
        string description = IRtcEngine.GetErrorDescription(error);
        string errorMessage = string.Format("onError callback {0} {1} {2}", error, msg, description);
        Debug.Log(errorMessage);
    };

    mRtcEngine.OnRtcStats += (RtcStats stats) => {
        // string rtcStatsMessage = string.Format("onRtcStats callback duration {0}, tx: {1}, rx: {2}, tx kbps: {3}, rx kbps: {4}, tx(a) kbps: {5}, rx(a) kbps: {6} users {7}",
        //     stats.duration, stats.txBytes, stats.rxBytes, stats.txKBitRate, stats.rxKBitRate, stats.txAudioKBitRate, stats.rxAudioKBitRate, stats.users);
        // Debug.Log(rtcStatsMessage);

        int lengthOfMixingFile = mRtcEngine.GetAudioMixingDuration();
        int currentTs = mRtcEngine.GetAudioMixingCurrentPosition();

        string mixingMessage = string.Format("Mixing File Meta {0}, {1}", lengthOfMixingFile, currentTs);
        Debug.Log(mixingMessage);
    };

    mRtcEngine.OnAudioRouteChanged += (AUDIO_ROUTE route) => {
        string routeMessage = string.Format("onAudioRouteChanged {0}", route);
        Debug.Log(routeMessage);
    };

    mRtcEngine.OnRequestToken += () => {
        string requestKeyMessage = string.Format("OnRequestToken");
        Debug.Log(requestKeyMessage);
    };

    mRtcEngine.OnConnectionInterrupted += () => {
        string interruptedMessage = string.Format("OnConnectionInterrupted");
        Debug.Log(interruptedMessage);
    };

    mRtcEngine.OnConnectionLost += () => {
        string lostMessage = string.Format("OnConnectionLost");
        Debug.Log(lostMessage);
    };

    mRtcEngine.SetLogFilter(LOG_FILTER.INFO);

    // mRtcEngine.setLogFile("path_to_file_unity.log");

    mRtcEngine.SetChannelProfile(CHANNEL_PROFILE.CHANNEL_PROFILE_COMMUNICATION);

	}

  JsonData getUserType(int i){
    int userIndex = room.users.IndexOf(room.getUser(room.userBottom.id));
    int ii = (userIndex)%4;
    JsonData dic = new JsonData();
    int type = 0;
    int start = 0;
    if(i<(ii+1)*deng*2 && i>=ii*deng*2){
      //下面
      type = 0;
      start = ii*deng*2;
    }
    ii = (userIndex+1)%4;
    if(i<(ii+1)*deng*2 && i>=(ii)*deng*2){
      //左边
      type =  3;
      start = ii*deng*2;
    }

    // 上面
    ii = (userIndex+2)%4;
    if(i<(ii+1)*deng*2 && i>=(ii)*deng*2){
      type = 2;
      start = ii*deng*2;
    }

    // 右边
    ii = (userIndex+3)%4;
    if(i<(ii+1)*deng*2 && i>=(ii)*deng*2){
      type = 1;
      start = ii*deng*2;
    }
    dic["type"] = type;
    dic["start"] = start;
    return dic;

  }

  void fanAnimate(){
    Vector3 fanPosition = room.fan.gameObject.transform.position;
    transform.DOLocalRotate(cameraRotation,0.2f);
    transform.DOLocalMove(new Vector3(fanPosition.x,fanPosition.y+1f,fanPosition.z-0.4f),0.4f).OnComplete(()=>{
      if(room.fan.direction==0){
        room.fan.gameObject.transform.DOLocalRotate(new Vector3(180f,180f,0f),1f,RotateMode.FastBeyond360);
      }else if(room.fan.direction==1){
        room.fan.gameObject.transform.DOLocalRotate(new Vector3(180f,90f,0f),1f,RotateMode.FastBeyond360);
      }else if(room.fan.direction==2){
        room.fan.gameObject.transform.DOLocalRotate(new Vector3(180f,0f,0f),1f,RotateMode.FastBeyond360);
      }else if(room.fan.direction==3){
        room.fan.gameObject.transform.DOLocalRotate(new Vector3(180f,270f,0f),1f,RotateMode.FastBeyond360);
      }
      
    });
    Invoke("recoverCamera", 3.1f);
  }


  // 洗牌后的结果
  void xipai(bool animate){

    float initDistance = 0.3f;//初始化麻将的位置距离最终的位置,从卡槽出来的位置
    float xipaiMoveDisYTemp = xipaiMoveDisY;
    if(!animate){
      initDistance = 0f;
      xipaiMoveDisYTemp = 0f;
    }

    int userIndex = room.users.IndexOf(room.getUser(room.userBottom.id));
    
    for (int i = 0; i < room.allMajiangIds.Count; i++) {
      int majiangId = room.allMajiangIds[i];
      Majiang majiang = GetMajiang(majiangId);
      float space = 0;
      Vector3 pos = new Vector3(0f,0f,0f);
      GameObject gameObject =  majiang.gameObject;
      gameObject.transform.Find("PlaneTianying").gameObject.active = false;
      JsonData typeData = getUserType(i);
      int type = (int)typeData["type"];
      int start = (int)typeData["start"];
      if(type==0){
        //创建下面的
        space = ((i-start)/2)*majiangWidth;
        pos = new Vector3(bottomLeaveComputeStart.x-space, bottomLeaveComputeStart.y+majiangHeight-xipaiMoveDisYTemp, bottomLeaveComputeStart.z-initDistance);
        if(i%2==1){
          pos = new Vector3(bottomLeaveComputeStart.x-space, bottomLeaveComputeStart.y-xipaiMoveDisYTemp, bottomLeaveComputeStart.z-initDistance);
        }
        gameObject.GetComponent<Transform>().localEulerAngles = new Vector3(0,180,0);
        majiang.direction = 0;
      }else if(type==1){
        //创建右边的
        space = ((i-start)/2)*majiangWidth;
        pos = new Vector3(rightLeaveComputeStart.x+initDistance, rightLeaveComputeStart.y+majiangHeight-xipaiMoveDisYTemp, rightLeaveComputeStart.z-space);
        if(i%2==1){
          pos = new Vector3(rightLeaveComputeStart.x+initDistance, rightLeaveComputeStart.y-xipaiMoveDisYTemp, rightLeaveComputeStart.z-space);
        }
        gameObject.GetComponent<Transform>().localEulerAngles = new Vector3(0,90,0);
        majiang.direction = 1;
      }else if(type==2){
        //创建顶部
        space = ((i-start)/2)*majiangWidth;
        pos = new Vector3(topLeaveComputeStart.x+space, topLeaveComputeStart.y+majiangHeight-xipaiMoveDisYTemp, topLeaveComputeStart.z+initDistance);
        if(i%2==1){
          pos = new Vector3(topLeaveComputeStart.x+space, topLeaveComputeStart.y-xipaiMoveDisYTemp, topLeaveComputeStart.z+initDistance);
        }
        gameObject.GetComponent<Transform>().localEulerAngles = new Vector3(0,0,0);
        majiang.direction = 2;
      }else if(type==3){
        //创建左边的
        space = ((i-start)/2)*majiangWidth;
        pos = new Vector3(leftLeaveComputeStart.x-initDistance, leftLeaveComputeStart.y+majiangHeight-xipaiMoveDisYTemp, leftLeaveComputeStart.z+space);
        if(i%2==1){
          pos = new Vector3(leftLeaveComputeStart.x-initDistance, leftLeaveComputeStart.y-xipaiMoveDisYTemp, leftLeaveComputeStart.z+space);
        }
        gameObject.GetComponent<Transform>().localEulerAngles = new Vector3(0,270,0);
        majiang.direction = 3;
      }
      gameObject.GetComponent<Transform>().position = pos;
    }

    topMajiangCao.transform.DOLocalMoveY(300, 1);
    bottomMajiangCao.transform.DOLocalMoveY(300, 1);
    leftMajiangCao.transform.DOLocalMoveY(300, 1);
    rightMajiangCao.transform.DOLocalMoveY(300, 1);
    if(animate){
      Invoke("xipai2", 1.1f);
      Invoke("xipai3", 2.2f);
    }
  }
  
  //第二步动画
  void xipai2(){
    float initDistance = 0.2f;//初/初始化麻将的位置距离最终的位置,从卡槽出来的位置
    for (int i = 0; i < room.leavemajiangs.Count; i++) {
      Majiang majiang = room.leavemajiangs[i];

      float space = 0;
      Vector3 pos = new Vector3(0f,0f,0f);
      GameObject gameObject =  majiang.gameObject;
      JsonData typeData = getUserType(i);
      int type = (int)typeData["type"];
      int start = (int)typeData["start"];

      if(type==0){
        //创建底部的
        space = ((i-start)/2)*majiangWidth;
        pos = new Vector3(bottomLeaveComputeStart.x-space, bottomLeaveComputeStart.y+majiangHeight-xipaiMoveDisY, bottomLeaveComputeStart.z);
        if(i%2==1){
          pos = new Vector3(bottomLeaveComputeStart.x-space, bottomLeaveComputeStart.y-xipaiMoveDisY, bottomLeaveComputeStart.z);
        }
      }else if(type==1){
        //创建右边的
        space = ((i-start)/2)*majiangWidth;
        pos = new Vector3(rightLeaveComputeStart.x, rightLeaveComputeStart.y+majiangHeight-xipaiMoveDisY, rightLeaveComputeStart.z-space);
        if(i%2==1){
          pos = new Vector3(rightLeaveComputeStart.x, rightLeaveComputeStart.y-xipaiMoveDisY, rightLeaveComputeStart.z-space);
        }
      }else if(type==2){
        //创建顶部
        space = ((i-start)/2)*majiangWidth;
        pos = new Vector3(topLeaveComputeStart.x+space, topLeaveComputeStart.y+majiangHeight-xipaiMoveDisY, topLeaveComputeStart.z);
        if(i%2==1){
          pos = new Vector3(topLeaveComputeStart.x+space, topLeaveComputeStart.y-xipaiMoveDisY, topLeaveComputeStart.z);
        }
      }else if(type==3){
        //创建右边的
        space = ((i-start)/2)*majiangWidth;
        pos = new Vector3(leftLeaveComputeStart.x, leftLeaveComputeStart.y+majiangHeight-xipaiMoveDisY, leftLeaveComputeStart.z+space);
        if(i%2==1){
          pos = new Vector3(leftLeaveComputeStart.x, leftLeaveComputeStart.y-xipaiMoveDisY, leftLeaveComputeStart.z+space);
        }
      }
      gameObject.transform.DOLocalMove(new Vector3(pos.x,pos.y,pos.z), 1);
    }
  }

  //第三步动画
  void xipai3(){
    
    for (int i = 0; i < room.leavemajiangs.Count; i++) {
      Majiang majiang = room.leavemajiangs[i];
      GameObject gameObject =  majiang.gameObject;
      Vector3 pos = gameObject.transform.position;
      gameObject.transform.DOLocalMove(new Vector3(pos.x,pos.y+xipaiMoveDisY,pos.z), 1);
    }

    topMajiangCao.transform.DOLocalMoveY(323, 1);
    bottomMajiangCao.transform.DOLocalMoveY(323, 1);
    leftMajiangCao.transform.DOLocalMoveY(323, 1);
    rightMajiangCao.transform.DOLocalMoveY(323, 1);
  }



  //刚开始的抓牌动画
  void setZhuapaiPosition(Majiang majiang,Vector3 toPosition, int type,float delay,bool animate){
    Transform transform  = majiang.gameObject.GetComponent<Transform>();
    float time = 0.5f;
    float timeCount = 0f;
    Vector3 rot = new Vector3(0f,0f,0f);
    Majiang m = room.userBottom.getMajiangById(majiang.id);
    if(m!=null){
      if(majiang.tianying){
        majiang.gameObject.transform.Find("PlaneTianying").gameObject.active = true;
      }else{
        majiang.gameObject.transform.Find("PlaneTianying").gameObject.active = false;
      }
    }
    
    if(type==0){
      // 底部用户抓牌
      rot = bottomZhuapaiRotate;
    }else if(type==1){
      // 右边用户抓牌
      rot = rightZhuapaiRotate;
    }else if(type==2){
      // 顶部用户抓牌
      rot = topZhuapaiRotate;
    }else if(type==3){
      // 左边用户抓牌
      rot = leftZhuapaiRotate;
    }
    if(animate){
      DOTween.To(() => timeCount, a => timeCount = a, 1, delay).OnComplete(() => {
        transform.DOLocalMove(toPosition, time);
        transform.DOLocalRotate(rot, time);
      });
    }else{

      transform.localPosition = toPosition;
      transform.localEulerAngles = rot;
    }
  }


  void setPengGang(User user){
    if(user.pengmajiangs!=null){
      for (int i = 0; i < user.pengmajiangs.Count; i++) {
        Majiang majiang = user.pengmajiangs[i];
  
        setPengGangPosition(majiang,i,user.id,false);
      }
    }
    
    if(user.gangmajiangs!=null){
      for (int i = 0; i < user.gangmajiangs.Count; i++) {
        Majiang majiang = user.gangmajiangs[i];
  
        setPengGangPosition(majiang,i+user.pengmajiangs.Count,user.id,false);
      }
    }
  }

  //设置牌，一般是重新进入房间调用
  void setGamePais(){
    float delayUnit = 0.06f;
    for (int i = 0; i < room.userBottom.majiangs.Count; i++) {
      Majiang majiang = room.userBottom.majiangs[i];
      setZhuapaiPosition(majiang,new Vector3(bottomPaisComputeStart.x+i*majiangWidth,bottomPaisComputeStart.y,bottomPaisComputeStart.z),0,i*delayUnit,false);
    }
    for (int i = 0; i < room.userBottom.chumajiangs.Count; i++) {
      Majiang majiang = room.userBottom.chumajiangs[i];
      setChupaiPosition(majiang,room.userBottom.id,false,i);
    }
    destoryMajiangs();
    createMajiangs(false);

    setPengGang(room.userBottom);
    
    for (int i = 0; i < room.userRight.majiangs.Count; i++) {
      Majiang majiang = room.userRight.majiangs[i];
      setZhuapaiPosition(majiang,new Vector3(rightPaisComputeStart.x,rightPaisComputeStart.y,rightPaisComputeStart.z+i*majiangWidth),1,i*delayUnit+room.userBottom.majiangs.Count*delayUnit,false);
    }

    for (int i = 0; i < room.userRight.chumajiangs.Count; i++) {
      Majiang majiang = room.userRight.chumajiangs[i];
      setChupaiPosition(majiang,room.userRight.id,false,i);
    }

    setPengGang(room.userRight);

    for (int i = 0; i < room.userTop.majiangs.Count; i++) {
      Majiang majiang = room.userTop.majiangs[i];
      setZhuapaiPosition(majiang,new Vector3(topPaisComputeStart.x-i*majiangWidth,topPaisComputeStart.y,topPaisComputeStart.z),2,i*delayUnit+room.userBottom.majiangs.Count*delayUnit+room.userRight.majiangs.Count*delayUnit,false);
    }

    for (int i = 0; i < room.userTop.chumajiangs.Count; i++) {
      Majiang majiang = room.userTop.chumajiangs[i];
      setChupaiPosition(majiang,room.userTop.id,false,i);
    }

    setPengGang(room.userTop);

    for (int i = 0; i < room.userLeft.majiangs.Count; i++) {
      Majiang majiang = room.userLeft.majiangs[i];

      setZhuapaiPosition(majiang,new Vector3(leftPaisComputeStart.x,leftPaisComputeStart.y,leftPaisComputeStart.z-i*majiangWidth),3,i*delayUnit+room.userBottom.majiangs.Count*delayUnit+room.userRight.majiangs.Count*delayUnit+room.userTop.majiangs.Count*delayUnit,false);
    }

    for (int i = 0; i < room.userLeft.chumajiangs.Count; i++) {
      Majiang majiang = room.userLeft.chumajiangs[i];
      setChupaiPosition(majiang,room.userLeft.id,false,i);
    }

    setPengGang(room.userLeft);


  }

  void DestroyScene(){
    Destroy(gameObject);
  }

  // 开局抓牌
  void animateZhuapais(){
    float delayUnit = 0.06f;
    for (int i = 0; i < room.userBottom.majiangs.Count; i++) {
      Majiang majiang = room.userBottom.majiangs[i];

      setZhuapaiPosition(majiang,new Vector3(bottomPaisComputeStart.x+i*majiangWidth,bottomPaisComputeStart.y,bottomPaisComputeStart.z),0,i*delayUnit,true);
    }
    createMajiangs(true);

    for (int i = 0; i < room.userRight.majiangs.Count; i++) {
      Majiang majiang = room.userRight.majiangs[i];

      setZhuapaiPosition(majiang,new Vector3(rightPaisComputeStart.x,rightPaisComputeStart.y,rightPaisComputeStart.z+i*majiangWidth),1,i*delayUnit+room.userBottom.majiangs.Count*delayUnit,true);
    }

    for (int i = 0; i < room.userTop.majiangs.Count; i++) {
      Majiang majiang = room.userTop.majiangs[i];

      setZhuapaiPosition(majiang,new Vector3(topPaisComputeStart.x-i*majiangWidth,topPaisComputeStart.y,topPaisComputeStart.z),2,i*delayUnit+room.userBottom.majiangs.Count*delayUnit+room.userRight.majiangs.Count*delayUnit,true);
    }

    for (int i = 0; i < room.userLeft.majiangs.Count; i++) {
      Majiang majiang = room.userLeft.majiangs[i];

      setZhuapaiPosition(majiang,new Vector3(leftPaisComputeStart.x,leftPaisComputeStart.y,leftPaisComputeStart.z-i*majiangWidth),3,i*delayUnit+room.userBottom.majiangs.Count*delayUnit+room.userRight.majiangs.Count*delayUnit+room.userTop.majiangs.Count*delayUnit,true);
    }

    setOptions();
      
  }

  // 抓牌排序
  void sortZhuapais(User user){
    Majiang zhuapai = user.zhuapai;
    user.sortMajiangs();
    if(zhuapai!=null){
      int zhuaIndex = user.getMajiangIndex(zhuapai.id);
      if(zhuaIndex!=-1 && zhuaIndex!=user.majiangs.Count-1){
        Majiang trueZhuaMajiang = GetMajiang(zhuapai.id);
        Transform transform = trueZhuaMajiang.gameObject.transform;
        trueZhuaMajiang.gameObject.transform.DOLocalMoveY(transform.position.y+majiangLength,0.3f).OnComplete(()=>{
          Vector3 v = sortZhuapais(user,zhuapai);
          trueZhuaMajiang.gameObject.transform.DOLocalMove(new Vector3(v.x,v.y+majiangLength,v.z),0.5f);
          float timeCount = 0f;
          DOTween.To(() => timeCount, a => timeCount = a, 1, 0.5f).OnComplete(() => {
            trueZhuaMajiang.gameObject.transform.DOLocalMove(new Vector3(v.x,v.y,v.z),0.3f);
          });
        });
      }else{
        sortZhuapais(user,zhuapai);
      }
    }else{
      sortZhuapais(user,zhuapai);
    }
      
  }

  // 抓牌排序
  void sortZhuapais2D(User user){
    
    if(user.zhuapai!=null){
      //上次抓牌位置
      int lastZhuaIndex = user.getMajiangIndex(user.zhuapai.id);
      user.sortMajiangs();
      Majiang zhuapai = user.zhuapai;
      if(zhuapai!=null){
        int zhuaIndex = user.getMajiangIndex(zhuapai.id);
        if(zhuaIndex!=-1 && zhuaIndex!=user.majiangs.Count-1 && lastZhuaIndex!=zhuaIndex){
          Transform transform = zhuapai.gameObject2D.transform;
          zhuapai.gameObject2D.transform.DOLocalMoveY(majiangLength2D,0.3f).SetEase(Ease.Linear).OnComplete(()=>{

            float x = sortZhuapais2D(user,zhuapai,false);

            zhuapai.gameObject2D.transform.DOLocalMoveX(x,0.5f).SetEase(Ease.Linear) ;
            float timeCount = 0f;
            DOTween.To(() => timeCount, a => timeCount = a, 1, 0.5f).OnComplete(() => {
              zhuapai.gameObject2D.transform.DOLocalMoveY(0,0.3f).SetEase(Ease.Linear) ;
            });
          });
          return;
        }
      }
    }
    user.sortMajiangs();
    sortZhuapais2D(user,user.zhuapai,true);
      
  }


  // 返回抓牌的最终位置
  float sortZhuapais2D(User user,Majiang zhuapai,bool moveZhuapai){
    
    List<Majiang> majiangs = room.userBottom.majiangs;
    int count = room.userBottom.majiangs.Count;
    float width = majiangWidth2D;
    float height = majiangLength2D;
    float zhuapaiEndX = 0;

    for(int i=0;i<majiangs.Count;i++){
      Majiang  majiang = majiangs[i];
      float endX =  myMajiangsLeft+width*i;
      if(majiang==zhuapai){
        if(moveZhuapai){
          majiang.gameObject2D.transform.DOLocalMoveX(endX, 0.5f).SetEase(Ease.Linear) ;
        }
        zhuapaiEndX = endX;
      }else{
        majiang.gameObject2D.transform.DOLocalMoveX(endX, 0.5f).SetEase(Ease.Linear) ;
      }
    }

    return zhuapaiEndX;
  }

  // 返回抓牌的最终位置
  Vector3 sortZhuapais(User user,Majiang zhuapai){
    Vector3 zhuapaiPos = new Vector3(0f,0f,0f);
    for (int i = 0; i < user.majiangs.Count; i++) {
      Majiang majiang = user.majiangs[i];

      Vector3 p = new Vector3(0f,0f,0f);
      int type = 0;
      if(user.id == room.userBottom.id){
        p = new Vector3(bottomPaisComputeStart.x+i*majiangWidth,bottomPaisComputeStart.y,bottomPaisComputeStart.z);
        type = 0;
      }else if(user.id == room.userRight.id){
        p = new Vector3(rightPaisComputeStart.x,rightPaisComputeStart.y,rightPaisComputeStart.z+i*majiangWidth);
        type = 1;
      }else if(user.id == room.userTop.id){
        p = new Vector3(topPaisComputeStart.x-i*majiangWidth,topPaisComputeStart.y,topPaisComputeStart.z);
        type = 2;
      }else if(user.id == room.userLeft.id){
        p = new Vector3(leftPaisComputeStart.x,leftPaisComputeStart.y,leftPaisComputeStart.z-i*majiangWidth);
        type = 3;
      }
      if(zhuapai!=null && majiang.id!=zhuapai.id){
        setZhuapaiPosition(majiang,p,type,0,true);
      }else{
        zhuapaiPos = p;
      }
      
    }

    return zhuapaiPos;
  }



  //抓牌动画
  void zhuapaiAnimate(Majiang majiang,User user){
    Transform transform  = majiang.gameObject.GetComponent<Transform>();
    float time = 0.5f;
    float timeCount = 0f;
    Vector3 rot = new Vector3(0f,0f,0f);
    Vector3 toPosition = new Vector3(0f,0f,0f);
    float space = majiangWidth/2;
    if(user.id==room.userBottom.id){
      // 底部用户抓牌
      rot = bottomZhuapaiRotate;
      toPosition = new Vector3(bottomPaisComputeStart.x+user.majiangs.Count*majiangWidth+space,bottomPaisComputeStart.y,bottomPaisComputeStart.z);
    }else if(user.id==room.userRight.id){
      // 右边用户抓牌
      rot = rightZhuapaiRotate;
      toPosition = new Vector3(rightPaisComputeStart.x,rightPaisComputeStart.y,rightPaisComputeStart.z+user.majiangs.Count*majiangWidth+space);

    }else if(user.id==room.userTop.id){
      // 顶部用户抓牌
      rot = topZhuapaiRotate;
      toPosition = new Vector3(topPaisComputeStart.x-user.majiangs.Count*majiangWidth-space,topPaisComputeStart.y,topPaisComputeStart.z);

    }else if(user.id==room.userLeft.id){
      // 左边用户抓牌
      rot = leftZhuapaiRotate;
      toPosition = new Vector3(leftPaisComputeStart.x,leftPaisComputeStart.y,leftPaisComputeStart.z-user.majiangs.Count*majiangWidth-space);

    }

    transform.DOLocalMove(toPosition, time);
    transform.DOLocalRotate(rot, time);
  }


  //麻将动画
  void animateOneMajiang(Majiang majiang,Vector3 toPosition,Vector3 toRotate,float delay){

    Transform transform  = majiang.gameObject.GetComponent<Transform>();
    float time = 0.5f;
    float totalTimes = time*60f;

    // majiangAnimateStepPostion = new Vector3((toPosition.x-transform.position.x)/totalTimes,(toPosition.y-transform.position.y)/totalTimes,(toPosition.z-transform.position.z)/totalTimes);
    // majiangAnimateStepRotation = new Vector3((toRotate.x-transform.rotation.x)/totalTimes,(toRotate.y-transform.rotation.y)/totalTimes,(toRotate.z-transform.rotation.z)/totalTimes);
    // print(majiangAnimateStepPostion.x+","+majiangAnimateStepPostion.y+","+majiangAnimateStepPostion.z);
    float timeCount = 0f;
    DOTween.To(() => timeCount, a => timeCount = a, 1, delay).OnComplete(() => {
      transform.DOLocalMove(toPosition, time);
      transform.DOLocalRotate(toRotate, time);
    });
    

    // MajiangAnimate majiangAnimate = new MajiangAnimate();
    // majiangAnimate.majiang = majiang;
    // majiangAnimate.toPosition = toPosition;
    // majiangAnimate.toRotate = toRotate;
    // majiangAnimate.stepPostion = majiangAnimateStepPostion;
    // majiangAnimate.stepRotate = majiangAnimateStepRotation;
    // majiangAnimate.time = time;

    // majiangAnimates.Add(majiangAnimate);
  }

  void OnEnable()
    {
        // Agora.io Implimentation
        mRtcEngine = IRtcEngine.GetEngine(agoraAppId); // Get a reference to the Engine
        if (mRtcEngine != null)
        {
            Debug.Log("Leaving Channel");
            leaveChannel();// leave the channel
        }

    }
  
	
	// Update is called once per frame
	void Update () {
    if (mRtcEngine != null) {
      // mRtcEngine.Poll();
    }
		if(animateMajiang!=null && animateTimes<totalAnimateTimes){
      animateTimes++;

      Transform transform  = animateMajiang.gameObject.GetComponent<Transform>();
      print(animateTimes+","+transform.position.x+","+transform.position.y+","+transform.position.z);

      animateMajiang.gameObject.transform.Translate(majiangAnimateStepPostion.x,majiangAnimateStepPostion.y,majiangAnimateStepPostion.z);
      animateMajiang.gameObject.transform.Rotate(majiangAnimateStepRotation);
    }

    //PC();

    // for(int i=0;i<majiangAnimates.Count;i++){
    //   MajiangAnimate majiangAnimate = majiangAnimates[i];
    //   Majiang majiang = majiangAnimate.majiang;
    //   Transform transform  = majiang.gameObject.GetComponent<Transform>();
    //   print(animateTimes+","+transform.position.x+","+transform.position.y+","+transform.position.z);
    //   if(majiangAnimate.doTime>majiangAnimate.time){
    //     majiangAnimates.RemoveAt(i);
    //     i--;
    //     continue;
    //   }
    //   majiangAnimate.doTime = majiangAnimate.doTime+1/60f;
      
    //   majiang.gameObject.transform.Translate(majiangAnimate.stepPostion);
    //   majiang.gameObject.transform.Rotate(majiangAnimate.stepRotate);
    // }
	}
  void rotateLeft()
  {
    // transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y+2, transform.localEulerAngles.z);
    // print("执行了M方法!");
    zhupai(10);
  }


  void newRoom()
  {
    // transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y+2, transform.localEulerAngles.z);
    // print("执行了M方法!");
    socketClient.sendRandom(1,(JsonData res)=>{
      Debug.Log("执行回调");
      int no = (int)res["roomNo"];
      room.no = no;
    });
  }

  
  // 准备出牌
  void readyChuPai(GameObject plane){
    if(plane==null || plane.transform.parent.gameObject==null){
      return;
    }
    playClick();
    GameObject parent = plane.transform.parent.gameObject;
    for(int i=0;i<room.userBottom.majiangs.Count;i++){
      Majiang mj = room.userBottom.majiangs[i];
      Majiang majiang = GetMajiang(mj.id);
      if(majiang.gameObject==parent){
        Transform transform = majiang.gameObject.GetComponent<Transform>();
        if(readyChuMajiang==majiang){
          if(room.nowUserid==AppUtil.user.id){
            chupai(majiang);
            readyChuMajiang = null;
          }else{
            resetChuPai(readyChuMajiang);
          }
        }else{
          resetChuPai(readyChuMajiang);
          transform.DOLocalMove(new Vector3(transform.position.x,transform.position.y+majiangLength/2,transform.position.z), 0.3f);
          readyChuMajiang = majiang;
        }
        
        break;
      }
    }
  }

  //出牌  
  void chupai(Majiang majiang){
    if(room.userBottom.status==0 && (room.userBottom.gangOptions==null || room.userBottom.gangOptions.Count==0)  && room.userBottom.id==room.nowUserid && room.status!=3){
      //只有0的状态才可以出牌
      socketClient.sendChupai(AppUtil.roomNo,majiang.id,(JsonData res)=>{

      });
    }
  }

  //出牌  
  void setChupaiPosition(Majiang majiang,string userId,bool animate,int index){
    Transform transform = majiang.gameObject.GetComponent<Transform>();
    Vector3 pos = new Vector3();
    Vector3 rot = new Vector3(180,90,0);
    int perRowCount = 6;
    if(room.userBottom.id == userId){
      pos =new Vector3(bottomChuPaisComputeStart.x+(index%perRowCount)*majiangWidth,bottomChuPaisComputeStart.y,bottomChuPaisComputeStart.z-(index/perRowCount)*majiangLength);
      rot = new Vector3(180,180,0);
    }else if(room.userRight.id == userId){
      pos =new Vector3(rightChuPaisComputeStart.x+(index/perRowCount)*majiangLength,rightChuPaisComputeStart.y,rightChuPaisComputeStart.z+(index%perRowCount)*majiangWidth);
      rot = new Vector3(180,270,0);
    }else if(room.userTop.id == userId){
      pos =new Vector3(topChuPaisComputeStart.x-(index%perRowCount)*majiangWidth,topChuPaisComputeStart.y,topChuPaisComputeStart.z+(index/perRowCount)*majiangLength);
      rot = new Vector3(180,0,0);
    }else if(room.userLeft.id == userId){
      pos =new Vector3(leftChuPaisComputeStart.x-(index/perRowCount)*majiangLength,leftChuPaisComputeStart.y,leftChuPaisComputeStart.z-(index%perRowCount)*majiangWidth);
      rot = new Vector3(180,90,0);
    }
    if(animate){
      transform.DOLocalMove(pos, 0.5f);
      transform.DOLocalRotate(rot, 0.5f);
    }else{
      transform.position = pos;
      transform.localEulerAngles = rot;
    }
  }

  //设置杠牌与出牌的位置
void setPengGangPosition(Majiang majiang,int index, string userId,bool animate){
  Transform transform = majiang.gameObject.GetComponent<Transform>();
  Vector3 pos = new Vector3();
  Vector3 rot = new Vector3(180,90,0);
  int perRowCount = 5;
  if(room.userBottom.id == userId){
    pos =new Vector3(bottomPengPaisComputeStart.x-index*majiangWidth,bottomPengPaisComputeStart.y,bottomPengPaisComputeStart.z);
    rot = new Vector3(180,180,0);
  }else if(room.userRight.id == userId){
    pos =new Vector3(rightPengPaisComputeStart.x,rightPengPaisComputeStart.y,rightPengPaisComputeStart.z-index*majiangWidth);
    rot = new Vector3(180,270,0);
  }else if(room.userTop.id == userId){
    pos =new Vector3(topPengPaisComputeStart.x+index*majiangWidth,topPengPaisComputeStart.y,topPengPaisComputeStart.z);
    rot = new Vector3(180,0,0);
  }else if(room.userLeft.id == userId){
    pos =new Vector3(leftPengPaisComputeStart.x,leftPengPaisComputeStart.y,leftPengPaisComputeStart.z+index*majiangWidth);
    rot = new Vector3(180,90,0);
  }
  if(animate){
    transform.DOLocalMove(pos, 0.5f);
    transform.DOLocalRotate(rot, 0.5f);
  }else{
    transform.position = pos;
    transform.localEulerAngles = rot;
  }
}

  void resetChuPai(Majiang majiang){
    if(majiang!=null){
      Transform transform = majiang.gameObject.GetComponent<Transform>();
      transform.DOLocalMove(new Vector3(transform.position.x,bottomPaisComputeStart.y,transform.position.z), 0.3f);
    }
  }

  void PC()
  {
      if (Input.GetMouseButtonDown(0))    // 如果鼠标左键按下
      {
          RaycastHit hitInfo;
          // 射线从鼠标点击处射出
          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          Transform targetTransform = null;
          // 在maxDistance长度之内，射线ray与第一个对象（无论层级）进行的物理碰撞的信息，存储在hitInfo中；如果有碰撞物体，返回true, 反之false;
          if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, -1))
          {
            Debug.DrawLine(ray.origin,hitInfo.point);
            GameObject gameObj = hitInfo.collider.gameObject;
            
            Debug.Log("click object name is " + gameObj.name);
              // 获取碰撞到的物体信息
              //readyChuPai(gameObj);
              targetTransform = hitInfo.collider.transform;
          }
          if (targetTransform != null)
          {
              // DoSomething();
          }
      }
  }

  // private void Mobile()
  // {
  //   // 单指触控
  //   if(Input.touchCount == 1)
  //   {
  //     // 存储触摸点信息
  //     Touch touch = Input.GetTouch(0);		
  //     // 如果手指触摸屏幕，但并未移动,不做处理
  //     if(touch.phase == TouchPhase.Stationary)
  //       return;
  //     // 判断是否点击在UI上
  //     if(EventSystem.current.IsPointerOverFameObject(touch.fingerID))
  //       return;
      
  //     RaycastHit hitInfo;
  //     Ray ray = Camera.main.ScreenPointToRay(touch.position);
  //     Transform targetTransform = null;
  //     if(Physics.Raycast(ray, out hitInfo, Mathf.Infinity, -1))					
  //     {
  //       targetTransform = hitInfo.collider.transform;
  //     }
  //     if(targetTransform!= null)
  //     {
  //       // DoSomething();
  //     }		
  //   }
  // }
  void rotateRight()
  {
    //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y-2, transform.localEulerAngles.z);
    print("执行了F方法!");
  }
  private void OnGUI()
    {
      // print("执行了方法!");
      //   if (GUI.Button(new Rect(0, 0, 60, 20), "按钮"))
      //   {
      //       Application.Quit();
      //   }
    }
}
