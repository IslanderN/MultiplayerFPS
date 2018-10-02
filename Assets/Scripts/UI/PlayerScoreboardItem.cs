using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreboardItem : MonoBehaviour
{
    [SerializeField]
    private Text usernameText;

    [SerializeField]
    private Text killsText;

    [SerializeField]
    private Text deathText;

    public void Setup(string username, int kills, int death)
    {
        usernameText.text = username;
        killsText.text = "Kills: " + kills;
        deathText.text = "Deaths: " + death;
    }
}
