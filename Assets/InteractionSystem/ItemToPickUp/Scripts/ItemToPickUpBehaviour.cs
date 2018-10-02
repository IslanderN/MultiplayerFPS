using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ItemToPickUpBehaviour : NetworkBehaviour
{
    [SerializeField]
    private Item item;

    private bool itemTaken = false;

    //public bool PickUpRezult { get {return pickUpRezult;}}

    [TargetRpc]
    public void TargetAddItemToInventory(NetworkConnection target)
    {
        itemTaken = Inventory.instance.Add(item); 
    }


    [TargetRpc]
    public void TargetGetPickUpResult(NetworkConnection target, NetworkIdentity player)
    {
         //Debug.Log("pick up result is = " + itemTaken);

        if(itemTaken == true)
        {
            var playerResult = player.GetComponent<PlayerPickUpResult>();
            playerResult.Value = true;
        }

            // CmdDestoyItem();
    }


    // public void DestroyItemOnAllClientsIfItTaken()
    // {
    //     if(itemTaken == true)
    //         CmdDestoyItem();
    // }

    [Command]
    private void CmdDestoyItem(/*ItemToPickUpBehaviour item*/)
    {
        Destroy(this.gameObject);
    }

    public void DestoyItem(/*ItemToPickUpBehaviour item*/)
    {
        Destroy(this.gameObject);
    }



    

    
    
}
