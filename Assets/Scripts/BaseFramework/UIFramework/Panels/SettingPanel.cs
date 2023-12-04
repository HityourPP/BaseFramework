using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    private static readonly string abName = "ui";
    private static readonly string resName = "SettingPanel";

    public SettingPanel() : base(new UIType(abName, resName)){}

    public override void OnEnter()
    {
        uiManager.GetOrAddComponentInChildren<Button>("ResumeButton").onClick.AddListener((() =>
        {
            Debug.Log("Resume");
            panelManager.PopUI();
        }));        
        uiManager.GetOrAddComponentInChildren<Button>("RestartButton").onClick.AddListener((() =>
        {
            Debug.Log("Restart");
        }));       
        uiManager.GetOrAddComponentInChildren<Button>("MainMenuButton").onClick.AddListener((() =>
        {
            Debug.Log("MainMenu");
            GameManager.GetInstance().SceneSystem.SetSceneState(new MainMenuScene());
        }));
        Time.timeScale = 0f;
    }

    public override void OnExit()
    {
        base.OnExit();
        Time.timeScale = 1f;
    }
}
