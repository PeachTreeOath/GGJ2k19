using System.Collections;
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
       Movement();
    }

   private void Movement()
   {
      transform.Translate(Vector3.up * speed * Time.deltaTime);
   }
}
