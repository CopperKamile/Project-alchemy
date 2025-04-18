using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class WeightBalance : MonoBehaviour
{
    [SerializeField] private DragonAbillitiesSetting dragonAbillities;
    [SerializeField] private TrolleyBalanceBrokenAnimation brokenAnimation;
    private CheckColliisionWeightBalance collisionScript;

    [HideInInspector] public bool isInRiskZone = false;

    public List<MovingObjectSettings> movingObjectSettingsList;
    public List<PathMovementScript> pathMovementScriptList;
    public SpawningEnvironment Spawner;

    private RectTransform ballRectTransform;
    public RectTransform riskZoneLeft;
    public RectTransform riskZoneRight;
    private RectTransform rectTransformBalanceBar;
    private float ballPosition;
    [SerializeField] private GameObject balanceBar;

    private float widthOfBar; //px

    private float ballVelocity = 0.0f;

    [Header("How fast balance ball should move")]
    //[SerializeField] private float timePenalty;
    private bool hasSlowDown;
    [SerializeField] private float saveTrolleyTimePenalty;
    private float lockedPosition;
    [SerializeField] private float moveDistanceIncrament;
    [SerializeField] private float slowDownInRiskZone;

    [SerializeField] private float speedBeforeSpeedBoost;
    [SerializeField] private float ReturningSpeedBeforeBoost;
    [SerializeField] private float AmountToSlowDownObjectsSpeed;

    //[SerializeField] private float speedAfterSpeedBoost;
    //[SerializeField] private float ReturningSpeedAfterBoost;


    private void Start()
    {
        collisionScript = GetComponent<CheckColliisionWeightBalance>();

        isInRiskZone = false;

        rectTransformBalanceBar = balanceBar.GetComponent<RectTransform>();
        widthOfBar = rectTransformBalanceBar.rect.width;
        
       //update the RectTransform so the ball always ends up in the center of balance bar
        ballRectTransform = GetComponent<RectTransform>();
        ballPosition = ballRectTransform.anchoredPosition.x;
    }


    private void Update()
    {
        if (collisionScript.IsRectTransformOverlaping(ballRectTransform, riskZoneLeft) || collisionScript.IsRectTransformOverlaping(ballRectTransform, riskZoneRight))
        {
            if (!isInRiskZone)
            {
                isInRiskZone = true;
                Debug.Log("Show broken balance ");
                StartCoroutine(ShowTrolleyBrokenBalance());
            }
        }
        else
        {
            isInRiskZone = false;
            Debug.Log("Balance is not broken");
        }

        //float smoothTime = dragonAbillities.isSpeedBoostApplied ? speedBeforeSpeedBoost : speedAfterSpeedBoost;
        MoveBalanceBall(speedBeforeSpeedBoost); 

        if (!dragonAbillities.isDragonBreathInputPressed) 
        {
            //float returnSpeed = dragonAbillities.isSpeedBoostApplied ? ReturningSpeedBeforeBoost : ReturningSpeedAfterBoost;
            ReturnBallToCenter(ReturningSpeedBeforeBoost);

            //TO DO (later)
            //fix ball movement when speed boost is applied
        }
       
    }

    private void MoveBalanceBall(float customSmoothTime)
    {
        if (!TrollyController.instance.isTrollyMoving || TrollyController.instance.currentHealth <= 0) { return; }

        float inputX = TrollyController.instance.inputAxis.x;

       
        if (TrollyController.instance.inputAxis.x > 0.1f)//moving to the left, the input value is +1
        {
            float targetPosition = ballPosition + moveDistanceIncrament;

            ballPosition = Mathf.SmoothDamp(ballPosition, targetPosition, ref ballVelocity, customSmoothTime);

            ballPosition = CheckBarBoundaries(ballPosition);

            ballRectTransform.anchoredPosition = new Vector2(ballPosition, ballRectTransform.anchoredPosition.y);
        }
        else if (TrollyController.instance.inputAxis.x < -0.1f) //moving to the left, the input value is -1
        {
            float targetPosition = ballPosition - moveDistanceIncrament;
           
            ballPosition = Mathf.SmoothDamp(ballPosition, targetPosition, ref ballVelocity, customSmoothTime);

            ballPosition = CheckBarBoundaries(ballPosition);

            ballRectTransform.anchoredPosition = new Vector2(ballPosition, ballRectTransform.anchoredPosition.y);
        }

        ballPosition = CheckBarBoundaries(ballPosition);
    }

    private void ReturnBallToCenter(float smoothTime)
    {
        ballPosition = Mathf.SmoothDamp(ballPosition, 0, ref ballVelocity, smoothTime);
        ballRectTransform.anchoredPosition = new Vector2(ballPosition, ballRectTransform.anchoredPosition.y);
    }

    private float CheckBarBoundaries(float position)
    {
        Vector3 barPosition = rectTransformBalanceBar.position; //in world position

        float padding = 55.0f;
        float barLeftX = barPosition.x  - (widthOfBar / 2) + padding;
        float barRightX = barPosition.x + (widthOfBar / 2) - padding;

        if(position <= barLeftX)
        {
            position = barLeftX;
        }
        else if(position >= barRightX)
        {
            position = barRightX;
        }

        return position;
    }
    
    private IEnumerator ShowTrolleyBrokenBalance()
    {
        isInRiskZone = true;

        float timePassed = 0.0f;
        hasSlowDown = false;

        while(timePassed < saveTrolleyTimePenalty)
        {
            if (PlayerDidSaveBalance() && TrollyController.instance.gameObject != null) 
            {
                isInRiskZone = false;
                ResetSpeed();
                brokenAnimation.FixedTrolleyBalanceIndications();
                yield break;
            }

            if(!hasSlowDown && TrollyController.instance.gameObject != null)
            {
                SlowDownBalanceBall(slowDownInRiskZone);
                hasSlowDown = true;
            }

            MoveBalanceBall(slowDownInRiskZone);

            timePassed += Time.deltaTime;
            yield return null;
        }

        ShowFullBalanceFail();
    }


    private bool PlayerDidSaveBalance()
    {
        return (TrollyController.instance.inputAxis.x < 0 && ballPosition > 0) ||
               (TrollyController.instance.inputAxis.x > 0 && ballPosition < 0);
    }

    private void SlowDownBalanceBall(float slowDownTime)
    {
        Debug.Log(" BALANCE BALL IS SLOWED DOWN");
        if (hasSlowDown) return;

        TrollyController.instance.trollySpeed /= AmountToSlowDownObjectsSpeed;
        
        foreach (var movingObj in movingObjectSettingsList)
        {
            movingObj.speed = TrollyController.instance.trollySpeed;
        }

        foreach (var pathObj in pathMovementScriptList)
        {
            pathObj.speed = TrollyController.instance.trollySpeed;
        }

        hasSlowDown = true;
    }


    private void ShowFullBalanceFail()
    {
        //show the animation of upside down wagon
        //game 'stops' - stop path and objects from moving
        //reset the ball position at the center
        //after some time, show animation that wagon is fixed, allow the movement and spawning
        Debug.Log(" TROLLEY COMPLETLY LOST ITS BALANCE");

        brokenAnimation.FullyBrokenBalanceIndications();
        TrollyController.instance.trollySpeed = 0.0f;
        TrollyController.instance.isTrollyMoving = false;

        foreach (var movingObj in movingObjectSettingsList)
        {
            movingObj.speed = 0.0f;
        }

        foreach (var pathObj in pathMovementScriptList)
        {
            pathObj.speed = 0.0f;
        }

        ballPosition = ballRectTransform.anchoredPosition.x;

        Spawner.enabled = false;

        StartCoroutine(WaitForPlayerRecovery());

    }

    private IEnumerator WaitForPlayerRecovery()
    {
        while(true)
        {
            if (PlayerDidSaveBalance() && TrollyController.instance.gameObject != null)
            {
                isInRiskZone = false;
                ResetSpeed();
                brokenAnimation.FixedTrolleyBalanceIndications();
                yield break;
            }

            yield return null;
        }
    }



    private void ResetSpeed()
    {
        TrollyController.instance.trollySpeed = TrollyController.instance.originalTrollySpeed;
        TrollyController.instance.isTrollyMoving = true;

        foreach (var movingObj in movingObjectSettingsList)
        {
            movingObj.speed = TrollyController.instance.trollySpeed;
        }

        foreach (var pathObj in pathMovementScriptList)
        {
            pathObj.speed = TrollyController.instance.trollySpeed;
        }

        Spawner.enabled = true;
        hasSlowDown = false;
    }
}
