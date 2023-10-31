using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager 
{
    private Stack<BasePanel> panelStack = new Stack<BasePanel>();
    private UIManager uiManager = new UIManager(null);
    private BasePanel panel;

    public void PushUI(BasePanel nextPanel)
    {
        if (panelStack.Count > 0)
        {
            panel = panelStack.Peek();
            panel.OnPause();
        }
        panelStack.Push(nextPanel);
        GameObject spawnUI = uiManager.GetSingleUI(nextPanel.UIType);
        nextPanel.Initialize(this);
        nextPanel.Initialize(new UIManager(spawnUI));
        nextPanel.OnEnter();
    }

    public void PopUI()
    {
        if (panelStack.Count > 0)
        {
            panelStack.Pop().OnExit();
        }

        if (panelStack.Count > 0)
        {
            panelStack.Peek().OnResume();
        }
    }

    public void PopAll()
    {
        while (panelStack.Count > 0)
        {
            panelStack.Pop().OnExit();
        }
    }
}
