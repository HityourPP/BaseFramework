using System;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        MainMenuScene,
        LoadingScene,
        GameScene,
        CharacterSelectScene
    }

    public static Scene targetScene;

    public static void Load(Scene targetScene)
    {
        Loader.targetScene = targetScene;
        //先加载到LoadingScene中
        SceneManager.LoadScene(Loader.Scene.LoadingScene.ToString());
    }

    public static void LoaderCallBack()
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
