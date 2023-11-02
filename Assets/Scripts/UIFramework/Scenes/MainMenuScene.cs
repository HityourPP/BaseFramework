using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScene : BaseScene
{
    private PanelManager panelManager;
    public override void OnEnter()
    {
        panelManager = new PanelManager();
        if (SceneManager.GetActiveScene().name != Loader.Scene.MainMenuScene.ToString())
        {
            Loader.Load(Loader.Scene.MainMenuScene);    
            // SceneManager.LoadScene(sceneName);
            SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
        }
        else
        {
            panelManager.PushUI(new MainPanel());
        }
    }

    private void SceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == Loader.Scene.MainMenuScene.ToString())
        {
            panelManager.PushUI(new MainPanel());
        }
    }

    public override void OnExit()
    {
        Debug.Log("取消订阅");
        SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
        panelManager.PopAll();
    }
}
