using UnityEngine;
using UnityEngine.InputSystem;

public class DragonAbillitiesSetting : MonoBehaviour
{
    public GameObject dragonFire;
    private InputAction dragonBreathInput;
    public TrollyController trolly;

    [SerializeField] private float currentSpeed;
    public float speedBoost;

    public SpawningEnvironment movingObjects;

    private void Start()
    {
        dragonFire.SetActive(false);
        currentSpeed = trolly.trollySpeed;
        dragonBreathInput = InputSystem.actions.FindAction("DragonBreath"); //press E
    }

    private void Update()
    {
        if(dragonBreathInput.WasPressedThisFrame())
        {
            dragonFire.SetActive(true);
            MovementSpeedIncreased();
            ApplySpeedToObstacles();
        }
        else if(dragonBreathInput.WasReleasedThisFrame())
        {
            dragonFire.SetActive(false);
            MovementSpeedDecreased();
            DeapplySpeedToObstacles();
        }
    }



    private void MovementSpeedIncreased()
    {
        Debug.Log("Speed without boost: " + currentSpeed);
        currentSpeed *= speedBoost;
      
        Debug.Log("Speed with boost: " + currentSpeed);
    }

    private void MovementSpeedDecreased()
    {
        Debug.Log("Speed without boost: " + currentSpeed);
        currentSpeed = trolly.trollySpeed;
       
        Debug.Log("Speed with boost: " + currentSpeed);
    }

    private void ApplySpeedToObstacles()
    {
        movingObjects.currentGravity *= speedBoost;

        movingObjects.nextSpawnTime /= speedBoost;
        movingObjects.minTimeDelay /= speedBoost;
        movingObjects.maxTimeDelay /= speedBoost;
    }

    private void DeapplySpeedToObstacles()
    {
        movingObjects.currentGravity /= speedBoost;

        movingObjects.nextSpawnTime *= speedBoost;
        movingObjects.minTimeDelay *= speedBoost;
        movingObjects.maxTimeDelay *= speedBoost;
    }


}
