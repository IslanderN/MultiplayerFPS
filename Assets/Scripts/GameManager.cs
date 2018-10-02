using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public const string MAIN_CAM_TAG = "MainCamera";
    public const string UNTAGGED_CAM_TAG = "Untagged";

    public MatchSettings matchSettings;

    //public GameObject playerUI;
    //public delegate void OnPlayerUISetup(GameObject playerUI);
    //public OnPlayerUISetup OnSetUp;

    [SerializeField]
    private GameObject sceneCamera;

    public delegate void OnPlayerKilledCallback(string player, string source);
    public OnPlayerKilledCallback onPlayerKilledCallback;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one GameManager in scene.");
        }
        else
        {
            instance = this;
        }
        Application.targetFrameRate = 60;
    }

    public void SetSceneCameraActive(bool _isActive)
    {
        if(sceneCamera == null)
        {
            return;
        }

        sceneCamera.SetActive(_isActive);
        if(sceneCamera.activeSelf)
        {
            sceneCamera.tag = MAIN_CAM_TAG;
        }
        else
        {
            sceneCamera.tag = UNTAGGED_CAM_TAG;
        }
    }

    #region Player tracking

    private const string PLAYER_ID_PREFIX = "Player ";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public static void RegisterPlayer(string _netID, Player _player)
    {
        string _playerID = PLAYER_ID_PREFIX + _netID;

        if(players.ContainsKey(_playerID))
        {
            Debug.Log("key already exist");
            return;
        }

        players.Add(_playerID, _player);
        _player.transform.name = _playerID;
    }

    public static void UnRegisterPlayer(string _playerID)
    {
        players.Remove(_playerID);
    }

    public static Player GetPlayer(string _playerID)
    {
        return players[_playerID];
    }

    public static Player[] GetAllPlayers()
    {
        return players.Values.ToArray();
    }

    //void OnGUI()
    //{
    //    GUILayout.BeginArea(new Rect(200, 200, 200, 500));
    //    GUILayout.BeginVertical();

    //    foreach(string _playerID in players.Keys)
    //    {
    //        GUILayout.Label(_playerID + " - " + players[_playerID].transform.name);
    //    }

    //    GUILayout.EndVertical();
    //    GUILayout.EndArea();
    //}

    #endregion
}
