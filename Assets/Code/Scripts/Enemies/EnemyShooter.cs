using UnityEngine;

public class EnemyShooter : Enemy
{
    [SerializeField] private GameObject enemyBulletPrefab;
    [SerializeField] private Transform bulletSpawn;
    
    [SerializeField] private float fireRate = 1.5f;
    [SerializeField] private float bulletSpeed = 8f;
    [SerializeField] private float detectionRange = 10f;
    
    [SerializeField] private int initialFacingDirection = 1;
    [SerializeField] private bool aimVertically = false;
    
    [SerializeField] private Transform playerTarget;
    [SerializeField] private Transform bulletContainer;
    
    private float nextFireTime;
    private bool isFacingRight = true; // Kept as private for internal use

    protected override void Awake()
    {
        base.Awake();
        if (playerTarget == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) playerTarget = playerObj.transform;
            else Debug.LogWarning("No player target found for " + gameObject.name);
        }
        nextFireTime = Time.time + fireRate;

        // Set initial facing direction and apply scale
        isFacingRight = (initialFacingDirection > 0);
        ApplyFacingDirection(); // Call a helper to apply the initial flip
    }

    void Update()
    {
        if (isDead) return;
        isAttacking = false;
        
        if (playerTarget == null || bulletSpawn == null || enemyBulletPrefab == null) return;
        
        float distanceToPlayer = Vector2.Distance(transform.position, playerTarget.position);
        if (distanceToPlayer < detectionRange)
        {
            isAttacking = true;
            FlipToPlayer(); // New

            if (Time.time > nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void Shoot()
    {
        Vector2 shootDirection;
        if (aimVertically)
        {
            shootDirection = (playerTarget.position - bulletSpawn.position).normalized;
        }
        else
        {
            // Modified
            shootDirection = new Vector2(isFacingRight ? 1f : -1f, 0).normalized;
            // Modified
        }
        
        GameObject bullet = Instantiate(enemyBulletPrefab, bulletSpawn.position, Quaternion.identity);
        
        if (bulletContainer != null)
        {
            bullet.transform.SetParent(bulletContainer);
        }
        else
        {
            Debug.LogWarning("No bullet container found");
        }
        
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();

        if (bulletRB != null)
        {
            bulletRB.velocity = shootDirection * bulletSpeed;
        }
        else
        {
            Debug.LogWarning("Bullet is missing a Rigidbody2D.");
        }
    }

    // New
    private void ApplyFacingDirection()
    {
        Vector3 currentScale = transform.localScale;
        if (isFacingRight && currentScale.x < 0) currentScale.x *= -1; // If should face right but scaled left, flip
        if (!isFacingRight && currentScale.x > 0) currentScale.x *= -1; // If should face left but scaled right, flip
        transform.localScale = currentScale;
    }
    // New

    // New
    private void FlipToPlayer()
    {
        if (playerTarget == null) return;

        bool playerIsToTheRight = playerTarget.position.x > transform.position.x;
        if (isFacingRight != playerIsToTheRight)
        {
            isFacingRight = playerIsToTheRight;
            ApplyFacingDirection();
        }
    }
    // New

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}