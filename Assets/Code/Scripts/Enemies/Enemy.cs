using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    protected int currentHealth;
    [SerializeField] protected int damageToPlayer;
    
    public bool isDead {get; protected set;}
    public bool isAttacking {get; protected set;}

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
        Destroy(gameObject, 0.5f);
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(gameObject.name + " hit " + damageToPlayer + "damage to player.");
        }
            
    }
}
