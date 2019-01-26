using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NDream.AirConsole;

public class GameManager : Singleton<GameManager>
{
    bool gameStarted;

    private LavaBehavior lava;

    [SerializeField]
    private GameObject title;

    // Start is called before the first frame update
    void Start()
    {
        lava = FindObjectOfType<LavaBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted && Input.GetKeyDown(KeyCode.Return))
        {
            gameStarted = true;

            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                Destroy(player.GetComponentInChildren<Canvas>());
            }

            Debug.Log("Starting Lava");
            lava.lavaRising = true;

            Image ttleImage = title.GetComponent<Image>();
            ttleImage.enabled = false;

            PlatformToggle.instance.ActivatePlatformObjects();
        }
    }
}
