using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NDream.AirConsole;

public class GameManager : Singleton<GameManager>
{
    private const string CURRENT_PLAYERS_STRING = "Current Players: ";

    private LavaBehavior lava;

    [SerializeField]
    private GameObject title;

    [SerializeField]
    private GameObject currentPlayers;

    [SerializeField]
    private GameObject victoryText;

    [SerializeField]
    private GameObject victorName;

    bool gameStarted;

    bool gameVictorySccreen;

    // Start is called before the first frame update
    void Start()
    {
        lava = FindObjectOfType<LavaBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameVictorySccreen)
        {
            // Start up game
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

            // Current player count
            TextMeshProUGUI currentPlayerText = currentPlayers.GetComponent<TextMeshProUGUI>();
            currentPlayerText.text = CURRENT_PLAYERS_STRING + players.Length;

            if (!gameStarted)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    StartUpGame(players);
                }
            }
            else if (players.Length == 1)
            {
                lava.StopLava();

                // Victory screen if only one player left
                Debug.Log("Winner winner chicken dinner!");
                gameVictorySccreen = true;
                victoryText.SetActive(true);

                TextMeshProUGUI victorNameText = victorName.GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI playerNameText = players[0].GetComponent<TextMeshProUGUI>();
                if (playerNameText != null)
                {
                    victorName.SetActive(true);
                    victorNameText.text = playerNameText.text;
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                RestartGame();
            }
        }
    }

    private void StartUpGame(GameObject[] players)
    {
        gameStarted = true;

        foreach (GameObject player in players)
        {
            Destroy(player.GetComponentInChildren<Canvas>());
        }

        Debug.Log("Starting Lava");
        lava.lavaRising = true;

        Image titleImage = title.GetComponent<Image>();
        titleImage.enabled = false;

        PlatformToggle.instance.ActivatePlatformObjects();
    }

    private void RestartGame()
    {
        // Reset UI
        gameVictorySccreen = false;
        victoryText.SetActive(false);
        victorName.SetActive(false);

        Image titleImage = title.GetComponent<Image>();
        titleImage.enabled = true;

        // Reset game
        gameStarted = false;
    }
}
