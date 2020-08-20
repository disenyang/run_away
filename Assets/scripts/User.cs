
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class User{
  //主键
	public string id;
	//手机号
	public string mobile;
	//昵称
	public string nickname;
  public bool voice=false;
  public bool online=false;
	//登录时间
	public string logintime;
	//创建时间
	public string createtime;
	//头像
	public string headimg="http://u.xiaowanwu.cn/DEC16B87-5206-40E0-BB7D-D02C11A84553";
	//性别
	public int sex;
	//设备token
	public string deviceToken;
	//手机系统
	public string clientostype;
	//验证码
	public string vcode;
  //缺一门
  public int que;

  //距离
  public int distance;

  public string figure ="laoshu";
  public string skin;

  // 手中的麻将
	public List<Majiang> majiangs;

  public List<Majiang> gangOptions;

	static int perscore = 20;

	// 0：真人 1:机器
	public int type;

	//游戏状态 0:在线 1:在打游戏 2:随机匹配等待中
	public int gameStatus;

	//选择的模式 1:随机模式 2:比赛模式
	public int selectMode;

	//选择的麻将类型 1:旌德中心五 2:湖南麻将 3:上海垃圾胡
	public int selectType;


  // //得分记录
  // public List<Map> scoreTotals;
	public bool connected;

  //剩余的子
	public int zi=20;
	//角
	public int jiao;

  public string goldDesc;

	//当前房间的积分
	public int score;
	//金币
	public int gold;
	//钻石
	public int diamond;
	// 状态 0:出牌中 1:碰 2:杠 3:胡 -1:准备 -2:未准备
	public int status=-2;

  public string desc;

  public List<Majiang> chumajiangs;

  public Majiang zhuapai;
  public List<Majiang> pengmajiangs;
  public List<Majiang> gangmajiangs;

  public GameObject bodyObject;
  //上衣
  public string upper;
  public GameObject upperObject;

  //裤子
  public string trousers;
  public GameObject trousersObject;


  //鞋子
  public string shoes;
  public GameObject shoesObject;


  //头发
  public string hair;
  public GameObject hairObject;

  //帽子
  public string hat;
  public GameObject hatObject;

  //坐标位置
  public float x;
  public float y;
  public float z;

  //旋转位置
  public float rx;
  public float ry;
  public float rz;

  static Majiang GetMajiang(int id,List<Majiang> allmajiangs){
    for(int i=0;i<allmajiangs.Count;i++){
			Majiang majiang = allmajiangs[i];
			if(majiang.id==id){
        return majiang;
      }
		}
    return null;
  }

  public static User toUserOnly(JsonData map){
    User user = new User();
    user.id = (string)map["id"];
    if(map["nickname"]!=null){
      user.nickname = (string)map["nickname"];
    }

    if(map["headimg"]!=null){
      user.headimg = (string)map["headimg"];
    }
    
    user.gold =  (int)map["gold"];
    if(map["diamond"]!=null){
      user.diamond = (int)map["diamond"];
    }
    user.online = (bool)map["online"];
    if(map["mobile"]!=null){
      user.mobile = (string)map["mobile"];
    }

    if(map["upper"]!=null){
      user.upper = (string)map["upper"];
    }

    if(map["trousers"]!=null){
      user.trousers = (string)map["trousers"];
    }

    if(map["shoes"]!=null){
      user.shoes = (string)map["shoes"];
    }

    if(map["hair"]!=null){
      user.hair = (string)map["hair"];
    }

    if(map["hat"]!=null){
      user.hat = (string)map["hat"];
    }

    
    return user;
  }

  //转换为房间
  public static User toUser(JsonData map,List<Majiang> allmajiangs){
    User user = new User();
    
    if(map["id"]!=null){
      user.id = (string)map["id"];
    }
    if(map["figure"]!=null){
      user.figure = (string)map["figure"];
    }
    if(map["skin"]!=null){
      user.skin = (string)map["skin"];
    }
    if(map["nickname"]!=null){
      user.nickname = (string)map["nickname"];
    }
    if(map["headimg"]!=null){
      user.headimg = (string)map["headimg"];
    }

    if(map["goldDesc"]!=null){
      user.goldDesc = (string)map["goldDesc"];
    }

    user.status = (int)map["status"];
    user.zi = (int)map["zi"];
    user.jiao = (int)map["jiao"];
    user.score = (int)map["score"];
    if(map["desc"]!=null){
      user.desc = (string)map["desc"];
    }
    // 灯光 FFF4D6FF
    user.voice = (bool)map["voice"];
    user.distance = (int)map["distance"];
    user.gold = (int)map["gold"];


    if(map["upper"]!=null){
      user.upper = (string)map["upper"];
    }

    if(map["trousers"]!=null){
      user.trousers = (string)map["trousers"];
    }

    if(map["shoes"]!=null){
      user.shoes = (string)map["shoes"];
    }

    if(map["hair"]!=null){
      user.hair = (string)map["hair"];
    }

    if(map["hat"]!=null){
      user.hat = (string)map["hat"];
    }

    if(map["x"]!=null){
      user.x = (int)map["x"];
    }

    if(map["y"]!=null){
      user.y = (int)map["y"];
    }

    if(map["z"]!=null){
      user.z = (int)map["z"];
    }

    if(map["rx"]!=null){
      user.rx = (int)map["rx"];
    }

    if(map["ry"]!=null){
      user.ry = (int)map["ry"];
    }

    if(map["rz"]!=null){
      user.rz = (int)map["rz"];
    }
    
    if(map["gangOptions"]!=null){
      JsonData gangOptions = map["gangOptions"];
      List<Majiang> gangOptionList = new List<Majiang>();;
      if(gangOptions!=null){
        for(int i=0;i<gangOptions.Count;i++){
          JsonData majiangMap = gangOptions[i];
          gangOptionList.Add(Majiang.toMajiang(majiangMap));
        }
        user.gangOptions = gangOptionList;
      }
    }
    

    // user.gangOptions = new List<Majiang>();
    // for(int i=0;i<3;i++){
    //   Majiang majiang = new Majiang();
    //   majiang.type=1;
    //   majiang.value=3;
    //   majiang.id = 100;
    //   user.gangOptions.Add(majiang);
    // }
    
    

    

    
    if(map["majiangs"]!=null && allmajiangs!=null  && allmajiangs.Count>0){
      JsonData majiangList = map["majiangs"];
      List<Majiang> majiangs = new List<Majiang>();;
      if(majiangList!=null){
        for(int i=0;i<majiangList.Count;i++){
          JsonData majiangMap = majiangList[i];
          Majiang majiang = Majiang.toMajiang(majiangMap);
          majiangs.Add(GetMajiang(majiang.id,allmajiangs));
        }
        user.majiangs = majiangs;
      }
    }

    if(map["chumajiangs"]!=null && allmajiangs!=null  && allmajiangs.Count>0){
      JsonData chumajiangList = map["chumajiangs"];
      if(chumajiangList!=null){
        List<Majiang> chumajiangs = new List<Majiang>();
        for(int i=0;i<chumajiangList.Count;i++){
          JsonData majiangMap = chumajiangList[i];
          if(majiangMap!=null){
            Majiang majiang = Majiang.toMajiang(majiangMap);
            chumajiangs.Add(GetMajiang(majiang.id,allmajiangs));
          }
        }
        user.chumajiangs = chumajiangs;
      }
    } 
    // if(map["pengmajiangs"]!=null){
    //   JsonData pengmajiangList = map["pengmajiangs"];
    //   if(pengmajiangList!=null){
    //     List<Majiang> pengmajiangs = new List<Majiang>();
    //     for(int i=0;i<pengmajiangList.Count;i++){
    //       JsonData majiangsMap = pengmajiangList[i];
    //       for(int j=0;j<majiangsMap.Count;j++){
    //         JsonData majiangMap = majiangsMap[j];
    //         pengmajiangs.Add(Majiang.toMajiang(majiangMap));
    //       }
    //     }
    //     user.pengmajiangs = pengmajiangs;
    //   }
    // }
    // if(pengmajiangList == null || pengmajiangList.length==0){
    //   //测试
    //   user.pengmajiangs = [];
    //   for(int i=0;i<3;i++){
    //     Majiang majiang = new Majiang();
    //     majiang.type=1;
    //     majiang.value=3;
    //     user.pengmajiangs.add(majiang);
    //   }
    // }

    // if(map["gangmajiangs"]!=null){
    //   JsonData gangmajiangList = map["gangmajiangs"];
    //   if(gangmajiangList!=null){
    //     List<Majiang> gangmajiangs = new List<Majiang>();
    //     for(int i=0;i<gangmajiangList.Count;i++){
    //       JsonData majiangsMap = gangmajiangList[i];
    //       for(int j=0;j<majiangsMap.Count;j++){
    //         JsonData majiangMap = majiangsMap[j];
    //         gangmajiangs.Add(Majiang.toMajiang(majiangMap));
    //       }
    //     }
    //     user.gangmajiangs = gangmajiangs;
    //   }
    // }

    if(map["pengmajiangs"]!=null && allmajiangs!=null  && allmajiangs.Count>0){
      JsonData pengmajiangList = map["pengmajiangs"];
      if(pengmajiangList!=null){
        List<Majiang> pengmajiangs = new List<Majiang>();
        for(int i=0;i<pengmajiangList.Count;i++){
          JsonData majiangMap = pengmajiangList[i];
          Majiang majiang = Majiang.toMajiang(majiangMap);
          pengmajiangs.Add(GetMajiang(majiang.id,allmajiangs));
        }
        user.pengmajiangs = pengmajiangs;
      }
    }
    if(map["gangmajiangs"]!=null && allmajiangs!=null  && allmajiangs.Count>0){
      JsonData gangmajiangList = map["gangmajiangs"];
      if(gangmajiangList!=null){
        List<Majiang> gangmajiangs = new List<Majiang>();
        for(int i=0;i<gangmajiangList.Count;i++){
            JsonData majiangMap = gangmajiangList[i];
            Majiang majiang = Majiang.toMajiang(majiangMap);
            gangmajiangs.Add(GetMajiang(majiang.id,allmajiangs));
        }
        user.gangmajiangs = gangmajiangs;
      }
    }

    // if(gangmajiangList == null || gangmajiangList.length==0){
    //   //测试
    //   user.gangmajiangs = [];
    //   for(int i=0;i<4;i++){
    //     Majiang majiang = new Majiang();
    //     majiang.type=1;
    //     majiang.value=3;
    //     user.gangmajiangs.add(majiang);
    //   }
    // }

    JsonData zhuapai = map["zhuapai"];
    if(zhuapai!=null){
      Majiang majiang = Majiang.toMajiang(zhuapai);
      user.zhuapai = GetMajiang(majiang.id,allmajiangs);
    }
    
    return user;
  }


  //移除一个麻将
  public void removeMajiang(int id) {
    for (int i = 0; i < majiangs.Count; i++) {
			Majiang majiang = majiangs[i];
			majiang.status = 0;
			if (majiang.id == id) {
				majiangs.RemoveAt(i);
				break;
			}
		}
  }


  //移除一个出的麻将
  public void removeChuMajiang(int id) {
    for (int i = 0; i < chumajiangs.Count; i++) {
			Majiang majiang = chumajiangs[i];
			majiang.status =0;
			if (majiang.id == id) {
				chumajiangs.RemoveAt(i);
				break;
			}
		}
  }

  // 碰
	public List<Majiang> peng(Majiang majiang) {
		int type = majiang.type;
		int value = majiang.value;
		List<Majiang> pengs = new List<Majiang>();
		pengs.Add(majiang);
		for (int i = 0; i < majiangs.Count; i++) {
			Majiang mj = majiangs[i];
			mj.status = 0;
			if (type == mj.type && value == mj.value) {
				pengs.Add(mj);
				majiangs.RemoveAt(i);
				i--;
        if(pengs.Count>=3){
          break;
        }
			}
		}
    if (pengs.Count == 3) {
      for (int i = 0; i < pengs.Count; i++) {
        Majiang mj = pengs[i];
        pengmajiangs.Add(mj);
      }				
    }
    return pengs;
	}

  // 杠
  public List<Majiang> gang(Majiang majiang) {
		int type = majiang.type;
		int value = majiang.value;
		bool peng = false;
		int samenum = 1;
		Majiang last;
		List<Majiang> gangs = new List<Majiang>();
		gangs.Add(majiang);
		for (int i = 0; i < majiangs.Count; i++) {
			Majiang mj = majiangs[i];
			mj.status =0;
			if (type == mj.type && value == mj.value && majiang.id!=mj.id) {
				gangs.Add(mj);
				majiangs.RemoveAt(i);
				i--;
			}
		}

		for (int i = 0; i < pengmajiangs.Count; i++) {
				Majiang mj = pengmajiangs[i];
				if (type == mj.type && value == mj.value) {
					gangs.Add(mj);
					pengmajiangs.RemoveAt(i);
					i--;
				}
		}

		if (gangs.Count >= 4) {
      for (int i = 0; i < gangs.Count; i++) {
        Majiang mj = gangs[i];
        gangmajiangs.Add(mj);
      }				
    }

    return gangs;

	}

  public int getMajiangIndex(int id){
    for (int i = 0; i < majiangs.Count; i++) {
      Majiang mj = majiangs[i];
      if(mj.id == id){
        return i;
      }
    }
    return -1;
  }


  public Majiang getMajiangById(int id){
    for (int i = 0; i < majiangs.Count; i++) {
      Majiang mj = majiangs[i];
      if(mj.id == id){
        return mj;
      }
    }
    return null;
  }

  public void sortMajiangs(){
    if(majiangs==null){
      return;
    }
    List<Majiang> tianyings = new List<Majiang>();
    for (int i = 0; i < majiangs.Count; i++) {
      Majiang mj = majiangs[i];
      if(mj.tianying){
        tianyings.Add(mj);
        majiangs.RemoveAt(i);
        i--;
      }
    }
    tianyings.Sort((Majiang majiang1,Majiang majiang2) => majiang1.CompareTo(majiang2));

    majiangs.Sort((Majiang majiang1,Majiang majiang2) => majiang1.CompareTo(majiang2));
    for (int i = 0; i < tianyings.Count; i++) {
      Majiang mj = tianyings[i];
      majiangs.Insert(i,mj);
    }
  }

  public static string getStatusText(int status){
    if(status==-2){
      return "未准备";
    }else if(status==-1){
      return "已准备";
    }else if(status==0){
      return "在打游戏";
    }else if(status==1){
      return "碰中";
    }else if(status==2){
      return "杠中";
    }else if(status==3){
      return "胡";
    }
    return "";
  }
}