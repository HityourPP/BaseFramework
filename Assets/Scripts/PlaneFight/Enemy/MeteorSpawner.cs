using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlaneFight
{
    public class MeteorSpawner : MonoBehaviour
    {
        private int spawnNum = 1;
        public int amountOfRes;
        public float spawnDistance;
        public float trajectoryOffset;   //轨迹偏移量
        public float spawnTimer;
        private Vector3 spawnDirection;

        private void Start()
        {
            InvokeRepeating(nameof(SpawnMeteor), spawnTimer, spawnTimer);
        }

        private void SpawnMeteor()
        {
            float gameTime = Time.time;
            spawnNum = Mathf.Max(1, (int)gameTime / 100);
            for (int i = 0; i < spawnNum; i++)
            {
                float spawnX, spawnY;
                do
                {
                    spawnDirection = Random.insideUnitCircle.normalized * spawnDistance;
                    spawnX = Mathf.Abs(spawnDirection.x);
                    spawnY = Mathf.Abs(spawnDirection.y);
                } while (spawnX < 12f && spawnY < 8f);
                //以生成器为中心
                spawnDirection = transform.position + spawnDirection;
                //设置角度偏移
                float offset = Random.Range(-trajectoryOffset, trajectoryOffset);
                Quaternion rotation = Quaternion.AngleAxis(offset, - Vector3.forward);
                Meteor meteor = PoolManager.GetInstance()
                    .GetGameObject("planefight", "Meteor_Big" + Random.Range(1, amountOfRes + 1))
                    .GetComponent<Meteor>();
                meteor.gameObject.transform.position = spawnDirection;
                meteor.size = Random.Range(meteor.minSize, meteor.maxSize);
                meteor.InitValue();
                meteor.SetTrajectory(rotation * - spawnDirection);   
            }
        }
    }
}

