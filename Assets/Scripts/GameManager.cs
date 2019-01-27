using NDream.AirConsole;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NDream.AirConsole;

public class GameManager : Singleton<GameManager>
{
    private const string CURRENT_PLAYERS_STRING = "Current Players: ";

    bool gameStarted;

    private LavaBehavior lava;

    [SerializeField]
    private GameObject title;


    [SerializeField]
    private GameObject currentPlayers;

    // Start is called before the first frame update
    void Start()
    {
        lava = FindObjectOfType<LavaBehavior>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Start up game
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (!gameStarted && Input.GetKeyDown(KeyCode.Return))
        {
            gameStarted = true;

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

        // Current player count
        Text currentPlayerText = currentPlayers.GetComponent<Text>();
        currentPlayerText.text = CURRENT_PLAYERS_STRING + players.Length;

        if (Input.GetKeyDown(KeyCode.R))
        {
            AirConsole.instance.Broadcast("view:alive_view");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            AirConsole.instance.Broadcast("view:dead_view");
        }
    }
}
