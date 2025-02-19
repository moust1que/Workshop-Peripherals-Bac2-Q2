using UnityEngine;
using TMPro;

public class UIContoller : MonoBehaviour
{
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI windText;
    public RectTransform windArrow;

    public void UpdateScoreUI() {
        CameraController cameraController = UnityEngine.Object.FindFirstObjectByType<CameraController>(); 
        scoreText.text = cameraController.score.ToString();
    }

    public void UpdateWindUI(Vector3 windDirection, float windStrength)
    {
        Debug.Log("UpdateWindUI appelée : Direction = " + windDirection + ", Force = " + windStrength);
        windStrength = windStrength / 8;
        windText.text = windStrength.ToString("F0");

        // Calcul de l'angle pour aligner la flèche avec la direction du vent.
        float angle = Mathf.Atan2(windDirection.x, windDirection.z) * Mathf.Rad2Deg;
        Debug.Log("Angle calculé pour la flèche : " + angle);
        windArrow.rotation = Quaternion.Euler(0, 0, -angle);
    }
}
