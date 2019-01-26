using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AHoldableObject : MonoBehaviour
{
    [SerializeField]
    private Vector2 holdLocation;

    [SerializeField]
    private float tossForce;

    public virtual void PickUp(PlayerController player)
    {
        transform.SetParent(player.transform);
        transform.localPosition = holdLocation;
    }

    public virtual void Use(PlayerController player)
    {
        transform.SetParent(null);
        TossObject(tossForce, Vector2.right * player.MovementDirection);
    }

    public virtual void TossObject(float force, Vector2 direction)
    {
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<Rigidbody2D>().AddForce(direction * force,ForceMode2D.Impulse);
        gameObject.tag = "TossedObject";
    }
}
