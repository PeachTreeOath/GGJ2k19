using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FeetBehavior : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    void Start()
    {
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
                playerController.grounded = false;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            if (Math.Abs(playerController.rigidBody.velocity.y) <= playerController.groundedVDelta)
            {
                playerController.grounded = true;
            }
        }
    }
}
