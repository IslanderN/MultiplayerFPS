using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "BoxForPlayerLogic", menuName = "InteractionSystem/Logic/BoxForPlayerLogic", order = 0)]
public class BoxForPlayerLogic : Logic
{
    public override void DoSomething(NetworkIdentity interactableObj, NetworkIdentity player,  out bool keepAuthority)
    {
        InteractWithBox(interactableObj, player, out keepAuthority);
    }


    public void InteractWithBox(NetworkIdentity box, NetworkIdentity player, out bool keepAuthority)
    {
        keepAuthority = false;

        var boxBehaviour = box.GetComponent<BoxForPlayerBehaviour>();

        if (boxBehaviour.PlayerInside == player)
        {
            GetOutFromBox(box, player);
        }
        else if (boxBehaviour.PlayerInside == null)
        {
            GetInsideBox(box, player);
        }
        else
            Debug.Log("Cant interact, somebody inside");
    }


    public void GetInsideBox(NetworkIdentity box, NetworkIdentity player)
    {
        var boxBehaviour = box.GetComponent<BoxForPlayerBehaviour>();

        boxBehaviour.RpcPutPlayerInside(player);

        var targetConnection = player.GetComponent<NetworkBehaviour>().connectionToClient;
        boxBehaviour.TargetShowInterface(targetConnection);
        boxBehaviour.TargetShowCamera(targetConnection);

    }

    public void GetOutFromBox(NetworkIdentity box, NetworkIdentity player)
    {
        var boxBehaviour = box.GetComponent<BoxForPlayerBehaviour>();

        boxBehaviour.RpcGetOutPlayer(player);

        var targetConnection = player.GetComponent<NetworkBehaviour>().connectionToClient;
        boxBehaviour.TargetHideInterface(targetConnection);
        boxBehaviour.TargetHideCamera(targetConnection);


    }

}
