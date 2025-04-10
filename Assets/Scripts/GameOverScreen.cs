using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public TrollyController trolly;
    public GameObject gameOverScreenObj;
    public SpawningEnvironment spawner;
    public PathMovementScript[] pathMovement;
    void Start()
    {
        gameOverScreenObj.SetActive(false);
    }

    
    void FixedUpdate()
    {
        if(trolly.currentHealth <= 0)
        {
            ActivateGameOverScreen();
        }
    }

    private void SetScreenActive()
    {
        gameOverScreenObj.SetActive(true);
    }


    private void disablePathMovement()
    {
        foreach(var path in pathMovement)
        {
            path.enabled = false;
        }
    }


    public void ActivateGameOverScreen()
    {
        disablePathMovement();
        SetScreenActive();
        spawner.enabled = false;
    }
}
