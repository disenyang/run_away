using UnityEngine;


//麻将动画封装

public class MajiangAnimate{
  public Majiang majiang;
  public Vector3 toPosition;
  public Vector3 toRotate;

  public Vector3 stepPostion;
  public Vector3 stepRotate;

  //执行时间
  public float time;

  //已经执行的时间
  public float doTime;
}