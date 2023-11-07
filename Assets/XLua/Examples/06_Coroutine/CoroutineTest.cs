using UnityEngine;
using XLua;

namespace XLuaTest
{
    public class CoroutineTest : MonoBehaviour
    {
        LuaEnv luaenv = null;
        void Start()
        {
            luaenv = new LuaEnv();
            //使用require时，若未自定义loader，默认访问Resources文件夹下的lua文件
            //运行coruntine_test.lua文件
            luaenv.DoString("require 'coruntine_test'");
        }
        void Update()
        {
            if (luaenv != null)
            {
                luaenv.Tick();
            }
        }
        void OnDestroy()
        {
            luaenv.Dispose();
        }
    }
}
