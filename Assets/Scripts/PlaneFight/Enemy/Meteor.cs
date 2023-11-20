using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlaneFight
{
    public class Meteor : MonoBehaviour
    {
        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        private GameObject explosionEffect;
        [SerializeField] private Sprite[] sprites;

        public float speed;
        public float size;
        public float minSize = 0.5f;
        public float maxSize = 3.5f;
        private float currentHealth;
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
            currentHealth = size * 2;
            transform.localScale = Vector3.one * size;
            rb.mass = size;         //让质量与大小匹配
        }
        /// <summary>
        /// 向对应移动方向施加力，质量越大，速度越小
        /// </summary>
        /// <param name="direction"></param>
        private void SetTrajectory(Vector2 direction)
        {
            rb.AddForce(direction * speed);
            Invoke(nameof(DestroySelf), 30f);
        }

        private void DestroySelf()
        {
            PoolManager.GetInstance().AddGameObject(gameObject.name, gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                currentHealth -= 1;
                if (currentHealth <= 0)
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
}

