
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class GoldConfig {
  public int type;
  public int gold;//数量
  public int diamond;//钻石数量
  public static List<GoldConfig> goldConfigs;
  public GoldConfig(int type,int gold,int diamond) {
    this.type = type;
    this.gold = gold;
    this.diamond = diamond;
  }

   //转换为房间列表
  public static List<GoldConfig> toList(JsonData res){
    List<GoldConfig> newList = new List<GoldConfig>();
    for(int i=0;i<res.Count;i++){
      JsonData map = res[i];
      var help = GoldConfig.toGoldConfig(map);
      newList.Add(help);
    }
    return newList;
  }
  //转换为房间
  public static GoldConfig toGoldConfig(JsonData map){
    GoldConfig goldConfig = new GoldConfig((int)map["type"],(int)map["gold"],(int)map["diamond"]);
    return goldConfig;
  }
}