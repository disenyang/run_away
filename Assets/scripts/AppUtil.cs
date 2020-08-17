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

public class AppUtil{
  public static int mainPageEnterCount;
  public static User user;
  public static Room room;
  public static int roomNo;
  public static int selectRoomType=1;
  public static bool test = false;
  public static void saveUser(User user){
    JsonData map = new JsonData();
    map["id"] = user.id;
    map["mobile"] = user.mobile;
    map["nickname"] = user.nickname;
    map["gold"] = user.gold;
    map["headimg"] = user.headimg;
    map["diamond"] = user.diamond;
    map["sex"] = user.sex;
    if(user.upper!=null){
      map["upper"] = user.upper;
    }
    if(user.trousers!=null){
      map["trousers"] = user.trousers;
    }
    if(user.shoes!=null){
      map["shoes"] = user.shoes;
    }
    if(user.hair!=null){
      map["hair"] = user.hair;
    }
    if(user.hat!=null){
      map["hat"] = user.hat;
    }
    string str = map.ToJson();
    PlayerPrefs.SetString("user",str);
  }
  public static void saveConfig(bool music,bool audio){
    JsonData map = new JsonData();
    map["music"] = music;
    map["audio"] = audio;
    string str = map.ToJson();
    PlayerPrefs.SetString("config",str);
  }

  public static void deleteUser(){
    PlayerPrefs.DeleteKey("user");
  }

  public static bool  getMusicConfig(){
    string configStr=PlayerPrefs.GetString("config",null);
    Debug.Log("configStr="+configStr);
    if(configStr!=null && configStr!=""){
      JsonData userMap = JsonMapper.ToObject(configStr);
      bool music = (bool)userMap["music"];
      return music;
    }
    return true;
  }

  public static bool  getAudioConfig(){
    string configStr=PlayerPrefs.GetString("config",null);
    Debug.Log("configStr="+configStr);
    if(configStr!=null && configStr!=""){
      JsonData userMap = JsonMapper.ToObject(configStr);
      bool audio = (bool)userMap["audio"];
      return audio;
    }
    return true;
  }
  public static User  getUser(){
    string userStr=PlayerPrefs.GetString("user",null);
    Debug.Log("userStr="+userStr);
    if(userStr!=null && userStr!=""){
      JsonData userMap = JsonMapper.ToObject(userStr);
      user = new User();
      user.id = (string)userMap["id"];
      if(userMap["mobile"]!=null){
        user.mobile =  (string)userMap["mobile"];
      }
      user.nickname =  (string)userMap["nickname"];
      user.headimg =  (string)userMap["headimg"];
      if(userMap["gold"]!=null){
        user.gold =  (int)userMap["gold"];
        user.diamond = (int)userMap["diamond"];
      }
      if(userMap["sex"]!=null){
        user.sex =  (int)userMap["sex"];
      }
      if(AppUtil.hasKey(userMap,"upper") && userMap["upper"]!=null){
        user.upper = (string)userMap["upper"];
      }
      if(AppUtil.hasKey(userMap,"trousers") && userMap["trousers"]!=null){
        user.trousers = (string)userMap["trousers"];
      }
      if(AppUtil.hasKey(userMap,"shoes") && userMap["shoes"]!=null){
        user.shoes = (string)userMap["shoes"];
      }
      if(AppUtil.hasKey(userMap,"hair") && userMap["hair"]!=null){
        user.hair = (string)userMap["hair"];
      }
      if(AppUtil.hasKey(userMap,"hat") && userMap["hat"]!=null){
        user.hat = (string)userMap["hat"];
      }
      return user;
    }
    return null;
  }

  public static Color GetColor(string str){
    Color nowColor ;
    ColorUtility.TryParseHtmlString(str, out nowColor);
    return nowColor;
  }

  //获取豆子显示
  public static string castNumToStr(int gold){
    if(gold>10000){
      return (gold/10000.0f).ToString("f2")+"万";
    }else{
      return gold+"";
    }
  }

