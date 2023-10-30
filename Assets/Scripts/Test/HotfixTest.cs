using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace XLuaProject
{
    [Hotfix]
    public class HotfixTest : MonoBehaviour
    {
        public LuaEnv luaEnv;
        // Start is called before the first frame update
        void Start()
        {
            luaEnv = new LuaEnv();
            luaEnv.DoString("CS.UnityEngine.Debug.Log('hello world')");
            luaEnv.Dispose();
        }
    }
}

