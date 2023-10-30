using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonAutoMono<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T Instance;
    /// <summary>
    /// 获取单例，若无，就创建一个
    /// </summary>
    public static T GetInstance()
    {
        if (!Instance)
        {
            GameObject obj = new GameObject();
            obj.name = typeof(T).ToString();
            DontDestroyOnLoad(obj);
            Instance = obj.AddComponent<T>();
        }
        return Instance;
    }
}