  //将image转换成byte[]数据
  // public static byte[] imageToByte(System.Drawing.Image _image)
  // {
  //     MemoryStream ms = new MemoryStream();
  //     _image.Save(ms,System.Drawing.Imaging.ImageFormat.Jpeg);
  //     return  ms.ToArray();
  // }
  // //将byte[]数据转换成image
  // public static Image byteToImage(byte[]  myByte)
  // {
  //     MemoryStream ms = new MemoryStream(myByte);
  //     Image _Image = Image.FromStream(ms);
  //     return _Image;
  // }

  public static Texture2D spriteToTexture2D(Sprite  sprite){
    
    //sprite为图集中的某个子Sprite对象
    var targetTex = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
    var pixels = sprite.texture.GetPixels(
        (int)sprite.textureRect.x,
        (int)sprite.textureRect.y,
        (int)sprite.textureRect.width,
        (int)sprite.textureRect.height);
    targetTex.SetPixels(pixels);
    targetTex.Apply();

    return targetTex;
  }


  //查询战绩
  public static void queryRecords(GameObject panel,Transform transform,GameObject zhanjiRowPanel,MonoBehaviour monoBehaviour,string userid){
    JsonData ps = new JsonData();
    ps["userid"]=userid;
    WebService.post("/majiangrecord/query", ps,transform,(JsonData res)=>{
      JsonData data = res["data"];
      int top =0;
      AppUtil.RemoveAllChildren(panel);
      RectTransform zhanjiListContentTransform = panel.transform.GetComponent<RectTransform>();
      zhanjiListContentTransform.sizeDelta = new Vector2(0, data.Count*130);
      for(int i=0;i<data.Count;i++){
        top = -i*130;
        JsonData d = data[i];
        int gold = (int)d["gold"];
        int score = (int)d["total"];
        int zi = (int)d["zi"];
        int jiao = (int)d["jiao"];
        string winmethod = (string)d["winmethod"];
        string winusernickname = (string)d["winusernickname"];
        string createtime = (string)d["createtime"];

        GameObject prefab = MonoBehaviour.Instantiate(zhanjiRowPanel);    // 对象初始化
        prefab.transform.parent = panel.transform;
        prefab.transform.localScale = Vector3.one;
        prefab.transform.localPosition = new Vector3(0,top,0);
        RectTransform rectTransform = prefab.transform.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(1250f,129f);

        Text text1 = prefab.transform.Find("Text1").GetComponent<Text>();
        Text text2 = prefab.transform.Find("Text2").GetComponent<Text>();
        Text text3 = prefab.transform.Find("Text3").GetComponent<Text>();
        Text text4 = prefab.transform.Find("Text4").GetComponent<Text>();
        Text text5 = prefab.transform.Find("Text5").GetComponent<Text>();
        Text text6 = prefab.transform.Find("Text6").GetComponent<Text>();

        text1.text = (gold>0?"+":"")+gold;
        text2.text = score+"";
        text3.text = zi+"/"+jiao;
        text4.text = winmethod;
        text5.text = winusernickname;
        text6.text = createtime;
      }
     
    },(JsonData res)=>{
      
    },monoBehaviour);
  }

  //移除所有子组件
  public static void RemoveAllChildren(GameObject gameObject) {
    for(int i=0;i<gameObject.transform.childCount;i++){
      Transform t = gameObject.transform.GetChild(i);
      GameObject.Destroy(t.gameObject);
    }
	}

  public static bool hasKey(JsonData data,string key){
    if(((IDictionary)data).Contains(key)){
      return data[key]!=null;
    }
    return false;
  }

  public static void audioControl(AudioSource backAudioSource,Toggle musicToggle,Toggle audioToggle){
    musicToggle.onValueChanged.AddListener((bool value)=>{
      AppUtil.saveConfig(value,audioToggle.isOn);
      if(value){
        backAudioSource.Play();
      }else{
        backAudioSource.Pause();
      }
    });
    audioToggle.onValueChanged.AddListener((bool value)=>{
      AppUtil.saveConfig(audioToggle.isOn,value);
    });
    musicToggle.isOn = AppUtil.getMusicConfig();
    audioToggle.isOn = AppUtil.getAudioConfig();

    if(musicToggle.isOn){
      backAudioSource.Play();
    }else{
      backAudioSource.Pause();
    }
  }

