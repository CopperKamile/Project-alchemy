using UnityEngine;

public class PathMovementScript : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rigidBody;
    public float scrennStartYPosition;
    public float screenEndYPosition;
    private void FixedUpdate()
    {
        speed = TrollyController.instance.trollySpeed;
        rigidBody.linearVelocity = new Vector2(0, -speed);
        if (transform.position.y < screenEndYPosition)
        {
            transform.position = new Vector2(transform.position.x, scrennStartYPosition);
        }
    }
}
