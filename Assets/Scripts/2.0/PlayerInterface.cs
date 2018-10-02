using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInterface : NetworkBehaviour
{

    #region Prefabs

    [SerializeField]
    private GameObject playerStatsPrefab;

    [SerializeField]
    private GameObject inventoryPrefab;

    [SerializeField]
    private GameObject pauseMenuPreafab;

    #endregion

    #region Instances

    // [SerializeField]
    private GameObject playerStatsInstance;

    // [SerializeField]
    private GameObject inventoryInstance;

    // [SerializeField]
    private GameObject pauseMenuInstance;

    // [SerializeField]
    // private Scoreboard scoreboard;

    #endregion


    private void Start()
    {
        if (isLocalPlayer)
        {
            var player = GetComponent<Player>();

            InstantiatePrefabs();
            AssignReferencesForInstances(player);

        }
    }

    private void InstantiatePrefabs()
    {
        playerStatsInstance = Instantiate(playerStatsPrefab);
        inventoryInstance = Instantiate(inventoryPrefab);
        pauseMenuInstance = Instantiate(pauseMenuPreafab);

        playerStatsInstance.name = playerStatsPrefab.name;
        inventoryInstance.name = inventoryPrefab.name;
        pauseMenuInstance.name = pauseMenuPreafab.name;

    }

    private void AssignReferencesForInstances(Player player)
    {
        playerStatsInstance.GetComponent<PlayerStatsUI>().SetPlayer(player);
    }

    //  public void TogglePauseMenu()
    // {
    //     pauseMenu.SetActive(!pauseMenu.activeSelf);
    //     PauseMenu.isOn = pauseMenu.activeSelf;
    // }

    private void OnDisable()
    {
        if (isLocalPlayer)
        {

        }
    }


}
