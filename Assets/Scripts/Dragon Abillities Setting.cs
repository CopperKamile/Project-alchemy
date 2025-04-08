using UnityEngine;
using UnityEngine.InputSystem;

public class DragonAbillitiesSetting : MonoBehaviour
{
    public GameObject dragonFire;
    public GameObject trollyShadow;
    public GameObject dragonWings;

    private InputAction dragonBreathInput;
    private InputAction JumpInput;
    public TrollyController trolly;
    public PlayerCollisiosn collisionWithObjects;

    [SerializeField] private float currentSpeed;
    public float speedBoost;

    public SpawningEnvironment movingObjects;

    private void Start()
    {
        dragonWings.SetActive(false);
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
            //ApplySpeedToObstacles();
        }
        else if (dragonBreathInput.WasReleasedThisFrame())
        {
            dragonFire.SetActive(false);
            MovementSpeedDecreased();
            //DeapplySpeedToObstacles();
        }
        else if (JumpInput.WasPressedThisFrame())
        {
            dragonWings.SetActive(true);
            trollyShadow.SetActive(true);
            JumpOver();
        }
        else if (JumpInput.WasReleasedThisFrame())
        {
            dragonWings.SetActive(false);
            trollyShadow.SetActive(false);
            LandOnGround();
        }
    }



    private void MovementSpeedIncreased()
    {
       // Debug.Log("Speed without boost: " + currentSpeed);
        //currentSpeed *= speedBoost;
        TrollyController.instance.trollySpeed *= speedBoost;

        //Debug.Log("Speed with boost: " + currentSpeed);
    }

    private void MovementSpeedDecreased()
    {
        // Debug.Log("Speed without boost: " + currentSpeed);
        //currentSpeed = trolly.trollySpeed;
        TrollyController.instance.trollySpeed /= speedBoost;


        //Debug.Log("Speed with boost: " + currentSpeed);
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
        SetLayerMask(trolly.gameObject, LayerMask.NameToLayer("JumpingTrolly"));
    }

    private void LandOnGround()
    {
        collisionWithObjects.enabled = true;
        SetLayerMask(trolly.gameObject, LayerMask.NameToLayer("Player"));
    }

    private void SetLayerMask(GameObject gameObjectParent, int newLayer)
    {
        if(gameObjectParent == null) { return; }

        gameObjectParent.layer = newLayer;

        foreach(Transform child in gameObjectParent.transform)
        {
            if(child != null)
            {
                child.gameObject.layer = newLayer;
            }
        }
    }
}
