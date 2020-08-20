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
  public Button moveButton;
  public static User mine;
  public static Home home;
  SocketClient socketClient;

  private List<User> roomUsers;

  public GameObject mainPanel;

  public Button pushButton; //推人动作
  public Button crouchButton;//蹲下动作
  Room room;
  void Awake () {
    if(AppUtil.user==null){
      Invoke("DestroyScene",1.5f);
      SceneManager.LoadScene("Login");
      return;
    }
    home = this;
    Debug.Log("pushButton:"+(pushButton==null));
    pushButton.onClick.AddListener(()=>{
       Debug.Log("pushButton:");
      Debug.Log("pushButton:"+mine.id);
      playUserAnimator(mine,"A_Push");
    });

    crouchButton.onClick.AddListener(()=>{
      playUserAnimator(mine,"A_Crouch");
    });

    // moveButton.onClick.AddListener(()=>{
    //   playUserAnimator(mine,"A_Walk");
    // });

    socketClient = SocketClient.socketClient;
    socketClient.monoBehaviour = this;
    socketClient.transform = mainPanel.transform;
    socketClient.onRecieveMessage = onRecieveMessage;

    socketClient.sendGetRoom(AppUtil.roomNo,(JsonData resData)=>{
      JsonData roomData = (JsonData)resData["room"];
      room = Room.toRoom(roomData,new List<Majiang>());
      dealRoom();
      roomUsers = room.users;
      for(int i=0;i<roomUsers.Count;i++){
        User user = roomUsers[i];
        initUserModel(user,i);
        if(AppUtil.user.id==user.id){
          mine = user;
          // transform.LookAt(user.bodyObject.transform.localPosition);
        }
      }
    });


    
  }

  
  public void onRecieveMessage(JsonData data){
    string cmd = (string)data["cmd"];
    JsonData content = data["con"];

    if(cmd=="start"){
      room = Room.toRoom(content,new List<Majiang>());
      dealRoom();
    }else if(cmd=="room"){
      room = Room.toRoom(content,new List<Majiang>());
      dealRoom();
    }else if(cmd=="voice"){
      string userId = (string)content["user"];
      bool voice =  (bool)content["voice"];
      User user =  room.getUser(userId);
      user.voice = voice;
    }else if(cmd=="move"){
      //移动
      JsonData userList = content["users"];
      setUserValues(userList,true);
      string userId = (string)content["user"];
      User user =  room.getUser(userId);
      playUserAnimator(user,"A_Walk");
    }
  }

  
  public void setUserValues(JsonData userList,bool sync){
    if(userList==null){
      return;
    }
   for(int i=0;i<userList.Count;i++){
      JsonData userMap = userList[i];
      string id = (string)userMap["id"];
      User user = room.getUser(id);
      float x =  (float)userMap["x"];
      float y =  (float)userMap["y"];
      float z =  (float)userMap["z"];
      float rx =  (float)userMap["rx"];
      float ry =  (float)userMap["ry"];
      float rz =  (float)userMap["rz"];
      user.x = x;
      user.y = y;
      user.z = z;
      user.rx = rx;
      user.ry = ry;
      user.rz = rz;
      setUserPosition(user,new Vector3(x,y,z),new Vector3(rx,ry,rz));
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
      
    }else if(room.status==1 || room.status==2 || room.status==3){
      
    }else{
      
    }
    


  }


  void initUserModel(User user,int i){
    float x = 0;
    float y = 0;
    float z = 0;
    float ry = 0;
    float rx = 0;
    if(i==0){
      x = 0f;
      y = 36.1f;
      z = 36.8f;
      ry = 0;
    }else if(i==1){
      x = 20f;
      y = 36.1f;
      z = 36.8f;
      ry = 0;
    }else if(i==2){
      x = 30f;
      y = 36.1f;
      z = 36.8f;
      ry = 0;
    }else if(i==3){
      x = -20f;
      y = 36.1f;
      z = 36.8f;
      ry = 0;
    }
    
    
    user.figure = "female_majiang";
    GameObject obj = AppUtil.createUserModel(user,"AnimatorControllers/PlayMajiangController",transform,this,new Vector3(x,y,z),new Vector3(30,30,30),new Vector3(rx,ry,0));
  }
  public void moveUser(User user,Vector3 move){
    Vector3 bodyPosition = user.bodyObject.transform.localPosition;
    user.bodyObject.transform.localPosition = new Vector3(move.x+bodyPosition.x,move.y+bodyPosition.y,move.z+bodyPosition.z);

    if(user.upperObject!=null){
      Vector3 upperObjectPosition = user.upperObject.transform.localPosition;
      user.upperObject.transform.localPosition = new Vector3(move.x+upperObjectPosition.x,move.y+upperObjectPosition.y,move.z+upperObjectPosition.z);
    }

    if(user.trousersObject!=null){
      Vector3 trousersObjectPosition = user.trousersObject.transform.localPosition;
      user.trousersObject.transform.localPosition = new Vector3(move.x+trousersObjectPosition.x,move.y+trousersObjectPosition.y,move.z+trousersObjectPosition.z);
    }

    if(user.shoesObject!=null){
      Vector3 shoesObjectPosition = user.shoesObject.transform.localPosition;
      user.shoesObject.transform.localPosition = new Vector3(move.x+shoesObjectPosition.x,move.y+shoesObjectPosition.y,move.z+shoesObjectPosition.z);
    }

    if(user.hairObject!=null){
      Vector3 hairObjectPosition = user.hairObject.transform.localPosition;
      user.hairObject.transform.localPosition = new Vector3(move.x+hairObjectPosition.x,move.y+hairObjectPosition.y,move.z+hairObjectPosition.z);
    }

    if(user.hatObject!=null){
      Vector3 hatObjectPosition = user.hatObject.transform.localPosition;
      user.hatObject.transform.localPosition = new Vector3(move.x+hatObjectPosition.x,move.y+hatObjectPosition.y,move.z+hatObjectPosition.z);
    }


    // 改变摄像机的位置
    transform.localPosition = new Vector3(move.x+transform.localPosition.x,move.y+transform.localPosition.y,move.z+transform.localPosition.z);


    //移动
    socketClient.sendMove(AppUtil.roomNo,user.bodyObject.transform.localPosition,user.bodyObject.transform.localEulerAngles,(JsonData resData)=>{
      
    });
  }

  //设置用户位置和旋转角度
  public void setUserPosition(User user,Vector3 postion,Vector3 rotate){
    Vector3 bodyPosition = user.bodyObject.transform.localPosition;
    user.bodyObject.transform.localPosition = postion;

    if(user.upperObject!=null){
      Vector3 upperObjectPosition = user.upperObject.transform.localPosition;
      user.upperObject.transform.localPosition = postion;
    }

    if(user.trousersObject!=null){
      Vector3 trousersObjectPosition = user.trousersObject.transform.localPosition;
      user.trousersObject.transform.localPosition = postion;
    }

    if(user.shoesObject!=null){
      Vector3 shoesObjectPosition = user.shoesObject.transform.localPosition;
      user.shoesObject.transform.localPosition = postion;
    }

    if(user.hairObject!=null){
      Vector3 hairObjectPosition = user.hairObject.transform.localPosition;
      user.hairObject.transform.localPosition = postion;
    }

    if(user.hatObject!=null){
      Vector3 hatObjectPosition = user.hatObject.transform.localPosition;
      user.hatObject.transform.localPosition = postion;
    }

    user.bodyObject.transform.localEulerAngles = rotate;

    if(user.upperObject!=null){
      Vector3 upperObjectPosition = user.upperObject.transform.localPosition;
      user.upperObject.transform.localEulerAngles = rotate;
    }

    if(user.trousersObject!=null){
      Vector3 trousersObjectPosition = user.trousersObject.transform.localPosition;
      user.trousersObject.transform.localEulerAngles = rotate;
    }

    if(user.shoesObject!=null){
      Vector3 shoesObjectPosition = user.shoesObject.transform.localPosition;
      user.shoesObject.transform.localEulerAngles = rotate;
    }

    if(user.hairObject!=null){
      Vector3 hairObjectPosition = user.hairObject.transform.localPosition;
      user.hairObject.transform.localEulerAngles = rotate;
    }

    if(user.hatObject!=null){
      Vector3 hatObjectPosition = user.hatObject.transform.localPosition;
      user.hatObject.transform.localEulerAngles = rotate;
    }

  }

  // 旋转用户
  public void rotateUser(User user,float argle){
    Vector3 bodyPosition = user.bodyObject.transform.localPosition;
    user.bodyObject.transform.localEulerAngles = new Vector3(0,argle,0);

    if(user.upperObject!=null){
      Vector3 upperObjectPosition = user.upperObject.transform.localPosition;
      user.upperObject.transform.localEulerAngles = new Vector3(0,argle,0);
    }

    if(user.trousersObject!=null){
      Vector3 trousersObjectPosition = user.trousersObject.transform.localPosition;
      user.trousersObject.transform.localEulerAngles = new Vector3(0,argle,0);
    }

    if(user.shoesObject!=null){
      Vector3 shoesObjectPosition = user.shoesObject.transform.localPosition;
      user.shoesObject.transform.localEulerAngles = new Vector3(0,argle,0);
    }

    if(user.hairObject!=null){
      Vector3 hairObjectPosition = user.hairObject.transform.localPosition;
      user.hairObject.transform.localEulerAngles = new Vector3(0,argle,0);
    }

    if(user.hatObject!=null){
      Vector3 hatObjectPosition = user.hatObject.transform.localPosition;
      user.hatObject.transform.localEulerAngles = new Vector3(0,argle,0);
    }

  }
  
  // 播放麻将出牌动作
  void playUserAnimator(User user,string name){
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
