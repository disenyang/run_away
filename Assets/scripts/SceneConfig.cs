
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

class SceneConfig {
  public string name;
  public int type;

  public int times;//倍数量

  //最小豆子
  public int minGold;
  //最大豆子
  public int maxGold;

  public int userNos;
  public SceneConfig(string name, int type,int times, int minGold, int maxGold, int userNos) {
        this.name = name;
        this.type = type;
        this.times = times;
        this.minGold = minGold;
        this.maxGold = maxGold;
        this.userNos = userNos;
    }

   //转换为房间列表
  static List<SceneConfig> toList(JsonData res){
    List<SceneConfig> newList = new List<SceneConfig>();
    for(int i=0;i<res.Count;i++){
      JsonData map = res[i];
      SceneConfig help = SceneConfig.toSceneConfig(map);
      newList.Add(help);
    }
    return newList;
  }
  //转换为房间
  static SceneConfig toSceneConfig(JsonData map){
    string name = (string) map["name"];
    int type = (int)map["type"];

    int times = (int)map["times"];//倍数量

    //最小豆子
    int minGold = (int)map["minGold"];//倍数量
    //最大豆子
    int maxGold = (int)map["maxGold"];//倍数量

    int userNos = (int)map["userNos"];//倍数量
    SceneConfig sceneConfig = new SceneConfig(name,type,times,minGold,maxGold,userNos);
    return sceneConfig;
  }
}