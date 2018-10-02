using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(WeaponManager))]
public class SwitchWeapons : NetworkBehaviour
{

    private bool blockedControll = false;

    void Update()
    {
        // do nothing here if controll is blocked
        if(blockedControll) return;

        if(!isLocalPlayer) return;


        WeaponManager weaponManager = GetComponent<WeaponManager>();

        if (Input.GetAxis("Mouse ScrollWheel") != 0f && !weaponManager.IsInAct())
        {
            weaponManager.SwitchWeapon(Input.GetAxis("Mouse ScrollWheel"));
        }
    }

    // public function for GameEvents
    public void BlockControll(bool block)
    {
        blockedControll = block;
    }


}
