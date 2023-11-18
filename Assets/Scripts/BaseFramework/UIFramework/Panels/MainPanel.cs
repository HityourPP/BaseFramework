using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BasePanel
{
    private static readonly string abName = "ui";
    private static readonly string resName = "MainPanel";

    public MainPanel() : base(new UIType(abName, resName)){}

    public override void OnEnter()
    {
        uiManager.GetOrAddComponentInChildren<Button>("SettingButton").onClick.AddListener((() =>
        {
            panelManager.PushUI(new SettingPanel());
        }));        
        uiManager.GetOrAddComponentInChildren<Button>("PlayButton").onClick.AddListener((() =>
        {
            Debug.Log("Play");
            GameManager.GetInstance().SceneSystem.SetSceneState(new GameScene());
        }));
    }
}
