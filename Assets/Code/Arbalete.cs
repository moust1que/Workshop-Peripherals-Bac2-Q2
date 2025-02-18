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

    void Start(){
        canFire = true;
    }

    private void OnEnable(){
        inputFire.action.started += OnFireStarted;
        inputFire.action.Enable();
    }

    private void OnDisable(){
        inputFire.action.started -= OnFireStarted;
        inputFire.action.Disable();
    }


    private void OnFireStarted(InputAction.CallbackContext context){
        Fire();
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     Fire();
        // }
        if(Input.GetKeyDown(KeyCode.JoystickButton0)){
            Fire();
        }
        if(Input.GetKeyDown(KeyCode.O)){
            reloadCheck = 100;
        }

        Reload();
    }

    void Fire()
    {
        if(canFire){
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
            canFire = false;
            reloadCheck = 0;
        }
    }


    void Reload(){
        if(canFire == false){
            if(reloadCheck == 100){
                canFire = true;
            }
        }
    }
}
