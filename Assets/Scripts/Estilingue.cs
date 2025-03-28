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
    
    [Header("Projectiles")]
    public int amountOfProjectiles;
    public Transform spawnProjectilePoint;
    public Vector3 spawnOffset;
    private List<GameObject> projectilesToLaunch = new List<GameObject>();
    private bool _isProjectileFlying;
    
    private bool directionLocked = false;
    private float currentAngle;
    private float currentForce;
    private bool isCharging = false;
    private bool hasMoreProjectiles = false;

    private float directionProgress = 0f;
    private float forceProgress = 0f;

    private GameManager gameManager;

    private void Start()
    {

        gameManager = GameManager.Instance;
        
        for (int i = 0; i < amountOfProjectiles; i++)
        {
            var spawnPoint = spawnProjectilePoint.position + spawnOffset * i;
            
            projectilesToLaunch.Add(Instantiate(projectilePrefab, spawnPoint, Quaternion.identity, this.transform));
        }
        
        SetNextProjectile();
    }

    private void Update()
    {
        if (gameManager.isGameOver)
        {
            return;
        }
        
        if (_isProjectileFlying)
        {
            return;
        }

        if (!hasMoreProjectiles)
        {
            Debug.Log("Fim de jogo!");
            gameManager.FinishLevel();
            return;
        }
        
        if (!directionLocked)
        {
            // Oscila a direção da seta
            float t = Mathf.PingPong(directionProgress, 1);
            currentAngle = Mathf.Lerp(minAngle, maxAngle, t);
            arrow.rotation = Quaternion.Euler(0, 0, currentAngle);
            directionProgress+= Time.deltaTime * directionSpeed;
        }
        else if (isCharging)
        {
            // Oscila a força do lançamento
            float t = Mathf.PingPong(forceProgress, 1);
            currentForce = Mathf.Lerp(minForce, maxForce, t);
            arrow.localScale = new Vector3(1, 1 + (currentForce - minForce) / (maxForce - minForce), 1);
            forceProgress += Time.deltaTime * forceSpeed;
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
        var projectile = projectilesToLaunch[projectilesToLaunch.Count - 1];
        projectilesToLaunch.RemoveAt(projectilesToLaunch.Count - 1);
        _isProjectileFlying = true;
        
        var almondega = projectile.GetComponent<Almondega>();

        almondega.OnHitSomething += SetNextProjectile;
        
        // aplica força no projectile
        Vector2 launchDirection = (Quaternion.Euler(0, 0, currentAngle) * Vector2.up).normalized;
        almondega.Launch(launchDirection, currentForce);
        
        // Esconde a seta
        arrow.localScale = Vector3.zero;

    }
    

    private void SetNextProjectile()
    {
        _isProjectileFlying = false;
        
        if (projectilesToLaunch.Count <= 0)
        {
            hasMoreProjectiles = false;
            return;
        }
        
        forceProgress = 0;
        directionLocked = false;
        arrow.localScale = Vector3.one;

        hasMoreProjectiles = true;
        var projectile = projectilesToLaunch[projectilesToLaunch.Count - 1];
        projectile.transform.position = launchPoint.position;

        
    }
}
