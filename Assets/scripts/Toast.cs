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
using DG.Tweening;

public class Toast{
  static GameObject loadingGameObject;
  public static void info(string str, Transform trans)
  {
    Toast.info( str,  trans,false);
  }
  
  public static void info(string str, Transform trans,bool top)
  {
    GameObject prefab = Resources.Load("Prefab/InfoToastPanel") as GameObject;
    GameObject m_toast = MonoBehaviour.Instantiate(prefab);    // 对象初始化
    m_toast.transform.parent = trans;
    m_toast.transform.localScale = Vector3.one;
    m_toast.transform.localPosition = Vector3.zero;
    GameObject panel = m_toast.transform.Find("Panel").gameObject;
    RectTransform transform = panel.transform.GetComponent<RectTransform>();
    Text tips = panel.transform.Find("Text").GetComponent<Text>();

    RectTransform textTransform = tips.transform.GetComponent<RectTransform>();
    RectTransform panelTransform = panel.transform.GetComponent<RectTransform>();
    RectTransform prefabTransform = m_toast.transform.GetComponent<RectTransform>();

    if(top){
      prefabTransform.position = new Vector3(prefabTransform.position.x,Screen.height-150,0);
      panel.GetComponent<Image>().color = AppUtil.GetColor("#ffaa42");
    }
    

    Debug.Log("tips="+tips);
    tips.text = str; 
    // 根据字符长度，适配窗体大小
    // arg0: 字符串长度*文本字体大小，再加上边距为提示窗体的宽度
    // arg1: 提示窗体高度 45 (可根据自己需求做适当修改)
    transform.sizeDelta = new Vector2(str.Length * tips.fontSize + 145, 100);
    textTransform.sizeDelta = new Vector2(str.Length * tips.fontSize + 45, 100);

    GameObject.Destroy(m_toast, 2);
  }

   public static void hideLoading(){
     if(loadingGameObject!=null){
      GameObject.Destroy(loadingGameObject);
      loadingGameObject = null;
    }
   }

  public static void loading(Transform trans)
  {
    loading("加载中",trans);
  }


  public static void loading(string title, Transform trans)
  {
    GameObject prefab = Resources.Load("Prefab/LoadingToastPanel") as GameObject;
    GameObject m_toast = MonoBehaviour.Instantiate(prefab);    // 对象初始化
    m_toast.transform.parent = trans;
    m_toast.transform.localScale = Vector3.one;
    m_toast.transform.localPosition = Vector3.zero;
    GameObject panel = m_toast.transform.Find("Panel").gameObject;
    RectTransform transform = panel.transform.GetComponent<RectTransform>();
    Image image = panel.transform.Find("Image").GetComponent<Image>();
    Text text = panel.transform.Find("Text").GetComponent<Text>();
    text.text = title;
    image.GetComponent<Transform>().DORotate(new Vector3(0f,0f,-360f), 0.8f,RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);

    transform.sizeDelta = new Vector2(title.Length * text.fontSize + 100, 300);

    if(loadingGameObject!=null){
      GameObject.Destroy(loadingGameObject);
    }
    loadingGameObject=m_toast ;
    
  }

  public static void error(string str, Transform trans){
    GameObject prefab = Resources.Load("Prefab/ErrorToastPanel") as GameObject;
    error(str,trans,prefab);
  }

  public static void error(string str, Transform trans,GameObject prefab)
  {
    GameObject m_toast = MonoBehaviour.Instantiate(prefab);    // 对象初始化
    m_toast.transform.parent = trans;
    m_toast.transform.localScale = Vector3.one;
    m_toast.transform.localPosition = Vector3.zero;
    GameObject panel = m_toast.transform.Find("Panel").gameObject;
    RectTransform transform = panel.transform.GetComponent<RectTransform>();
    Text tips = panel.transform.Find("Text").GetComponent<Text>();
    RectTransform textTransform = tips.transform.GetComponent<RectTransform>();
    Debug.Log("tips="+tips);
    tips.text = str; 
    // 根据字符长度，适配窗体大小
    // arg0: 字符串长度*文本字体大小，再加上边距为提示窗体的宽度
    // arg1: 提示窗体高度 45 (可根据自己需求做适当修改)
    transform.sizeDelta = new Vector2(str.Length * tips.fontSize + 145, 100);
    textTransform.sizeDelta = new Vector2(str.Length * tips.fontSize + 45, 100);

    GameObject.Destroy(m_toast, 2);
  }
} 