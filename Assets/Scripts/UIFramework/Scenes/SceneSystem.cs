using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSystem : MonoBehaviour
{
    private BaseScene baseScene;

    public void SetSceneState(BaseScene scene)
    {
        baseScene?.OnExit();
        baseScene = scene;
        baseScene?.OnEnter();
    }
}
