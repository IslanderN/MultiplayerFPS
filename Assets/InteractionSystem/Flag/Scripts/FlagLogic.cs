using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[CreateAssetMenu(fileName = "FlagLogic", menuName = "InteractionSystem/Logic/FlagLogic", order = 0)]
public class FlagLogic : Logic
{
    public override void DoSomething(NetworkIdentity interactableObj, NetworkIdentity player, out bool keepAuthority)
    {
        //base.DoSomething(interactableObj, player);
        Debug.Log("Capturing Flag");
        CaptureFlag(interactableObj, player, out keepAuthority);
    }
    private void CaptureFlag(NetworkIdentity interactableObj, NetworkIdentity player,  out bool keepAuthority)
    {
        keepAuthority = false;

        var flag = interactableObj.GetComponent<Flag>();

        if (!flag) { Debug.Log("Cant find flag component"); return; }

        var playerName = player.GetComponent<PlayerName>();
        if (!playerName) { Debug.Log("Cant find player name component"); return; }

        flag.ChangeOwner(playerName.Value);

        Debug.Log("Flag captured by " + playerName.Value);
    }
}