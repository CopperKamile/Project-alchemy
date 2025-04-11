using UnityEngine;

public class MovingObjectSettings : MonoBehaviour
{
    public float damage;
    private float speed;
    public Rigidbody2D rigidBody;
    public float screenEndYPosition;


    private void FixedUpdate()
    {
        speed = TrollyController.instance.trollySpeed;
        rigidBody.linearVelocity = new Vector2(0, -speed);
        if (transform.position.y < screenEndYPosition)
        {
            Destroy(gameObject);
        }
    }
}