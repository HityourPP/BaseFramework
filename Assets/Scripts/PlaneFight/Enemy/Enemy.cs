using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlaneFight
{
    public class Enemy : MonoBehaviour
    {
        private float lifeTime = 10f;
        private GameObject explosionEffect;
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            rb.velocity = -transform.up;
            Invoke(nameof(DestroySelf), lifeTime);
        }

        private void DestroySelf()
        {
            PoolManager.GetInstance().AddGameObject(gameObject.name, gameObject);
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                explosionEffect = PoolManager.GetInstance().GetGameObject("planefight", "ExplosionEffect");
                explosionEffect.transform.position = transform.position;
                explosionEffect.GetComponent<ParticleSystem>().Play();
                CancelInvoke(nameof(DestroySelf));
                DestroySelf();
            }
        }
    }
}
