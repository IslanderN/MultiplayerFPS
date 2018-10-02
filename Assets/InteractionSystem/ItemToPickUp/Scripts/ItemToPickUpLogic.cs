using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "ItemToPickUpLogic", menuName = "InteractionSystem/Logic/ItemToPickUpLogic")]
public class ItemToPickUpLogic : Logic
{
    public override void DoSomething(NetworkIdentity interactableObj, NetworkIdentity player,  out bool keepAuthority)
    {
        Debug.Log("Doing Something");
        PickUp(interactableObj, player, out keepAuthority);
    }

    private void PickUp(NetworkIdentity interactableObj, NetworkIdentity player,  out bool keepAuthority)
    {
        keepAuthority = false;

        var item = interactableObj.GetComponent<ItemToPickUpBehaviour>();
        var targetConnection = player.GetComponent<NetworkBehaviour>().connectionToClient;
        


        item.TargetAddItemToInventory(targetConnection);
        item.TargetGetPickUpResult(targetConnection, player);

        //var playerPickUpResult = player.GetComponent<PlayerPickUpResult>();

        // if(playerPickUpResult.Value == true)
        //     item.DestoyItem();

        item.DestoyItem();

        //item.TargetDestroyItemOnAllClientsIfItTaken(targetConnection);

        //item.DestroyItemOnAllClientsIfItTaken();
    }
}
