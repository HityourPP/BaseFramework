using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public GameObject activePanel;
    private readonly Dictionary<UIType, GameObject> uiDic;
    public UIManager(GameObject panel)
    {
        activePanel = panel;
        uiDic = new Dictionary<UIType, GameObject>();
    }
    /// <summary>
    /// 获取UI
    /// </summary>
    public GameObject GetSingleUI(UIType uiType)
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (!canvas)
        {
            Debug.LogError("请先创建Canvas对象！！");
            return null;
        }

        GameObject singleUI;
        
        if (uiDic.TryGetValue(uiType,out singleUI))
        {
            return singleUI;
        }

        singleUI = GameObject.Instantiate(AssetBundlesManager.GetInstance()
            .LoadResource<GameObject>(uiType.AbName, uiType.ResName), canvas.transform);
        singleUI.name = uiType.ResName;
        uiDic.Add(uiType, singleUI);
        Debug.Log(singleUI.name);
        return singleUI;
    }
    /// <summary>
    /// 销毁UI
    /// </summary>
    public void DestroyUI(UIType uiType)
    {
        if (uiDic.TryGetValue(uiType,out GameObject ui))
        {
            Debug.Log("销毁" + uiType.ResName);
            GameObject.Destroy(uiDic[uiType]);
            uiDic.Remove(uiType);
        }
    }
    /// <summary>
    /// 向当前活动面板获取或添加组件
    /// </summary>
    public T GetOrAddComponent<T>() where T : Component
    {
        if (activePanel.GetComponent<T>() == null)
        {
            activePanel.AddComponent<T>();
        }

        return activePanel.GetComponent<T>();
    }
    /// <summary>
    /// 根据名称寻找当前活动面板的子对象
    /// </summary>
    public GameObject FindChildGameObject(string name)
    {
        Transform child = activePanel.GetComponentInChildren<Transform>();
        foreach (Transform item in child)
        {
            if (item.name == name)
            {
                return item.gameObject;
            }
        }
        Debug.LogError("无法找到名称为" + name + "的子对象");
        return null;
    }
    /// <summary>
    /// 向活动面板的子对象获取或添加组件
    /// </summary>
    public T GetOrAddComponentInChildren<T>(string name) where T : Component
    {
        var child = FindChildGameObject(name);
        if (child.GetComponent<T>() == null)
        {
            child.AddComponent<T>();
        }
        return child.GetComponent<T>();
    }
}
