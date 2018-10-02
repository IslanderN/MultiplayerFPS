using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace Interactions
{
    public class Flag : Interactable
    {
        [SerializeField]
        private Text FlagField;

        [SyncVar]
        public string KingName;

        [SyncVar]
        public string NetInfo;

        public Text InfoDisplay;


        /// <summary>
        /// not used now
        /// </summary>
        /// <param name="playerName"></param>
        [Command]
        public override void CmdInteract(string playerName)
        {

            //RpcInteractOnAllClinets(playerName);

            CaptureFlag(playerName);
        }

        public void Interact(NetworkIdentity client, string playerName)
        {
            Debug.Log("Interacting with flag: " + transform.name);

            NetworkIdentity identity = GetComponent<NetworkIdentity>();
            var currentOwner = identity.clientAuthorityOwner;


            NetInfo = "";

            if (currentOwner == connectionToClient)
            {
                NetInfo = NetInfo + "currentOwner == Connection to client" + "\n";

            }
            else
                NetInfo = NetInfo + "currentOwner != Connection to client" + "\n";


            if (currentOwner == connectionToServer)
            {
                NetInfo = NetInfo + "currentOwner == connectionToServer" + "\n";
            }
            else
                NetInfo = NetInfo + "currentOwner != connectionToServer" + "\n";

            if (hasAuthority)
                NetInfo = NetInfo + " Has authority" + "\n";
            else
                NetInfo = NetInfo + " Has no authority" + "\n";

            //Debug.Log(connectionToClient)


            //NetworkIdentity ni = this.GetComponent<NetworkIdentity>();

            //if(ni.AssignClientAuthority(client.connectionToClient))
            //{
            //    Debug.Log(ni.name + " assign client authority");
            //}
            //else
            //{
            //    Debug.Log("ER: "+ ni.name + " don't assign client authority");
            //}
            

            Cmd_AssignLocalAuthority(client, this.gameObject);

            CmdInteractTemp(playerName);
        }

        [Command]
        public void CmdInteractTemp(string playerName)
        {
            Debug.Log("Interacting with flag: " + transform.name);
            // RpcInteractOnAllClinets(playerName);

            RpcInteractOnAllClinets(playerName);
        }

        [Command]
        void Cmd_AssignLocalAuthority(NetworkIdentity client, GameObject obj)
        {

            Debug.Log("inside cmd assign");
            NetworkInstanceId nIns = obj.GetComponent<NetworkIdentity>().netId;
            //					GameObject _client = NetworkServer.FindLocalObject (nIns);
            //					NetworkIdentity ni = client.GetComponent<NetworkIdentity> ();

            //					NetworkIdentity ni = client;

            NetworkIdentity ni = this.GetComponent<NetworkIdentity>();

            if (ni.AssignClientAuthority(client.connectionToClient))
            {
                Debug.Log(ni.name + " assign client authority");
            }
            else
            {
                Debug.Log("ER: " + ni.name + " don't assign client authority");
            }
        }

        [Command]
        void Cmd_RemoveLocalAuthority(GameObject obj)
        {
            NetworkInstanceId nIns = obj.GetComponent<NetworkIdentity>().netId;
            GameObject client = NetworkServer.FindLocalObject(nIns);
            NetworkIdentity ni = client.GetComponent<NetworkIdentity>();
            ni.RemoveClientAuthority(ni.clientAuthorityOwner);
        }

        private void CaptureFlag(string playerName)
        {

            #region old

            // NetworkIdentity identity = GetComponent<NetworkIdentity>();

            // var currentOwner = identity.clientAuthorityOwner;

            // if (currentOwner == connectionToClient)
            // {
            //     return;
            // }
            // else
            // {
            //     if (currentOwner != null)
            //     {
            //         identity.RemoveClientAuthority(currentOwner);
            //     }
            //     identity.AssignClientAuthority(connectionToClient);

            // }
            #endregion




        }

        [ClientRpc]
        public override void RpcInteractOnAllClinets(string playerName)
        {

            #region old





            // 	if(currentOwner == connectionToClient)
            // 		Debug.Log("connection to client");
            // 	else if (currentOwner == connectionToServer)
            // 	{
            // 		Debug.Log("connection to server");
            // 	}
            // 	else
            // 	Debug.Log("Current owner is " + currentOwner.ToString());	

            //     return;
            // }
            // else
            // {
            //     if (currentOwner != null)
            //     {
            //         identity.RemoveClientAuthority(currentOwner);
            // 		Debug.Log("Remobing client authority");
            //     }
            //     identity.AssignClientAuthority(connectionToClient);
            // 	Debug.Log("Connection to the client");
            // }

            #endregion

            // CaptureFlag(playerName);
            KingName = playerName;
        }

        private void Start()
        {
            NetInfo = "";
        }

        private void Update()
        {
            FlagField.text = KingName;
            InfoDisplay.text = NetInfo;
        }

    }

}
