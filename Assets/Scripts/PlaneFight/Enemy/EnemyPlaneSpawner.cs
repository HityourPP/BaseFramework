using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlaneFight
{
    public class EnemyPlaneSpawner : MonoBehaviour
    {
        private const string AbName = "planefight";
        private const string ResName = "Enemy";
        public int amountOfRes;
        public List<string> enemyColor = new List<string>();
        public float spawnTimer;

        private int randomColor, num;
        private void Start()
        {
            StartCoroutine(SpawnEnemyPlane2());
        }

        IEnumerator SpawnEnemyPlane1()
        {
            while (true)
            {
                randomColor = Random.Range(0, enemyColor.Count);
                num = Random.Range(1, amountOfRes + 1);
                for (int i = 1; i < 5; i++)
                {
                    GameObject plane1 = PoolManager.GetInstance()
                        .GetGameObject(AbName, ResName + enemyColor[randomColor] + num);           
                    GameObject plane2 = PoolManager.GetInstance()
                        .GetGameObject(AbName, ResName + enemyColor[randomColor] + num);
                    plane1.transform.position = transform.position - new Vector3(1.5f * i, 0, 0);
                    plane2.transform.position = transform.position + new Vector3(1.5f * i, 0, 0);
                }

                yield return new WaitForSeconds(1f);
            
                randomColor = Random.Range(0, enemyColor.Count);
                num = Random.Range(1, amountOfRes + 1);
                for (int i = 1; i < 3; i++)
                {
                    GameObject plane1 = PoolManager.GetInstance()
                        .GetGameObject(AbName, ResName + enemyColor[randomColor] + num);           
                    GameObject plane2 = PoolManager.GetInstance()
                        .GetGameObject(AbName, ResName + enemyColor[randomColor] + num);
                    plane1.transform.position = transform.position - new Vector3(2.5f * i, 0, 0);
                    plane2.transform.position = transform.position + new Vector3(2.5f * i, 0, 0);
                }
                yield return new WaitForSeconds(5f);
            }
        }
        IEnumerator SpawnEnemyPlane2()
        {
            while (true)
            {
                randomColor = Random.Range(0, enemyColor.Count);
                num = Random.Range(1, amountOfRes + 1);

                GameObject plane = PoolManager.GetInstance()
                    .GetGameObject(AbName, ResName + enemyColor[randomColor] + num);
                plane.transform.position = transform.position;
                
                yield return new WaitForSeconds(1f);
                
                for (int i = 1; i < 2; i++)
                {
                    GameObject plane1 = PoolManager.GetInstance()
                        .GetGameObject(AbName, ResName + enemyColor[randomColor] + num);           
                    GameObject plane2 = PoolManager.GetInstance()
                        .GetGameObject(AbName, ResName + enemyColor[randomColor] + num);
                    plane1.transform.position = transform.position - new Vector3(4f * i, 0, 0);
                    plane2.transform.position = transform.position + new Vector3(4f * i, 0, 0);
                }    
                
                yield return new WaitForSeconds(1f);
                
                for (int i = 1; i < 3; i++)
                {
                    GameObject plane1 = PoolManager.GetInstance()
                        .GetGameObject(AbName, ResName + enemyColor[randomColor] + num);           
                    GameObject plane2 = PoolManager.GetInstance()
                        .GetGameObject(AbName, ResName + enemyColor[randomColor] + num);
                    plane1.transform.position = transform.position - new Vector3(3f * i, 0, 0);
                    plane2.transform.position = transform.position + new Vector3(3f * i, 0, 0);
                }   
                
                yield return new WaitForSeconds(1f);
                
                for (int i = 1; i < 4; i++)
                {
                    GameObject plane1 = PoolManager.GetInstance()
                        .GetGameObject(AbName, ResName + enemyColor[randomColor] + num);           
                    GameObject plane2 = PoolManager.GetInstance()
                        .GetGameObject(AbName, ResName + enemyColor[randomColor] + num);
                    plane1.transform.position = transform.position - new Vector3(2.5f * i, 0, 0);
                    plane2.transform.position = transform.position + new Vector3(2.5f * i, 0, 0);
                }               
                
                yield return new WaitForSeconds(1f);
                
                for (int i = 1; i < 6; i++)
                {
                    GameObject plane1 = PoolManager.GetInstance()
                        .GetGameObject(AbName, ResName + enemyColor[randomColor] + num);           
                    GameObject plane2 = PoolManager.GetInstance()
                        .GetGameObject(AbName, ResName + enemyColor[randomColor] + num);
                    plane1.transform.position = transform.position - new Vector3(2f * i, 0, 0);
                    plane2.transform.position = transform.position + new Vector3(2f * i, 0, 0);
                }
                
                yield return new WaitForSeconds(5f);
            }
        }
    }
}
