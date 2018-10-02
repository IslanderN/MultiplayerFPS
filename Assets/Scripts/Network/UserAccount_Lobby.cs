using UnityEngine;
using UnityEngine.UI;

public class UserAccount_Lobby : MonoBehaviour
{
    public Text userNameText;

    void Start()
    {
        if (UserAccountManager.isLoggedIn)
        {
            userNameText.text = "Logged In As: " + UserAccountManager.playerUsername;
        }
        else
        {
            userNameText.text = "Not Logged In";
        }
    }
    public void LogOut()
    {
        if (UserAccountManager.isLoggedIn)
        {
            UserAccountManager.instance.LogOut();
        }
    }
}
