using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Interactions
{

	public class PlayerInteraction : MonoBehaviour
	{
		[Header("WIP, press f to interact")]
		public string TempPlayerName;

		[SerializeField]
		private Player playerRef;

		

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
			var interactable = other.GetComponent<Flag>();

			if (interactable != null)
			{ 
				// Debug.Log("Stay");

				if (Input.GetKeyDown(KeyCode.F))
				{
					var client = playerRef.GetComponent<NetworkIdentity>();
					
					interactable.Interact(client, TempPlayerName);
				}

			}
		}

	}

}