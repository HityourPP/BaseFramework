using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private PanelManager panelManager;
    private void Awake()
    {
        Instance = this;
        panelManager = new PanelManager();
    }

    private void Start()
    {
        panelManager.PushUI(new StartPanel());
    }
}
