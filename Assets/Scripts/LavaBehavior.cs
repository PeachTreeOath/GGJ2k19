using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBehavior : MonoBehaviour
{
   [SerializeField]
   private float speed = 0.1f;

   private const double LAVA_DAMAGE = 10;
   public bool lavaRising {get; set;}

    // Start is called before the first frame update
    void Start()
    {
        
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
      transform.localScale += new Vector3(0, speed * Time.deltaTime, 0);
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
       lavaRising = false;
       transform.localScale = new Vector3(transform.localScale.x, 1, 0);
    }
}
