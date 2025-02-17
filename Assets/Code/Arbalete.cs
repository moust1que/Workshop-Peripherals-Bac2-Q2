using UnityEngine;
using UnityEngine.InputSystem;

public class Arbalete : MonoBehaviour
{
    public GameObject projectile;          
    public Transform spawnPoint;             
    public InputActionReference inputFire;
    // public float projectileSpeed = 10f;       

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
            // projScript.speed = projectileSpeed;
        }
    }
}
