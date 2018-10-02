using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "SimpleCarLogic", menuName = "InteractionSystem/Logic/SimpleCarLogic")]
public class SimpleCarLogic : Logic
{
    public override void DoSomething(NetworkIdentity interactableObj, NetworkIdentity player, out bool keepAuthority)
    {
        Debug.Log("Interacting with car");
        InteractWithCar(interactableObj, player, out keepAuthority);
    }

    private void InteractWithCar(NetworkIdentity car, NetworkIdentity player, out bool keepAuthority)
    {
        var simpleCar = car.GetComponent<SimpleCarBehaviour>();

        if (simpleCar.PlayerInside == null)
        {
            Debug.Log("Get inside this car");
            GetInsideCar(car, player);
            keepAuthority = true;
        }
        else if (simpleCar.PlayerInside == player)
        {
            Debug.Log("Get out of this car");
            GetOutFromCar(car, player);
            keepAuthority = false;
        }
        else if ( simpleCar.PlayerInside != player)
        {
            Debug.Log("Cant interact, somebody inside");
            keepAuthority = false;
        }
        else
        {
            Debug.LogWarning("exeption here");
            keepAuthority = false;
        }
    }

    public void GetInsideCar(NetworkIdentity car, NetworkIdentity player)
    {
        var simpleCar = car.GetComponent<SimpleCarBehaviour>();

        simpleCar.RpcPutPlayerInside(player);
        player.GetComponent<Player>().SetActiveAsPlayer(false);

        var targetConnection = player.GetComponent<NetworkBehaviour>().connectionToClient;
        simpleCar.TargetShowCarInterface(targetConnection);
        simpleCar.TargetShowCamera(targetConnection);
        simpleCar.TargetActivateControll(targetConnection);
        simpleCar.TargetOnPlayerGetIn(targetConnection);

    }

    public void GetOutFromCar(NetworkIdentity car, NetworkIdentity player)
    {
        var simpleCar = car.GetComponent<SimpleCarBehaviour>();

        simpleCar.RpcGetOutPlayer(player, simpleCar.ExitPoint.position);
        player.GetComponent<Player>().SetActiveAsPlayer(true);

        var targetConnection = player.GetComponent<NetworkBehaviour>().connectionToClient;
        simpleCar.TargetHideCarInterface(targetConnection);
        simpleCar.TargetHideCamera(targetConnection);
        simpleCar.TargetDisableControll(targetConnection);
        simpleCar.TargetOnPlayerGetOut(targetConnection);


    }


}
