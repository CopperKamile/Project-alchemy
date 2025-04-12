using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class WeightBalance : MonoBehaviour
{
    [SerializeField] private DragonAbillitiesSetting dragonAbillities;
    public bool isBalanceOff = false;
    private bool isBallLocked = false;

    private RectTransform ballRectTransform;
    private RectTransform rectTransformBalanceBar;
    private float ballPosition;
    [SerializeField] private GameObject balanceBar;

    private float widthOfBar; //px

    private float ballVelocity = 0.0f;

    [Header("How fast balance ball should move")]
    [SerializeField] private float lockTime;
    private float lockedPosition;
    [SerializeField] private float moveDistanceIncrament;
    [SerializeField] private float speedBeforeSpeedBoost;
    [SerializeField] private float speedAfterSpeedBoost;
    [SerializeField] private float ReturningSpeedBeforeBoost;
    [SerializeField] private float ReturningSpeedAfterBoost;


    private void Start()
    {
        isBallLocked = false;
        isBalanceOff = false;

        rectTransformBalanceBar = balanceBar.GetComponent<RectTransform>();
        widthOfBar = rectTransformBalanceBar.rect.width;
        
       //update the RectTransform so the ball always ends up in the center of balance bar
        ballRectTransform = GetComponent<RectTransform>();
        ballPosition = ballRectTransform.anchoredPosition.x;
        
    }


    private void Update()
    {
        if(isBallLocked && isBalanceOff)
        {
            MoveBallanceBallAccordingPlayerMovement();
        }
        else if (!isBalanceOff && !isBallLocked) //when balance is off == true & ball is not locked
        {
            StartCoroutine(DelayBallMovement(ballPosition));
        }


        if (!dragonAbillities.isDragonBreathInputPressed && isBallLocked && isBalanceOff)
        {
            float smoothSpeed = dragonAbillities.isSpeedBoostApplied ? ReturningSpeedBeforeBoost : ReturningSpeedAfterBoost;
            ReturnBallToCenter(smoothSpeed);

            //TO DO
            //fix ball movement when speed boost is applied
        }
       
    }
    //Probably I need to rewrite it by using Vector2
    //Because I wrote it in mind that UI elements will not have any rigid body
    //(Once I tried to rewrite it like balance ball has dynamic rigid body, but it didn't work out very well)
    //but for collision detection I could Write another script by using check if the rects transforms overlaps eachother
    //but I wanted a simple and fast solution, so I added rigid body on balance ball 

    private void MoveBallanceBallAccordingPlayerMovement()
    {
        if (!TrollyController.instance.isTrollyMoving || TrollyController.instance.currentHealth <= 0) { return; }
        
        float smoothTime = dragonAbillities.isSpeedBoostApplied ? speedBeforeSpeedBoost : speedAfterSpeedBoost;

        //moving to the left, the input value is +1
        if (TrollyController.instance.inputAxis.x > 0.1f) //trolley went to right
        {
            float targetPosition = ballPosition + moveDistanceIncrament;

            ballPosition = Mathf.SmoothDamp(ballPosition, targetPosition, ref ballVelocity, smoothTime);

            ballPosition = CheckBarBoundaries(ballPosition);

            ballRectTransform.anchoredPosition = new Vector2(ballPosition, ballRectTransform.anchoredPosition.y);
        }

        //moving to the left, the input value is -1
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

    //TO DO:
    //fix booleans, because it is janky, and the problem should be in how I use booleans
    //if you reach 'danger zone' the balance ball stays in here for a bit, prompting a player to not wait and activly press opposite button
    //if the player doesn't react in time -> show animation when the wagon is upside down
    //loose some potions
    //loose some health
    //game slow down - stop path and objects from moving
    //reset the ball position at the center
    //after some time, show animation that wagon is fixed, allow the movement and spawning
    private IEnumerator DelayBallMovement(float position)
    {
        isBalanceOff = true;
        isBallLocked = true;

        lockedPosition = position; //setting the last position of ball

        float timePassed = 0.0f;

        while(timePassed <= lockTime)
        {
            if (TrollyController.instance.inputAxis.x < 0 && ballPosition > 0) //moving left when ball is on the right
            {
                isBalanceOff = false;
                isBallLocked = false;
                yield break;
            }
            else if (TrollyController.instance.inputAxis.x > 0 && ballPosition < 0) // Moving right when ball is on the left
            {
                isBalanceOff = false;
                isBallLocked = false;
                yield break;
            }

            timePassed += Time.deltaTime;
            yield return null;
        }

        //reset everything after time locked is passed
        isBalanceOff = false;
        isBallLocked = false;
    }
}
