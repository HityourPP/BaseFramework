using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlaneFight
{
    public class GameManager : MonoBehaviour
    {
        private const string AbName = "planefight";
        public static GameManager Instance { get; private set; }
        public float score;
        public int health;
        [SerializeField] private float invincibleTime = 3f;
        [SerializeField] private float respawnTime = 2f;
        public GameObject player;
        private GameObject meteorSpawner;
        private GameObject enemyPlaneSpawner;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;
            score = 0;
            health = 3;
        }

        private void Start()
        {
            player = PoolManager.GetInstance().GetGameObject(AbName, "Player");
            meteorSpawner = PoolManager.GetInstance().GetGameObject(AbName, "MeteorSpawner");
            enemyPlaneSpawner = PoolManager.GetInstance().GetGameObject(AbName, "EnemyPlaneSpawner");
            player.transform.position = Vector3.zero;
            meteorSpawner.transform.position = Vector3.zero;
            enemyPlaneSpawner.transform.position = new Vector3(0f, 15f, 0f);
            EventManager.GetInstance().AddEventListener("PlayerDead", () =>
            {
                health--;
                if (health < 0)
                {
                    Debug.Log("GameOver");
                }
                else
                {
                    Invoke(nameof(RespawnPlayer), respawnTime);
                }
            });
        }

        private void Update()
        {
            
        }

        private void RespawnPlayer()
        {
            player = PoolManager.GetInstance().GetGameObject(AbName, "Player");
            player.gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");
            Invoke(nameof(TurnOnCollision), invincibleTime);//一段时间后将图层转换回来
        }

        private void TurnOnCollision()
        {
            player.gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }
}


