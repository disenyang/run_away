using UnityEngine;
using System;
public class Test : MonoBehaviour {
   void OnDestroy()
    {
      Debug.Log("麻将被销毁");
      throw new Exception("麻将被销毁");
    }
}
