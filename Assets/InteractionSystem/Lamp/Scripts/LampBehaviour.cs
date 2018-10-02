using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LampBehaviour : NetworkBehaviour
{
    // [SerializeField]
    // private Material yellow;
    // [SerializeField]
    // private Material white;


[SerializeField]
private GameObject white;
[SerializeField]
private GameObject yellow;


    // private Material activeMateral;

    // [ClientRpc]
    // public void RpcSwitchMaterials()
    // {
    //     var mat = gameObject.GetComponent<Renderer>().material;

    //     if (mat == yellow)
    //     {
    //         mat = white;
    //     }
    //     else
    //     {s
    //         mat = yellow;
    //     }

    // }

    [ClientRpc]
    public void RpcSwitch(){
        if(white.activeSelf){
            white.SetActive(false);
            yellow.SetActive(true);
        }
        else if(yellow.activeSelf){
            yellow.SetActive(false);
            white.SetActive(true);
        }
    }
}
