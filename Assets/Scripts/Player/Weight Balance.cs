using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class WeightBalance : MonoBehaviour
{
    public DragonAbillitiesSetting dragonAbillities;
    private RectTransform ballRectTransform;
    private RectTransform rectTransformBalanceBar;
    private float ballPosition;
    public GameObject balanceBar;
    private float widthOfBar; //px
    private float ballVelocity = 0.0f;
    [Header("How fast balance ball should move")]
    public float moveDistanceIncrament;
    public float speedBeforeSpeedBoost;
    public float speedAfterSpeedBoost;
    public float ReturningSpeedBeforeBoost;
    public float ReturningSpeedAfterBoost;


    private void Start()
    {
        
        rectTransformBalanceBar = balanceBar.GetComponent<RectTransform>();
        widthOfBar = rectTransformBalanceBar.rect.width;
        
       //update the RectTransform so the ball always ends up in the center of balance bar
        ballRectTransform = GetComponent<RectTransform>();
        ballPosition = ballRectTransform.anchoredPosition.x;
    }


    private void Update()
    {
        MoveBallanceBallAccordingPlayerMovement();

        if (!dragonAbillities.isDragonBreathInputPressed)
        {
            float smoothSpeed = dragonAbillities.isSpeedBoostApplied ? ReturningSpeedBeforeBoost : ReturningSpeedAfterBoost;
            ReturnBallToCenter(smoothSpeed);

            //TO DO
            //fix ball movement when speed boost is applied
            //but if you reach 'danger zone' the balance ball stays in here for a bit, prompting a player to not wait and activly press opposite button
            //if the player doesn't react in time -> show animation when the wagon is upside down
            //loose some potions
            //loose some health
            //game slow down - stop path and objects from moving
            //reset the ball position at the center
            //after some time, show animation that wagon is fixed, allow the movement and spawning
        }
    }

    private void MoveBallanceBallAccordingPlayerMovement()
    {
        if (!TrollyController.instance.isTrollyMoving || TrollyController.instance.currentHealth <= 0) { return; }
        
        float smoothTime = dragonAbillities.isSpeedBoostApplied ? speedBeforeSpeedBoost : speedAfterSpeedBoost;

        if (TrollyController.instance.inputAxis.x > 0.1f) //trolley went to right
        {
            float targetPosition = ballPosition + moveDistanceIncrament;

            ballPosition = Mathf.SmoothDamp(ballPosition, targetPosition, ref ballVelocity, smoothTime);

            ballPosition = CheckBarBoundaries(ballPosition);

            ballRectTransform.anchoredPosition = new Vector2(ballPosition, ballRectTransform.anchoredPosition.y);
        }


        if (TrollyController.instance.inputAxis.x < -0.1f) //troley went to left
        {
            float targetPosition = ballPosition - moveDistanceIncrament;
            ballPosition = Mathf.SmoothDamp(ballPosition, targetPosition, ref ballVelocity, smoothTime);

            ballPosition = CheckBarBoundaries(ballPosition);

            ballRectTransform.anchoredPosition = new Vector2(ballPosition, ballRectTransform.anchoredPosition.y);
        }
    }

    private void ReturnBallToCenter(float smoothTime)
    {
        ballPosition = Mathf.SmoothDamp(ballPosition, 0, ref ballVelocity, smoothTime);
        ballRectTransform.anchoredPosition = new Vector2(ballPosition, ballRectTransform.anchoredPosition.y);
    }



    private float CheckBarBoundaries(float position)
    {
        Vector3 barPosition = rectTransformBalanceBar.position;

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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Risk zone"))
        {
            Debug.Log("Ball entered risk zone");
        }
    }

}
