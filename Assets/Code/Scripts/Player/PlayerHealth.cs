using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    public int currentHealth;

    [SerializeField] private float invincibilityDuration = 2f;
    private float invincibilityTimer;
    private bool isInvincible;
    
    private PlayerMovement playerMovement;
    private Collider2D playerCollider;

    void Awake()
    {
        currentHealth = maxHealth;
        playerMovement = GetComponent<PlayerMovement>();
        playerCollider = GetComponent<Collider2D>();
        invincibilityTimer = 0f;
        isInvincible = false;
    }

    void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            playerMovement.isHurt = false;
        }
        playerMovement.isDead = (currentHealth <= 0);
    }

    public void TakeDamage(int damageAmount)
    {
        if (isInvincible || playerMovement.isDead) return;
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            {
                isInvincible = true;
                invincibilityTimer = invincibilityDuration;
                playerMovement.isHurt = true;
                Debug.Log("Player is now invincible for " + invincibilityDuration + " seconds.");
            }
        }
    }

    void Die()
    {
        Debug.Log("Player died!");
        playerMovement.isDead = true;
        playerMovement.isHurt = false;
        
        playerMovement.enabled = false;
        GetComponent<PlayerShooting>().enabled = false;
        if (playerCollider != null) playerCollider.enabled = false;
    }
    
}
