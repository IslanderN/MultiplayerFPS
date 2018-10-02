using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using System.Linq;

public class InteractionBox : MonoBehaviour
{
    public PlayerInteraction Player;

    public InteractionValidation Validator;

    [Space]
    [SerializeField]
    public InteractionUI interactionUI;

    private NetworkIdentity playerNetworkIdentity;

    #region legacy
    //  public delegate void OnEnterDelegate(NetworkIdentity other);
    //     public event OnEnterDelegate EnterDelegate;

    //     public delegate void OnStayDelegate(NetworkIdentity other);
    //     public event OnEnterDelegate StayDelegate;

    //     public delegate void OnExitDelegate(NetworkIdentity other);
    //     public event OnEnterDelegate ExitDelegate;

    #endregion

    //private List<Interactable> interactableList = null;

    private void Start()
    {
        playerNetworkIdentity = Player.GetComponent<NetworkIdentity>();
    }

    public KeyCode Key;

    private void OnTriggerStay(Collider other)
    {

        if (!Player.GetComponent<NetworkIdentity>().isLocalPlayer) return;

        // ignoring colliders on this object (but not preventing them of interacting with itself?)
        if (other.gameObject == gameObject) return;

        // Debug.Log("On Trigger other is " + other.name);

        interactionUI.On = false;
        PreInteraction(other);
        interactionUI.Repaint();
    }

    private void OnTriggerEnter(Collider other)
    {

    }


    private void PreInteraction(Collider other)
    {
        // Debug.Log("Pre Interaction");

        if (!playerNetworkIdentity.isLocalPlayer)
        {
            //Debug.Log(Player.name + " not local player");
            return;
        }
        else
        {
            //Debug.Log(Player.name + " is local player");
        }

        var localPlayer = other.GetComponent<PlayerInteraction>();
        if(localPlayer != null && localPlayer == Player)
        {
            //Debug.Log("preventing player from preinteracting with himself");
            return;
        }

        var networkIdentity = other.GetComponent<NetworkIdentity>();

        if (networkIdentity != null)
        {
            var interactable = networkIdentity.GetComponent<Interactable>();
            if (interactable != null)
            {
                //Debug.Log("Interactable other is " + other.gameObject.name);
                interactionUI.On = true;
                if (Validator == null)
                {
                    Debug.Log("No validator, assign it in inspector");
                }
                else if (Validator.CanInteract(Player))
                {
                    //Debug.Log("validation passed");
                    Player.Interact(interactable.GetComponent<NetworkIdentity>());
                }
                else
                {
                    //Debug.Log("No validation");
                }
            }
        }
    }
}


// if (other.tag == "Area")
//     if (EnterDelegate != null)
//         EnterDelegate.Invoke(other.GetComponent<NetworkIdentity>());