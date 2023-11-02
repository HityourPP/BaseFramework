using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : BaseScene
{
    private PanelManager panelManager;

    public override void OnEnter()
    {
        panelManager = new PanelManager();
        if (SceneManager.GetActiveScene().name != Loader.Scene.GameScene.ToString())
        {
            Loader.Load(Loader.Scene.GameScene);
            SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
        }
    }

    private void SceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == Loader.Scene.GameScene.ToString())
        {
            panelManager.PushUI(new GamePanel());
        }
    }

    public override void OnExit()
    {
        SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
        panelManager.PopAll();
    }
}
