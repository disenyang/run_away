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
public class ConnectTimer : MonoBehaviour {
  public bool destroyed = false;
  public bool connectReturn = true;
	void Start () {
    Invoke("IsConnect",4);
	}
  public void IsConnect(){
    if(destroyed){
      return;
    }
    try{
      connectReturn = false;
      SocketClient.socketClient.sendConnect((res)=>{
        Debug.Log("返回连接");
        connectReturn = true;
      });
      Debug.Log("判断是否重新连接");
    } catch (Exception ex) {
      Debug.Log(ex);
    }
    Invoke("IsReturn",3);
  }

  public void IsReturn(){
    if(!connectReturn){
      SocketClient.socketClient.connect();
    }
    Invoke("IsConnect",4);
  }


  private void OnDestroy()
  {
    destroyed = true;
    Debug.Log("connecttimmer OnDestroy");
  }
}
