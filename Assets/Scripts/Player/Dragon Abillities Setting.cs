using UnityEngine;
using UnityEngine.InputSystem;

public class DragonAbillitiesSetting : MonoBehaviour
{
    public GameObject dragonFire;
    public GameObject trollyShadow;
    public GameObject dragonWings;
    public bool isSpeedBoostApplied = false;
    public bool isDragonBreathInputPressed;

    private InputAction dragonBreathInput;
    private InputAction JumpInput;
    private PlayerCollisiosn collisionWithObjects;

    [SerializeField] private float currentSpeed;
    public float speedBoost;

    public SpawningEnvironment movingObjects;

    private void Start()
    {
        collisionWithObjects = GetComponentInParent<PlayerCollisiosn>();

        isSpeedBoostApplied = false;
        isDragonBreathInputPressed = false;

        dragonWings.SetActive(false);
        dragonFire.SetActive(false);
        trollyShadow.SetActive(false);

        currentSpeed = TrollyController.instance.trollySpeed;

        dragonBreathInput = InputSystem.actions.FindAction("DragonBreath"); //press E
        JumpInput = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    { 
        //Later use states (enums or switch)
        if (dragonBreathInput.WasPressedThisFrame())
        {
            isDragonBreathInputPressed = true;
            isSpeedBoostApplied = true;
            dragonFire.SetActive(true);
            MovementSpeedIncreased();
        }
        else if (dragonBreathInput.WasReleasedThisFrame())
        {
            isDragonBreathInputPressed = false;
            isSpeedBoostApplied = false;
            dragonFire.SetActive(false);
            MovementSpeedDecreased();
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
        TrollyController.instance.trollySpeed *= speedBoost;
    }

    private void MovementSpeedDecreased()
    {
        TrollyController.instance.trollySpeed /= speedBoost;
    }

    private void JumpOver() //jump over boulders and potions
    {
        collisionWithObjects.enabled = false;
        SetLayerMask(TrollyController.instance.gameObject, LayerMask.NameToLayer("JumpingTrolly"));
    }

    private void LandOnGround()
    {
        collisionWithObjects.enabled = true;
        SetLayerMask(TrollyController.instance.gameObject, LayerMask.NameToLayer("Player"));
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
