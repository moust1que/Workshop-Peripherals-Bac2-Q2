using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;       
    public float lifeTime = 10.5f;

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

        WindManager windManager = UnityEngine.Object.FindFirstObjectByType<WindManager>();
        if (windManager != null)
        {
            Vector3 windDir = windManager.GetCurrentWindDirection();
            float windForce = windManager.GetCurrentWindStrength();
            ApplyWind(windDir, windForce);
        }
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Stopper")) 
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                Debug.Log("ChangePosition ");
                StartCoroutine(WaitAndChangePosition());
            }
        }
    }

    IEnumerator WaitAndChangePosition()
    {
        yield return new WaitForSeconds(2f); 
        Cible cibleRef = UnityEngine.Object.FindFirstObjectByType<Cible>();
        if (cibleRef != null)
        {
            cibleRef.ChangePosition();
        }
    }

    public void ApplyWind(Vector3 wind, float strength)
    {
        if (rb != null)
        {
            rb.AddForce(wind * strength, ForceMode.Force);
            Debug.Log("Vent appliqué : " + wind + " avec force : " + strength);
        }
    }
}
