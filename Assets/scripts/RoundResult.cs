using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using LitJson;
using UnityEngine.SceneManagement;


public class RoundResult : MonoBehaviour {
  public GameObject mainPanel;

  public GameObject uploadErrorPanel;
  public Text uploadErrorText;

  public Button colseUploadErrorButton;
  public Button confirmUploadErrorButton;

  public Animator userAnimator1;

  public Animator userAnimator2;

  public Animator userAnimator3;

  public Animator userAnimator4;

  public Text huMethodText;

  public AudioClip clickSound;
	private AudioSource audioSource;

  public GameObject userButtonPrefab;
  public GameObject majiangButtonPrefab;

  public GameObject majiangResultPanel;


  public GameObject userButtonPanel;

  public GameObject selectUserButton;

  public Button nextButton;

  public Button uploadButton;

  public GameObject user1;
  public GameObject user2;
  public GameObject user3;
  public GameObject user4;

  User selectUser;

  private float effect_sound_volume = 0.6f;

	// Use this for initialization
	void Start () {
    if(AppUtil.user==null){
      Invoke("DestroyScene",1.5f);
      SceneManager.LoadScene("Login");
      return;
    }
    audioSource = GetComponent<AudioSource> ();
    selectUser = AppUtil.room.getUser(AppUtil.room.huUserId);
    if(selectUser==null){
      selectUser = AppUtil.room.users[0];
    }else{
     
      
    }
    createUserTabs();
    createMajiangs();
    huMethodText.text = selectUser.desc;

    nextButton.onClick.AddListener(()=>{
      playClick();
      Invoke("DestroyScene",1.5f);
      SceneManager.LoadScene("Pipei");
    });

    uploadButton.onClick.AddListener(()=>{
      playClick();
      uploadErrorPanel.active = true;
    });
    
    colseUploadErrorButton.onClick.AddListener(()=>{
      playClick();
      uploadErrorPanel.active = false;
    });

    confirmUploadErrorButton.onClick.AddListener(()=>{
      playClick();
      JsonData ps = new JsonData();
      ps["no"]=AppUtil.roomNo;
      ps["userid"]=AppUtil.user.id;
      ps["remark"]=uploadErrorText.text;
      if(uploadErrorText.text==""){
        Toast.error("请输入上报信息",mainPanel.transform);
        return;
      }

      WebService.post("/room/uploadRound", ps,mainPanel.transform,(JsonData res)=>{
        Toast.info("上报成功",mainPanel.transform);
        uploadErrorPanel.active = false;
      },(JsonData res)=>{
        Toast.info("上报失败",mainPanel.transform);
      },this);
    });
    uploadErrorPanel.active = false;
    for(int i=0;i<AppUtil.room.users.Count;i++){
      User user = AppUtil.room.users[i];
      initUserModel(user,i);
      // if(u.id==AppUtil.room.huUserId){
      //   playAction(i,"Samba Dancing");
      // }else{
      //   playAction(i,"Stand Up");
      // }
    }
	}

  void IsConnect(){
    SocketClient.socketClient.IsConnect();
  }

  void DestroyScene(){
    Destroy(gameObject);
  }

