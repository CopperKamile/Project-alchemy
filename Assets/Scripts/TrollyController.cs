using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
public class TrollyController : MonoBehaviour
{
    //Human controlls the trolly: left and right and collect the potions

    public float health;
    public float currentHealth;

    public bool isTrollyMoving;
    private InputAction moveAction;

    Rigidbody2D trollyRigidBody;

    private Vector2 trollyMovement;

    public float trollySpeed; //connect to dragon abillities setting script

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    private void OnEnable()
    {
        moveAction.Enable();
    }

    void Start()
    {
        currentHealth = health;
        isTrollyMoving = false;
        trollyRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveTrolly();
    }

    private void MoveTrolly()
    {
        Vector2 inputAxis = moveAction.ReadValue<Vector2>();

        inputAxis.y = 0; //disable y axis (W and S buttons)

        trollyMovement = inputAxis * trollySpeed;

        trollyRigidBody.AddForce(trollyMovement);

        isTrollyMoving = true;
    }
}
