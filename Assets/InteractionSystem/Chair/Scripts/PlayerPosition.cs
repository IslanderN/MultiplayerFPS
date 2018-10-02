using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerPosition : NetworkBehaviour
{
    [Command]
    public void CmdMovePlayerToPosition(Vector3 somePosition)
    {
        var newPlayerPosition = somePosition;

        transform.position = newPlayerPosition;
    }

}
