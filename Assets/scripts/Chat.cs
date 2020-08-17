
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class Chat{
  public string msg;
  public string voice;
  public Chat(string msg,string voice){
    this.msg = msg;
    this.voice = voice;
  }
  public static Chat toChat(JsonData map){
    Chat chat = new Chat((string)map["msg"], (string)map["voice"]);
    return chat;
  }
}
