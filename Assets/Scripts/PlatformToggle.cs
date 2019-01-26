using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformToggle : MonoBehaviour
{
    [SerializeField]
    private GameObject[] platformObjects;

    public bool platformToggleOn { get; set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if (platformToggleOn)
      {
         ActivatePlatformObjects();
      }
    }

    // turn on all the platform objects in the scene
    private void ActivatePlatformObjects()
    {
       for (int i = 0; i < platformObjects.Length; i++)
       {
          platformObjects[i].SetActive(true);
       }
    }
}
