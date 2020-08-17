
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class Room{
  public User userTop;
  public User userLeft;
  public User userBottom;
  public User userRight;
  public bool random;
  public string id;
	public List<User> users = new List<User>();
	public int no;
	// 状态0:等待游戏 1:开始游戏 2:碰 3:杠 4:胡,5:牌局结束
	public int status;

  //掷骰子的用户
  public string shaiziUser;

	// 麻将类型
	public int majiangtype;

	public string statustext;

  //房主
  public string owner;

  //庄家-上一把胡的
  public string huUserId;

	// 打到第几局
	public int gameindex;

	// 总共几局
	public int gametotal = 8;
  public int circleRound = 0;

	// 翻出的麻将
	public Majiang fan;

	// 翻出的麻将位置
	public int fanindex;

	//骰子
	public int shaizi1;
	public int shaizi2;
	// 后面摸的麻将
	public List<Majiang> backmomjs;

  //剩余的麻将
  public List<Majiang> leavemajiangs;
  //洗牌的麻将
  public List<int> allMajiangIds = new List<int>();

  public List<Majiang> startLeavemajiangs;


	// 天应
	public Majiang tianyan;

	// 当前打出的麻将
	public Majiang nowmajiang;

	// 现在的轮到的用户
	public string nowUserid;

  // 倍数
  public int timesType;

  public string extMessage;

  //转换为房间
  int indexOfUser(string id){
    int index = -1;
    for(int i=0;i<users.Count;i++){
      User user = users[i];
      if(user.id == id){
        index = i;
      }
    }
    return index;
  }


  public User getUser(string id) {
		for(int i=0;i<users.Count;i++){
			User user = users[i];
			if(user.id==id){
				return user;
			}
		}
		return null;
	}
  int getUserCount() {
    int count=0;
		for(int i=0;i<users.Count;i++){
			User user = users[i];
			if(user.id!=null){
				count++;
			}
		}
		return count;
	}

  public Majiang getLeaveMajiang(int id) {
    for(int i=0;i<leavemajiangs.Count;i++){
      Majiang majiang = leavemajiangs[i];
      if(majiang.id == id){
        return majiang;
      }
    }
    return null;
  }


  //清楚数据
  // clear(){
    // if(leavemajiangs!=null){
    //   leavemajiangs.clear();
    // }
    // if(backmomjs!=null){
    //   backmomjs.clear();
    // }
    // if(startLeavemajiangs!=null){
    //   startLeavemajiangs.clear();
    // }
    
    // users.forEach((user){
    //   if(user.majiangs!=null){
    //     user.majiangs.clear();
    //   }
    //   if(user.pengmajiangs!=null){
    //     user.pengmajiangs.clear();
    //   } 
    //   if(user.gangmajiangs!=null){
    //     user.gangmajiangs.clear();
    //   }  
    //   if(user.chumajiangs!=null){
    //     user.chumajiangs.clear();
    //   }  
      
    // });
    // if(leavemajiangs!=null){
    //   leavemajiangs.clear();
    // }
    // if(backmomjs!=null){
    //   backmomjs.clear();
    // }
    // if(startLeavemajiangs!=null){
    //   startLeavemajiangs.clear();
    // }
  // }


  static Majiang GetMajiang(int id,List<Majiang> allmajiangs){
    for(int i=0;i<allmajiangs.Count;i++){
			Majiang majiang = allmajiangs[i];
			if(majiang.id==id){
        return majiang;
      }
		}
    return null;
  }

  public static Room toRoom(JsonData data,List<Majiang> allmajiangs){
    Debug.Log("typeof(map)="+data.GetType().FullName);
    Room room = new Room();
    
    room.no = (int)data["no"];
    room.id = (string)data["id"];
    if(data["nowUserid"]!=null){
      room.nowUserid = (string)data["nowUserid"];
    }

    if(data["random"]!=null){
      room.random = (bool)data["random"];
    }

    
    if(data["shaiziUser"]!=null){
      room.shaiziUser = (string)data["shaiziUser"];
    }
    if(data["allMajiangIds"]!=null){
      List<int> ids = new List<int>();
      JsonData mjs = data["allMajiangIds"];
      for(int i=0;i<mjs.Count;i++){
        int id = (int)mjs[i];
        ids.Add(id);
      }
      room.allMajiangIds = ids;
    }
    
    room.fanindex = (int)data["fanindex"];
    room.status = (int)data["status"];

    if(data["huUserId"]!=null){
      room.huUserId = (string)data["huUserId"];
    }

    room.owner = (string)data["owner"];
    room.gameindex = (int)data["gameindex"];
    room.timesType = (int)data["timesType"];
    if(data["extMessage"]!=null){
      room.extMessage = (string)data["extMessage"];
    }

    // room.status = 4;
    if(room.huUserId==null){
      room.huUserId = room.owner;
    }
    if(room.huUserId==null){
      room.huUserId = AppUtil.user.id;
    }
    if(data["nowmajiang"]!=null){
      JsonData nowmajiangMap = data["nowmajiang"];
      if(nowmajiangMap!=null){
        Majiang majiang = Majiang.toMajiang(nowmajiangMap);
        room.nowmajiang = GetMajiang(majiang.id,allmajiangs);
      }
    }

    

    JsonData fanMap = data["fan"];
    if(fanMap!=null){
      Majiang majiang = Majiang.toMajiang(fanMap);
      room.fan = GetMajiang(majiang.id,allmajiangs);
    }

    JsonData tianyanMap = data["tianyan"];
    if(tianyanMap!=null){
      room.tianyan = Majiang.toMajiang(tianyanMap);
      for(int i=0;i<allmajiangs.Count;i++){
        Majiang majiang = allmajiangs[i];
        if(majiang.type==room.tianyan.type && majiang.value==room.tianyan.value){
          majiang.tianying = true;
        }else{
          majiang.tianying = false;
        }
      }
    }

    room.shaizi1 = (int)data["shaizi1"];
    room.shaizi2 = (int)data["shaizi2"];
    room.circleRound = (int)data["circleRound"];

    // room.shaizi1 = 1;
    // room.shaizi2 = 1;

    JsonData userList = data["users"];
    List<User> users = new List<User>();
    for(int i=0;i<userList.Count;i++){
      JsonData userMap = userList[i];
      users.Add(User.toUser(userMap,allmajiangs));

    }
    
    room.users = users;

    // JsonData backmomjsList = data["backmomjs"];
    // if(backmomjsList!=null){
    //   List<Majiang> backmomjs = new List<Majiang>();
    //   for(int i=0;i<backmomjsList.Count;i++){
    //     JsonData majiangMap = backmomjsList[i];
    //     Majiang majiang = Majiang.toMajiang(majiangMap);
    //     backmomjs.Add(GetMajiang(majiang.id,allmajiangs));
    //   }
    //   room.backmomjs = backmomjs;
    // }

    JsonData leavemajiangsList = data["leavemajiangs"];
    Debug.Log("房间剩余麻将列表="+room.leavemajiangs);
    if(leavemajiangsList!=null){
      List<Majiang> leavemajiangs = new List<Majiang>();;
      for(int i=0;i<leavemajiangsList.Count;i++){
        JsonData majiangMap = leavemajiangsList[i];
        Majiang majiang = Majiang.toMajiang(majiangMap);
        Majiang trueMajiang = GetMajiang(majiang.id,allmajiangs);
        leavemajiangs.Add(trueMajiang);
      }
      room.leavemajiangs = leavemajiangs;
      Debug.Log("房间处理后="+room.leavemajiangs.Count);
    }

    // JsonData startLeaveMajiangsList = data["startLeaveMajiangs"];
    // if(startLeaveMajiangsList!=null){
    //   List<Majiang> startLeaveMajiangs = new List<Majiang>();
    //   for(int i=0;i<startLeaveMajiangsList.Count;i++){
    //     JsonData majiangMap = startLeaveMajiangsList[i];
    //     startLeaveMajiangs.Add(Majiang.toMajiang(majiangMap));
    //   }
    //   room.startLeavemajiangs = startLeaveMajiangs;
    // }

    int myIndex = room.indexOfUser(AppUtil.user.id);
    if(myIndex!=-1){
      if(myIndex<room.users.Count-1){
        room.userRight = room.users[myIndex+1];
      }else{
        room.userRight = room.users[0];
      }

      if(myIndex>0){
        room.userLeft = room.users[myIndex-1];
      }else{
        room.userLeft = room.users[3];
      }

      if(myIndex<2){
        room.userTop = room.users[myIndex+2];
      }else{
        room.userTop = room.users[myIndex==2?0:1];
      }

      room.userBottom = room.users[myIndex];
    }

    if(users!=null){
      for(int i=0;i<users.Count;i++){
        User user = users[i];
        user.sortMajiangs();
      }
    }
    return room;
  }

  public int getLeavesNum(){
    return leavemajiangs.IndexOf(fan);
  }

}