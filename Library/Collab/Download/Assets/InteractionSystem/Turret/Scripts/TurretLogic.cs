using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "TurretLogic", menuName = "InteractionSystem/Logic/TurretLogic")]
public class TurretLogic : Logic
{
    public override void DoSomething(NetworkIdentity interactableObj, NetworkIdentity player, out bool keepAuthority)
    {
        SitDownAtTurret(interactableObj, player, out keepAuthority);
    }

    private bool isSittingDown = false;

    private void SitDownAtTurret(NetworkIdentity interactableObj, NetworkIdentity player, out bool keepAuthority)
    {
        keepAuthority = false;

        if (!isSittingDown)
        {
            isSittingDown = true;
            interactableObj.GetComponent<TurretBehaviour>().RpcEnterTurret(player);
        }
        else
        {
            isSittingDown = false;
            interactableObj.GetComponent<TurretBehaviour>().RpcExitTurret(player);
        }
    }
}
