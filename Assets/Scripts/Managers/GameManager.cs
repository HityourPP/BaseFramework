using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonAutoMono<GameManager>
{
    public SceneSystem SceneSystem { get; private set; }
    private void Awake()
    {
        SceneSystem = transform.gameObject.AddComponent<SceneSystem>();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneSystem.SetSceneState(new MainMenuScene());
    }
}
