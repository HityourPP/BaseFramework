using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlaneFight
{
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
            Invoke(nameof(DestroySelf), lifeTime);
        }

        private void DestroySelf()
        {
            PoolManager.GetInstance().AddGameObject(gameObject.name, gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            //碰撞后要取消函数启用
            CancelInvoke(nameof(DestroySelf));
            DestroySelf();
        }
    }
}
