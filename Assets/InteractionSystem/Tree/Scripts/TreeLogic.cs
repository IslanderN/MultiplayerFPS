using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[CreateAssetMenu(fileName = "TreeLogic", menuName = "InteractionSystem/Logic/TreeLogic", order = 0)]
public class TreeLogic : Logic {
	public override void DoSomething(NetworkIdentity interactableObj, NetworkIdentity player, out bool keepAuthority)
    {
        CutTree(interactableObj,player,out keepAuthority);
    }

    private void CutTree(NetworkIdentity interactableObj, NetworkIdentity player, out bool keepAuthority)
    {
        Debug.Log("Cutting tree");
        interactableObj.GetComponent<TreeBehaviour>().CutIt();
        keepAuthority = false;
    }
}