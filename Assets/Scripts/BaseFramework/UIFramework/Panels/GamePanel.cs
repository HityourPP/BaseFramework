using TMPro;
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
        TextMeshProUGUI healthNumText = uiManager.GetOrAddComponentInChildren<TextMeshProUGUI>("HealthNum");
        healthNumText.text = PlaneFight.GameManager.Instance.health.ToString();      
        TextMeshProUGUI scoreNumText = uiManager.GetOrAddComponentInChildren<TextMeshProUGUI>("ScoreNum");
        scoreNumText.text = PlaneFight.GameManager.Instance.score.ToString("0");
        EventManager.GetInstance().AddEventListener<int>("ChangeHealth", health =>
        {
            healthNumText.text = health.ToString();
        });    
        EventManager.GetInstance().AddEventListener<float>("GetScore", score =>
        {
            scoreNumText.text = score.ToString("0");
        });
        EventManager.GetInstance().AddEventListener("GameOver",() =>
        {
            panelManager.PushUI(new GameOverPanel());
        });
    }
}
