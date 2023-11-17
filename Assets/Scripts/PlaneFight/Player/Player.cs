using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlaneFight
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Camera mainCamera;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float shootCoolTime;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private Transform shootPos;
        private GameObject playerBullet;
        private float shootStartTime;
    
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            shootStartTime = Time.time;
            mainCamera = Camera.main;
            playerBullet = AssetBundlesManager.GetInstance().LoadResource<GameObject>("planefight", "Bullet");
        }
    
        private void Update()
        {
            Aim();
            Shoot();
        }
    
        private void FixedUpdate()
        {
            MoveController();
        }
    
        private void MoveController()
        {
            Vector2 moveDir = InputManager.Instance.GetMoveDirection();
            rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
        }
    
        private void Aim()
        {
            Vector2 shootDir = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
        }
        private void Shoot()
        {
            if (Time.time > shootStartTime + shootCoolTime)
            {
                if (Input.GetMouseButton(0))
                {
                    Instantiate(playerBullet, shootPos.position, transform.rotation);
                    shootStartTime = Time.time;
                }
            }
        }
    }
}
