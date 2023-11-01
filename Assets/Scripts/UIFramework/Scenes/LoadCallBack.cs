using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LoadCallBack : MonoBehaviour
{
    //下面的方法是在下一帧执行加载另一个场景，由于每帧会很快，所以会一闪而过
    // private bool isFirstUpdate = true;
    //
    // private void Update()
    // {
    //     if (isFirstUpdate)
    //     {
    //         isFirstUpdate = false;
    //         Loader.LoaderCallBack();
    //     }
    // }
    //下面的是设置一段随机的时间进行加载
    private float startTime;
    private void OnEnable()
    {
        startTime = Time.time;
    }
    
    private void Update()
    {
        if (Time.time > startTime + Random.Range(0.5f, 1.25f))
        {
            Loader.LoaderCallBack();
        }
    }
}
