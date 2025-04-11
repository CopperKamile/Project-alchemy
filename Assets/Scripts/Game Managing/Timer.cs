using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TrollyController trolly;
    public TextMeshProUGUI timer;
    public GameOverScreen screen;
    public float timeLimit;
    private float startTime;
    private bool timeHasStarted = false;

    private void Start()
    {
        trolly.gameObject.SetActive(true);
    }

    void Update()
    {
        if (trolly.isTrollyMoving && !timeHasStarted)
        {
            startTime = Time.time;
            timeHasStarted = true;
        }

        if(timeHasStarted)
        {
            CountTimePassed();
        }
    }


    private void CountTimePassed()
    {
        float timeHasPassed = Time.time - startTime;
        float timeLeft = Mathf.Clamp(timeLimit - timeHasPassed, 0, timeLimit);
        UpdateTimer(timeLeft);
       // Debug.Log("TimeLeft: " + timeLeft + " | TimePassed: " + timeHasPassed + " | TimeLimit: " + timeLimit);

        if (timeLeft <= 0 && trolly.currentHealth >= 0)
        {
            screen.ActivateGameOverScreen();
            trolly.gameObject.SetActive(false);
        }
    }

    private void UpdateTimer(float currentTime)
    {
        timer.text = ("Time left: " + ((int)currentTime).ToString());
    }
}
