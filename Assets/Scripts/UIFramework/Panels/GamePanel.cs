using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    private static readonly string abName = "ui";
    private static readonly string resName = "GamePanel";

    public GamePanel() : base(new UIType(abName, resName)){}

    public override void OnEnter()
    {
        uiManager.GetOrAddComponentInChildren<Button>("SettingButton").onClick.AddListener((() =>
        {
            panelManager.PushUI(new SettingPanel());
        }));
    }
}
