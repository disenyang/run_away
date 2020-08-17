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
  void Awake () {
    home = this;
    mine = new User();
    initUserModel(mine);
    // moveButton.onClick.AddListener(()=>{
    //   playUserAnimator(mine,"A_Walk");
    // });
  }

  void initUserModel(User user){
    float x = 0;
    float y = 0;
    float z = 0;
    float ry = 0;
    float rx = 0;
    x = 0f;
    y = 10f;
    z = 10f;
    ry = 0;
    user.figure = "female_majiang";
    GameObject obj = AppUtil.createUserModel(user,"AnimatorControllers/PlayMajiangController",transform,this,new Vector3(x,y,z),new Vector3(10,10,10),new Vector3(rx,ry,0));
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
