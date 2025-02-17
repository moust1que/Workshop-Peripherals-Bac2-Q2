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

    void FixedUpdate()
    {
        
        if(rb != null && rb.linearVelocity.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(rb.linearVelocity);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bleu")){
            addScore(6);
        }else if (collision.gameObject.CompareTag("Rouge")){
            addScore(8);
        }else if (collision.gameObject.CompareTag("Jaune")){
            addScore(10);
        }else if (collision.gameObject.CompareTag("Noir")){
            addScore(2);
        }
        
        Destroy(gameObject, 0.5f);
    }

    void addScore(int points)
    {
        CameraController cameraController = Object.FindFirstObjectByType<CameraController>();
        cameraController.score += points;
        Debug.Log("Score actuel : " + cameraController.score);
    }
}
