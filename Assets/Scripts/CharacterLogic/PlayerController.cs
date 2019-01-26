using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Collider2D col;

    [SerializeField]
    public double playerHealth = 30;

    private bool leftButton;
    private bool rightButton;
    private bool jumpButton;
    private bool isJumping;
    private bool interactButton;
    private int movementDirection;
    private bool collidingWithWallLeft;
    private bool collidingWithWallRight;
    private float jumpTimer;
    private bool grounded;
    public AHoldableObject heldObject;
    private Color defaultColor;

    private float interactTimer;
    private float damageTimer;
    private bool interacted;

    public bool keyboardControlsOn;

    [SerializeField]
    private float groundedVDelta = 0.01f;

    [SerializeField]
    private float playerSpeed = 1f;

    [SerializeField]
    private float jumpForce = 35f;

    [SerializeField]
    private float initJumpForce = 200f;

    [SerializeField]
    private float maxJumpTime = .2f;

    private bool isInSphere;

    public int MovementDirection { get; }


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        defaultColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    void Update()
    {
        GetKeyboardInput();
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
            case "jump-up":
                jumpButton = false;
                break;
            case "interact":
                interactButton = true;
                break;
        }
    }

    private void FixedUpdate()
    {
        Movement();
        InteractActions();
    }

    private void GetKeyboardInput()
    {
        if (keyboardControlsOn)
        {
            if (Input.GetAxis("Horizontal") > 0)
                ButtonInput("right");

            if (Input.GetAxis("Horizontal") < 0)
                ButtonInput("left");

            if (Input.GetKeyDown(KeyCode.Space))
                ButtonInput("jump");

            if (Input.GetKeyUp(KeyCode.Space))
                ButtonInput("jump-up");

            if (Input.GetAxis("Horizontal") == 0)
            {
                rightButton = false;
                leftButton = false;
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                interactButton = true;
            }
        }
    }

    private void Movement()
    {
        movementDirection = Convert.ToInt32(rightButton) - Convert.ToInt32(leftButton);
        transform.Translate(new Vector2(playerSpeed * movementDirection * Time.fixedDeltaTime, 0));

        if (jumpButton)
        {
            if (grounded)
            {
                grounded = false;
                isJumping = true;
                rigidBody.AddForce(transform.up * initJumpForce);
                jumpTimer = 0;
            }

            if (isJumping && jumpTimer <= maxJumpTime)
            {
                jumpTimer += Time.fixedDeltaTime;

                rigidBody.AddForce(transform.up * jumpForce);
            }
            else
            {
                jumpButton = false;
            }

        }
        else
        {
            jumpTimer = 0;
            isJumping = false;
        }
    }

    public void takeDamage(double damageValue)
    {
        float currentTime = Time.time;
        if (currentTime >= damageTimer + 1)
        {
            damageTimer = currentTime;
            Debug.Log("Player taking damage");
            playerHealth -= damageValue;
            if (playerHealth <= 0)
            {
                Debug.Log("Player dead");
                Destroy(this.gameObject);
            }
        }
    }

    private void InteractActions()
    {
        if (interactButton)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            interacted = true;
            interactTimer = 0;

            if (heldObject)
            {
                heldObject.Use(this);
                heldObject = null;
            }
            else
            {
                Collider2D[] colliders = new Collider2D[30];
                ContactFilter2D filter = new ContactFilter2D();
                filter.layerMask = LayerMask.GetMask("Interactable");
                filter.useLayerMask = true;

                gameObject.GetComponent<BoxCollider2D>().OverlapCollider(filter, colliders);

                foreach(Collider2D item in colliders)
                {
                    if(item.gameObject.tag == "Interactable")
                    {
                        item.gameObject.GetComponent<AInteractableObject>().Interact(this);
                    }
                }
            }

            interactButton = false;

        }
        if(interacted)
        {
            interactTimer += Time.fixedDeltaTime;
            if(interactTimer >= .1)
            {
                gameObject.GetComponent<SpriteRenderer>().color = defaultColor;
                interacted = false;
                interactTimer = 0;
            }
        }
    }

    //Track if the player capsule is currently inside the transparent sphere or not
    void OnTriggerEnter2d(Collider2D trigger)
    {
        if (trigger.tag == "PlatformSphere")
        {
            isInSphere = true;
        }
    }

    void OnTriggerExit2D(Collider2D trigger)
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