using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections;

public class JointGame : MonoBehaviour
{
    List<GameObject> roomList = new List<GameObject>();

    [SerializeField]
    private Text status;

    [SerializeField]
    private GameObject roomListItemPrefab;

    [SerializeField]
    private Transform roomListParent;

    private NetworkManager networkManager;

    void Start()
    {
        networkManager = NetworkManager.singleton;
        if(networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }

        RefreshRoomList();
    }

    public void RefreshRoomList()
    {
        CleaRoomList();

        if(networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();   
        }

        networkManager.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
        status.text = "Loading...";
    }

    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        status.text = "";

        if (!success || matchList == null) 
        {
            status.text = "Couldn't get room list.";
            return;
        }

        foreach(MatchInfoSnapshot match in matchList)
        {
            GameObject _roomListItemGameObject = Instantiate(roomListItemPrefab);
            _roomListItemGameObject.transform.SetParent(roomListParent);

            RoomListItem _roomListItem = _roomListItemGameObject.GetComponent<RoomListItem>();
            if(_roomListItem != null)
            {
                _roomListItem.Setup(match, JoinRoom);
            }
            

            // as well as setting up a callback function that will joint the game.

            roomList.Add(_roomListItemGameObject);
        }

        if(roomList.Count == 0)
        {
            status.text = "No rooms at the moment.";
        }
            
    }

    private void CleaRoomList()
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            Destroy(roomList[i]);
        }

        roomList.Clear();
    }

    public void JoinRoom(MatchInfoSnapshot _match)
    {
        networkManager.matchMaker.JoinMatch(_match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
        StartCoroutine(WaitForJoin());
    }

    IEnumerator WaitForJoin()
    {
        CleaRoomList();
        

        int countdown = GameManager.instance.matchSettings.timeWaitForConecting;
        while (countdown > 0)
        {
            status.text = "JOINING... (" + countdown + ")";

            yield return new WaitForSeconds(1f);

            countdown--;
        }

        status.text = "Failed to connect.";
        yield return new WaitForSeconds(1f);

        MatchInfo matchInfo = networkManager.matchInfo;
        if (matchInfo != null)
        {
            networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
            networkManager.StopHost();
        }

        RefreshRoomList();
    }
}
