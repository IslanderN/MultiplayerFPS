using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BoxForPlayerBehaviour : NetworkBehaviour
{

    [Header("Test Var: must be null")]
    [SerializeField]
    private NetworkIdentity playerInside; // serialized only for testing

    [Space]

    [SerializeField]
    private GameObject BoxUI;

    [SerializeField]
    private Camera cam;

    public KeyCode Key;

    public NetworkIdentity PlayerInside
    {
        get { return playerInside; }
    }

    [ClientRpc]
    public void RpcPutPlayerInside(NetworkIdentity player)
    {
        player.gameObject.SetActive(false);
        playerInside = player;

        // if(isLocalPlayer) так чтобы только у одного чувака все вызывалось.

    }

    [ClientRpc]
    public void RpcGetOutPlayer(NetworkIdentity player)
    {
        player.gameObject.SetActive(true);
        playerInside = null;

        // if(isLocalPlayer) так чтобы только у одного чувака все вызывалось.

    }


    [TargetRpc]
    public void TargetShowInterface(NetworkConnection target)
    {
        BoxUI.SetActive(true);
    }

    [TargetRpc]
    public void TargetShowCamera(NetworkConnection target)
    {
        cam.gameObject.SetActive(true);
    }

    [TargetRpc]
    public void TargetHideInterface(NetworkConnection target)
    {
        BoxUI.SetActive(false);
    }


    [TargetRpc]
    public void TargetHideCamera(NetworkConnection target)
    {
        cam.gameObject.SetActive(false);
    }



    private void Update()
    {
        if (PlayerInside)
        {
            if (Input.GetKeyDown(Key))
            {
                Debug.Log("Interacting with box inside box");
                var player = playerInside.GetComponent<PlayerInteraction>();
                var thisBox = this.GetComponent<NetworkIdentity>();
                player.Interact(thisBox);

            }
        }
    }


}
