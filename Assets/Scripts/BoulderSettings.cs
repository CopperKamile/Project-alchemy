using UnityEngine;

public class BoulderSettings : MonoBehaviour
{
    public float damage;
    public float speed;
    public Rigidbody2D rigidBody;
    public float screenEndYPosition;


    private void FixedUpdate()
    {
        speed = TrollyController.instance.trollySpeed;
        rigidBody.linearVelocity = new Vector2(0, -speed);
        //boulderRigidBody.MovePosition(boulderRigidBody.position + new Vector2(0, -speed * Time.fixedDeltaTime));
        if (transform.position.y < screenEndYPosition)
        {
            Destroy(gameObject);
        }
    }
}