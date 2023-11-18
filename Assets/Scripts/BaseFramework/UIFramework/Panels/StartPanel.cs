using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    private static readonly string abName = "ui";
    private static readonly string resName = "StartPanel";

    public StartPanel() : base(new UIType(abName, resName)){}


    public override void OnEnter()
    {
        uiManager.GetOrAddComponentInChildren<Button>("QuitButton").onClick.AddListener(() =>
        {
            Debug.Log("Quit");
            Application.Quit();
        });
        uiManager.GetOrAddComponentInChildren<Button>("PlayButton").onClick.AddListener(() =>
        {
            Debug.Log("Play");
            GameManager.GetInstance().SceneSystem.SetSceneState(new CharacterSelelctScene());
        });    
        uiManager.GetOrAddComponentInChildren<Button>("SettingButton").onClick.AddListener(() =>
        {
            panelManager.PushUI(new SettingPanel());
            Debug.Log("Setting");
        });
        
    }
}
