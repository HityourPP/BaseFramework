/*
 * Tencent is pleased to support the open source community by making xLua available.
 * Copyright (C) 2016 THL A29 Limited, a Tencent company. All rights reserved.
 * Licensed under the MIT License (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
 * http://opensource.org/licenses/MIT
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XLua;
using System;

namespace XLuaTest
{
    [System.Serializable]
    public class Injection
    {
        public string name;
        public GameObject value;
    }

    [LuaCallCSharp]
    public class LuaBehaviour : MonoBehaviour
    {
        public TextAsset luaScript;             //lua脚本文件
        public Injection[] injections;          //需要注入到环境变量的物体

        private static LuaEnv luaEnv = new LuaEnv();    //all lua behaviour shared one luaenv only!
        private static float lastGCTime = 0;
        private const float GCInterval = 1;             //1 second 

        private Action luaStart;
        private Action luaUpdate;
        private Action luaOnDestroy;
        //脚本环境，也就是每一个挂靠在游戏对象上的脚本组件，即脚本对象，都有一个独立的脚本环境，但总体都是由类的一个luaEnv new出来的
        private LuaTable scriptEnv;

        void Awake()
        {
            scriptEnv = luaEnv.NewTable();

            // 为每个脚本设置一个独立的环境，可一定程度上防止脚本间全局变量、函数冲突
            LuaTable meta = luaEnv.NewTable();
            meta.Set("__index", luaEnv.Global);
            scriptEnv.SetMetaTable(meta);
            meta.Dispose();
            
            //配置环境变量
            //将lua脚本的self设为当前脚本指针，这样就能通过该指针获取游戏对象的组件    
            scriptEnv.Set("self", this);
            //将外部unity组件传入的环境变量的lua表中
            foreach (var injection in injections)
            {
                scriptEnv.Set(injection.name, injection.value);
            }
            
            //运行lua代码，其中三个参数分别为：1，lua代码块，2，lua代码块名字，用于报错时debug显示信息，3，代码块的环境变量
            luaEnv.DoString(luaScript.text, "LuaTestScript", scriptEnv);

            Action luaAwake = scriptEnv.Get<Action>("Awake");
            
            //获取lua代码中的函数
            scriptEnv.Get("Start", out luaStart);
            scriptEnv.Get("Update", out luaUpdate);
            scriptEnv.Get("OnDestroy", out luaOnDestroy);
            if (luaAwake != null)
            {
                luaAwake();
            }
        }
        void Start()
        {
            if (luaStart != null)
            {
                luaStart();
            }
        }
        void Update()
        {
            if (luaUpdate != null)
            {
                luaUpdate();
            }
            //定时调用，清除未手动释放的luaBase对象，如luaTable,luaFunction等
            if (Time.time - lastGCTime > GCInterval)
            {
                luaEnv.Tick();
                lastGCTime = Time.time;
            }
        }
        void OnDestroy()
        {
            if (luaOnDestroy != null)
            {
                luaOnDestroy();
            }
            //释放脚本环境
            luaOnDestroy = null;
            luaUpdate = null;
            luaStart = null;
            scriptEnv.Dispose();
            injections = null;
        }
    }
}
