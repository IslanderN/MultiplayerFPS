using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


[RequireComponent(typeof(PlayerSetup))]
public class Player : NetworkBehaviour
{

	[SyncVar]
    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }


	[SerializeField]
    private Camera cam;
    [SerializeField]
    private GameEvent cameraController;
    [SerializeField]
    private GameEvent returnCameraController;

    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;

    [SyncVar]
    public string username = "Loading...";

    public int kills;
    public int death;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    [SerializeField]
    private GameObject[] disableGameObjectsOnDeath;

    [SerializeField]
    private GameObject deathEffect;

    [SerializeField]
    private GameObject spawnEffect;

    private bool firstSetup = true;

    public float GetHealthPct()
    {
        return (float)currentHealth / maxHealth;
    }

    public void SetupPlayer()
    {
        // Switch cameras
        if (isLocalPlayer)
        {
            GameManager.instance.SetSceneCameraActive(false);
            cam.tag = GameManager.MAIN_CAM_TAG;
        }
        CmdBroadCastNewPlayerSetup();
    }

    [Command]
    private void CmdBroadCastNewPlayerSetup()
    {

        RpcSetupPlayerOnAllClients();
    }

    [ClientRpc]
    private void RpcSetupPlayerOnAllClients()
    {
        if (firstSetup)
        {
            wasEnabled = new bool[disableOnDeath.Length];
            for (int i = 0; i < wasEnabled.Length; i++)
            {
                wasEnabled[i] = disableOnDeath[i].enabled;
            }

            firstSetup = false;
        }

        SetDefaults();
    }

    // For testing player death
    //void Update()
    //{
    //    if (!isLocalPlayer)
    //        return;
    //    if (Input.GetKeyDown(KeyCode.K))
    //    {
    //        RpcTakeDamage(999999);
    //    }
    //}

    [ClientRpc]
    public void RpcTakeDamage(int _amount, string _sourceID)
    {
        if (isDead)
        {
            return;
        }
        //PushForceAfterShoot();

        currentHealth -= _amount;

        Debug.Log(transform.name + " now has " + currentHealth + " health.");

        if (currentHealth <= 0)
        {
            Die(_sourceID);
        }
    }

    [ClientRpc]
    public void RpcPushForceAfterShoot(Vector3 direction, float force)
    {
        //float speed = 1f;
        Vector3 velocirty = direction.normalized * force;

        GetComponent<PlayerMotor>().Move(velocirty);
    }

    private void Die(string _sourceID)
    {
        isDead = true;

        Player sourcePlayer = GameManager.GetPlayer(_sourceID);
        if (sourcePlayer != null)
        {
            sourcePlayer.kills++;
            GameManager.instance.onPlayerKilledCallback.Invoke(username, sourcePlayer.username);
        }


        death++;

        // Disable components
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        // Disable GameObjects 
        for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
        {
            disableGameObjectsOnDeath[i].SetActive(false);
        }


        // Disable the collider
        Collider _col = GetComponent<Collider>();
        if (_col != null)
        {
            _col.enabled = false;
        }

        //Spawn death effect
        GameObject _gfxIns = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(_gfxIns, 3f);

        // Switch cameras
        if (isLocalPlayer)
        {
            cam.tag = GameManager.UNTAGGED_CAM_TAG;
            GameManager.instance.SetSceneCameraActive(true);
        }

        Debug.Log(transform.name + " is DEAD");

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTime);

        Transform _spawnPosition = NetworkManager.singleton.GetStartPosition();
        transform.position = _spawnPosition.position;
        transform.rotation = _spawnPosition.rotation;

        yield return new WaitForSeconds(0.1f);

        SetupPlayer();

        Debug.Log(transform.name + " respawned");
    }

    public void SetDefaults()
    {
        isDead = false;

        currentHealth = maxHealth;

        // Enable the components
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        // Enable the GameObjects
        for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
        {
            disableGameObjectsOnDeath[i].SetActive(true);
        }

        // Enable the collider
        Collider _col = GetComponent<Collider>();
        if (_col != null)
        {
            _col.enabled = true;
        }


        // Create spawn effect
        GameObject _gfxIns = (GameObject)Instantiate(spawnEffect, transform.position, Quaternion.identity);
        Destroy(_gfxIns, 3f);
    }

    public void SetActiveAsPlayer(bool active)
    {
        Debug.Log(transform.name + " is activated = " + active);

        if (!active)
        {
            // Disable components
            for (int i = 0; i < disableOnDeath.Length; i++)
            {
                disableOnDeath[i].enabled = false;
            }

            // Disable GameObjects 
            for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
            {
                disableGameObjectsOnDeath[i].SetActive(false);
            }


            // Disable the collider
            Collider _col = GetComponent<Collider>();
            if (_col != null)
            {
                _col.enabled = false;
            }

            // Switch cameras
            if (isLocalPlayer)
            {
                cam.tag = GameManager.UNTAGGED_CAM_TAG;
                //GameManager.instance.SetSceneCameraActive(true);
            }
        }
        else
        {
            // Enable the components
            for (int i = 0; i < disableOnDeath.Length; i++)
            {
                disableOnDeath[i].enabled = wasEnabled[i];
            }

            // Enable the GameObjects
            for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
            {
                disableGameObjectsOnDeath[i].SetActive(true);
            }

            // Enable the collider
            Collider _col = GetComponent<Collider>();
            if (_col != null)
            {
                _col.enabled = true;
            }

            if (isLocalPlayer)
            {
                //GameManager.instance.SetSceneCameraActive(false);
                cam.tag = GameManager.MAIN_CAM_TAG;
            }
        }
    }


    public void ChangeCamera(Camera camera)
    {
        if (camera)
        {
            cam.enabled = false;
            cam.tag = GameManager.UNTAGGED_CAM_TAG;
            
            cam = camera;
            cam.tag = GameManager.MAIN_CAM_TAG;

            cam.enabled = true;
        }
    }
    public Camera GetCamera()
    {
        return cam;
    }

    public void ChangeMainCamera()
    {
        cameraController.Raise();
    }


}
