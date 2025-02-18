using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;       
    public float lifeTime = 5f;

    public float windStrength = 100f;

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
        WindDirection selectedWind = GetRandomWindDirection();
        ApplyWind(GetWindVector(selectedWind));
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
        yield return new WaitForSeconds(2f); // Attend 2 secondes
        Cible cibleRef = UnityEngine.Object.FindFirstObjectByType<Cible>();
        if (cibleRef != null)
        {
            cibleRef.ChangePosition();
        }
    }

    // void WindDirection()
    // {
    //     float randomX = UnityEngine.Random.Range(-1f, 1f); // -1 à 1
    //     float randomZ = UnityEngine.Random.Range(-1f, 1f); // -1 à 1
    //     Vector3 windDirection = new Vector3(randomX, 0, randomZ).normalized; // Direction normalisée
    //     ApplyWind(windDirection);
    // }

    // public void ApplyWind(Vector3 wind)
    // {
    //     Rigidbody rb = GetComponent<Rigidbody>(); // Ton carreau doit avoir un Rigidbody
    //     if (rb != null)
    //     {
    //         rb.AddForce(wind * windStrength, ForceMode.Force); // Applique la force du vent
    //     }
    // } 

    WindDirection GetRandomWindDirection()
    {
        WindDirection[] directions = (WindDirection[])System.Enum.GetValues(typeof(WindDirection));
        return directions[UnityEngine.Random.Range(0, directions.Length)];
    }

    public enum WindDirection
    { 
        North,
        South,
        East,
        West,
        NorthEast,
        NorthWest,
        SouthEast,
        SouthWest
    }


    Vector3 GetWindVector(WindDirection direction)
    {
        switch (direction)
        {
            case WindDirection.North:
                return Vector3.forward;
            case WindDirection.South:
                return Vector3.back;
            case WindDirection.East:
                return Vector3.right;
            case WindDirection.West:
                return Vector3.left;
            case WindDirection.NorthEast:
                return (Vector3.forward + Vector3.right).normalized;
            case WindDirection.NorthWest:
                return (Vector3.forward + Vector3.left).normalized;
            case WindDirection.SouthEast:
                return (Vector3.back + Vector3.right).normalized;
            case WindDirection.SouthWest:
                return (Vector3.back + Vector3.left).normalized;
            default:
                return Vector3.zero;
        }
    }

    public void ApplyWind(Vector3 wind)
    {
        if (rb != null)
        {
            rb.AddForce(wind * windStrength, ForceMode.Force);
            Debug.Log("Vent appliqué : " + wind);
        }
    }
}
