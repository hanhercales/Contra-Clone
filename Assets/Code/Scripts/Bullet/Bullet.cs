using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f; // Bullet disappears after this time
    [SerializeField] private int damage = 1;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Bullet hit an enemy!");
            Destroy(gameObject);
        }
        // If it hits a wall/obstacle
        else if (other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}