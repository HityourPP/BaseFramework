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
            currentHealth = size;
            transform.localScale = Vector3.one * size;
            rb.mass = size;         //让质量与大小匹配
        }
        /// <summary>
        /// 向对应移动方向施加力，质量越大，速度越小
        /// </summary>
        /// <param name="direction"></param>
        public void SetTrajectory(Vector2 direction)
        {
            rb.AddForce(direction * speed);
            Invoke(nameof(DestroySelf), 60f);
        }

        public void InitValue()
        {
            currentHealth = size;
            transform.localScale = Vector3.one * size;
            rb.mass = size;  
        }

        private void DestroySelf()
        {
            PoolManager.GetInstance().AddGameObject(gameObject.name, gameObject);
        }

        private void CreateSplitMeteor()
        {
            Vector2 position = transform.position;
            //Random.insideUnitCircle获取圆形，半径为1，内的随机点
            position += Random.insideUnitCircle;
            Meteor half =
                PoolManager.GetInstance().GetGameObject("planefight", gameObject.name).GetComponent<Meteor>();
            half.size = size * 0.6f;
            half.gameObject.transform.position = position;
            half.spriteRenderer.sprite = spriteRenderer.sprite;
            half.InitValue();
            //随机选择一个方向 
            half.SetTrajectory(Random.insideUnitCircle.normalized * Mathf.Max(3.5f, 3.5f / size));
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
                    if (size * 0.5f > minSize)
                    {
                        CreateSplitMeteor();
                        CreateSplitMeteor();
                    }
                    DestroySelf();
                }
            }
        }
    }
}

