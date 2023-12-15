using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : BasePanel
{
    private static readonly string abName = "ui";
    private static readonly string resName = "GameOverPanel";
    public GameOverPanel() : base(new UIType(abName, resName)){}

    public override void OnEnter()
    {
        TextMeshProUGUI scoreNumText = uiManager.GetOrAddComponentInChildren<TextMeshProUGUI>("ScoreNum");
        scoreNumText.text = PlaneFight.GameManager.Instance.score.ToString("0");
        uiManager.GetOrAddComponentInChildren<Button>("RestartButton").onClick.AddListener((() =>
        {
            Debug.Log("Restart");
        }));       
        uiManager.GetOrAddComponentInChildren<Button>("MainMenuButton").onClick.AddListener((() =>
        {
            Debug.Log("MainMenu");
            PoolManager.GetInstance().Clear();
            EventManager.GetInstance().ClearEvent();
            GameManager.GetInstance().SceneSystem.SetSceneState(new MainMenuScene());
        }));
    }
}
