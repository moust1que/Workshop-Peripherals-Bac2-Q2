using UnityEngine;
using UnityEngine.InputSystem;

public class Arbalete : MonoBehaviour
{
    public GameObject projectile;          
    public Transform spawnPoint;             
    public InputActionReference inputFire; 

    private void OnEnable()
    {
        inputFire.action.started += OnFireStarted;
        inputFire.action.Enable();
    }

    private void OnDisable()
    {
        inputFire.action.started -= OnFireStarted;
        inputFire.action.Disable();
    }

    private void OnFireStarted(InputAction.CallbackContext context)
    {
        Fire();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
        if(Input.GetKeyDown(KeyCode.JoystickButton0)){
            Fire();
        }
    }

    void Fire()
    {
        GameObject newProjectile = Instantiate(
            projectile, 
            spawnPoint.position, 
            spawnPoint.rotation
        );
        Projectile projScript = newProjectile.GetComponent<Projectile>();

        if (projScript != null)
        {
            projScript.InitialiserDirection(spawnPoint.forward);
        }
    }
}
