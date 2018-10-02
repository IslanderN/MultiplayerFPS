using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(TurretRotation))]
public class TurretBehaviour : NetworkBehaviour
{
   // [SerializeField]
   // private Transform sittingPlace;

    //private bool isSittingDown = false;
    private PlayerMotor playerMotor;
    private PlayerController playerController;

    [SerializeField]
    private PlayerWeapon weapon;

    private TurretRotation turretRotation;

    private Camera standartPlayerCamera;
    [SerializeField]
    private Camera turretCamera;

    [SerializeField]
    private GameEvent OnEnterTurret;

    [SerializeField]
    private GameEvent OnExitTurret;

    //float standartYCameraRotationLimit;

    void Start()
    {
        turretRotation = GetComponent<TurretRotation>();
    }
    

    [ClientRpc]
    public void RpcEnterTurret(NetworkIdentity player)
    {
        //ActivatePlayer(false, player);
    }

    [ClientRpc]
    public void RpcExitTurret(NetworkIdentity player)
    {
        //ActivatePlayer(true, player);
    }

    [TargetRpc]
    private void TargetOnEnter(NetworkConnection player)
    {

        OnEnterTurret.Raise();
    }

    [TargetRpc]
    private void TargetOnExit(NetworkConnection player)
    {

        OnExitTurret.Raise();
    }


    private void ActivatePlayer(bool activate, NetworkIdentity player)
    {
        if (activate)
        {
            GetComponent<TurretRotation>().enabled = false;

            player.GetComponent<PlayerController>().enabled = true;
            player.GetComponent<PlayerMotor>().enabled = true;

            if (standartPlayerCamera)
            {
                player.GetComponent<Player>().ChangeCamera(standartPlayerCamera);
                player.GetComponent<PlayerShoot>().ChangeCamera(standartPlayerCamera);

                standartPlayerCamera.enabled = true;
                turretCamera.enabled = false;
            }
            else
            {
                Debug.LogError("Turret don't have Standart Camera for Player in TurretBehaviour.");
            }
            WeaponManager weaponManager = player.GetComponent<WeaponManager>();
            if (weaponManager)
            {
                weaponManager.DeactivateWeapon(weapon);
            }
        }
        else
        {
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<PlayerMotor>().enabled = false;
            standartPlayerCamera = player.GetComponent<Player>().GetCamera();
            if (turretCamera)
            {
                player.GetComponent<Player>().ChangeCamera(turretCamera);
                player.GetComponent<PlayerShoot>().ChangeCamera(turretCamera);

                turretCamera.enabled = true;
                standartPlayerCamera.enabled = false;
            }
            else
            {
                Debug.LogError("Turret isn't camera in TurretBehaviour.");
            }
            GetComponent<TurretRotation>().enabled = true;

            WeaponManager weaponManager = player.GetComponent<WeaponManager>();
            if (weaponManager)
            {
                weaponManager.ActivateWeapon(weapon, gameObject);
            }

        }
    }

    //[ClientRpc]
    //public void RpcMovePlayerOnSittingPosition(NetworkIdentity player)
    //{
    //    if (isServer)
    //    {
    //        // Debug.Log("setting params on a server ");
    //        SitDown(player);


    //    }
    //    else
    //    {
    //        SitDown(player);
    //        // Debug.Log("setting params not on a server ");

    //        //var newPlayerPosition = sittingPlace.position;

    //        //player.transform.position = newPlayerPosition;
    //    }
    //}

    //private void SitDown(NetworkIdentity player)
    //{
    //    if (!isSittingDown)
    //    {
    //        //var newPlayerPosition = sittingPlace.position;

    //        //player.transform.position = newPlayerPosition;
    //    }

    //    isSittingDown = !isSittingDown;

    //    playerMotor = player.GetComponent<PlayerMotor>();
    //    playerController = player.GetComponent<PlayerController>();

    //    if (playerController && playerMotor)
    //    {
    //        playerController.BlockMovement(isSittingDown);
    //        if (isSittingDown)
    //        {
    //            standartYCameraRotationLimit = playerMotor.CameraRotationLimit;
    //            playerMotor.CameraRotationLimit = 30f;
    //        }
    //        else
    //        {

    //            playerMotor.CameraRotationLimit = standartYCameraRotationLimit;
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError(player.name + " dont have PlayerMotor of PlayerController scripts.");
    //    }

    //    WeaponManager weaponManager = player.GetComponent<WeaponManager>();
    //    if (weaponManager)
    //    {
    //        //weaponManager.ActivateWeapon(new PlayerWeapon());
    //    }


    //}

    ////Limit player movement
    //private void DontMove()
    //{
    //    Debug.Log("Dont move");

    //    //playerMotor.Move(Vector3.zero);

    //}
}
