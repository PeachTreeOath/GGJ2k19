using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSwitch : AInteractableObject
{
    [SerializeField]
    private float radius = 5.0F;

    [SerializeField]
    private float power = 10.0F;

    [SerializeField]
    private float upwardsMod;

    [SerializeField]
    private float lifeTime;
    private float lifeTimer;

    [SerializeField]
    private GameObject lampOff;

    [SerializeField]
    private GameObject lampOn;


    public override void Interact(PlayerController player)
    {
        if (canInteract)
        {
            lifeTimer = 0;
            canInteract = false;
            lampOff.SetActive(false);
            lampOn.SetActive(true);
            Vector3 explosionPos = transform.position;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius);
            foreach (Collider2D hit in colliders)
            {
                Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();

                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, upwardsMod);
            }
        }
    }
    public override void Update()
    {
        base.Update();
        lifeTimer += Time.deltaTime;

        if (!canInteract && lifeTimer > lifeTime)
        {
            canInteract = true;
            lampOff.SetActive(true);
            lampOn.SetActive(false);
        }
    }



}
