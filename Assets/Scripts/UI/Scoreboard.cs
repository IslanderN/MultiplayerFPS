using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField]
    GameObject playerScoreboardItemPrefab;

    [SerializeField]
    Transform playerScoreboardList;

    void OnEnable()
    {
        // Get an array of  players
        Player[] players = GameManager.GetAllPlayers();

        foreach (Player p in players)
        {
            GameObject itemGO = (GameObject)Instantiate(playerScoreboardItemPrefab, playerScoreboardList);
            PlayerScoreboardItem item = itemGO.GetComponent<PlayerScoreboardItem>();
            if (item != null)
            {
                item.Setup(p.username, p.kills, p.death);
            }
        }


    }

    void OnDisable()
    {
        foreach (Transform child in playerScoreboardList)
        {
            Destroy(child.gameObject);
        }
    }
}
