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
    [HideInInspector] public Vector2 inputAxis;

    public static TrollyController instance; //singleton

    public float trollySpeed; //connect to dragon abillities setting script


    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
        inputAxis = moveAction.ReadValue<Vector2>();
        //Debug.Log("Input axis: " + inputAxis);

        inputAxis.y = 0; //disable y axis (W and S buttons)

        trollyMovement = Vector2.Lerp(trollyMovement, inputAxis * trollySpeed, Time.deltaTime * 15f); //smooth movement
        trollyRigidBody.linearVelocity = trollyMovement;

        isTrollyMoving = true;
    }
}
