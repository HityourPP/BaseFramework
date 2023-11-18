using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingletonAutoMono<PoolManager>
{
    public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();
    private GameObject poolGameObject;

    public GameObject GetGameObject(string abName,string resName)
    {
        if (poolDic.ContainsKey(resName) && poolDic[resName].poolList.Count > 0)
        {
            return poolDic[resName].GetGameObject();
        }
        else
        {
            GameObject gameObject =
                Instantiate(AssetBundlesManager.GetInstance().LoadResource<GameObject>(abName, resName));
            gameObject.name = resName;
            return gameObject;
        }
    }

    public void AddGameObject(string name, GameObject gameObject)
    {
        if (!poolGameObject) poolGameObject = new GameObject("Pool");
        if (poolDic.ContainsKey(name))
        {
            poolDic[name].AddGameObject(gameObject);
        }
        else
        {
            poolDic.Add(name, new PoolData(gameObject, poolGameObject));
        }
    }
    /// <summary>
    /// 加载场景时清空，进行初始化
    /// </summary>
    public void Clear()
    {
        poolDic.Clear();
        poolGameObject = null;
    }
}
