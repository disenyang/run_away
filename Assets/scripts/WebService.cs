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

public class WebService{
  private static string defaultErrMsg=  "系统正在维护中";
  private static string domain=  "http://www.xiaowanwu.cn/run_away";
  
  public static void post(string url,JsonData param,Transform transform,Action<JsonData> onSucess,Action<JsonData> onFaild,MonoBehaviour behaviour){
		behaviour.StartCoroutine(SendPost(domain+url,param,transform,onSucess,onFaild));	
  }

  public static IEnumerator SendPost(string url,JsonData param,Transform transform,Action<JsonData> onSucess,Action<JsonData> onFaild)
  {
    string input = param.ToJson();
    WWWForm form = new WWWForm();		
    Dictionary<String,String> headers = form.headers;
    byte[] rawData = Encoding.UTF8.GetBytes(input);
    headers["Content-Type"] = "application/json";
    headers["Accept"] = "application/json";

    // DateTime date = DateTime.Now;
    // string time =  date.ToString("ddd, yyyy-mm-dd HH':'mm':'ss 'UTC'",DateTimeFormatInfo.InvariantInfo);
    // headers["Date"] = time;

    WWW www = new WWW(url, rawData, headers);
    yield return www;		
    try{
      if (www.error != null)
      {
        Debug.Log("error is login:"+ www.error  + "  " +input );
        JsonData data = new JsonData();
        data["errno"] = 404;
        data["msg"] = "网络连结异常";
        onFaild(data);	
      } else{
        Debug.Log("onSuccess22...."+www.text);
        JsonData response = JsonMapper.ToObject(www.text); 
        JsonData meta = response["meta"];
        int errno = (int)meta["errno"];
        if(errno!=0){
          string msg = (string)meta["msg"];
          if(msg==null || msg==""){
            msg = defaultErrMsg;
          }
          onFaild(meta);
        }else{
          JsonData result = response["result"];
          onSucess(result);
        }
        
      }
    }
    catch (Exception ex)
    {
      Debug.Log(ex);
    }
  }
}