using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Text killCount;
    public Text deathCount;

    void Start()
    {
        if (UserAccountManager.isLoggedIn)
        {
            UserAccountManager.instance.GetData(OnReceivedData);
        }
    }

    void OnReceivedData(string data)
    {
        if (killCount == null || deathCount == null)
            return;

        killCount.text = "Kills: " + DataTranslator.DataToKills(data);
        deathCount.text = "Deaths: " + DataTranslator.DataToDeath(data);
    }

}
