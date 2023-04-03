using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // creating a variable for the move speed
    [SerializeField] private float moveSpeed = 5f;
    // creating a variable for the input value
    Vector2 rawInput;


    // creating variables for the padding so the player doesn't go off screen 
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    // storing the min and max bounds of the camera
    Vector2 minBounds;
    Vector2 maxBounds;

    Shooter shooter;

    void Awake()
    {
        // getting the shooter component
        shooter = GetComponent<Shooter>();
    }
    

    void Start()
    {
        // calling the InitBounds function
        // forgot to call it in the first place lol 1 hour wasted...
        InitBounds();
    }

    void Update()
    {
        // calling the move function
        Move();
    }

    void InitBounds()
    {
        // getting the main camera and the min and max bounds of the camera
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));

        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private void Move()
    {
        // multiplying the input value by the move speed and the time between frames
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();

        // clamping the position of the player to the min and max bounds of the camera
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;
    }

    void OnMove(InputValue value)
    {
        // getting the input value
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        // calling the fire function

        if(shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }
}
