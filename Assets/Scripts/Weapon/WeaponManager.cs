using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System;

public class WeaponManager : NetworkBehaviour
{
    [SerializeField]
    private PlayerWeapon[] weapons;
    private int indexWeponInArray;


    [SerializeField]
    private PlayerWeapon primaryWeapon;

    [SerializeField]
    private Transform weaponHolder;

    [SerializeField]
    private string weaponLayerName = "Weapon";

    private PlayerWeapon currentWeapon;
    private WeaponGraphics currentGraphics;

    private Dictionary<PlayerWeapon, GameObject> weaponsDic = new Dictionary<PlayerWeapon, GameObject>();

    public bool isReloading = false;
    public bool isPuttingDown = false;
    public bool isPuttingOn = false;

    public bool IsInAct()
    {
        return isReloading || isPuttingDown || isPuttingOn;
    }


    void Start()
    {
        if (weapons != null && weapons.Length > 0)
        {
            primaryWeapon = weapons[0];
        }
        else
        {
            Debug.LogError("WeaponManager: No weapon in weapons");
        }
        CmdEquipWeapon(0);
        //RpcEquipWeapon(0);
        indexWeponInArray = 0;
    }

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public WeaponGraphics GetCurrentGraphics()
    {
        return currentGraphics;
    }

    [Command]
    private void CmdEquipWeapon(int indexInWeapons)
    {
       // Debug.Log(transform.name + " Cmd Test: weapon.graphics is null " + (_weapon.graphics == null));
        RpcEquipWeapon(indexInWeapons);
    }

    [ClientRpc]
    private void RpcEquipWeapon(int indexInWeapons)
    {
        //Debug.Log(transform.name + " Rpc Test: weapon.graphics is null " + (_weapon.graphics == null) + " weapon: [0].graphics is null " + (weapons[0].graphics == null));
        if (currentWeapon != null && weaponsDic.ContainsKey(currentWeapon))
        {
            //PutDown();
            weaponsDic[currentWeapon].SetActive(false);

        }
        if(indexInWeapons >= weapons.Length && indexInWeapons < 0)
        {
            Debug.LogError(transform.name + " WeaponManager: indexInWeapons = " + indexInWeapons + " IndexOutOfRangeException.");
        }
        currentWeapon = weapons[indexInWeapons];

        if (!weaponsDic.ContainsKey(currentWeapon))
        {
            CreateWeapon(weapons[indexInWeapons]);
        }
        else
        {
            weaponsDic[currentWeapon].SetActive(true);
            currentGraphics = weaponsDic[currentWeapon].GetComponent<WeaponGraphics>();
        }

        //PutOn();


    }

    private void CreateWeapon(PlayerWeapon _weapon)
    {
        //NetworkServer.Spawn(_weapon.graphics);
        GameObject _weaponIns = Instantiate(_weapon.graphics, weaponHolder.position + _weapon.graphics.transform.position, _weapon.graphics.transform.rotation);
        _weaponIns.transform.SetParent(weaponHolder);

        weaponsDic.Add(currentWeapon, _weaponIns);

        currentGraphics = _weaponIns.GetComponent<WeaponGraphics>();
        if (currentGraphics == null)
        {
            Debug.LogError("No WeaponGraphics component on the weapon object: " + _weaponIns.name);
        }


        if (isLocalPlayer)
        {
            Util.SetLayerRecursively(_weaponIns, LayerMask.NameToLayer(weaponLayerName));
        }

        _weapon.SetMaxBullets();
    }

    #region Reload

    public void Reload()
    {
        if (isReloading)
            return;
        StartCoroutine(Reload_Courotine());
    }

    private IEnumerator Reload_Courotine()
    {
        isReloading = true;

        CmdOnReload();

        yield return new WaitForSeconds(currentWeapon.reloadTime);

        currentWeapon.bullets = currentWeapon.maxBullets;

        isReloading = false;
    }

    [Command]
    private void CmdOnReload()
    {
        RpcOnReload();
    }

