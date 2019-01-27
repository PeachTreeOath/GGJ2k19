using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformToggle : Singleton<PlatformToggle>
{
    private Renderer rend;
    private BoxCollider2D col;

    [SerializeField]
    private List<GameObject> platformObjects = new List<GameObject>();

    public bool platformToggleOn { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        DeactivatePlatformObjects();
    }
    public void WipeRegistries()
    {
        platformObjects.Clear();
    }
    public void Register(GameObject obj)
    {
        platformObjects.Add(obj);
    }
    public void DeactivatePlatformObjects()
    {
        foreach(GameObject obj in platformObjects)
        { 
            obj.SetActive(false);
        }
    }

    // turn on all the platform objects in the scene
    public void ActivatePlatformObjects()
    {
        foreach (GameObject obj in platformObjects)
        {
            obj.SetActive(true);
        }
    }
}
