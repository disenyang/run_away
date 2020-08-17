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

public class Confirm{

  public static void show(string str, Transform trans,Action onConfirm,Action onCancel)
  {
    GameObject prefab = Resources.Load("Prefab/ConfirmPanel") as GameObject;
    GameObject confirmPanel = MonoBehaviour.Instantiate(prefab);    // 对象初始化
    confirmPanel.transform.parent = trans;
    // confirmPanel.transform.localScale = Vector3.one;
    confirmPanel.GetComponent<RectTransform>().localPosition = Vector3.one;
    confirmPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width,Screen.height);
    GameObject panel = confirmPanel.transform.Find("Panel").gameObject;

    RectTransform transform = panel.transform.GetComponent<RectTransform>();
    Text tips = panel.transform.Find("Text").GetComponent<Text>();

    Button confirmButton = panel.transform.Find("ConfirmButton").GetComponent<Button>();
    Button cancelButton = panel.transform.Find("CancelButton").GetComponent<Button>();

    confirmButton.onClick.AddListener(()=>{
      if(onConfirm!=null){
        onConfirm();
      }
      GameObject.Destroy(confirmPanel, 0);
    });

    cancelButton.onClick.AddListener(()=>{
      if(onCancel!=null){
        onCancel();
      }
      GameObject.Destroy(confirmPanel, 0);
    });

    RectTransform textTransform = tips.transform.GetComponent<RectTransform>();
    Debug.Log("tips="+tips);
    tips.text = str; 
    // 根据字符长度，适配窗体大小
    // arg0: 字符串长度*文本字体大小，再加上边距为提示窗体的宽度
    // arg1: 提示窗体高度 45 (可根据自己需求做适当修改)
    // transform.sizeDelta = new Vector2(str.Length * tips.fontSize + 145, 100);
    // textTransform.sizeDelta = new Vector2(str.Length * tips.fontSize + 45, 100);

    
  }

  public static void error(string str, Transform trans,GameObject prefab)
  {
    GameObject confirmPanel = MonoBehaviour.Instantiate(prefab);    // 对象初始化
    confirmPanel.transform.parent = trans;
    confirmPanel.transform.localScale = Vector3.one;
    confirmPanel.transform.localPosition = Vector3.zero;
    GameObject panel = confirmPanel.transform.Find("Panel").gameObject;
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

    GameObject.Destroy(confirmPanel, 2);
  }
} 