    [ClientRpc]
    private void RpcOnReload()
    {
        Animator anim = currentGraphics.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("Reload");
        }
    }
    #endregion

    #region PuttingDown

    private void PutDown()
    {
        if (isPuttingDown)
        {
            return;
        }
        StartCoroutine(PuttingDown_Courotine());
    }

    private IEnumerator PuttingDown_Courotine()
    {
        isPuttingDown = true;

        CmdOnPutDown();

        yield return new WaitForSeconds(currentWeapon.puttingDownTime);

        isPuttingDown = false;
    }

    [Command]
    private void CmdOnPutDown()
    {
        RpcOnPutDown();
    }

    [ClientRpc]
    private void RpcOnPutDown()
    {
        Animator anim = currentGraphics.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("PutDown");
        }
    }

    #endregion

    #region PuttingOn

    private void PutOn()
    {
        if (isPuttingOn)
            return;
        StartCoroutine(PuttinOn_Courotine());
    }

    private IEnumerator PuttinOn_Courotine()
    {
        isPuttingOn = true;

        CmdOnPutOn();

        yield return new WaitForSeconds(currentWeapon.puttingOnTime);

        isPuttingOn = false;
    }

    [Command]
    private void CmdOnPutOn()
    {
        RpcOnPutOn();
    }

    [ClientRpc]
    private void RpcOnPutOn()
    {
        Animator anim = currentGraphics.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("PutOn");
        }
    }
    #endregion

    //indexOfChanging: if > 0 change weapon to the next one
    //                 if < 0 change weapon to the previous one
    //                 if == 0 don't change
    [Client]
    public void SwitchWeapon(float indexOfChanging)
    {
        if (indexOfChanging == 0)
        {
            return;
        }

        if (indexOfChanging > 0)
        {
            indexWeponInArray++;
            if (indexWeponInArray >= weapons.Length)
            {
                indexWeponInArray = 0;
            }

             CmdEquipWeapon(indexWeponInArray);
            //RpcEquipWeapon(indexWeponInArray);
        }
        else if (indexOfChanging < 0)
        {
            indexWeponInArray--;
            if (indexWeponInArray < 0)
            {
                indexWeponInArray = weapons.Length - 1;
            }

            CmdEquipWeapon(indexWeponInArray);
            //RpcEquipWeapon(indexWeponInArray);
        }


    }

    public Vector3 GetSpredFactorVector()
    {
        return new Vector3(Random.Range(-currentWeapon.spreadFactor, currentWeapon.spreadFactor), Random.Range(-currentWeapon.spreadFactor, currentWeapon.spreadFactor));
    }

    private void AddWeapon(PlayerWeapon weapon)
    {
        Debug.Log("Add weapon");
        PlayerWeapon[] mass = new PlayerWeapon[weapons.Length + 1];
        weapons.CopyTo(mass, 0);
        mass[mass.Length - 1] = weapon;
        weapons = mass;

    }
    private void RemoveWeapon(PlayerWeapon weapon)
    {
        int index = System.Array.IndexOf(weapons, weapon);

        PlayerWeapon[] mass = new PlayerWeapon[weapons.Length - 1];
        int indMass = 0;
        for (int i = 0; i < weapons.Length; i++)
        {
            if(index != i)
            {
                mass[indMass] = weapons[i];
                indMass++;
            }
        }

        weapons = mass;
    }
    //private int indexOfWeaponBeforeActivate = 0;
    public void ActivateWeapon(PlayerWeapon weapon, GameObject weaponGO)
    {
        AddWeapon(weapon);

        if (weapons.Contains(weapon))
        {
            int index = System.Array.IndexOf(weapons, weapon);

            weaponsDic.Add(weapon, weaponGO);

            CmdEquipWeapon(index);
        }
        else
        {
            Debug.Log("Weapons doesn't contain this weapon. First you need AddWeapon().");
        }
    }

    public void DeactivateWeapon(PlayerWeapon weapon)
    {
        if (weaponsDic.ContainsKey(weapon))
        {
            weaponsDic.Remove(weapon);
            RemoveWeapon(weapon);

            CmdEquipWeapon(indexWeponInArray);
        }
    }

    //Haven't finished
    public void DeleteWeapon(PlayerWeapon weapon)
    {
        if (weaponsDic.ContainsKey(weapon))
        {
            GameObject weaponInstanse = weaponsDic[weapon];
            

        }
        else
        {
            Debug.LogError("Can't delete weapon. Weapon Dictionary doesn't contain weapon.");
        }
    }
}
