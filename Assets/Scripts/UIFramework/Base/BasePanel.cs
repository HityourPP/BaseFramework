public class BasePanel
{
    public UIType UIType { get; private set; }

    protected UIManager uiManager;
    protected PanelManager panelManager;

    protected BasePanel(UIType uiType)
    {
        UIType = uiType;
    }

    public void Initialize(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }    
    public void Initialize(PanelManager panelManager)
    {
        this.panelManager = panelManager;
    }
    public virtual void OnEnter(){}
    public virtual void OnPause(){}
    public virtual void OnResume(){}
    public virtual void OnExit(){}
}
