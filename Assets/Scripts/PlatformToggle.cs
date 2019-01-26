using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformToggle : Singleton<PlatformToggle>
{
    private Renderer rend;
    private BoxCollider2D col;

    [SerializeField]
    private GameObject[] platformObjects;

    public bool platformToggleOn { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < platformObjects.Length; i++)
        {
            platformObjects[i].SetActive(false);
        }
    }

    // turn on all the platform objects in the scene
    public void ActivatePlatformObjects()
    {
       for (int i = 0; i < platformObjects.Length; i++)
       {
          platformObjects[i].SetActive(true);
       }
    }
}
