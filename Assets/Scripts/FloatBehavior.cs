using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatBehavior : MonoBehaviour
{
   [SerializeField]
    private float floatingTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        while(floatingTime-- <= 0)
        {
            //BoxCollider2D collider = GetComponentInChildren<BoxCollider2D>();
           // collider.size -= new Vector2(0, 0.1f);
        }
    }
}
