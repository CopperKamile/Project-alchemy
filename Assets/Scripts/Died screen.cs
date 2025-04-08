using UnityEngine;

public class Diedscreen : MonoBehaviour
{
    public TrollyController trolly;
    public GameObject diedScreen;
    public SpawningEnvironment spawner;
    public PathMovementScript[] pathMovement;
    void Start()
    {
        diedScreen.SetActive(false);
    }

    
    void FixedUpdate()
    {
        if(trolly.currentHealth <= 0)
        {
            disablePathMovement();
            SetScreenActive();
            spawner.enabled = false;
        }
    }

    private void SetScreenActive()
    {
        diedScreen.SetActive(true);
    }


    private void disablePathMovement()
    {
        foreach(var path in pathMovement)
        {
            path.enabled = false;
        }
    }
}
