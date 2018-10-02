using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "ChairLogic", menuName = "InteractionSystem/Logic/ChairLogic", order = 0)]
public class ChairLogic : Logic
{
    public override void DoSomething(NetworkIdentity interactableObj, NetworkIdentity player,  out bool keepAuthority)
    {
		UseChair(interactableObj,player, out keepAuthority);
    }

	private void UseChair(NetworkIdentity interactableObj, NetworkIdentity player,  out bool keepAuthority)
	{

		keepAuthority = false;
		Debug.Log("Using chair");
		var chair = interactableObj.GetComponent<ChairBehaviour>();
		// var playerPosition = player.transform.position;

		// playerPosition.CmdMovePlayerToPosition(chair.SittingPlace.position);

		chair.RpcMovePlayerOnSittingPosition(player);
	}
}
