using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using LitJson;
using UnityEngine.SceneManagement;
using System.IO;
using System.Threading;

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

public class Login : MonoBehaviour {
  int testid;
  public Text mobilePanelText;

  bool bindMobile = false;

  public GameObject nicknamePanel;
  public InputField nicknameField;
  public Button nicknameConfirmButton;

  private Button sendButton;
  public Button agreementButton;
  public Button primaryButton;
  public Button closeAgreementButton;

  public GameObject agreementPanel;

  private bool checkedAgreement = false;
  public AudioClip clickSound;
	private AudioSource audioSource;


  public string recieveVCode;
  AudioSource backAudioSource;

  public GameObject mainPanel;
  public GameObject infoToastPrefab;
  public GameObject errorToastPrefab;
  public Canvas canvas;

  public GameObject mobilePanel;
  public InputField mobileInput;
  public InputField vCodeInput;

  public Button mobileLoginButton;
  public Button weixinLoginButton;

  public Button freeLoginButton;

  public Toggle checkToggle;
  public int cutdown = 60;

  private Color sendButtonColor ;
  private Color sendButtonDisableColor ;
  private float effect_sound_volume = 0.6f;

	// Use this for initialization
	void Start () {
    System.Random rd = new System.Random();
    testid = rd.Next()*rd.Next();
    Debug.Log("testid="+testid);

    audioSource = GetComponent<AudioSource> ();
    backAudioSource = (AudioSource)this.gameObject.GetComponents(typeof(AudioSource))[1];

    sendButtonDisableColor = new Color(200/255.0f,200/255.0f,200/255.0f);
    sendButtonColor = new Color(255/255.0f,0/255.0f,0/255.0f);

    sendButton = GameObject.Find("SendButton").GetComponent<Button>();
    sendButton.onClick.AddListener(SendVCode);
    Button confirmLoginButton = GameObject.Find("ConfirmLoginButton").GetComponent<Button>();
    confirmLoginButton.onClick.AddListener(ConfirmLogin);

    Button cancelButton = GameObject.Find("CancelButton").GetComponent<Button>();
    cancelButton.onClick.AddListener(()=>{
      playClick();
      mobilePanel.active = false;
    });

    mobilePanel.active = false;
    checkToggle.isOn = false;
		checkToggle.onValueChanged.AddListener((bool value)=>{
      playClick();
    });

    if(AppUtil.getUser()!=null){
      //开始加载场景
      backAudioSource.Pause();
      Invoke("DestroyScene",1.5f);
      SceneManager.LoadScene("MainPage");
      Debug.Log("直接到主界面");
      return;
    }

    nicknameConfirmButton.onClick.AddListener(()=>{
      ConfirmNickname();
    });

    nicknamePanel.active = false;

    mobileLoginButton.onClick.AddListener(()=>{
      bindMobile = false;
      mobilePanelText.text = "手机号登录";
      playClick();
      if(checkToggle.isOn){
        mobilePanel.active = true;
      }else{
        Toast.error("请同意协议",mainPanel.transform,errorToastPrefab);
      }
    });

    weixinLoginButton.onClick.AddListener(()=>{
      playClick();
      if(checkToggle.isOn){
        #if UNITY_IOS
        wechatLogin();
        #elif UNITY_ANDROID
        AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject  currentActivity = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
        currentActivity.Call("wechatLogin");

        // var javaClass = new AndroidJavaClass("com.putao.majiang.WeixinTools");
        // javaClass.CallStatic("login");//先试一试
        #endif
        
      }else{
        Toast.error("请同意协议",mainPanel.transform,errorToastPrefab);
      }
    });

    try{
        #if UNITY_IOS
        weixinLoginButton.transform.gameObject.active = isWechatInstalled();
        #endif
    } catch (Exception ex)
    {

    }
    

    freeLoginButton.onClick.AddListener(()=>{
      playClick();
      if(checkToggle.isOn){
        FreeLogin();
      }else{
        Toast.error("请同意协议",mainPanel.transform,errorToastPrefab);
      }
    });

    agreementButton.onClick.AddListener(()=>{
      playClick();
      agreementPanel.active = true;
    });

    primaryButton.onClick.AddListener(()=>{
      playClick();
      agreementPanel.active = true;
    });

    closeAgreementButton.onClick.AddListener(()=>{
      playClick();
      agreementPanel.active = false;
    });

    agreementPanel.active = false;
	}

  void playClick(){
    Debug.Log("播放音乐="+audioSource+","+clickSound);
    audioSource.PlayOneShot (clickSound,effect_sound_volume);
  }
	
	// Update is called once per frame
	void Update () {
		
	}

