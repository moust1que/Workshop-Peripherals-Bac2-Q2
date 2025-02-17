using UnityEngine;
using UnityEngine.InputSystem;
using InputGyro = UnityEngine.InputSystem.Gyroscope;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 10f;
    float x, y;

    public Camera mainCamera;
    public float normalFOV = 60f; 
    public float aimFOV = 30f; 
    public float transitionDuration = 0.1f; 

    private bool isAiming = false;


    public int score;

    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
        else
        {
            Debug.LogWarning("Aucun gyroscope supporté sur cet appareil.");
        }
        mainCamera.fieldOfView = normalFOV;
        score = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isAiming)
        {
            isAiming = true;
            StopAllCoroutines();
            StartCoroutine(LerpFOV(mainCamera.fieldOfView, aimFOV, transitionDuration));
        }

        if (Input.GetMouseButtonDown(0) && isAiming)
        {
            isAiming = false;
            StopAllCoroutines();
            StartCoroutine(LerpFOV(mainCamera.fieldOfView, normalFOV, transitionDuration));
        }

        x += Input.gyro.rotationRateUnbiased.y * sensitivity;
        y += Input.gyro.rotationRateUnbiased.x * sensitivity;
        y = Mathf.Clamp(y, -80f, 80f);
        transform.localRotation = Quaternion.Euler(y, x, 0);
    }

    IEnumerator LerpFOV(float startFOV, float endFOV, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            mainCamera.fieldOfView = Mathf.Lerp(startFOV, endFOV, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        mainCamera.fieldOfView = endFOV;
    }
    
    public void ExitAimMode()
    {
        StopAllCoroutines();
        StartCoroutine(LerpFOV(mainCamera.fieldOfView, normalFOV, transitionDuration));
        isAiming = false;
    }
}
