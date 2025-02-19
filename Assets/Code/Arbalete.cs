using UnityEngine;
using UnityEngine.InputSystem;

public class Arbalete : MonoBehaviour
{
    public GameObject projectile;          
    public Transform spawnPoint;             
    public InputActionReference inputFire; 
    public CameraController cameraController;

    public int reloadCheck = 100;
    public bool canFire;
    public bool hasShooted = false;

    void Start(){
        canFire = false;
    }

    // private void OnEnable(){
    //     inputFire.action.started += OnFireStarted;
    //     inputFire.action.Enable();
    // }

    // private void OnDisable(){
    //     inputFire.action.started -= OnFireStarted;
    //     inputFire.action.Disable();
    // }


    // private void OnFireStarted(InputAction.CallbackContext context){
    //     Fire();
    // }

    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.JoystickButton0)){
        //     Fire();
        // }
        // if(Input.GetKeyDown(KeyCode.O)){
        //     reloadCheck = 100;
        // }

        if(ArduinoLink.instance.button3 && !hasShooted && ArduinoLink.instance.isLoaded){
            hasShooted = true;
            Fire();
        }

        if(hasShooted && !ArduinoLink.instance.isLoaded) {
            hasShooted = false;
        }

        // Reload();
    }

    void Fire()
    {
        GameObject newProjectile = Instantiate(
            projectile, 
            spawnPoint.position, 
            spawnPoint.rotation
        );
        Projectile projScript = newProjectile.GetComponent<Projectile>();

        if (projScript != null){
            projScript.InitialiserDirection(spawnPoint.forward);
        }
        cameraController.ExitAimMode();
        // hasShooted = false;
    }


    // void Reload(){
    //     if(canFire == false){
    //         if(ArduinoLink.instance.isLoaded){
    //             canFire = true;
    //         }
    //     }
    // }
}
