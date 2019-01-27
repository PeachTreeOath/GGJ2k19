using NDream.AirConsole;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NDream.AirConsole;
using System.Linq;

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

    [SerializeField]
    private List<PlayerController> players;

    // Start is called before the first frame update
    void Start()
    {
        lava = FindObjectOfType<LavaBehavior>();
        players = new List<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameVictorySccreen)
        {
            // Start up game
            List<PlayerController> alivePlayers = players.Where(p => p.playerAlive).ToList();

            // Current player count
            TextMeshProUGUI currentPlayerText = currentPlayers.GetComponent<TextMeshProUGUI>();
            currentPlayerText.text = CURRENT_PLAYERS_STRING + alivePlayers.Count;

            if (!gameStarted)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    StartUpGame(alivePlayers);
                }
            }
            else if (alivePlayers.Count == 1)
            {
                lava.StopLava();

                // Victory screen if only one player left
                Debug.Log("Winner winner chicken dinner!");
                gameVictorySccreen = true;
                victoryText.SetActive(true);

                TextMeshProUGUI victorNameText = victorName.GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI playerNameText = alivePlayers[0].GetComponentInChildren<TextMeshProUGUI>();

                Color playerColor = alivePlayers[0].GetComponent<SpriteRenderer>().color;
                if (playerNameText != null)
                {
                    victorName.SetActive(true);
                    victorNameText.text = playerNameText.text;

                    if (playerColor != null)
                    {
                        victorNameText.color = playerColor;
                    }
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

        // Cheats
        if (Input.GetKeyDown(KeyCode.R))
        {
            AirConsole.instance.Broadcast("view:alive_view");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            AirConsole.instance.Broadcast("view:dead_view");
        }
    }

    private void StartUpGame(List<PlayerController> alivePlayers)
    {
        gameStarted = true;

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
        foreach (PlayerController player in players)
        {
            if (!player.playerAlive)
            {
                player.PlayerAlive();
            }
            Vector2 spawnPosition = SpawnZone.instance.GetSpawnLocation();
            player.transform.position = new Vector3(spawnPosition.x, spawnPosition.y, 0);
        }
    }

    public void RegisterPlayer(PlayerController player)
    {
        players.Add(player);
    }

    public void RemovePlayer(PlayerController player)
    {
        players.Remove(player);
    }
}
