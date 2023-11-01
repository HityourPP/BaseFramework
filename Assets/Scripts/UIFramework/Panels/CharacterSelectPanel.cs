using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectPanel : BasePanel
{
    private static readonly string abName = "ui";
    private static readonly string resName = "CharacterSelectPanel";

    public CharacterSelectPanel() : base(new UIType(abName, resName)){}

    public override void OnEnter()
    {
        uiManager.GetOrAddComponentInChildren<Button>("MainMenuButton").onClick.AddListener((() =>
        {
            GameManager.GetInstance().SceneSystem.SetSceneState(new MainMenuScene());
        }));
            
    }
    
}
