using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Interactions
{

	public class PlayerInteraction : NetworkBehaviour
	{
		[Header("WIP, press f to interact")]
		public string TempPlayerName;

		

		//public Key = KeyCode.F;
		


		public PlayableDirector InteractionUI;

		private void OnTriggerEnter(Collider other)
		{
			var interactable = other.GetComponent<Flag>();

			if (interactable != null)
			{
				// Debug.Log("Enter");

				InteractionUI.gameObject.SetActive(true);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			var interactable = other.GetComponent<Flag>();

			if (interactable != null)
			{
				// Debug.Log("Exit");

				InteractionUI.gameObject.SetActive(false);
			}
		}

		private void OnTriggerStay(Collider other)
		{
			if(!isLocalPlayer)
				return;

			var interactable = other.GetComponent<Flag>();

			if (interactable != null)
			{ 
				// Debug.Log("Stay");

				if (Input.GetKeyDown(KeyCode.F))
				{
					//interactable.Interact(client, TempPlayerName);



					CmdRemoveAuthority(other.GetComponent<NetworkIdentity>(), this.GetComponent<NetworkIdentity>());
					CmdSetAuthority(other.GetComponent<NetworkIdentity>(), this.GetComponent<NetworkIdentity>());
				}

			}
		}


		
	// public void OnTriggerStay(Collider other)
	// {
	
	// CmdSetAuthority(other.GetComponent<NetworkIdentity>(), this.GetComponent<NetworkIdentity>());

	// }

	// public void OnTriggerExit(Collider other)
	// {
	
	// CmdRemoveAuthority(other.GetComponent<NetworkIdentity>(), this.GetComponent<NetworkIdentity>());

	// }
		

	/// ASSIGN AND REMOVE CLIENT AUTHORITY///

	[Command]
	void CmdSetAuthority (NetworkIdentity grabID, NetworkIdentity playerID) {
		grabID.AssignClientAuthority (playerID.connectionToClient);
	}

	[Command]
	void CmdRemoveAuthority (NetworkIdentity grabID, NetworkIdentity playerID) {
		grabID.RemoveClientAuthority (playerID.connectionToClient);
	}

	}


	

}