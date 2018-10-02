using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChairBehaviour : NetworkBehaviour
{

    public Transform SittingPlace;

    [ClientRpc]
    public void RpcMovePlayerOnSittingPosition(NetworkIdentity player)
    {
        if (isServer)
        {
            // Debug.Log("setting params on a server ");

            var newPlayerPosition = SittingPlace.position;

            player.transform.position = newPlayerPosition;
        }
        else
        {
            // Debug.Log("setting params not on a server ");

            var newPlayerPosition = SittingPlace.position;

            player.transform.position = newPlayerPosition;
        }
    }
}
