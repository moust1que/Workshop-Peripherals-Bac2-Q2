using UnityEngine;
using TMPro;

public class UIContoller : MonoBehaviour
{
    
    public TextMeshProUGUI scoreText;

    void Update()
    {
        CameraController cameraController = UnityEngine.Object.FindFirstObjectByType<CameraController>();
        // scoreText.text = "Score: " + CameraController.score.ToString();
    
    }
}
