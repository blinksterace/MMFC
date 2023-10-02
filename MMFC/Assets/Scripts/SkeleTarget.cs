using UnityEngine;

public class SkeleTarget : MonoBehaviour
{
    public float health = 50f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
        else
        {
            // Disable Rigidbody component when hit by a raycast
            GetComponent<Rigidbody>().isKinematic = true;
            Invoke("EnableRigidbody", 0.5f); // Re-enable Rigidbody after a short delay
        }
    }

    void EnableRigidbody()
    {
        // Re-enable Rigidbody component after a short delay
        GetComponent<Rigidbody>().isKinematic = false;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
