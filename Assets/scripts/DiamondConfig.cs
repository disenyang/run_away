
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class DiamondConfig {
  public string id;//产品id
  public int type;

  public int diamond;//钻石数量
  public int price;//价格

  public static List<DiamondConfig> goldConfigs;
  public DiamondConfig(string id,int type,int diamond,int price) {
    this.id = id;
    this.type = type;
    this.diamond = diamond;
    this.price = price;
  }

   //转换为房间列表
  public static List<DiamondConfig> toList(JsonData res){
    List<DiamondConfig> newList = new List<DiamondConfig>();
    for(int i=0;i<res.Count;i++){
      JsonData map = res[i];
      var help = DiamondConfig.toDiamondConfig(map);
      newList.Add(help);
    }
    return newList;
  }
  //转换为房间
  static DiamondConfig toDiamondConfig(JsonData map){
    DiamondConfig goldConfig = new DiamondConfig((string)map["id"],(int)map["type"],(int)map["diamond"],(int)map["price"]);
    return goldConfig;
  }
}