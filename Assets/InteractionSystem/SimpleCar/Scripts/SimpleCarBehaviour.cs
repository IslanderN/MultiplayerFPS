using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Vehicles.Car;

public class SimpleCarBehaviour : NetworkBehaviour
{
    [Header("Test Var: must be null")]
    [SerializeField]
    private NetworkIdentity playerInside; // serialized only for testing

    [SerializeField]
    private Transform exitPoint; // serialized only for testing


    [Space]

    [SerializeField]
    private GameObject CarUI;

    [SerializeField]
    private Camera cam;

    public KeyCode Key;

    //public KeyCode SetAuthorityKey;

    public NetworkIdentity PlayerInside { get { return playerInside; } }
    public Transform ExitPoint { get { return exitPoint; } }

    [Space]

    [SerializeField]
    private GameEvent OnPlayerGetIn;

    [SerializeField]
    private GameEvent OnPlayerGetOut;

    [ClientRpc]
    public void RpcPutPlayerInside(NetworkIdentity player)
    {
        player.gameObject.SetActive(false);
        playerInside = player;

        // if(isLocalPlayer) так чтобы только у одного чувака все вызывалось.

    }

    [ClientRpc]
    public void RpcGetOutPlayer(NetworkIdentity player, Vector3 exitPosition)
    {
        player.transform.position = exitPosition;
        player.gameObject.SetActive(true);
        playerInside = null;

        // if(isLocalPlayer) так чтобы только у одного чувака все вызывалось.

    }


    [TargetRpc]
    public void TargetShowCarInterface(NetworkConnection target)
    {
        CarUI.SetActive(true);
    }

    [TargetRpc]
    public void TargetShowCamera(NetworkConnection target)
    {
        cam.gameObject.SetActive(true);
    }

    [TargetRpc]
    public void TargetHideCarInterface(NetworkConnection target)
    {
        CarUI.SetActive(false);
    }


    [TargetRpc]
    public void TargetHideCamera(NetworkConnection target)
    {
        cam.gameObject.SetActive(false);
    }

    [TargetRpc]
    public void TargetActivateControll(NetworkConnection target)
    {
        var carController = GetComponent<CarController>();
        var carUserControl = GetComponent<CarUserControl>();

        carController.enabled = true;
        carUserControl.enabled = true;
    }

    [TargetRpc]
    public void TargetDisableControll(NetworkConnection target)
    {
        var carController = GetComponent<CarController>();
        var carUserControl = GetComponent<CarUserControl>();

        carController.enabled = false;
        carUserControl.enabled = false;
    }

    [TargetRpc]
    public void TargetOnPlayerGetIn(NetworkConnection target)
    {
        OnPlayerGetIn.Raise();
    }

    [TargetRpc]
    public void TargetOnPlayerGetOut(NetworkConnection target)
    {
        OnPlayerGetOut.Raise();
    }

    [Command]
    public void CmdSetAuthority(NetworkIdentity grabID, NetworkIdentity playerID)
    {
        grabID.AssignClientAuthority(playerID.connectionToClient);
    }



    private void Update()
    {
        if (playerInside)
        {
            if (playerInside.isLocalPlayer)
            {
                if (Input.GetKeyDown(Key))
                {
                    Debug.Log("Interacting with car inside car");
                    var player = playerInside.GetComponent<PlayerInteraction>();
                    var thiCar = this.GetComponent<NetworkIdentity>();
                    player.Interact(thiCar);

                }

                // if (Input.GetKeyDown(SetAuthorityKey))
                // {
                //     CmdSetAuthority(this.GetComponent<NetworkIdentity>(), playerInside);
                // }
            }
        }
    }
}
