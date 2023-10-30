using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XLuaProject
{
    public class ABTest : MonoBehaviour
    {
        private void Start()
        {
            GameObject obj = AssetBundlesManager.GetInstance().LoadResource<GameObject>("test", "Sun");
            Instantiate(obj);
        }
    } 
}