  //上报埋点数据
  public static void uploadData(int type,string remark,Transform transform,MonoBehaviour monoBehaviour){
    JsonData ps = new JsonData();
    ps["user"] = AppUtil.user.id;
    ps["type"] = type;
    ps["remark"] = remark+"[用户昵称:"+AppUtil.user.nickname+"]";
    WebService.post("/uploaddata/save", ps,transform,(res)=>{
    },(res)=>{
    },monoBehaviour);
  }

  // 创建用户模型
  public static GameObject createUserModel(User user,string animatorUrl,Transform transform,MonoBehaviour monoBehaviour,Vector3 localPosition,Vector3 localScale,Vector3 localEulerAngles){ 
    GameObject obj;
    if(user.sex==1){
      obj = Resources.Load("Models/female_majiang", typeof(GameObject)) as GameObject;
    }else{
      obj = Resources.Load("Models/female_majiang", typeof(GameObject)) as GameObject;
    }
    GameObject person = MonoBehaviour.Instantiate(obj);
    person.transform.parent = transform.parent;
    person.transform.localPosition = localPosition;
    person.transform.localEulerAngles = localEulerAngles;
    person.transform.localScale = localScale;
    user.bodyObject = person;
    Animator animator = person.AddComponent<Animator>();
    // animator.controller = animatorController;
    RuntimeAnimatorController anim = (RuntimeAnimatorController)Resources.Load(animatorUrl);//加载资源
    person.GetComponent<Animator>().runtimeAnimatorController = anim;//赋值

    MonoBehaviour.Destroy(user.upperObject);
    MonoBehaviour.Destroy(user.trousersObject);
    if(user.upper!=null && user.upper!=""){
      user.upperObject = createUserOneModel(user.upper,animatorUrl,person,monoBehaviour,localPosition,localScale,localEulerAngles);
      Debug.Log("AppUtil.user.upperObject22=="+(user.upperObject==null));
    }else{
      user.upperObject = createUserOneModel("Models/female/shangyi",animatorUrl,person,monoBehaviour,localPosition,localScale,localEulerAngles);
      Debug.Log("AppUtil.user.upperObject22=="+(user.upperObject==null));
    }

    if(user.trousers!=null && user.trousers!=""){
      user.trousersObject = createUserOneModel(user.trousers,animatorUrl,person,monoBehaviour,localPosition,localScale,localEulerAngles);
    }else{
      user.trousersObject = createUserOneModel("Models/female/kuzi",animatorUrl,person,monoBehaviour,localPosition,localScale,localEulerAngles);
    }
    if(user.shoes!=null && user.shoes!=""){
      user.shoesObject = createUserOneModel(user.shoes,animatorUrl,person,monoBehaviour,localPosition,localScale,localEulerAngles);
    }
    if(user.hair!=null && user.hair!=""){
      user.hairObject = createUserOneModel(user.hair,animatorUrl,person,monoBehaviour,localPosition,localScale,localEulerAngles);
    }
    if(user.hat!=null && user.hat!=""){
      user.hatObject = createUserOneModel(user.hat,animatorUrl,person,monoBehaviour,localPosition,localScale,localEulerAngles);
    }

    return person;
  }
 public static GameObject createUserOneModel(string url,string animatorUrl,GameObject person,MonoBehaviour monoBehaviour,Vector3 localPosition,Vector3 localScale,Vector3 localEulerAngles){ 
    GameObject modelRessource = Resources.Load(url) as GameObject;
    GameObject modelObject = MonoBehaviour.Instantiate(modelRessource);    // 对象初始化
    Animator animator = modelObject.AddComponent<Animator>();
    // animator.controller = animatorController;
    RuntimeAnimatorController anim = (RuntimeAnimatorController)Resources.Load(animatorUrl);//加载资源
    modelObject.GetComponent<Animator>().runtimeAnimatorController = anim;//赋值

    modelObject.transform.parent = person.transform.parent;
    modelObject.transform.localPosition = localPosition;
    modelObject.transform.localEulerAngles = localEulerAngles;
    modelObject.transform.localScale = localScale;
    return modelObject;
  }

    
}