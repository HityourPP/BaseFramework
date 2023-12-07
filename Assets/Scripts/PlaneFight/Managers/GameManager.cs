using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

        private float awardTime;
        private float awardScore;
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;
            score = 0;
            health = 3;
            awardTime = 100f;
            awardScore = 50f;
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
                ChangeHealth(-1);
                if (health <= 0)
                {
                    Debug.Log("GameOver");
                    EventManager.GetInstance().EventTrigger("GameOver");
                }
                else
                {
                    Invoke(nameof(RespawnPlayer), respawnTime);
                }
            });
        }

        private void Update()
        {
            Award();
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

        private void Award()
        {
            if (Time.time / awardTime > 1f)
            {
                awardTime = Mathf.Min(awardTime + 150f, 500f);
                ChangeHealth(1);
            }

            if (score > awardScore)
            {
                SpawnBuff();
                awardScore = score + awardScore;
            }
        }

        private void SpawnBuff()
        {
            GameObject buff = PoolManager.GetInstance().GetGameObject("planefight", "Buff");
            buff.transform.position = new Vector3(Random.Range(-5f, 5f), 10f, 0f);
        }

        public void GetScore(float score)
        {
            this.score += score;
            EventManager.GetInstance().EventTrigger("GetScore", this.score);
        }

        public void ChangeHealth(int change)
        {
            health += change;
            EventManager.GetInstance().EventTrigger("ChangeHealth", health);
        }
    }
}


