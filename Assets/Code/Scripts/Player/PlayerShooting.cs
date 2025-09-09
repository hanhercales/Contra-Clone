using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    // Shooting variables
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform topBulletSpawn;
    [SerializeField] private Transform bottomBulletSpawn;
    [SerializeField] private Transform horizontalBulletSpawn;
    
    // Bullet variables
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private float bulletSpeed = 10f;

    // Player Movement
    private PlayerMovement playerMovement;
    // private PlayerStateMachine playerStateMachine;

    // Shooting reload
    private float nextFireTime; // Reload time

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerMovement.isShooting && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null)
        {
            Debug.LogWarning(("Bullet Prefab is missing or not set."));
            return;
        }

        // Determine bullet direction
        Vector2 shootDirection = GetBulletDirection();
        // Decide the axis
        Transform currentBulletSpawn = GetBulletSpawn(shootDirection);
        if (currentBulletSpawn == null)
        {
            Debug.LogWarning(("Bullet spawn is missing or not set."));
            return;
        }
        
        // Create the bullet
        GameObject bullet = Instantiate(bulletPrefab, currentBulletSpawn.position, Quaternion.identity);
        
        // Provide it speed
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        if (bulletRB != null)
        {
            bulletRB.velocity = shootDirection  * bulletSpeed;
        }
        else
        {
            Debug.LogWarning(("Bullet Prefab is missing a Rigidbody2D."));
        }
    }

    private Transform GetBulletSpawn(Vector2 direction)
    {
        if (direction == Vector2.up) return topBulletSpawn;
        if (direction == Vector2.down) return bottomBulletSpawn;
        return horizontalBulletSpawn;
    }
    
    private Vector2 GetBulletDirection()
    {
        Vector2 direction = Vector2.zero;

        if (playerMovement.isAimingUp)
        {
            direction = Vector2.up;
        }
        else if (playerMovement.isAimingDown)
        {
            direction = Vector2.down;
        }
        else
        {
            direction = playerMovement.isFacingRight ?  Vector2.right : Vector2.left;
        }
        return direction;
    }
}
