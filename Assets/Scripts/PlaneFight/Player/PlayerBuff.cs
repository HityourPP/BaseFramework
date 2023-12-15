using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlaneFight
{
    public class PlayerBuff : MonoBehaviour
    {
        private Player _player;
        private string buffName;
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = gameObject.GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            rb.velocity = -transform.up * 3;
            Invoke(nameof(DestroySelf), 30f);
        }

        private void DestroySelf()
        {
            PoolManager.GetInstance().AddGameObject(gameObject.name, gameObject);
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _player = GameManager.Instance.player.GetComponent<Player>();
                int randomNum = Random.Range(0, 8);
                switch (randomNum)
                {
                    case <= 2: 
                        AddSpeed();
                        buffName = "Add Speed";
                        break;
                    case <= 4:
                        AddBulletNum();
                        buffName = "Add Bullet Num";
                        break;
                    case <= 6:
                        AddFireRate();
                        buffName = "Add Fire Rate";
                        break;
                    case < 8:
                        AddHealth();
                        buffName = "Add Health";
                        break;
                }
                CancelInvoke(nameof(DestroySelf));
                PoolManager.GetInstance().AddGameObject(gameObject.name, gameObject);
                Debug.Log(randomNum + buffName);
                EventManager.GetInstance().EventTrigger("BuffName", buffName);
            }
        }

        private void AddSpeed()
        {
            _player.moveSpeed += 1f;
        }   
        
        private void AddBulletNum()
        {
            _player.bulletNum++;
        }

        private void AddFireRate()
        {
            _player.shootCoolTime = Mathf.Max(_player.shootCoolTime - 0.05f, 0.1f);
        }

        private void AddHealth()
        {
            GameManager.Instance.ChangeHealth(1);
        }
    }
}

