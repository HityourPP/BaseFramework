using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PanelManager 
{
    public Stack<BasePanel> panelStack = new Stack<BasePanel>();
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
        uiManager.activePanel = spawnUI;
        nextPanel.Initialize(uiManager);
        nextPanel.OnEnter();
    }

    public void PopUI()
    {
        if (panelStack.Count > 0)
        {
            panelStack.Peek().OnExit();
            panelStack.Pop();
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
