using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Vector2 moveSpeed;

    private Material _material;
    private void Awake()
    {
        GetComponent<Renderer>().material = AssetBundlesManager.GetInstance().LoadResource<Material>("planefight", "Background");
        _material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        _material.mainTextureOffset += Time.deltaTime * moveSpeed;
    }
}
