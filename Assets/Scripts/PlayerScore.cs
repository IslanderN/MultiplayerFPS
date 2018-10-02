using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerScore : MonoBehaviour
{
    private int lastKills = 0;
    private int lastDeaths = 0;

    Player player;

    void Start()
    {
        player = GetComponent<Player>();
        StartCoroutine(SyncScoreLoop());
    }

    private void OnDestroy()
    {
        if (player != null)
        {
            SyncNow();
        }
    }

    IEnumerator SyncScoreLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            SyncNow();
        }
    }

    private void SyncNow()
    {

        if (UserAccountManager.isLoggedIn)
        {
            UserAccountManager.instance.GetData(OnDataRecieved);
        }
    }

    private void OnDataRecieved(string data)
    {
        if (player.kills <= lastKills && player.death <= lastDeaths)
            return;

        int killsSinceLast = player.kills - lastKills;
        int deathSinceLast = player.death - lastDeaths;

        //if (killsSinceLast <= 0 && deathSinceLast <= 0)
        //{
        //    return;
        //}

        int kills = DataTranslator.DataToKills(data);
        int deaths = DataTranslator.DataToDeath(data);

        int totalKills = killsSinceLast + kills;
        int totalDeaths = deathSinceLast + deaths;

        string newData = DataTranslator.ValuesToData(totalKills, totalDeaths);

        Debug.Log("Syncing: " + newData);

        lastKills = player.kills;
        lastDeaths = player.death;

        UserAccountManager.instance.SendData(newData);

    }
}
