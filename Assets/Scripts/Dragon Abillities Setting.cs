using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragonAbillitiesSetting : MonoBehaviour
{
    public GameObject dragonFire;
    public GameObject trollyShadow;
    private InputAction dragonBreathInput;
    private InputAction JumpInput;
    public TrollyController trolly;
    public PlayerCollisiosn collisionWithObjects;

    [SerializeField] private float currentSpeed;
    public float speedBoost;

    public SpawningEnvironment movingObjects;

    private void Start()
    {
        dragonFire.SetActive(false);
        trollyShadow.SetActive(false);
        currentSpeed = trolly.trollySpeed;
        dragonBreathInput = InputSystem.actions.FindAction("DragonBreath"); //press E
        JumpInput = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    { 

        //Later use states (enums or switch)
        if (dragonBreathInput.WasPressedThisFrame())
        {
            dragonFire.SetActive(true);
            MovementSpeedIncreased();
            ApplySpeedToObstacles();
        }
        else if (dragonBreathInput.WasReleasedThisFrame())
        {
            dragonFire.SetActive(false);
            MovementSpeedDecreased();
            DeapplySpeedToObstacles();
        }
        else if (JumpInput.WasPressedThisFrame())
        {
            trollyShadow.SetActive(true);
            JumpOver();
        }
        else if (JumpInput.WasReleasedThisFrame())
        {
            trollyShadow.SetActive(false);
            LandOnGround();
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


    private void JumpOver() //jump over boulders and potions
    {
        collisionWithObjects.enabled = false;
        foreach (GameObject obj in movingObjects.spawnedObjects)
        {
            Collider2D col = obj.GetComponent<Collider2D>();
            if (col != null)
                col.enabled = false;
        }
    }

    private void LandOnGround()
    {
        collisionWithObjects.enabled = true;
        foreach (GameObject obj in movingObjects.spawnedObjects)
        {
            Collider2D col = obj.GetComponent<Collider2D>();
            if (col != null)
                col.enabled = true;
        }
    }

}
