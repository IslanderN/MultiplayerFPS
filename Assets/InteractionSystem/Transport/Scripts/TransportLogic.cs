using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[CreateAssetMenu(fileName = "TransportLogic", menuName = "InteractionSystem/Logic/TransportLogic", order = 0)]
public class TransportLogic : Logic
{

    public override void DoSomething(NetworkIdentity interactableObj, NetworkIdentity player, out bool keepAuthority)
    {
        RpcGetInsideTransport(interactableObj, player, out keepAuthority);
    }

    [ClientRpc]
    private void RpcGetInsideTransport(NetworkIdentity interactableObj, NetworkIdentity player,out bool keepAuthority)
    {
        var transportB = interactableObj.GetComponent<TransportBehaviour>();

        if (transportB.Driver == null)
        {
            transportB.Driver = player;

            player.gameObject.SetActive(false);

            transportB.State = NetworkTransportState.controlled;

            transportB.TempText = "somebody";
        }
        else
        {
            Debug.LogWarning("Other man inside");
        }

        keepAuthority = true;



    }

}
