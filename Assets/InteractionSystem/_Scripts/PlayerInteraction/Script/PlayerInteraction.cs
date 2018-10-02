using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInteraction : NetworkBehaviour
{

    [SerializeField]
    private InteractionSystem interactionSystem;

    [Space]
    public InteractionBox Box;
    



    private void Start()
    {

        if (isLocalPlayer)
        {
            // Box.EnterDelegate += ShowInteractionText;

            // Box.StayDelegate += CanInteract;

            // Box.ExitDelegate += HideInteractionText; 
        }
    }



    private void OnDisable()
    {
        if (isLocalPlayer)
        {
            // Box.EnterDelegate -= ShowInteractionText;

            // Box.StayDelegate -= CanInteract;

            // Box.ExitDelegate -= HideInteractionText;
        }
    }

    public void Interact(NetworkIdentity interactableObj)
    {
        var interactable = interactableObj.GetComponent<Interactable>();

        if (interactable != null)
        {

            Tag tag = interactable.ObjectTag;

            foreach (var linq in interactionSystem.Dependencies)
            {
                if (linq.tag == tag)
                {
                    CmdInteract(interactableObj);
                    return;
                }
            }
        }
    }


    [Command]
    private void CmdInteract(NetworkIdentity interactableObj)
    {

        Debug.Log("Cmd start");
        //if(this.GetComponent<NetworkIdentity>().isServer) Debug.Log("Server here");

        var interactable = interactableObj.GetComponent<Interactable>();

        if (interactable != null)
        {
            Tag tag = interactable.ObjectTag;

            foreach (var linq in interactionSystem.Dependencies)
            {
                if (linq.tag == tag)
                {

                    CmdSetAuthority(interactableObj, this.GetComponent<NetworkIdentity>());

                    var player = GetComponent<NetworkIdentity>();
                    bool keepAuthority;
                    linq.logic.DoSomething(interactableObj, player, out keepAuthority);

                    if(!keepAuthority)
                        CmdRemoveAuthority(interactableObj, this.GetComponent<NetworkIdentity>());
                    return;
                }
            }
        }
    }



    [Command]
    void CmdSetAuthority(NetworkIdentity grabID, NetworkIdentity playerID)
    {
        grabID.AssignClientAuthority(playerID.connectionToClient);
    }

    [Command]
    void CmdRemoveAuthority(NetworkIdentity grabID, NetworkIdentity playerID)
    {
        grabID.RemoveClientAuthority(playerID.connectionToClient);
    }
}
