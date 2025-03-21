using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("Direção")]
    [SerializeField] private float minAngle = -45f;
    [SerializeField] private float maxAngle = 45f;
    [SerializeField] private float directionSpeed = 2f;
    
    [Header("Força")]
    [SerializeField] private float minForce = 5f;
    [SerializeField] private float maxForce = 15f;
    [SerializeField] private float forceSpeed = 3f;
    
    [Header("Referências")]
    [SerializeField] private Transform arrow; // Objeto da seta
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform launchPoint; // Ponto de spawn do projétil
    [SerializeField] private ScoreManager scoreManager;

    
    [Header("Projectiles")]
    public int amountOfProjectiles;
    public Transform spawnProjectilePoint;
    public Vector3 spawnOffset;
    private List<GameObject> projectiles = new List<GameObject>();
    
    private bool directionLocked = false;
    private float currentAngle;
    private float currentForce;
    private bool isCharging = false;
    private bool hasMoreProjectiles = false;

    private void Start()
    {
        for (int i = 0; i < amountOfProjectiles; i++)
        {
            var spawnPoint = spawnProjectilePoint.position + spawnOffset * i;
            
            projectiles.Add(Instantiate(projectilePrefab, spawnPoint, Quaternion.identity));
        }
        
        SetNextProjectile();
    }

    private void Update()
    {
        if (!hasMoreProjectiles)
        {
            return;
        }
        
        if (!directionLocked)
        {
            // Oscila a direção da seta
            float t = Mathf.PingPong(Time.time * directionSpeed, 1);
            currentAngle = Mathf.Lerp(minAngle, maxAngle, t);
            arrow.rotation = Quaternion.Euler(0, 0, currentAngle);
        }
        else if (isCharging)
        {
            // Oscila a força do lançamento
            float t = Mathf.PingPong(Time.time * forceSpeed, 1);
            currentForce = Mathf.Lerp(minForce, maxForce, t);
            arrow.localScale = new Vector3(1, 1 + (currentForce / maxForce), 1);
        }

        // Captura input do mouse
        if (Input.GetMouseButtonDown(0))
        {
            if (!directionLocked)
            {
                directionLocked = true; // Trava a direção e começa a carregar força
                isCharging = true;
            }
        }

        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            isCharging = false;
            LaunchProjectile();
        }
    }

    private void LaunchProjectile()
    {
        
        var projectile = projectiles[projectiles.Count - 1];
        projectiles.RemoveAt(projectiles.Count - 1);
        
        projectile.transform.position = launchPoint.position;
        
        // Ativa o colisor
        projectile.GetComponent<Collider2D>().enabled = true;
        
        // aplica força no projectile
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        Vector2 launchDirection = (Quaternion.Euler(0, 0, currentAngle) * Vector2.up).normalized;
        projectileRb.AddForce(launchDirection * currentForce, ForceMode2D.Impulse);
        
        // Inscreve o evento para contar os pontos
        projectile.GetComponent<Almondega>().OnHitPanela += scoreManager.AddScore;

        // Reseta para nova jogada
        directionLocked = false;
        arrow.localScale = Vector3.one;
        
        SetNextProjectile();
    }

    private void SetNextProjectile()
    {
        if (projectiles.Count <= 0)
        {
            hasMoreProjectiles = false;
            return;
        }
        hasMoreProjectiles = true;
        var projectile = projectiles[projectiles.Count - 1];
        projectile.transform.position = launchPoint.position;

        projectile.GetComponent<TrailRenderer>().enabled = true;
    }
}