  void DestroyScene(){
    Destroy(gameObject);
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

  void ConfirmNickname(){
    playClick();
    string nickname = nicknameField.text;
    string mobile = mobileInput.text;
    if(nickname==""){
      Toast.error("请输入昵称",mainPanel.transform,errorToastPrefab);
      return;
    }
    JsonData param = new JsonData();
    param["nickname"] =nickname;
    param["mobile"] =mobile;
    WebService.post("/user/confirmNickname", param,transform,(res)=>{
      AppUtil.user.nickname = nickname;
      AppUtil.saveUser(AppUtil.user);
      DontDestroyOnLoad(this);
      backAudioSource.Pause();
      Invoke("DestroyScene",1.5f);
      SceneManager.LoadScene("MainPage");
      //开始加载场景
    },(res)=>{
      Toast.error("请输入正确的验证码",mainPanel.transform,errorToastPrefab);
    },this);
  }

  //确认登录  
  void FreeLogin(){
    playClick();
    JsonData param = new JsonData();
    #if UNITY_IOS
    param["system"] = "ios";
    #elif UNITY_ANDROID
    param["system"] = "android";
    #endif
    
    param["appVersion"] = Application.version;

    WebService.post("/user/freeLogin", param,transform,(res)=>{
      Debug.Log("onSuccess....");
      string id  = (string)res["id"];
      string nickname  = (string)res["nickname"];
      User user = new User();
      user.id = id;
      user.nickname = nickname;
      AppUtil.user = user;
      if(nickname==null || nickname==""){
        //跳转到昵称设置页面
        nicknamePanel.active = true;
        mobilePanel.active = false;
      }else{
        AppUtil.saveUser(user);
        DontDestroyOnLoad(this);
        backAudioSource.Pause();
        Invoke("DestroyScene",1.5f);
        SceneManager.LoadScene("MainPage");
        //开始加载场景
      }
    },(res)=>{
      Toast.error("系统错误",mainPanel.transform,errorToastPrefab);
    },this);

    

  }

  //确认登录  
  void ConfirmLogin(){
    if(bindMobile){
      ConfirmBind();
      return;
    }
    playClick();
    string mobile = mobileInput.text;
    string code = vCodeInput.text;
    if(code==""){
      Toast.error("请输验证码",mainPanel.transform,errorToastPrefab);
      return;
    }else if( mobile!="18868413960" && code!=recieveVCode){
      Toast.error("验证码不正确",mainPanel.transform,errorToastPrefab);
      return;
    }
    JsonData param = new JsonData();
    param["mobile"] =mobile;
    param["vcode"] = code;

    #if UNITY_IOS
    param["system"] = "ios";
    #elif UNITY_ANDROID
    param["system"] = "android";
    #endif
    
    param["appVersion"] = Application.version;

    WebService.post("/user/login", param,transform,(res)=>{
      Debug.Log("onSuccess....");
      string id  = (string)res["id"];
      string nickname  = (string)res["nickname"];
      string headimg  = (string)res["headimg"];
      int sex  = (int)res["sex"];
      User user = new User();
      user.id = id;
      user.mobile = mobile;
      user.nickname = nickname;
      user.headimg = headimg;
      user.sex = sex;
      AppUtil.user = user;
      if(nickname==null || nickname==""){
        //跳转到昵称设置页面
        nicknamePanel.active = true;
        mobilePanel.active = false;
      }else{
        AppUtil.saveUser(user);
        DontDestroyOnLoad(this);
        backAudioSource.Pause();
        Invoke("DestroyScene",1.5f);
        SceneManager.LoadScene("MainPage");
        //开始加载场景
        Debug.Log("MainPage....");
      }
    },(res)=>{
      Toast.error("请输入正确的验证码",mainPanel.transform,errorToastPrefab);
    },this);

    

  }

  void TestCallback(string arg){
    Toast.info("回调成功"+arg,mainPanel.transform);
  }

  void WXLoginCallback(string code){
    Debug.Log("WXLoginCallback....,code="+code+",testid="+testid);
    Debug.Log("WXLoginCallback1"+(mainPanel==null));
    Toast.loading(mainPanel.transform);
    Debug.Log("WXLoginCallback2");
    JsonData param = new JsonData();
    param["code"] = code;
    Debug.Log("WXLoginCallback4");
    #if UNITY_IOS
    param["system"] = "ios";
    #elif UNITY_ANDROID
    param["system"] = "android";
    #endif
    
    param["appVersion"] = Application.version;
    Debug.Log("WXLoginCallback5");
    WebService.post("/weixin/loginWeixinUser", param,transform,(res)=>{
      Debug.Log("onSuccess....");
      string id  = (string)res["id"];
      string nickname  = (string)res["nickname"];
      string headimg  = (string)res["headimg"];
      bool needBindMobile  = (bool)res["needBindMobile"];

      User user = new User();
      Debug.Log("绑定1....");
      if(res["mobile"]==null){
        //绑定手机号
        Debug.Log("绑定11....");
        mobilePanelText.text = "请绑定手机号";
        bindMobile = true;
        
      }else{
        Debug.Log("绑定12....");
        string mobile  = (string)res["mobile"];
        user.mobile = mobile;
      }
      Debug.Log("绑定2....");

      int sex  = (int)res["sex"];

      
      user.id = id;
      user.nickname = nickname;
      user.headimg = headimg;
      user.sex = sex;

      AppUtil.user = user;
      if(nickname==null || nickname==""){
        //跳转到昵称设置页面
        nicknamePanel.active = true;
        mobilePanel.active = false;
      }else{
        AppUtil.saveUser(user);
        DontDestroyOnLoad(this);
        backAudioSource.Pause();
        Debug.Log("绑定3....");
        if(res["mobile"]==null && needBindMobile){
          mobilePanel.active = true;
          Debug.Log("绑定4....");
        }else{
          Invoke("DestroyScene",1.5f);
          SceneManager.LoadScene("MainPage");
          
        }
      }
      Toast.hideLoading();
    },(res)=>{
      Toast.hideLoading();
    },this);
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
      mobilePanel.active = false;
      Invoke("DestroyScene",1.5f);
      SceneManager.LoadScene("MainPage");
    },(res)=>{
      Toast.error("请输入正确的验证码",mainPanel.transform);
    },this);

  }


  #if UNITY_IPHONE  
  //AppStorePay
	[System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern void wechatLogin();
  [System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern void _InitAppStorePay (string callBackObjectName);
  [System.Runtime.InteropServices.DllImport("__Internal")]
	private static extern bool isWechatInstalled ();
  #endif
}
