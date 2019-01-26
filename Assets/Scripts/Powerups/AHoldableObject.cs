using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AHoldableObject : MonoBehaviour
{
    public abstract void PickUp(PlayerController player);

    public abstract void Use(PlayerController player);

    public virtual void TossObject(float force, Vector2 direction)
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<Rigidbody2D>().AddForce(direction * force);
    }
}
