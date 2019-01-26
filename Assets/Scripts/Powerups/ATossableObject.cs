using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ATossableObject : AHoldableObject
{
    [SerializeField]
    protected float tossForce;

    public override void Use(PlayerController player)
    {
        transform.SetParent(null);


        if (heldPlayer && heldPlayer.facingRight)
        {
            TossObject(tossForce, Vector2.right);
        }
        else
        {
            TossObject(tossForce, Vector2.left);
        }
    }
    protected virtual void TossObject(float force, Vector2 direction)
    {
        isHeld = false;
        GetComponent<Rigidbody2D>().isKinematic = false;
        heldPlayer = null;
        gameObject.tag = "TossedObject";
        gameObject.layer = 0;
        GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(gameObject.tag == "TossedObject")
        {
            gameObject.tag = "Interactable";
            gameObject.layer = 12;
        }
    }
}
