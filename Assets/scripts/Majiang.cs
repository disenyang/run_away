using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class Majiang {
  public int id;

  //类型 1:条子,2:筒子,3:万,4:东,5:西,6:南,7:北,8:中,9:发,10:白
	public int type;
  //值 
	public int value;
	
	// 0:正常 
	// 1:刚抓
	public int status;
  public double lastLeft=0;
  public double lastTop = 0;

  public double left=0;
  public double top=0;

  // 计算的位置
  public double cleft=0;
  public double ctop=0;

	
	//是否是天应
	public bool tianying;

	public bool tianyingused;

	//杠后抓的麻将
  public bool gang = false;

  public GameObject gameObject;

  public GameObject gameObject2D;

  public Vector3 majiangPostion;

  //麻将头的朝向 0:像下 1:向右 2:向上 3:向左
  public int direction;


  public string getMaterialName(){
    return type<4?(type+"_"+value):(type+"");
  }

  public string getImageName(){
    return type<4?(type+"_"+value):(type+"");
  }


  //转换为房间
  public static Majiang toMajiang(JsonData map){
    Majiang majiang = new Majiang();
    majiang.type = (int)map["type"];
    majiang.id =  (int)map["id"];
    majiang.value =  (int)map["value"];
    majiang.status =  (int)map["status"];
    majiang.tianying =  (bool)map["tianying"];
    majiang.tianyingused =  (bool)map["tianyingused"];
    majiang.gang = map["gang"]==null?false:((bool)map["gang"]);
    
    return majiang;
  }

  public int CompareTo(Majiang majiang) {
		if(tianying){
			return -id-200;
		}
		return (majiang.type-this.type)==0?((this.value-majiang.value)==0?(this.id-majiang.id):(this.value-majiang.value)):(this.type-majiang.type);
	}



}