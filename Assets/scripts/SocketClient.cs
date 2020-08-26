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
public class SocketClient{
    //全局的连接
    public static SocketClient socketClient;
    private int msgId=1;
    private TcpClient client;
    private  byte[] data;
    public Transform transform;
    bool inited;

    private NetworkStream stream;
    public MonoBehaviour monoBehaviour;
    private Thread t;

    private List<Callback> callbacks;

    public Action<JsonData> onRecieveMessage; 

    public bool connectReturn = true;
    WebSocketSharp.WebSocket webSocket;

    // Use this for initialization
    public void connect()
    {
      Loom.RunAsync(() =>{
        try {
          if(!inited){
            init();
          }
          try {
            if (webSocket != null)
            {
              webSocket.Close();
            }
          } catch (Exception ex) {
            Debug.Log(ex);
          }
          var url = "ws://139.196.40.161:"+(AppUtil.test?8082:8081)+"/run_away/websocket/majiang";
          Debug.Log("连接服务器:"+url);
          webSocket = new WebSocketSharp.WebSocket(url);
          var result = "";
          webSocket.Connect();
          webSocket.OnMessage += (sender, e) =>{
            Debug.Log("收到信息:"+e.Data);
            result = e.Data;
            ReceiveMessage(result);
            
          };
          webSocket.OnOpen += (sender, e) => {
              Debug.Log("连接建立+"+e);
          };
          webSocket.OnError += (sender, e) => {
            Debug.Log("连接错误+"+e);
          };

          

          sendNew((res)=>{
            Debug.Log("收到新建用户信息");
            Toast.hideLoading();

            if(AppUtil.roomNo!=0){
              //重新获取房间
              sendSyncRoom(AppUtil.roomNo,(JsonData data)=>{

              });
            } 
          });

          webSocket.OnClose += (sender, e) => {
            Debug.Log("连接断开+"+e);
            Loom.QueueOnMainThread((param)=>{
              Toast.loading("连接服务中...",transform);
              connect();
            },null);
          };


        } catch (Exception ex) {
          Debug.Log(ex);
        }
        Loom.QueueOnMainThread((param)=>{
          Toast.loading("连接服务中...",transform);
        },null);

      });
    }

    void init(){
      
      inited = true;
      callbacks = new List<Callback>();
      
      
      //每隔3秒发送一个连接状态请求
      // Loom.QueueOnMainThread((param)=>{
      //   try
      //   {
      //     monoBehaviour.StartCoroutine(Timer());
      //   }
      //   catch (Exception ex)
      //   {
      //     Debug.Log(ex);
      //   }
      // },null);
    }
    public void IsConnect(){
      
    }
    

    public void close(){
      onRecieveMessage = null;
      if (webSocket != null)
      {
        webSocket.Close();
      }
    }

    //客户端接收消息
    void ReceiveMessage(string message) {
      Debug.Log("从服务器受到了:"+message);
      JsonData map = JsonMapper.ToObject(message);
      string cmd = (string)map["cmd"];
      
      if(cmd=="ret"){
        JsonData content = (JsonData)map["con"];
        string sid = (string)content["sid"];
        Callback callback = GetCallback(sid);
        Debug.Log("sid:"+sid);
        if(sid!=null){ 
          Loom.QueueOnMainThread((param)=>{
            try
            {
              callback.call(content);
            }
            catch (Exception ex)
            {
              Debug.Log(ex);
            }
          },null);
        }
      }else{
        if(onRecieveMessage!=null){
          Loom.QueueOnMainThread((param)=>{
            Debug.Log("onRecieveMessage=======================,map"+(onRecieveMessage==null)+",map=="+(map==null));
            try
            {
              onRecieveMessage(map);
            }
            catch (Exception ex)
            {
              Debug.Log(ex);
            }
          },null);
        }
      }
    }
  public void sendNew(Action< JsonData> call){
    JsonData json = new JsonData();
    json["nickname"]= AppUtil.user.nickname;
    json["headimg"] = AppUtil.user.headimg;
    SendMessage("new",json,call);
  }

  public void sendHasRoom(int inTimes,Action< JsonData> call){
    JsonData json = new JsonData();
    json["nickname"]= AppUtil.user.nickname;
    json["headimg"] = AppUtil.user.headimg;
    json["inTimes"] = inTimes;
    SendMessage("hasRoom",json,call);
  }

  public void sendSkill(String skill,Action< JsonData> call){
    JsonData json = new JsonData();
    json["roomNo"]= AppUtil.roomNo;
    json["user"] = AppUtil.user.id;
    json["skill"] = skill;
    SendMessage("skill",json,call);
  }

  

  

   public void sendConnect(Action< JsonData> call){
    JsonData json = new JsonData();
    json["t"]= "";
    SendMessage("connect",json,call);
  }

  

  public void sendVoiceEnabled(bool voice,Action< JsonData> call){
    JsonData json = new JsonData();
    json["voice"]= voice;
    json["roomNo"]= AppUtil.roomNo;
    SendMessage("voice",json,call);
  }


  //新建房间
  public void sendNewRoom(int timesType,Action< JsonData> call){
    JsonData json = new JsonData();
    json["timesType"]= timesType;
    SendMessage("newRoom",json,call);
  }

  public void sendInRoom(int no,Action< JsonData> call){
    JsonData json = new JsonData();
    json["roomNo"]= no;
    SendMessage("in",json,call);
  }

