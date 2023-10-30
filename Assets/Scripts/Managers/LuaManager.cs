using System.IO;
using UnityEngine;
using XLua;

public class LuaManager : MonoBehaviour
{
    public static LuaManager Instance { get; private set; }
    public static LuaEnv LuaEnv;

    private void Awake()
    {
        Instance = this;
        LuaEnvInit();
    }

    /// <summary>
    /// 初始化lua虚拟机
    /// </summary>
    public void LuaEnvInit()
    {
        LuaEnv = new LuaEnv();
        LuaEnv.AddLoader(Loader);
        //加载lua文件
        LuaEnv.DoString(@"require 'Hello'");
    }
    /// <summary>
    /// 自定义Loader
    /// </summary>
    private byte[] Loader(ref string fileName)
    {
        string absPath = Application.dataPath + "/Resources/Lua/" + fileName + ".lua";
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(absPath));
    }
}

