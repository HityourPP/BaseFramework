using System.Collections.Generic;
using UnityEngine;
public class PoolData
{
    public GameObject parentGameObject;
    public List<GameObject> poolList;

    public PoolData(GameObject gameObject,GameObject poolGameObject)
    {
        parentGameObject = new GameObject(gameObject.name);
        parentGameObject.transform.parent = poolGameObject.transform;
        poolList = new List<GameObject>();
        AddGameObject(gameObject);
    }
    /// <summary>
    /// 向缓存池添加对象
    /// </summary>
    public void AddGameObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
        poolList.Add(gameObject);
        gameObject.transform.parent = parentGameObject.transform;
    }
    /// <summary>
    /// 取出缓存池中的对象
    /// </summary>
    public GameObject GetGameObject()
    {
        GameObject gameObject = poolList[0];
        poolList.RemoveAt(0);
        gameObject.SetActive(true);
        gameObject.transform.parent = null;
        return gameObject;
    }
}
