using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    // This method is called automatically when the bullet collides with another collider.
    private void OnCollisionEnter(Collision collision)
    {
        // Try to get the ZombieAI component from the collision or its parent.
        ZombieAI zombie = collision.transform.GetComponentInParent<ZombieAI>();
        if (zombie != null)
        {
            // If a zombie is hit, trigger its kill function.
            zombie.Kill();

            // Optionally, destroy the bullet immediately after hitting a zombie.
            Destroy(gameObject);
        }
    }
}
