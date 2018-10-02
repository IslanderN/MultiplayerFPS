using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerExecution : NetworkBehaviour
{

    public void PickUpObject(GameObject obj)
    {
        Debug.Log(obj.name + " destroy");
        CmdDestroy(obj);
    }

    public void InteractWithTransport(GameObject transport)
    {
        NetworkTransportState state = transport.GetComponent<ChangeContolledPlayer>().State;

        switch (state)
        {
            case NetworkTransportState.empty:
                {
                    EnableTransport(transport);
                    break;
                }
            case NetworkTransportState.controlled:
                {
                    Debug.Log("No free place");
                    break;
                }
        }
    }

    private void EnableTransport(GameObject transport)
    {
        GetComponent<Player>().SetActiveAsPlayer(false);
        var carExit = transport.GetComponent<CarExit>();
 
        var carSetup = transport.GetComponent<CarSetup>();
        if(carSetup != null)
        {
            carSetup.SetActiveAsPlayer(true);
        }
        if (carExit != null)
        {
            carExit.SetPlayer(gameObject);
        }

    }

    //private void SetCarCameraAsMain()
    //{
    //    GameManager.instance.SetSceneCameraActive(false);
    //    //cam.tag = GameManager.MAIN_CAM_TAG;
    //}

    [Command]
    private void CmdDestroy(GameObject obj)
    {
        RpcDestroy(obj);
        //NetworkServer.Destroy(obj);
    }

    [ClientRpc]
    private void RpcDestroy(GameObject obj)
    {
        //Destroy(obj);
        NetworkServer.Destroy(obj);

    }
    
}
