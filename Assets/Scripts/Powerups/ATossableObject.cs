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
        TossObject(tossForce, Vector2.right * player.MovementDirection);
    }
    protected virtual void TossObject(float force, Vector2 direction)
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<Rigidbody2D>().AddForce(direction * force, ForceMode2D.Impulse);
        gameObject.tag = "TossedObject";
    }
}
