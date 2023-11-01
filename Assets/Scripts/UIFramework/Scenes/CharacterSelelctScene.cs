using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelelctScene : BaseScene
{
    private PanelManager panelManager;
    public override void OnEnter()
    {
        panelManager = new PanelManager();
        if (SceneManager.GetActiveScene().name != Loader.Scene.CharacterSelectScene.ToString())
        {
            Loader.Load(Loader.Scene.CharacterSelectScene);
            SceneManager.sceneLoaded += SceneManagerOnLoaded;
        }
        else
        {
            panelManager.PushUI(new CharacterSelectPanel());
        }
        Debug.LogError(panelManager.panelStack.Count);
    }

    private void SceneManagerOnLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == Loader.Scene.CharacterSelectScene.ToString())
        {
            panelManager.PushUI(new CharacterSelectPanel()); 
        }
    }

    public override void OnExit()
    {   
        //退出场景需要取消订阅事件，并且将生成的UI删去
        SceneManager.sceneLoaded -= SceneManagerOnLoaded;
        panelManager.PopAll();
    }
}
