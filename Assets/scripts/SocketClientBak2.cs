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
using HiSocket;
using System.Net;
using HiSocket.Tcp;
using HiFramework;
using HiSocket.Example;
public class SocketClientBak2{
    //全局的连接
    public static SocketClient socketClient;
    const int portNo = 2700;
    const int testPortNo = 2701;
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

    private String recieveMessage = "";
    bool connectReturn = true;

    TcpConnection tcp;

    // Use this for initialization
    public void connect()
    {
      if(!inited){
        init();
      } 


      var ip = IPAddress.Parse("139.196.40.161");
      var iep = new IPEndPoint(ip, AppUtil.test?testPortNo:portNo);
      tcp = new TcpConnection(new PackageExample());
      tcp.OnConnecting += OnConnecting;
      tcp.OnConnected += OnConnected;
      tcp.OnReceiveMessage += OnReceive;
      tcp.OnSendMessage += OnSendMessage;

      
      tcp.OnDisconnected += OnDisconnected;
      tcp.Connect(iep);

      // recieveMessage = "";
      // Loom.QueueOnMainThread((param)=>{
      //   Toast.loading("连接服务中...",transform);
      // },null);
      // if(callbacks==null){
      //    callbacks = new List<Callback>();
      // }
      // try{ 
      //   client = new TcpClient();
      //   client.Connect("139.196.40.161",AppUtil.test?testPortNo:portNo);
      //   Debug.Log("client.Connected=================="+client.Connected);
      //   // client.Connect("127.0.0.1", portNo);
      //   // client.Connect("139.196.40.161", testPortNo);
      //   if(client.Connected){
      //     stream = client.GetStream();
      //   // 用Loom的方法调用一个线程
      //     Loom.RunAsync(
      //         () =>
      //     {
      //       t = new Thread(ReceiveMessage);
      //       t.Start();
      //     });
      //     sendNew((res)=>{
      //       Debug.Log("收到新建用户信息");
      //       Toast.hideLoading();

      //       if(AppUtil.roomNo!=0){
      //         //重新获取房间
      //         sendSyncRoom(AppUtil.roomNo,(JsonData data)=>{

      //         });
      //       }
            
      //     });
         
          
      //   }else{
      //     connect();
      //   }
      // }catch (Exception ex){
      //   Debug.Log(ex);
      // } 
    }

    // 发送信息
    void OnSendMessage(byte[] bytes){
      string message = System.Text.Encoding.Default.GetString(bytes);
      Debug.Log("socket监听到发送消息:"+message);
    }

    void OnDisconnected()//当程序关闭的时候调用
    {
      Debug.Log("socket断开连接");
      //重新连接
      //connect();
    }
    void OnConnecting()//连接服务器时
    {
        Debug.Log("connecting...");
    }
 
    void OnConnected()//成功连接到服务器时
    {
      Debug.Log("connect success");
      sendNew((res)=>{
        Debug.Log("收到新建用户信息");
        Toast.hideLoading();
        
        if(AppUtil.roomNo!=0){
          //重新获取房间
          sendSyncRoom(AppUtil.roomNo,(JsonData data)=>{

          });
        }
      });
      
    }
 
    void OnReceive(byte[] bytes)    //接受到服务器的消息时传出数据
    {
        Debug.Log("receive message: " + bytes.Length);
        string message = System.Text.Encoding.Default.GetString(bytes);

//         byte[] data = new byte[1024*100];
//             int length = stream.Read(data, 0, data.Length);
//             String message = Encoding.UTF8.GetString(data, 0, length);

          // recieveMessage = recieveMessage+message;
          // if(message.EndsWith("\r\n")){
            //此次接受结束
            string  msg = recieveMessage+"";
            recieveMessage = "";

            Debug.Log("从服务器受到了:"+msg);
            JsonData map = JsonMapper.ToObject(msg);
            
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
          // }       
    }

    void init(){
      callbacks = new List<Callback>();
      inited = true;
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

    IEnumerator Timer() {
      while (true) {
        yield return new WaitForSeconds(3.0f);
        if(!connectReturn){
          connect();
          connectReturn = true;
        }else{
          connectReturn = false;
          sendConnect((res)=>{
            connectReturn = true;
          });
        }
      }
    }

    public void close(){
      onRecieveMessage = null;
      if (client != null)
      {
        try
        {
          stream.Close();
          client.Close();
        }
        catch (Exception ex)
        {
            
        }
      }
    }

    //客户端接收消息
    void ReceiveMessage() {
        while (true)
        {
          try{
            Debug.Log("等待接受信息");
            if (client.Connected == false)
            {
              Debug.Log("连结断开");
              connect();
              break;
            }
            byte[] data = new byte[1024*100];
            int length = stream.Read(data, 0, data.Length);
            String message = Encoding.UTF8.GetString(data, 0, length);

            recieveMessage = recieveMessage+message;
            if(message.EndsWith("\r\n")){
              //此次接受结束
              string  msg = recieveMessage+"";
              recieveMessage = "";

              Debug.Log("从服务器受到了:"+msg);
              JsonData map = JsonMapper.ToObject(msg);
              
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
            
          }catch(ThreadAbortException ex){

          }catch (System.Exception e){
            Debug.Log(e);
            connect();
          }
       }
    }

  public void sendNew(Action< JsonData> call){
    JsonData json = new JsonData();
    json["nickname"]= AppUtil.user.nickname;
    json["headimg"] = AppUtil.user.headimg;
    SendMessage("new",json,call);
  }

   public void sendConnect(Action< JsonData> call){
    JsonData json = new JsonData();
    SendMessage("connect",json,call);
  }

  

  public void sendVoiceEnabled(bool voice,Action< JsonData> call){
    JsonData json = new JsonData();
    json["voice"]= voice;
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
      string message = json.ToJson()+"\r\n";
      // string message = "{\"cmd\":\"new\"}";
      Debug.Log("发送信息:"+message);
      try
      {
        byte[] data = System.Text.Encoding.Default.GetBytes(message);
        tcp.Send(data);    //发送数据调用的方法  超简单吧
        Debug.Log("发送成功");
      }
      catch (Exception ex)
      {
        Debug.Log("Error:" + ex);
        connect();
      }
    }
    
  void Start () {
  }
  // Update is called once per frame
  void Update () {
    
  }
}

public class SocketCallback{
  public string id;
  public Action<JsonData> call;
}