using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerController))]
public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    private string remoteLayerName = "RemotePlayer";

    [SerializeField]
    private string dontDrawLayerName = "DontDraw";

    
    [SerializeField]
    private GameObject playerGraphicsPrefab;

    void Start()
    {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            //GameManager.instance.SetSceneCameraActive(true);

            //Disable player graphics for local player
            Util.SetLayerRecursively(playerGraphicsPrefab, LayerMask.NameToLayer(dontDrawLayerName));

            
            GetComponent<Player>().SetupPlayer();

            string _username = "Loading...";
            if (UserAccountManager.isLoggedIn)
            {
                _username = UserAccountManager.playerUsername;
            }
            else
            {
                _username = transform.name;
            }

            CmdSetUsername(transform.name, _username);
        }

    }

    [Command]
    private void CmdSetUsername(string playerID, string username)
    {
        Player player = GameManager.GetPlayer(playerID);
        if (player != null)
        {
            Debug.Log(username + " has joined");
            player.username = username;
        }
    }

    //private void SetLayerRecursively(GameObject _obj, int _layer)
    //{
    //    _obj.layer = _layer;

    //    foreach(Transform child in _obj.transform)
    //    {
    //        SetLayerRecursively(child.gameObject, _layer);
    //    }
    //}

    public override void OnStartClient()
    {
        base.OnStartClient();

        RegisterThisPlayer();
        
    }

    private void RegisterThisPlayer()
    {
        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();

        GameManager.RegisterPlayer(_netID, _player);
    }

    private void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    private void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    //When object is destroyed
    void OnDisable()
    {
        Debug.Log("destroyed or maybe not active");

        if (isLocalPlayer)
        {
            GameManager.instance.SetSceneCameraActive(true);
        }
        GameManager.UnRegisterPlayer(transform.name);
    }

    private void OnEnable() {
        RegisterThisPlayer();

    }

}
