using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NDream.AirConsole;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    private BoxCollider2D col;
    private Animator anim;
    private SpriteRenderer spr;

    [SerializeField]
    public bool playerAlive;

    [SerializeField]
    public bool playerSinking;

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
    public bool grounded { get; set; }
    public AHoldableObject heldObject;
    private Color defaultColor;

    private float interactTimer;
    private float damageTimer;
    private bool interacted;

    private float onFireTimer;

    public bool keyboardControlsOn;

    public bool facingRight = true;
    public bool launched = false;

    [SerializeField]
    public float groundedVDelta = 0.01f;

    [SerializeField]
    private float playerSpeed = 1f;

    [SerializeField]
    private float jumpForce = 35f;

    [SerializeField]
    private float initJumpForce = 200f;

    [SerializeField]
    private float maxJumpTime = .2f;
    private float launchedColliderDisableTime = 0;
    [SerializeField]
    private float launchedMaxColliderDisableTime = .2f;

    public string nickname = "";
    public int deviceID = -1;

    private bool isInSphere;

    public int MovementDirection { get; }

    public Vector2 originalColliderSize;
    public Vector2 originalColliderOffset;

    public ParticleSystem particleSystem;


    private void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
        particleSystem.Stop();
        rigidBody = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        originalColliderSize = col.size;
        originalColliderOffset = col.offset;
        spr = GetComponent<SpriteRenderer>();
        defaultColor = spr.color;
        playerAlive = true;
        anim = GetComponent<Animator>();
        GameManager.instance.RegisterPlayer(this);
    }

    void Destroy()
    {
        GameManager.instance.RemovePlayer(this);
    }

    void Update()
    {
        if (launched && launchedColliderDisableTime < launchedMaxColliderDisableTime)
        {
            launchedColliderDisableTime += Time.deltaTime;
        }
        else if (launched && launchedColliderDisableTime >= launchedMaxColliderDisableTime)
        {
            launched = false;
            launchedColliderDisableTime = 0;
            BoxCollider2D[] colliders = this.gameObject.GetComponentsInChildren<BoxCollider2D>(true);
            foreach (BoxCollider2D collider in colliders)
            {
                collider.enabled = true;
            }
        }

        if (onFireTimer + 5 < Time.time && particleSystem.isPlaying)
        {
            particleSystem.Stop();
        }

        if (playerAlive)
        {
            GetKeyboardInput();
        }
    }

    public void ButtonInput(string input)
    {
        switch (input)
        {
            case "right":
                anim.SetBool("isRunning", true);
                rightButton = true;
                spr.flipX = false;
                break;
            case "left":
                anim.SetBool("isRunning", true);
                leftButton = true;
                spr.flipX = true;
                break;
            case "right-up":
                rightButton = false;
                anim.SetBool("isRunning", false);
                break;
            case "left-up":
                leftButton = false;
                anim.SetBool("isRunning", false);
                break;
            case "jump":
                jumpButton = true;
                anim.SetBool("isJumping", true);
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
                anim.SetBool("isRunning", false);
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

        if (movementDirection == 1)
            facingRight = true;
        else if (movementDirection == -1)
            facingRight = false;

        transform.Translate(new Vector2(playerSpeed * movementDirection * Time.fixedDeltaTime, 0));

        if (jumpButton)
        {
            if (grounded)
            {
                anim.SetBool("isJumping", true);
                grounded = false;
                isJumping = true;
                rigidBody.AddForce(transform.up * initJumpForce);
                jumpTimer = 0;
                //AudioManager.instance.PlaySound("jump");
                AirConsole.instance.Message(deviceID, "sound:jump");
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
    public void SetGroundedAnimation()
    {
        anim.SetBool("isJumping", false);
    }

    public void takeDamage(double damageValue)
    {
        if (gameObject.activeInHierarchy)
        {
            if (!particleSystem.isPlaying)
            {
                particleSystem.Play();
            }
            Debug.Log("starting particles");
            onFireTimer = Time.time;
            float currentTime = Time.time;
            if (currentTime >= damageTimer + 1)
            {
                damageTimer = currentTime;
                Debug.Log("Player taking damage");
                playerHealth -= damageValue;

                if (playerHealth <= 0)
                {
                    Debug.Log("Player dead");
                    playerSinking = true;
                    PlayerDead();
                }
                else
                {
                    AirConsole.instance.Message(deviceID, "sound:lava_burn");
                }
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

                BoxCollider2D box = gameObject.GetComponent<BoxCollider2D>();
                Physics2D.OverlapBox(box.bounds.center, box.bounds.size, 0, filter, colliders);

                foreach (Collider2D item in colliders)
                {
                    if (item && item.gameObject.tag == "Interactable")
                    {
                        item.gameObject.GetComponent<AInteractableObject>().Interact(this);
                    }
                }
            }

            interactButton = false;

        }
        if (interacted)
        {
            interactTimer += Time.fixedDeltaTime;
            if (interactTimer >= .1)
            {
                gameObject.GetComponent<SpriteRenderer>().color = defaultColor;
                interacted = false;
                interactTimer = 0;
            }
        }
    }

    public void PlayerDead()
    {
        if (playerAlive)
        {
            this.playerAlive = false;

            if (playerSinking)
            {
                gameObject.transform.Rotate(Vector3.back, 90f);
                particleSystem.transform.Rotate(Vector3.back, -90f);
                FloatBehavior fb = gameObject.AddComponent<FloatBehavior>();
                fb.floatingTime = 5;
                fb.sinkSpeed = 0.5f;
                fb.sinkDelayTime = 0.75f;
                fb.floatXMove = true;
                fb.xMoveAmount = .005f;

                Canvas canvas = gameObject.GetComponentInChildren<Canvas>();
                canvas.enabled = false;
            }
            else
            {
                gameObject.SetActive(false);
            }
            AirConsole.instance.Message(deviceID, "sound:scream");
            AirConsole.instance.Message(deviceID, "view:dead_view");
            //AudioManager.instance.PlaySound("lava_burn");
       }
    }

    public void PlayerAlive()
    {
        this.playerAlive = true;
        this.playerHealth = 30;

        // Unhide Player
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        particleSystem.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        gameObject.layer = LayerMask.NameToLayer("Player");
        gameObject.SetActive(true);

        col.size = originalColliderSize;
        col.offset = originalColliderOffset;

        Destroy(gameObject.GetComponent<FloatBehavior>());

        Canvas canvas = gameObject.GetComponentInChildren<Canvas>(true);
        canvas.enabled = true;

        AirConsole.instance.Message(deviceID, "view:alive_view");
    }
}