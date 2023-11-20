using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PlaneFight
{
    public class ExplosionEffect : MonoBehaviour
    {
        private void OnEnable()
        {
            Invoke(nameof(DestroySelf), 1f);
        }

        private void DestroySelf()
        {
            PoolManager.GetInstance().AddGameObject(gameObject.name, gameObject);
        }
    }
}

