using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private int damage = 1;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // NEW
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("Player bullet hit " + other.name + " for " + damage + " damage!");
            }
            // NEW
            Destroy(gameObject);
        }
        
        else if (other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
