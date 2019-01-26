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
        
        Collider2D[] colliders = new Collider2D[30];
        ContactFilter2D filter = new ContactFilter2D();

        Collider2D box = gameObject.GetComponent<Collider2D>();
        Physics2D.OverlapBox(box.bounds.center, box.bounds.size, 0, filter, colliders);

        playerNear = false;
        foreach (Collider2D item in colliders)
        {
            if (item && item.gameObject.tag == "Player")
            {
                playerNear = true;
            }
        }
    }
    public abstract void Interact(PlayerController player);
}