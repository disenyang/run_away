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
using DG.Tweening;

public class MoveRole : MonoBehaviour {
    enum slideVector { nullVector, up, down, left, right };
    private Vector2 touchFirst = Vector2.zero; //手指开始按下的位置
    private Vector2 touchSecond = Vector2.zero; //手指拖动的位置
    private slideVector currentVector = slideVector.nullVector;//当前滑动方向
    private float timer;//时间计数器  
    private float offsetTime = 0.1f;//判断的时间间隔 
    private float SlidingDistance = 80f;
    public GameObject centerImage;
    private bool moving; //正在移动

    // 禁止区域
    private List<Area> forbiddenAreas;

    // 用户角度方向
    private double userDirectionArgle;

    public Transform FollowObject;  //被跟随的物体
    private Vector2 MouseStart, MouseEnd;
    private Vector3 DeltAngle;
    private float CameraDistence = 100;   //相机与其跟随物体保持的距离
    private float delt, deltz;
    private float PI = 3.14159f;
    private Vector3 DeltPosition;
    private bool IsDrag = false;

    private bool downLeft;

    Vector3 cameraRotation;
    Vector3 cameraPosition;

    DateTime lastDragDate;
 

    void Awake () {
        forbiddenAreas = new List<Area>();
        Area area1 = new Area();
        area1.points = new List<Vector3>();
        area1.points.Add(new Vector3(-441,13,349));
        area1.points.Add(new Vector3(194,13,349));
        area1.points.Add(new Vector3(194,13,2058));
        area1.points.Add(new Vector3(-441,13,2058));
        forbiddenAreas.Add(area1);
    }
    void OnGUI()   // 滑动方法02
    {
        if (Event.current.type == EventType.MouseDown) {
            MouseEnd = Input.mousePosition;
            if(Input.mousePosition.x>Screen.width/2){
                downLeft = false;
            }else{
                downLeft = true;
            }
            
        }
        if (Event.current.type == EventType.MouseDrag) {
            IsDrag = true;
        }
        if (Event.current.type == EventType.MouseUp) IsDrag = false;

        if(Input.mousePosition.x>Screen.width/2){
            return;
        }
        if (Event.current.type == EventType.MouseDown)
        //判断当前手指是按下事件 
        {
            touchFirst = Event.current.mousePosition;//记录开始按下的位置
            move();
        }
        if (Event.current.type == EventType.MouseDrag && downLeft)
        //判断当前手指是拖动事件
        {
            touchSecond = Event.current.mousePosition;

            timer += Time.deltaTime;  //计时器
            moving = true;

            if (timer > offsetTime)
            {
                touchSecond = Event.current.mousePosition; //记录结束下的位置
                Vector2 slideDirection = touchFirst - touchSecond;
                float x = slideDirection.x;
                float y = slideDirection.y;
                Debug.Log("drag");
                Vector3 p = centerImage.transform.GetComponent<RectTransform>().anchoredPosition;
                float ny = p.y + y;
                float nx = p.x - x;
                if(ny>0){
                    ny = 0f;
                }else if(ny<-270){
                    ny = -270;
                }
                if(nx<0){
                    nx = 0f;
                }else if(nx>270){
                    nx = 270;
                }
                    //向前
                
                double tan = Math.Abs((ny+150)/(nx-(150)));
                double arg = (float)Math.Atan(tan);
                if(nx>150 && ny>-150){
                    arg = (float)((Math.PI/2-arg)*180.0/Math.PI);
                }else if(nx<150 && ny>-150){
                    arg = (float)((arg-Math.PI/2)*180.0/Math.PI);
                }else if(nx<150 && ny<-150){
                    arg = (float)((-Math.PI/2-arg)*180.0/Math.PI);
                }else if(nx>150 && ny<-150){
                    arg = (float)((Math.PI/2+arg)*180.0/Math.PI);
                }

                userDirectionArgle = arg;
                 Home.home.rotateUser(Home.mine,(float)(arg));
                    playUserAnimator(Home.mine,"A_Run");
                
                centerImage.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(nx,ny,0);
                touchFirst = touchSecond;
                if (y + SlidingDistance < x && y > -x - SlidingDistance)
                {

                    if (currentVector == slideVector.left)
                    {
                        return;
                    }

                    Debug.Log("right");

                    currentVector = slideVector.left;
                }
                else if (y > x + SlidingDistance && y < -x - SlidingDistance)
                {
                    if (currentVector == slideVector.right)
                    {
                        return;
                    }

                    Debug.Log("left");

                    currentVector = slideVector.right;
                }
                else if (y > x + SlidingDistance && y - SlidingDistance > -x)
                {
                    if (currentVector == slideVector.up)
                    {
                        return;
                    }

                    Debug.Log("up");

                    currentVector = slideVector.up;
                }
                else if (y + SlidingDistance < x && y < -x - SlidingDistance)
                {
                    if (currentVector == slideVector.down)
                    {
                        return;
                    }

                    Debug.Log("Down");

                    currentVector = slideVector.down;
                }

                timer = 0;
                touchFirst = touchSecond;

            }
           
        }   // 滑动方法

         if (Event.current.type == EventType.MouseUp)
            {//滑动结束  
                currentVector = slideVector.nullVector;
                centerImage.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(150,-150,0);
                playUserAnimator(Home.mine,"A_Idle");
                moving = false;
            }
    }

    void move(){
        Invoke("move",0.03f);
        if(moving){
            float moveX = (float)Math.Sin(userDirectionArgle*Math.PI/180.0)*0.7f;
            float moveZ = (float)Math.Cos(userDirectionArgle*Math.PI/180.0)*0.7f;
            Debug.Log("userDirectionArgle="+userDirectionArgle+",moveX="+moveX+",moveZ="+moveZ);
            Home.home.moveUser(Home.mine,new Vector3(moveX,0,moveZ));
            playUserAnimator(Home.mine,"A_Run");
        }
        
    }


    void Start() {
      cameraRotation = transform.localEulerAngles;
      cameraPosition = transform.localPosition;
      DeltAngle = cameraRotation;
    }
    
    void moveInit(){
      transform.DOLocalMove(cameraPosition,2f);
      transform.DOLocalRotate(cameraRotation,2f);
    }

    void recoverCamera(){
      DateTime dNow = DateTime.Now;
      long seconds = dNow.ToFileTimeUtc();
      long lastSeconds = lastDragDate.ToFileTimeUtc();
      TimeSpan st = dNow-lastDragDate;
      if(Convert.ToInt64(st.TotalMilliseconds)>=1800){
        transform.DOLocalMove(cameraPosition,0.2f);
        transform.DOLocalRotate(cameraRotation,0.2f);
        DeltAngle = gameObject.transform.localEulerAngles;
      }
    } 
 
    void LateUpdate() {
        if (IsDrag && !downLeft ) {
          MouseStart = MouseEnd;
          MouseEnd = Input.mousePosition;
          DeltAngle.x -= (MouseEnd.y - MouseStart.y) / 7;
          DeltAngle.y += (MouseEnd.x - MouseStart.x) / 7;
            gameObject.transform.localEulerAngles = DeltAngle;
            delt = gameObject.transform.localEulerAngles.y * PI / 180;
            deltz = gameObject.transform.localEulerAngles.x * PI / 180;
            lastDragDate = DateTime.Now;
          //gameObject.transform.position = FollowObject.position + new Vector3(-CameraDistence * Mathf.Sin(delt) * Mathf.Cos(deltz), CameraDistence * Mathf.Sin(deltz), -CameraDistence * Mathf.Cos(delt) * Mathf.Cos(deltz));
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
}
