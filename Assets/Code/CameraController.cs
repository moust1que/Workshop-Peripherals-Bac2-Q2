using UnityEngine;
using UnityEngine.InputSystem;
using InputGyro = UnityEngine.InputSystem.Gyroscope;
using System.Collections;
using TMPro;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 10f;
    float x, y;

    public Camera mainCamera;
    public float normalFOV = 60f; 
    public float aimFOV = 30f; 
    public float transitionDuration = 0.1f; 

    private bool isAiming = false;
    public InputActionReference inputZoom;
    
    public int score;

    void Start()
    {
        mainCamera.fieldOfView = normalFOV;
        score = 0;
    }


    private void OnEnable()
    {
        inputZoom.action.started += OnZoomStarted;
        inputZoom.action.Enable();
    }

    private void OnDisable()
    {
        inputZoom.action.started -= OnZoomStarted;
        inputZoom.action.Disable();
    }

    private void OnZoomStarted(InputAction.CallbackContext context){
        // isAiming = false;
        // StopAllCoroutines();
        // StartCoroutine(LerpFOV(mainCamera.fieldOfView, normalFOV, transitionDuration));

        if (!isAiming)
        {
            isAiming = true;
            StopAllCoroutines();
            StartCoroutine(LerpFOV(mainCamera.fieldOfView, aimFOV, transitionDuration));
            Debug.Log("Zoom started and aiming activated");
        }
        else
        {
            isAiming = false;
            StopAllCoroutines();
            StartCoroutine(LerpFOV(mainCamera.fieldOfView, normalFOV, transitionDuration));
            Debug.Log("Zoom stopped and aiming deactivated");
        }
    }


    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Z) && !isAiming)
        // {
        //     isAiming = false;
        //     StopAllCoroutines();
        //     StartCoroutine(LerpFOV(mainCamera.fieldOfView, normalFOV, transitionDuration));
        // }

        if (Input.GetKeyDown(KeyCode.JoystickButton2) && isAiming)
        {
            AimMode();
        }

        if (ArduinoLink.instance != null)
        {
            float gyroX = ArduinoLink.instance.gyroX;
            float gyroY = ArduinoLink.instance.gyroY;

            x += gyroY * sensitivity * Time.deltaTime;
            y += gyroX * sensitivity * Time.deltaTime;
            y = Mathf.Clamp(y, -80f, 80f);

            transform.localRotation = Quaternion.Euler(y, x, 0);
        }

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

    public void AimMode(){
        isAiming = false;
        StopAllCoroutines();
        StartCoroutine(LerpFOV(mainCamera.fieldOfView, normalFOV, transitionDuration));
    }
    
    public void ExitAimMode()
    {
        StopAllCoroutines();
        StartCoroutine(LerpFOV(mainCamera.fieldOfView, normalFOV, transitionDuration));
        isAiming = false;
    }
}
