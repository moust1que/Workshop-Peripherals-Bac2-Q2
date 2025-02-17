using UnityEngine;
using UnityEngine.InputSystem;
using InputGyro = UnityEngine.InputSystem.Gyroscope;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 10f;
    private InputGyro gyro;

    void Start()
    {
        // Vérifie si un gyroscope du nouveau système d'Input est disponible.
        if (InputGyro.current != null)
        {
            gyro = InputGyro.current;
        }
        else
        {
            Debug.LogWarning("Aucun gyroscope n'est disponible sur cet appareil/manette.");
        }
    }

    void Update()
    {
        if (gyro != null)
        {
            // Lecture de la vitesse angulaire du gyroscope.
            Vector3 angularVelocity = gyro.angularVelocity.ReadValue();
            
            // Calcul du delta de rotation à appliquer en fonction de la sensibilité et du temps écoulé.
            Quaternion deltaRotation = Quaternion.Euler(angularVelocity * sensitivity * Time.deltaTime);
            
            // Application de la rotation sur la caméra.
            transform.rotation = transform.rotation * deltaRotation;
        }
    }
}
