using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private CapsuleCollider2D capsuleCollider;

    private bool leftButton;
    private bool rightButton;
    private bool jumpButton;
    private bool intertactButton;
    private int movementDirection;
    private bool collidingWithWallLeft;
    private bool collidingWithWallRight;
    private int jumpCount;
    private bool grounded;

    [SerializeField]
    private float groundedVDelta = 0.01f;

    [SerializeField]
    private float playerSpeed = 0.1f;

    [SerializeField]
    private float jumpForce = 350f;

    private bool isInSphere;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

void Update()
{
    if(Input.GetAxis("Horizontal") > 0)
    ButtonInput("right");

    if(Input.GetAxis("Horizontal") < 0)
    ButtonInput("left");
    
    if(Input.GetKey(KeyCode.Space))
    ButtonInput("jump");
}

    public void ButtonInput(string input)
    {
        switch (input)
        {
            case "right":
                rightButton = true;
                break;
            case "left":
                leftButton = true;
                break;
            case "right-up":
                rightButton = false;
                break;
            case "left-up":
                leftButton = false;
                break;
            case "jump":
                jumpButton = true;
                break;
            case "interact":
                intertactButton = true;
                break;
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (grounded)
        {
            jumpCount = 0;
        }

        movementDirection = Convert.ToInt32(rightButton) - Convert.ToInt32(leftButton);

        rigidBody.MovePosition(rigidBody.position + new Vector2(playerSpeed * movementDirection, 0));

        if (jumpButton)
        {
            if (grounded)
            {
                grounded = false;
                rigidBody.AddForce(transform.up * jumpForce);
            }
            jumpButton = false;
        }
    }

    //Track if the player capsule is currently inside the transparent sphere or not
    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.tag == "PlatformSphere")
        {
            isInSphere = true;
        }
    }

    void OnTriggerExit(Collider trigger)
    {
        if (trigger.tag == "PlatformSphere")
        {
            isInSphere = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            grounded = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (Math.Abs(rigidBody.velocity.y) <= groundedVDelta)
            {
                grounded = true;
            }
        }
    }
}
