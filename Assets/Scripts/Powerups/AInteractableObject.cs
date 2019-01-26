using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AInteractableObject : MonoBehaviour
{
    protected bool playerNear;

    protected Color defaultColor;

    public virtual void Awake()
    {
        defaultColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    public virtual void Update()
    {
        if (playerNear)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = defaultColor;
        }
    }
    public abstract void Interact(PlayerController player);

    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        playerNear |= collision.gameObject.tag == "Player";
    }
    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        playerNear &= collision.gameObject.tag != "Player";
    }
}
