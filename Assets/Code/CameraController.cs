using UnityEngine;
using UnityEngine.InputSystem;
using InputGyro = UnityEngine.InputSystem.Gyroscope;
using System.Collections;
using TMPro;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 10f;
    float x, y, z;

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

        if (!isAiming)
        {
            isAiming = true;
            ArduinoLink.instance.isAiming = true;
            StopAllCoroutines();
            StartCoroutine(LerpFOV(mainCamera.fieldOfView, aimFOV, transitionDuration));
            Debug.Log("Zoom started and aiming activated");
        }
        else
        {
            isAiming = false;
            ArduinoLink.instance.isAiming = false;
            StopAllCoroutines();
            StartCoroutine(LerpFOV(mainCamera.fieldOfView, normalFOV, transitionDuration));
            Debug.Log("Zoom stopped and aiming deactivated");
        }
    }


    void Update()
    {
        UIContoller uiContoller = UnityEngine.Object.FindFirstObjectByType<UIContoller>();
        uiContoller.UpdateScoreUI();
        WindManager wind = UnityEngine.Object.FindFirstObjectByType<WindManager>();
        if (wind != null){
            Debug.Log("CameraController : WindManager trouvé, direction = " + wind.GetCurrentWindDirection());
            uiContoller.UpdateWindUI(wind.GetCurrentWindDirection(), wind.GetCurrentWindStrength());
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton2) && isAiming){
            AimMode();
        }

        if(ArduinoLink.instance.isAiming){
            AimMode();
        }

        // if(ArduinoLink.instance.isAiming == false){
        //     Debug.Log("isAiming = false");
        //     ExitAimMode();
        // }

        // if(Input.GetKeyDown(KeyCode.U)){
        //     ExitAimMode();
        // }

        if (ArduinoLink.instance != null){
            float gyroX = ArduinoLink.instance.gyroX;
            float gyroY = ArduinoLink.instance.gyroY;
            float gyroZ = ArduinoLink.instance.gyroZ;

            // x -= gyroX * sensitivity * Time.deltaTime;
            // x = Mathf.Clamp(x, -80f, 80f);
            // y -= gyroZ * sensitivity * Time.deltaTime;
            // y = Mathf.Clamp(y, -80f, 80f);
            // z -= gyroY * sensitivity * Time.deltaTime;

            transform.localRotation = Quaternion.Euler(-gyroY, -gyroZ, 0.0f);
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
        // isAiming = false;
        StopAllCoroutines();
        StartCoroutine(LerpFOV(mainCamera.fieldOfView, normalFOV, transitionDuration));
    }
    
    public void ExitAimMode()
    {
        Debug.Log("ExitAimMode");
        StopAllCoroutines();
        StartCoroutine(LerpFOV(mainCamera.fieldOfView, normalFOV, transitionDuration));
        // isAiming = false;
    }
}
