using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MoveObject: MonoBehaviour
{
    /// <summary>
    /// write script for spawning randomly potions, obstacles, 
    /// each has random position. but speed (how fast will appear) 
    /// can be customixed
    /// </summary>
    /// 
    // write script for taking a damage once the player collide s with obstacle

    //Move objects so it creates an illusion that the players move fowards
    public TrollyController trolly;
    public GameObject screenEnd;

    public float lifeSpan;
    
    public float speed;
    //TO DO: later add that objects only appeared on the screen can move

    void Update()
    {
        trolly.isTrollyMoving = true;

        MoveObjects();
    }

    private void MoveObjects()
    {
        //transform.position = Vector2.MoveTowards(transform.position, screenEnd.transform.position, speed * Time.deltaTime);
       
        //Debug.Log("Inside in each potion prefab Start position: " + transform.position);
        //Debug.Log("Inside in each potion prefab End position: " + screenEnd.transform.position);
    }

    //dragon abillity to speed trolly
    //take damage from boulders
    //collect potions
    




}
