using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AHoldableObject : AInteractableObject
{
    [SerializeField]
    protected Vector2 holdLocation;

    public override void Interact(PlayerController player)
    {
        transform.SetParent(player.transform);
        transform.localPosition = holdLocation;
        player.heldObject = this;
    }

    public abstract void Use(PlayerController player);

}

