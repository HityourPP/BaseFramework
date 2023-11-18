using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 3f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        transform.position += transform.up * Time.fixedDeltaTime * speed;
    }

    private void OnEnable()
    {
        // rb.velocity = transform.up * speed;
        Invoke(nameof(DestroySelf), lifeTime);
        // Destroy(gameObject, lifeTime);
    }

    private void DestroySelf()
    {
        PoolManager.GetInstance().AddGameObject(gameObject.name, gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }
}