  void initUserModel(User user,int i){
    float x = 0;
    float y = -12;
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
    GameObject obj = AppUtil.createUserModel(user,"AnimatorControllers/MainPageController",transform,this,new Vector3(x,y,-10),new Vector3(10,10,10),new Vector3(0,180,0));
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

  void playAction(int index,string name){
    if(index==0){
      userAnimator1.Play(name);
    }else if(index==1){
      userAnimator2.Play(name);
    }else if(index==2){
      userAnimator3.Play(name);
    }else if(index==3){
      userAnimator4.Play(name);
    }
  }
	
  void createUserTabs(){
    float width = 400;
    float height = 130;
    float space = 0;
    float left = Screen.width/2-(width*4)/2;
    List<User> users = AppUtil.room.users;
    for(int i=0;i<users.Count;i++){
      User  user = users[i];
      GameObject userButton = Instantiate(userButtonPrefab);    // 对象初始化
      userButton.transform.parent = userButtonPanel.transform;
      userButton.transform.localScale = Vector3.one;
      userButton.transform.localPosition = Vector3.zero;
      
      Text text = userButton.transform.Find("NicknameText").GetComponent<Text>();
      Text goldText = userButton.transform.Find("GoldText").GetComponent<Text>();

      RectTransform transform = userButton.GetComponent<RectTransform>();
      transform.anchoredPosition = new Vector3(left+width*(i)+(i)*space,0,0);

      text.text = user.nickname;
      goldText.text = user.goldDesc;
      if(user==selectUser){
        selectUserButton = userButton;
        userButton.GetComponent<Image>().color = AppUtil.GetColor("#CCC05Cff");
      }else{
        userButton.GetComponent<Image>().color = AppUtil.GetColor("#CCC05C00");
      }
      
      userButton.GetComponent<Button>().onClick.AddListener(()=>{
        playClick();
        destoryMajiangs();
        selectUserButton.GetComponent<Image>().color = AppUtil.GetColor("#CCC05C00");

        selectUserButton = userButton;
        selectUser = user;
        huMethodText.text = user.desc;
        userButton.GetComponent<Image>().color = AppUtil.GetColor("#CCC05Cff");
        createMajiangs();
        // selectRoomType = sceneConfig.type;
        // if(selectRoomTypeButton!=null){
        //   selectRoomTypeButton.image.sprite = null;
        // }
        // roomType.GetComponent<Button>().image.sprite = selectRoomTypeBgSprite;
        // selectRoomTypeButton = roomType.GetComponent<Button>();
      });
    }
  }

  void destoryMajiangs(){
    for(int i=0;i<selectUser.majiangs.Count;i++){
      Majiang  majiang = selectUser.majiangs[i];
      if(majiang.gameObject!=null){
        GameObject.Destroy(majiang.gameObject,0);
      }
    }
    for(int i=0;i<selectUser.pengmajiangs.Count;i++){
      Majiang  majiang = selectUser.pengmajiangs[i];
      if(majiang.gameObject!=null){
        GameObject.Destroy(majiang.gameObject,0);
      }
    }

    for(int i=0;i<selectUser.gangmajiangs.Count;i++){
      Majiang  majiang = selectUser.gangmajiangs[i];
      if(majiang.gameObject!=null){
        GameObject.Destroy(majiang.gameObject,0);
      }
    }
  }

  void createMajiangs(){
    int count = selectUser.majiangs.Count+selectUser.pengmajiangs.Count+selectUser.gangmajiangs.Count;
    float width = 120;
    float height = 170;
    float space = 0;
    float left = Screen.width/2-width*count/2;
    for(int i=0;i<selectUser.majiangs.Count;i++){
      Majiang  majiang = selectUser.majiangs[i];
      float l = left+width*(i)+(i)*space+((i==selectUser.majiangs.Count-1)?50f:0f) ;
      createMajiang(majiang,l);
    }

    for(int i=0;i<selectUser.gangmajiangs.Count;i++){
      Majiang  majiang = selectUser.gangmajiangs[i];
      float l = left+width*(selectUser.majiangs.Count+i)+100;
      createMajiang(majiang,l);
    }

    for(int i=0;i<selectUser.pengmajiangs.Count;i++){
      Majiang  majiang = selectUser.pengmajiangs[i];
      float l = left+width*(selectUser.majiangs.Count+selectUser.gangmajiangs.Count+i)+100;
      createMajiang(majiang,l);
    }
  }

  void createMajiang(Majiang majiang,float left){
    GameObject majiangButton = Instantiate(majiangButtonPrefab);    // 对象初始化
    majiang.gameObject = majiangButton;
    
    majiangButton.transform.parent = majiangResultPanel.transform;
    majiangButton.transform.localScale = Vector3.one;
    majiangButton.transform.localPosition = Vector3.zero;
    Image majiangImage = majiangButton.transform.Find("MajiangImage").GetComponent<Image>();
    Image tianyingImage = majiangButton.transform.Find("TianyingImage").GetComponent<Image>();
    Sprite sprite = Resources.Load("Images/"+majiang.getImageName(), typeof(Sprite)) as Sprite;
    majiangImage.sprite = sprite;
    if(!majiang.tianying){
      tianyingImage.gameObject.active= false;
    }

    RectTransform transform = majiangButton.GetComponent<RectTransform>();
    transform.anchoredPosition = new Vector3(left,10,0);
  }


  void playClick(){
    if(AppUtil.getAudioConfig()){
      audioSource.PlayOneShot (clickSound,effect_sound_volume);
    }
  }
	// Update is called once per frame
	void Update () {
		
	}



}
