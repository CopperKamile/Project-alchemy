using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MoveEnvironment : MonoBehaviour
{
    /// <summary>
    /// write script for spawning randomly potions, obstacles, 
    /// each has random position. but speed (how fast will appear) 
    /// can be customixed
    /// </summary>
    /// 
    // write script for taking a damage once the player collide s with obstacle



    //Generate lanes so it creates an illusion that the players move fowards
    public List<GameObject> MovingObjectLIst;
    private List<Transform> movingObjectTransform; //each object might have different positions
    public TrollyConttroller trolly;
    
    //TO DO: later add that objects only appeared on the screen can move

    void Start()
    {
        GetMovingObjectsTransform();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MoveObjects()
    {
        if(trolly.isTrollyMoving)
        {
            
        }
    }

    private void GetMovingObjectsTransform()
    {
        foreach(var movingObjects in MovingObjectLIst)
        {
            Transform @object = movingObjects.GetComponent<Transform>();

            if(@object != null)
            {
                movingObjectTransform.Add(@object);
            }
        }
    }
}
