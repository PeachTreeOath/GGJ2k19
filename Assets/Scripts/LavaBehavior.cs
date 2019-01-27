﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBehavior : MonoBehaviour
{
   [SerializeField]
   private float speed = 0.1f;

   private const double LAVA_DAMAGE = 10;
   public bool lavaRising {get; set;}
   BoxCollider2D collider2D;
    [SerializeField]
    SpriteRenderer lavaTop;
    [SerializeField]
    SpriteRenderer lavaBottom;

    // Start is called before the first frame update
    void Start()
    {
        collider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
       if (lavaRising)
       {
          Expand();
       }
    }

   // basic lava expanding behavior
   private void Expand()
   {
        collider2D.size = new Vector2(collider2D.size.x, collider2D.size.y + (speed * Time.deltaTime));
        lavaBottom.size = collider2D.size;
        lavaTop.gameObject.transform.position = new Vector2(0, lavaBottom.bounds.extents.y - lavaTop.size.y - .43f);
   }

    private void OnTriggerEnter2D(Collider2D other)
    {
        applyDamage(other.gameObject);
    }


    private void OnCollisionStay2D(Collision2D other)
    {
        applyDamage(other.gameObject);
    }

    private void applyDamage(GameObject gameObject)
    {
        if (gameObject.tag == "Player")
        {
            PlayerController pc = gameObject.GetComponent<PlayerController>();
            pc.takeDamage(LAVA_DAMAGE);
            Debug.Log("Lava collided with " + gameObject.name);
        }
    }

    public void StopLava()
    {
       //lavaRising = false;
       //transform.localScale = new Vector3(transform.localScale.x, 1, 0);
    }
}
