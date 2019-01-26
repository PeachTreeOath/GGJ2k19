using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatBehavior : MonoBehaviour
{
    [SerializeField]
    private float floatingTime;

    [SerializeField]
    private float shrinkSpeed;

    private BoxCollider2D collider;

    private Vector2 originalColliderSize;

    private bool touchedLava;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponentInChildren<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (touchedLava)
        {
            floatingTime -= Time.deltaTime;
            if (floatingTime <= 0)
            {
                collider.size -= new Vector2(0, shrinkSpeed * Time.deltaTime);
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
            touchedLava = true;
        }
    }
}
