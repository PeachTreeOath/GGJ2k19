using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AHoldableObject : AInteractableObject
{
    protected bool isHeld;
    [SerializeField]
    protected Vector2 holdLocation;
    protected PlayerController heldPlayer;

    public override void Interact(PlayerController player)
    {
        heldPlayer = player;
        transform.SetParent(player.transform);
        isHeld = true;
        player.heldObject = this;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    public override void Update()
    {
        base.Update();
        if (isHeld)
        {

            if (heldPlayer && heldPlayer.facingRight)
            {
                transform.localPosition = holdLocation;
            }
            else
            {
                transform.localPosition = new Vector2(holdLocation.x * -1, holdLocation.y);
            }
        }
    }

    public abstract void Use(PlayerController player);

}

