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

    private float nextFireTime;
    private bool isFacingRight = true;
    
    protected override void Awake()
    {
        base.Awake();
        if (playerTarget == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) playerTarget = playerObj.transform;
            else Debug.LogWarning("Player not found for EnemyShooter: " + gameObject.name);
        }
        nextFireTime = Time.time;
        
        isFacingRight = initialFacingDirection > 0;
        Vector3 currentScale = transform.localScale;
        if (isFacingRight && currentScale.x < 0) currentScale.x *= -1;
        if (!isFacingRight && currentScale.x > 0) currentScale.x *= -1;
        transform.localScale = currentScale;
    }

    void Update()
    {
        if (isDead) return;
        isAttacking = false; 

        if (playerTarget == null || bulletSpawn == null || enemyBulletPrefab == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTarget.position);

        if (distanceToPlayer <= detectionRange)
        {
            isAttacking = true;
            if (Time.time >= nextFireTime)
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
            float dirX = playerTarget.position.x > transform.position.x ? 1f : -1f;
            shootDirection = new Vector2(dirX, 0).normalized;
        }

        GameObject bullet = Instantiate(enemyBulletPrefab, bulletSpawn.position, Quaternion.identity);
    
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        if (bulletRb != null)
        {
            bulletRb.velocity = shootDirection * bulletSpeed;
        }
        else
        {
            Debug.LogWarning("Enemy Bullet Prefab is missing a Rigidbody2D!");
        }
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
