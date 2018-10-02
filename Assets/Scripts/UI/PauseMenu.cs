using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class PauseMenu : MonoBehaviour
{
    public static bool isOn = false;

    private NetworkManager networkManager;

    [SerializeField]
    private GameObject pausePanel;

    [SerializeField]
    private KeyCode pauseKey;

    [SerializeField]
    public GameEvent OnPauseMenuOpen;

    
    [SerializeField]
    public GameEvent OnPauseMenuClose;

    void Start()
    {
        networkManager = NetworkManager.singleton;
    }

    public void LeaveRoom()
    {
        MatchInfo matchInfo = networkManager.matchInfo;
        networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
        networkManager.StopHost();
    }

    public void ContinueGame()
    {
        SwitchPauseMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            SwitchPauseMenu();
        }
    }

    private void SwitchPauseMenu()
    {
        bool newMenuState = !isOn;
        isOn = newMenuState;

        pausePanel.SetActive(newMenuState);
        
        if(isOn)
            OnPauseMenuOpen.Raise();
        else
            OnPauseMenuClose.Raise();
    }
}
