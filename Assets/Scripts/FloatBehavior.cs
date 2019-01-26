using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatBehavior : MonoBehaviour
{
    // Time that object is floating without any sinking on top of the lava.
    [SerializeField]
    private float floatingTime;

    [SerializeField]
    private float sinkSpeed;

    private BoxCollider2D collider;

    private Vector2 originalColliderSize;

    [SerializeField]
    private bool touchingLava;

    // Very basic logic to create somewhat of a bobbing effect... should replace with actual bob logic.
    [SerializeField]
    private float sinkDelayTime;

    private float currentSinkDelayTime;

    [SerializeField]
    private bool floatXMove;

    [SerializeField]
    private float xMoveAmount;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponentInChildren<BoxCollider2D>();
        currentSinkDelayTime = sinkDelayTime;
        lastXMoveDelta = xMoveAmount * 0.01f;
    }

    private float lastXMoveDelta;

    // Update is called once per frame
    void Update()
    {

        if (touchingLava)
        {
            if (floatXMove)
            {
                // lastXMoveDelta = Random.Range(-1, 1) * xMoveAmount - lastXMoveDelta;
                transform.position += new Vector3(Random.Range(-1, 1) * xMoveAmount, 0, 0);
            }

            if (floatingTime > 0)
            {
                floatingTime -= Time.deltaTime;
            }
            else
            {
                currentSinkDelayTime -= Time.deltaTime;
                if (currentSinkDelayTime <= 0)
                {
                    float sinkSize = sinkSpeed * Time.deltaTime; 
                    collider.offset += new Vector2(0, sinkSize/2);
                    collider.size -= new Vector2(0, sinkSize);

                    if (collider.size.y <= 0.001)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        LavaBehavior lava = other.gameObject.GetComponentInChildren<LavaBehavior>();
        if (lava != null)
        {
            touchingLava = true;
        }
    }

    /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionExit2D(Collision2D other)
    {
        LavaBehavior lava = other.gameObject.GetComponentInChildren<LavaBehavior>();
        if (lava != null)
        {
            touchingLava = false;
            currentSinkDelayTime = sinkDelayTime;
        }
    }
}