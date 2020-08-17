using UnityEngine;
using System;
using DG.Tweening;

public class CameraCol : MonoBehaviour {
    public Transform FollowObject;  //被跟随的物体
    private Vector2 MouseStart, MouseEnd;
    private Vector3 DeltAngle;
    private float CameraDistence = 100;   //相机与其跟随物体保持的距离
    private float delt, deltz;
    private float PI = 3.14159f;
    private Vector3 DeltPosition;
    private bool IsDrag = false;

    Vector3 cameraRotation;
    Vector3 cameraPosition;

    DateTime lastDragDate;
 
    void Start() {
      cameraRotation = transform.localEulerAngles;
      cameraPosition = transform.position;
      DeltAngle = gameObject.transform.localEulerAngles;
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
        if (IsDrag) {
            MouseStart = MouseEnd;
            MouseEnd = Input.mousePosition;
            DeltAngle.x -= (MouseEnd.y - MouseStart.y) / 7;
            DeltAngle.y += (MouseEnd.x - MouseStart.x) / 7;
            gameObject.transform.localEulerAngles = DeltAngle;
            delt = gameObject.transform.localEulerAngles.y * PI / 180;
            deltz = gameObject.transform.localEulerAngles.x * PI / 180;
            lastDragDate = DateTime.Now;
            Invoke("recoverCamera",2);
            //gameObject.transform.position = FollowObject.position + new Vector3(-CameraDistence * Mathf.Sin(delt) * Mathf.Cos(deltz), CameraDistence * Mathf.Sin(deltz), -CameraDistence * Mathf.Cos(delt) * Mathf.Cos(deltz));
        }
    }
    void OnGUI() {
      // if(gameObject.hoverObject==null){
        if (Event.current.type == EventType.MouseDown) MouseEnd = Input.mousePosition;
        if (Event.current.type == EventType.MouseDrag) IsDrag = true;
        if (Event.current.type == EventType.MouseUp) IsDrag = false;
      // }
        
    }
}
