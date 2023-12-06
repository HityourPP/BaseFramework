using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlaneFight
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Camera mainCamera;
        public float moveSpeed;
        public float shootCoolTime;
        public int bulletNum;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private Transform shootPos;
        [SerializeField] private TextMeshProUGUI buffText;
        private GameObject playerBullet;
        private float shootStartTime;
    
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            shootStartTime = Time.time;
            mainCamera = Camera.main;
            playerBullet = AssetBundlesManager.GetInstance().LoadResource<GameObject>("planefight", "Bullet");
        }

        private void Start()
        {
            HideBuffText();
            EventManager.GetInstance().AddEventListener<string>("BuffName",buffName =>
            {
                buffText.text = buffName;
                buffText.gameObject.SetActive(true);
                Invoke(nameof(HideBuffText), 0.25f);
            });
        }

        private void HideBuffText()
        {
            buffText.gameObject.SetActive(false);
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
            Vector2 moveDir = InputManager.GetInstance().GetMoveDirection();
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
                    for (int i = 0; i < bulletNum; i++)
                    {
                        GameObject bullet = PoolManager.GetInstance().GetGameObject("planefight", "Bullet");
                        bullet.transform.Rotate(0, 0, 0);
                        bullet.transform.rotation = transform.rotation;
                        bullet.transform.position = shootPos.position;
                        // Instantiate(playerBullet, shootPos.position, transform.rotation);
                        shootStartTime = Time.time;
                    }    
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Meteor"))
            {
                GameObject explosionEffect = PoolManager.GetInstance().GetGameObject("planefight", "ExplosionEffect");
                explosionEffect.transform.position = transform.position;
                explosionEffect.GetComponent<ParticleSystem>().Play();
                EventManager.GetInstance().EventTrigger("PlayerDead");
                PoolManager.GetInstance().AddGameObject(gameObject.name, gameObject);
            }
        }
    }
}
