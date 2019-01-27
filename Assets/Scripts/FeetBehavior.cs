using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FeetBehavior : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    private Vector3 offset = new Vector3(0, .8f, 0);
    private Vector3 boxSize = new Vector3(.4f, .1f, 1);

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

    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = new Color(0, 0, 1, .4f);
                //Gizmos.DrawCube(transform.position - offset, boxSize);
      
           
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player")
        {
            if (Math.Abs(playerController.rigidBody.velocity.y) <= playerController.groundedVDelta)
            {
                //Collider2D[] cols = Physics2D.OverlapBox()
                //Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position - offset, boxSize, 0);
                //foreach (Collider2D col in cols)
                //{
                //if (col.tag == "Wall")
                //{
                playerController.grounded = true;
                playerController.SetGroundedAnimation();
                //break;
                //}
                //}
            }
        }
    }
    //void OnCollisionStay2D(Collision2D collision)
    //{
        //if (collision.gameObject.tag == "Wall")
        //{
        //    //if (Math.Abs(playerController.rigidBody.velocity.y) <= playerController.groundedVDelta)
        //    {
        //        //Collider2D[] cols = Physics2D.OverlapBox()
        //        //Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position - offset, boxSize, 0);
        //        //foreach (Collider2D col in cols)
        //        //{
        //            //if (col.tag == "Wall")
        //            //{
        //                playerController.grounded = true;
        //                playerController.SetGroundedAnimation();
        //                //break;
        //            //}
        //        //}
        //    }
        //}
    //}
}
