using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    protected int currentHealth;
    [SerializeField] protected int damageToPlayer = 1;

    public bool isDead;
    public bool isAttacking;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
        isDead = false;
        isAttacking = false;
    }
    
    // Take damage
    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;
        
        currentHealth -= damageAmount;
        Debug.Log(gameObject.name + " has been hit " + damageAmount + ". Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        if (isDead) return;
        
        isDead = true;
        Debug.Log(gameObject.name + " died!");
        // New
        GetComponent<Collider2D>().enabled = false;
        if (GetComponent<Rigidbody2D>() != null) GetComponent<Rigidbody2D>().simulated = false;
        // New
        Destroy(gameObject, 0.5f);
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    { // Modified
        if (isDead) return;
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageToPlayer);
                Debug.Log(gameObject.name + " hit " + damageToPlayer + "damage to player.");
            }
        }
    } // Modified
}
