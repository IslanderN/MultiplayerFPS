using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "LampLogic", menuName = "InteractionSystem/Logic/LampLogic")]
public class LampLogic : Logic
{
    public override void DoSomething(NetworkIdentity interactableObj, NetworkIdentity player, out bool keepAuthority)
    {
        Debug.Log("Doing Something");
        UseLamp(interactableObj, player, out keepAuthority);
    }

    private void UseLamp(NetworkIdentity interactableObj, NetworkIdentity player, out bool keepAuthority)
    {
        keepAuthority = false;

        var lamp = interactableObj.GetComponent<LampBehaviour>();
        lamp.RpcSwitch();

    }
}
