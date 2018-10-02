using UnityEngine;
using UnityEngine.Networking;

public class PlayerName : NetworkBehaviour
{
    public string Value = "mister Bond";


    private void OnConnectedToServer()
    {

    }

    private void Start()
    {
        UpdateName();
    }

    void UpdateName()
    {
        if (isServer && isLocalPlayer)
            Value = "Server";
        else
            Value = "Client";
    }
}