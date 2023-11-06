/*
 * Tencent is pleased to support the open source community by making xLua available.
 * Copyright (C) 2016 THL A29 Limited, a Tencent company. All rights reserved.
 * Licensed under the MIT License (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
 * http://opensource.org/licenses/MIT
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
*/

using System;
using UnityEngine;
using XLua;

namespace XLuaTest
{
    //用于事件中传递数据
    public class PropertyChangedEventArgs : EventArgs
    {
        public string name;
        public object value;
    }

    public class InvokeLua : MonoBehaviour
    {
        [CSharpCallLua]
        public interface ICalc
        {
            //定义包含数据的委托
            public event EventHandler<PropertyChangedEventArgs> PropertyChanged;
            int Add(int a, int b);
            int Mult { get; set; }
            object this[int index] { get; set; }
        }
        [CSharpCallLua]
        public delegate ICalc CalcNew(int mult, params string[] args);
        public TextAsset luaScript; 
        // Use this for initialization
        void Start()
        {
            LuaEnv luaEnv = new LuaEnv();
            Test(luaEnv);//调用了带可变参数的delegate，函数结束都不会释放delegate，即使置空并调用GC
            luaEnv.Dispose();
        }

        void Test(LuaEnv luaenv)
        {
            //执行lua脚本
            luaenv.DoString(luaScript.text);
            //返回脚本中的Calc.New
            CalcNew calcNew = luaenv.Global.GetInPath<CalcNew>("Calc.New");
            //将lua中的表转化为C#中的接口
            ICalc calc = calcNew(10, "hi", "john"); //constructor
            Debug.Log("sum(*10) =" + calc.Add(1, 2));
            calc.Mult = 100;
            Debug.Log("sum(*100)=" + calc.Add(1, 2));

            Debug.Log("list[0]=" + calc[0]);
            Debug.Log("list[1]=" + calc[1]);

            calc.PropertyChanged += Notify;
            calc[1] = "dddd";
            Debug.Log("list[1]=" + calc[1]);

            calc.PropertyChanged -= Notify;

            calc[1] = "eeee";
            Debug.Log("list[1]=" + calc[1]);
        }

        void Notify(object sender, PropertyChangedEventArgs e)
        {
            Debug.Log(string.Format("{0} has property changed {1}={2}", sender, e.name, e.value));
        }
    }
}