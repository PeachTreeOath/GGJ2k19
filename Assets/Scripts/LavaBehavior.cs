﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBehavior : MonoBehaviour
{
   [SerializeField]
   private float speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Expand();
    }

   // basic lava expanding behavior
   private void Expand()
   {
      transform.localScale += new Vector3(0, speed * Time.deltaTime, 0);
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.tag == "Player")
      {
         Debug.Log("Lava collided with " + other.name);
      }
   }
}
