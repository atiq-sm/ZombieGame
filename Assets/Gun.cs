using UnityEngine;

// This script controls the gun behavior, including shooting bullets.
public class Gun : MonoBehaviour
{
    // Public variables exposed in the Unity Inspector
    public OVRInput.RawButton shootingButton; // The VR controller button used to shoot
    public GameObject bulletPrefab;           // The prefab for the bullet to be instantiated
    public Transform bulletSpawnPoint;        // The point where the bullet will spawn
    public float bulletSpeed = 20f;           // The speed at which the bullet travels
    public AudioSource source;
    public AudioClip clip;
    public float maxLineDistance = 100f;      // (No longer used, but left here if needed later)
    public LayerMask layerMask;               // (No longer used, but left here if needed later)

    // Start is called once before the first frame update
    void Start()
    {
        // Initialization code can go here (currently empty)
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the designated shooting button is pressed down in this frame
        if (OVRInput.GetDown(shootingButton))
        {
            // If the button is pressed, call the Shoot method
            Shoot();
        }
    }

    // Method to handle the shooting logic
    public void Shoot()
    {
        source.PlayOneShot(clip); // Play the shooting sound effect
        Debug.Log("Shoot method called."); // Log message for debugging

        // Instantiate the bullet prefab at the spawn point's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Optional: Rotate the instantiated bullet 90 degrees around its local Y-axis.
        bullet.transform.Rotate(0, 90, 0);

        // Attach the BulletCollision script (if not already present) so the bullet can check for collisions.
        if (bullet.GetComponent<BulletCollision>() == null)
        {
            bullet.AddComponent<BulletCollision>();
        }

        // Get the Rigidbody component attached to the bullet prefab
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        // Check if the Rigidbody component exists
        if (bulletRb != null)
        {
            // Set the bullet's velocity to move forward from the spawn point.
            // We use bulletSpawnPoint.forward to ensure the initial velocity direction matches the gun's forward direction.
            bulletRb.linearVelocity = bulletSpawnPoint.forward * bulletSpeed;
        }
        else
        {
            // Log an error if the bullet prefab is missing the Rigidbody component
            Debug.LogError("Bullet prefab does not have a Rigidbody component.");
        }

        // Destroy the bullet GameObject after 2 seconds to prevent clutter
        Destroy(bullet, 2f);
    }
}
