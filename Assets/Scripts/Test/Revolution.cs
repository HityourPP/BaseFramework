using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace XLuaProject
{
    public class Revolution : MonoBehaviour
    {
        public Transform axis;
        [SerializeField] private float rotateSpeed = 50f;
        
        [LuaCallCSharp]
        private void Update()
        {
            //公转
            transform.RotateAround(axis.position, Vector3.right, rotateSpeed);
        }
    }
}

