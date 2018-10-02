using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerSetup))]
public class ToSpawnPrefab : NetworkBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private GameObject spawnPrefab;
    [SerializeField]
    private float spawnDistance = 5f;


    private NetworkManager networkManager;

    void Start()
    {
        networkManager = NetworkManager.singleton;

        if (cam == null)
        {
            Debug.LogError("ToSpawnPrefab: No camera referenced!");
            this.enabled = false;
        }

        if (spawnPrefab != null)
        {
            if (spawnPrefab.GetComponent<NetworkTransform>() == null)
            {
                Debug.LogError(transform.name + ": " + spawnPrefab.transform.name + " need to have comonent Network Transform.");
            }
            else
            {
                var spawnPrefabsList = networkManager.GetComponent<NetworkManager>().spawnPrefabs;
                if(!spawnPrefabsList.Contains(spawnPrefab))
                {
                    spawnPrefabsList.Add(spawnPrefab);
                }
            }
        }

    }

    void Update()
    {
        if (Input.GetButtonUp("SpawnObject"))
        {
            CmdSpawnObject();
        }
    }

    [Command]
    private void CmdSpawnObject()
    {
        //RpcSpawnObject();
        Spawn();
    }

    [ClientRpc]
    private void RpcSpawnObject()
    {
        Spawn();
        //spawnObj.transform.GetChild(0).GetComponent<PickUpSystem>().SetPickUpField(GetComponent<PlayerSetup>().pickUpFieldInstance, GetComponent<Player>().username);

    }

    private void Spawn()
    {
        Vector3 playerPos = cam.transform.position;
        Vector3 playerDirection = cam.transform.forward;
        Quaternion playerRotation = cam.transform.rotation;
        Vector3 spawnPos = playerPos + playerDirection * spawnDistance;

        GameObject spawnObj = Instantiate(spawnPrefab, spawnPos, playerRotation);
        //Instantiate(spawnPrefab, spawnPos, playerRotation);
        //if ()
        //{
        NetworkServer.SpawnWithClientAuthority(spawnObj, connectionToClient);     
        // NetworkServer.SpawnWithClientAuthority(spawnObj, this.gameObject); // instead of  NetworkServer.Spawn
        //}
        //Debug.Log("Children : " + spawnObj.transform.childCount.ToString());


        //RpcSetEventsReciever(spawnObj.transform.GetChild(0));
        Debug.Log(transform.name + " spawn object.");

    }
}
