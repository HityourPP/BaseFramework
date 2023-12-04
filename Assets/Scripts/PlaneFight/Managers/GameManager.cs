using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlaneFight
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public int score;
        public int health;

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

        private void Update()
        {
            
        }
        
    }
}

