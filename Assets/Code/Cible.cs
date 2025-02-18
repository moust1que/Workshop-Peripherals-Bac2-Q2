using UnityEngine;

public class Cible : MonoBehaviour
{
    public Transform targetCenter; 
    public float maxScore = 10f;  
    public float maxDistance = 3f; 

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Vector3 hitPoint = collision.contacts[0].point;
            float distance = Vector3.Distance(hitPoint, targetCenter.position); 

            float score = Mathf.Max(0, maxScore - (distance / maxDistance) * maxScore); 
            CameraController cameraController = Object.FindFirstObjectByType<CameraController>();
            if (cameraController != null)
            {
                cameraController.score += Mathf.RoundToInt(score);
                Debug.Log("Score : " + cameraController.score);
            }
        }
    }
}
