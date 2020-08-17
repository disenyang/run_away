using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadImag{
    // Use this for initialization
    public static void  load (string url,Image image,MonoBehaviour behaviour) {
      behaviour.StartCoroutine(DownSprite(url,image));
	  }
   
    public static IEnumerator DownSprite(string url,Image image)
    {
        UnityWebRequest wr = new UnityWebRequest(url);
        DownloadHandlerTexture texD1 = new DownloadHandlerTexture(true);
        wr.downloadHandler = texD1;
        yield return wr.SendWebRequest();
        int width = 1920;
        int high = 1080;
        if(!wr.isNetworkError)
        {
            Texture2D tex = new Texture2D(width, high);
            tex = texD1.texture;
            //保存本地          
            Byte[] bytes=tex.EncodeToPNG();
            //File.WriteAllBytes(Application.dataPath + "/02.png", bytes);
            Debug.Log("下载完成"+tex.width+","+tex.height+",image"+image);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            image.sprite = sprite;
        }
    }
   
    private void OnApplicationQuit()
    {
        //StopAllCoroutines();
    }
}