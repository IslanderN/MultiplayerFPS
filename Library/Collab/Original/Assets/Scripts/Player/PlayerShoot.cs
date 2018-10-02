using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour
{
    private const string PLAYER_TAG = "Player";

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    private PlayerWeapon currentWeapon;
    private WeaponManager weaponManager;

    private bool isInCooldown = false;

    void Start()
    {
        if (cam == null)
        {
            Debug.LogError("PlayerShoot: No camera referenced!");
            this.enabled = false;
        }

        weaponManager = GetComponent<WeaponManager>();
    }


    void Update()
    {
        currentWeapon = weaponManager.GetCurrentWeapon();

        if (PauseMenu.isOn)
        {
            CancelInvoke();
            return;
        }

        if (currentWeapon != null && currentWeapon.bullets < currentWeapon.maxBullets) 
        {
            if (Input.GetButtonDown("Reload"))
            {
                weaponManager.Reload();
                return;
            }

        }

        if (currentWeapon != null && currentWeapon.fireRate <= 0f)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else if (currentWeapon != null) 
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("Shoot", 0f, 1f / currentWeapon.fireRate);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shoot");
            }

        }
    }

    // Is called on the server when a player shoots
    [Command]
    private void CmdOnShoot()
    {
        RpcDoShootEffects();
    }

    // Is called on all clients when we need to
    // a shoot effects
    [ClientRpc]
    private void RpcDoShootEffects()
    {
        weaponManager.GetCurrentGraphics().muzzleFlash.Play();
    }

    // Is called on the server when we hit smth
    // Takes in the hit point and the normal of the surface
    [Command]
    private void CmdOnHit(Vector3 _pos, Vector3 _normal)
    {
        RpcDoHitEffects(_pos, _normal);
    }

    // Is called on all clients
    // Spawn effects
    [ClientRpc]
    private void RpcDoHitEffects(Vector3 _pos, Vector3 _normal)
    {
        GameObject _hitEffect = (GameObject)Instantiate(weaponManager.GetCurrentGraphics().hitEffectPrefab, _pos, Quaternion.LookRotation(_normal));

        Destroy(_hitEffect, 2f);
    }

    [Client]
    private void Shoot()
    {
        if (!isLocalPlayer || weaponManager.IsInAct() )
        {
            CancelInvoke();
            return;
        }
        if(isInCooldown)
        {
            return;
        }

        if (currentWeapon.bullets <= 0)
        {
            weaponManager.Reload();
            return;
        }

        currentWeapon.bullets--;

        Debug.Log("Remaining bullets: " + currentWeapon.bullets);

        // We are shooting, call the OnShoot method on the server
        CmdOnShoot();

        //int bulletsPerShoot = weaponManager.GetCurrentWeapon().bulletsPerShoot;
        //if(bulletsPerShoot<1 || bulletsPerShoot>)

        for (int i = 0; i < weaponManager.GetCurrentWeapon().bulletsPerShoot; i++)
        {
            SentRaycast();
        }
        //SentRaycast();

        if (currentWeapon.bullets <= 0)
        {
            weaponManager.Reload();
        }
        else
        {
            if (weaponManager.GetCurrentWeapon().cooldownShooting != 0f)
            {
                StartCoroutine(Cooldown_Courotine());
            }
        }
    }

    private void SentRaycast()
    {
        Vector3 direction = cam.transform.forward + weaponManager.GetSpredFactorVector();

        RaycastHit _hit;
        if (Physics.Raycast(cam.transform.position, direction, out _hit, currentWeapon.range, mask))
        {
            if (_hit.collider.tag == PLAYER_TAG)
            {

                CmdPlayerShoot(_hit.collider.name, currentWeapon.damage, transform.name);
                CmdPlayerShootForce(_hit.collider.name, direction, weaponManager.GetCurrentWeapon().force);

                GetComponent<PlayerSetup>().playerUIInstance.GetComponent<HitUIReaction>().HitReaction();
            }

            // We hit smth, call OnHit method on the server
            CmdOnHit(_hit.point, _hit.normal);

        }
    }

    private IEnumerator Cooldown_Courotine()
    {
        isInCooldown = true;
        float time = weaponManager.GetCurrentWeapon().cooldownShooting;
        Debug.Log("Wait " + time + " seconds");

        yield return new WaitForSeconds(time);
        //yield return new W

        isInCooldown = false;
    }
    

    [Command]
    private void CmdPlayerShoot(string _playerID, int _damage, string _sourceID)
    {
        Debug.Log(_playerID + " has been shoot!");

        Player _player = GameManager.GetPlayer(_playerID);
        _player.RpcTakeDamage(_damage, _sourceID);


        
    }

    [Command]
    private void CmdPlayerShootForce(string _playerID, Vector3 direction, float force)
    {
        GameManager.GetPlayer(_playerID).RpcPushForceAfterShoot(direction, force);
    }
}
