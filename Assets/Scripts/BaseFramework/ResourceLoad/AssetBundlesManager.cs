using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 用于ab包资源的加载与卸载
/// </summary>
public class AssetBundlesManager : SingletonAutoMono<AssetBundlesManager>
{
    private AssetBundle abMain = null;

    private AssetBundleManifest abManifest = null;

    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();

    private string PathUrl = Application.streamingAssetsPath + "/";
    private string MainABName
    {
        get
        {//设置不同平台下的不同名称
            #if UNITY_IOS
                return "IOS;
             #elif UNITY_ANDROID
                return "Android";
            #else       
                return "PC";
            #endif
        }
    }
    //加载依赖与所需AB包 
    private void LoadAb(string abName)
    {
        //加载主包与依赖配置文件
        if (!abMain)
        {
            abMain = AssetBundle.LoadFromFile(PathUrl + MainABName);
            abManifest = abMain.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        //获取依赖信息
        string[] dep = abManifest.GetAllDependencies(abName);
        foreach (var name in dep)
        {
            AddToAbDic(name);
        }   
        //加载所需包
        AddToAbDic(abName);
    }
    /// <summary>
    ///同步加载，不指定类型
    /// </summary>
    public Object LoadResource(string abName, string resName)
    {
        LoadAb(abName);
        //加载资源
        return abDic[abName].LoadAsset(resName); 
    }
    //同步加载根据type指定类型
    public Object LoadResource(string abName, string resName, System.Type type)
    {
        LoadAb(abName);
        //指定类型加载资源
        return abDic[abName].LoadAsset(resName, type); 
    }
    //同步加载，泛型加载资源
    public T LoadResource<T>(string abName, string resName) where T :Object
    {
        LoadAb(abName);
        //加载资源
        return abDic[abName].LoadAsset<T>(resName); 
    }
    
    //异步加载，这里的异步加载，加载ab包并没有使用异步加载，加载资源时使用了异步加载
    //异步加载，不指定类型
    public void LoadResourceAsync(string abName, string resName,UnityAction<Object> callBack)
    {
        StartCoroutine(RealLoadResourceAsync(abName, resName, callBack));
    }
    private IEnumerator RealLoadResourceAsync(string abName, string resName,UnityAction<Object> callBack)
    {
        LoadAb(abName);
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName);
        yield return abr;
        //异步加载后，通过委托传递给外部使用
        callBack(abr.asset);
    }
    
    //异步加载，通过Type指定类型
    public void LoadResourceAsync(string abName, string resName,System.Type type,UnityAction<Object> callBack)
    {
        StartCoroutine(RealLoadResourceAsync(abName, resName,type, callBack));
    }
    private IEnumerator RealLoadResourceAsync(string abName, string resName, System.Type type, UnityAction<Object> callBack)
    {
        LoadAb(abName);
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName, type);
        yield return abr;
        //异步加载后，通过委托传递给外部使用
        callBack(abr.asset);
    }
    
    //异步加载，通过泛型加载
    public void LoadResourceAsync<T>(string abName, string resName,UnityAction<T> callBack)where T : Object
    {
        StartCoroutine(RealLoadResourceAsync<T>(abName, resName, callBack));
    }
    private IEnumerator RealLoadResourceAsync<T>(string abName, string resName,UnityAction<T> callBack) where T : Object
    {
        LoadAb(abName);
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync<T>(resName);
        yield return abr;
        //异步加载后，通过委托传递给外部使用
        callBack(abr.asset as T);
    }
    //单个包卸载
    public void UnLoadSingle(string abName)
    {
        AssetBundle ab;
        if (abDic.TryGetValue(abName,out ab))
        {
            ab.Unload(false);           //为false时，会释放包中的数据，不能再通过这个包加载资源，但从这个包中实例化的物体将是完好的
                                        //为true时，所有从该包中加载的物体将被销毁，如果场景中有引用包中的资源，也会丢失引用
            abDic.Remove(abName);
        }
    }
    
    //所有包卸载
    public void ClearAll()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        abMain = null;
        abManifest = null;
    }
    
    // 将要加载的包添加到字典中
    private void AddToAbDic(string abName)  
    {//判断包是否被加载过
        if (!abDic.ContainsKey(abName))
        {
            AssetBundle ab = AssetBundle.LoadFromFile(PathUrl + abName);
            abDic.Add(abName, ab);
        }
    }
}


