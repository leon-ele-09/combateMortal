using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public LayerMask enemyLayer;  // Layer to check for enemy collisions
    public float lifetime = 3f;   // Time after which the projectile will disappear (in seconds)
    public float projectileDamage = 6f; // Damage dealt by the projectile

    private void Start()
    {
        // Destroy the projectile after a certain time if it hasn't collided
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the projectile collides with an enemy
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            // Assuming enemies have an EntityData script to handle health
            EntityData enemyData = collision.gameObject.GetComponent<EntityData>();
            if (enemyData != null)
            {
                // Deal damage to the enemy, passing the projectile's position for knockback calculation
                enemyData.reducirVida((int)projectileDamage, transform.position);
            }
        }

        // Destroy the projectile regardless of what it hits
        Destroy(gameObject);
    }
}