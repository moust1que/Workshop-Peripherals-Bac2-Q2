using System;
using UnityEngine;
using UnityEngine.UIElements;


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
            CameraController cameraController = UnityEngine.Object.FindFirstObjectByType<CameraController>();
            if (cameraController != null){
                cameraController.score += Mathf.RoundToInt(score);
                Debug.Log("Score : " + cameraController.score);
                UIContoller uiContoller = UnityEngine.Object.FindFirstObjectByType<UIContoller>();
                uiContoller.UpdateScoreUI();
            }
        }
    }

    public void ChangePosition()
    {
        float randomX = Mathf.Clamp(UnityEngine.Random.Range(-4,15), -3f, 10f);
        float randomZ = Mathf.Clamp(UnityEngine.Random.Range(0,15), 0f, 10f);
        Vector3 newPosition = new Vector3(randomX,1.5f,randomZ);
        transform.position = newPosition;


        WindManager windManager = UnityEngine.Object.FindFirstObjectByType<WindManager>();
        if (windManager != null)
        {
            windManager.SetRandomWind();
        }

    }
}