   public void sendChat(Chat chat, Action< JsonData> call){
    JsonData json = new JsonData();
    json["roomNo"]= AppUtil.roomNo;
    json["msg"]= chat.msg;
    json["voice"]= chat.voice;
    json["user"]= AppUtil.user.id;
    SendMessage("chat",json,call);
  }

  //新建房间
  public void sendInviteFriend(string friendId,int roomNo,Action< JsonData> call){
    JsonData json = new JsonData();
    json["friendId"]= friendId;
    json["roomNo"]= roomNo;
    SendMessage("invite",json,call);
  }

  //随机
  public void sendRandom(int timesType,Action<JsonData> call){
    JsonData json = new JsonData();
    json["timesType"]= timesType;
    json["test"]= true;
    SendMessage("random",json,call);
  }
  
  //准备
  
  public void sendReady(int roomNo,Action<JsonData> call){
    JsonData json = new JsonData();
    json["roomNo"]= roomNo;
    SendMessage("ready",json,call);    
  }

  public void sendExitRoom(int roomNo,Action<JsonData> call){
    JsonData json = new JsonData();
    json["roomNo"]= roomNo;
    SendMessage("exit",json,call);    
  }

  //获取房间
  public void sendGetRoom(int roomNo,Action<JsonData> call){
    JsonData json = new JsonData();
    json["roomNo"]= roomNo;
    SendMessage("getRoom",json,call);    
  }
  public void sendMove(int roomNo,Vector3 position,Vector3 rotate,Action<JsonData> call){
    JsonData json = new JsonData();
    json["roomNo"]= roomNo;
    json["userid"]= AppUtil.user.id;
    json["x"]= position.x;
    json["y"]= position.y;
    json["z"]= position.z;
    json["rx"]= rotate.x;
    json["ry"]= rotate.y;
    json["rz"]= rotate.z;
    SendMessage("move",json,call);    
  }

  


  //同步房间
  public void sendSyncRoom(int roomNo,Action<JsonData> call){
    JsonData json = new JsonData();
    json["roomNo"]= roomNo;
    SendMessage("syncRoom",json,call);    
  }

  //同步麻将
  public void sendSyncMajiangs(int roomNo,string userid,Action<JsonData> call){
    JsonData json = new JsonData();
    json["roomNo"]= roomNo;
    json["userid"]= userid;
    
    SendMessage("syncMajiangs",json,call);
  }

  //骰子
  public void sendShaizi(int roomNo,Action<JsonData> call){
    JsonData json = new JsonData();
    json["roomNo"]= roomNo;
    SendMessage("shaizi",json,call);
  }

  //出牌房间
  public void sendChupai(int roomNo,int id,Action<JsonData> call){
    JsonData json = new JsonData();
    json["roomNo"]= roomNo;
    json["id"]= id;
    SendMessage("chupai",json,call);
  }

  //出牌房间
  public void sendPeng(int roomNo,Action<JsonData> call){
    JsonData json = new JsonData();
    json["roomNo"]= roomNo;
    SendMessage("peng",json,call);
  }

  public void sendGang(int roomNo,Action<JsonData> call){
    JsonData json = new JsonData();
    json["roomNo"]= roomNo;
    SendMessage("gang",json,call);
  }
  public void sendSelfGang(int roomNo,int type,int value,Action<JsonData> call){
    JsonData json = new JsonData();
    json["roomNo"]= roomNo;
    json["type"]= type;
    json["value"]= value;
    SendMessage("selfGang",json,call);
  }

  public void sendGuo(int roomNo,Action<JsonData> call){
    JsonData json = new JsonData();
    json["roomNo"]= roomNo;
    SendMessage("guo",json,call);
  }

  public void sendHu(int roomNo,Action<JsonData> call){
    JsonData json = new JsonData();
    json["roomNo"]= roomNo;
    SendMessage("hu",json,call);
  }

  public Callback GetCallback(string id){
    for(int i=0;i<callbacks.Count;i++){
      Callback callback = callbacks[i];
      if(callback.id==id){
        return callback;
      }
    }
    return null;
  }

    /// <summary>
    /// 向服务器发送数据（发送聊天信息）
    /// </summary>
    /// <param name="message"></param>
    public void SendMessage(string cmd,JsonData content,Action<JsonData> callback)
    {
      JsonData json = new JsonData();
      json["cmd"] = cmd;
      json["from"] = AppUtil.user.id;
      json["content"] = content;
      if(callback!=null){
        Callback cb = new Callback();
        cb.id = (msgId++)+"";
        cb.call = callback;
        json["id"] = cb.id;
        callbacks.Add(cb);
      }
      string message = json.ToJson()+"\t\n";
      Debug.Log("发送信息:"+message);

      webSocket.Send(message);


      
      // try
      // {
      //   if (client.Connected == false){
      //     connect();
      //   }else{
      //     byte[] data = Encoding.UTF8.GetBytes(message);
      //     stream.Write(data, 0, data.Length);
      //     stream.Flush();
      //     Debug.Log("发送成功");
      //   } 
      // }
      // catch (Exception ex)
      // {
      //     Debug.Log("Error:" + ex);
      //     connect();
      // }
    }
    
  void Start () {
  }
  // Update is called once per frame
  void Update () {
    
  }
}

