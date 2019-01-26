using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHoldableObject : AHoldableObject
{
    [SerializeField]
    private Vector2 holdLocation;

    [SerializeField]
    private float tossForce;

    public override void PickUp(PlayerController player)
    {
        transform.SetParent(player.transform);
        transform.localPosition = holdLocation;
    }

    public override void Use(PlayerController player)
    {
        transform.SetParent(null);
        TossObject(tossForce,Vector2.one * player.MovementDirection);
    }
}
