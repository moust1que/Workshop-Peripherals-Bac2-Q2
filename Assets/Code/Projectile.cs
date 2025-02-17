using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;       
    public float lifeTime = 5f;     

    private Rigidbody rb;
    
    public void InitialiserDirection(Vector3 direction)
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.linearVelocity = direction.normalized * speed;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
