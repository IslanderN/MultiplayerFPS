using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace Interactions
{
    public class Flag : Interactable
    {

        #region vars for name under box gameobject
        [SerializeField]
        private Text FlagField;

        [SyncVar]
        public string KingName;
        #endregion
      
        #region displaying network info
        [SyncVar]
        public string NetInfo;

        public NetDisplay Display;


        #endregion




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


            #region NetInfo
            // NetInfo = "";

            // if (currentOwner == connectionToClient)
            // {
            //     NetInfo = NetInfo + "currentOwner == Connection to client" + "\n";

            // }
            // else
            //     NetInfo = NetInfo + "currentOwner != Connection to client" + "\n";


            // if (currentOwner == connectionToServer)
            // {
            //     NetInfo = NetInfo + "currentOwner == connectionToServer" + "\n";
            // }
            // else
            //     NetInfo = NetInfo + "currentOwner != connectionToServer" + "\n";

            // if (hasAuthority)
            //     NetInfo = NetInfo + " Has authority" + "\n";
            // else
            //     NetInfo = NetInfo + " Has no authority" + "\n";

            #endregion




            Cmd_RemoveLocalAuthority(client, this.gameObject);
            Cmd_AssignLocalAuthority(client, this.gameObject);

            CmdInteract(playerName);
        }

        [Command]
        public void CmdInteractTemp(string playerName)
        {
            Debug.Log("Interacting with flag: " + transform.name);
            // RpcInteractOnAllClinets(playerName);

            RpcInteractOnAllClinets(playerName);
        }

        private void UpdateNetDisplay()
        {

            NetworkIdentity identity = GetComponent<NetworkIdentity>();
            var currentOwner = identity.clientAuthorityOwner;

            if (currentOwner == connectionToClient)
                Display.currentOwnerConToClient = true;
            else
                Display.currentOwnerConToClient = false;



            if (currentOwner == connectionToServer)
                Display.currentOwnerConToServer = true;
            else
                Display.currentOwnerConToServer = false;

            if (hasAuthority)
                Display.HasAuthority = true;
            else
                Display.HasAuthority = false;

        }


        [Command]
        void Cmd_RemoveLocalAuthority(NetworkIdentity client, GameObject obj)
        {
            NetworkInstanceId nIns = obj.GetComponent<NetworkIdentity>().netId;
            GameObject _client = NetworkServer.FindLocalObject(nIns);
            NetworkIdentity ni = _client.GetComponent<NetworkIdentity>();



            ni.RemoveClientAuthority(obj.GetComponent<NetworkIdentity>().clientAuthorityOwner);
        }

        [Command]
        void Cmd_AssignLocalAuthority(NetworkIdentity client, GameObject obj)
        {

            Debug.Log("inside cmd assign");
            NetworkInstanceId nIns = obj.GetComponent<NetworkIdentity>().netId;
            GameObject _client = NetworkServer.FindLocalObject(nIns);
            NetworkIdentity ni = _client.GetComponent<NetworkIdentity>();

            //					NetworkIdentity ni = client;

            // NetworkIdentity ni = this.GetComponent<NetworkIdentity>();
            ni.AssignClientAuthority(connectionToClient);
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


            TestScreen.Instance.Info = NetInfo;
            UpdateNetDisplay();
        }

    }

}
