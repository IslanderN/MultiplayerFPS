using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Interactable : NetworkBehaviour
{

    public Tag ObjectTag;

    private Rigidbody rgb;

    /// Когда добавляетьсякомпонент Intractable, автоматически добавляеться NetworkIdentety.
    ///Для того чтобы PlayerInteraction работал коректно, Local playerAutrority должен ровняться true.
    /// таким образу дизайнеру не нужно выставлять это автоматически.
    private void Reset()
    {
        GetComponent<NetworkIdentity>().localPlayerAuthority = true;
    }

    private void Start()
    {
        rgb = GetComponent<Rigidbody>();

        if (!rgb)
            Debug.LogErrorFormat("No rigidbody here");
    }




    private void OnTriggerEnter(Collider other)
    {

        // ignoring colliders on this object
        if (other.gameObject == gameObject)
            return;

        var interactionBox = other.GetComponent<InteractionBox>();

        if (interactionBox && interactionBox.Player.isLocalPlayer)
        {

            //Debug.Log("on enter inside");
			//Debug.Log("other name: " + other.name);
			
            //if (other.isTrigger) Debug.Log("Is trigger");
            //else Debug.Log("not trigger");


            interactionBox.interactionUI.On = true;
            interactionBox.interactionUI.Repaint();

        }
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
        var interactionBox = other.GetComponent<InteractionBox>();

        if (interactionBox && interactionBox.Player.isLocalPlayer)
        {
            interactionBox.interactionUI.On = false;
            interactionBox.interactionUI.Repaint();

        }

    }
}
