using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour {
[SerializeField]
    private RectTransform thrusterFuelFill;

    [SerializeField]
    private RectTransform healthBarFill;

    [SerializeField]
    Text ammoText;

    [SerializeField]
    private GameObject crosshair;


    private Player player;
    private PlayerController controller;
    private WeaponManager weaponManager;

    public void SetPlayer(Player _player)
    {
        player = _player;
        controller = _player.GetComponent<PlayerController>();
        weaponManager = _player.GetComponent<WeaponManager>();
    }

    void Update()
    {
        SetFuelAmount(controller.GetThrusterFuelAmount());
        SetHealthAmount(player.GetHealthPct());
        if (weaponManager != null && weaponManager.GetCurrentWeapon() != null)
        {
            SetAmmoAmount(weaponManager.GetCurrentWeapon().bullets);
        }
    }

    void SetFuelAmount(float _amount)
    {
        thrusterFuelFill.localScale = new Vector3(1f, _amount, 1f);
    }

    void SetHealthAmount(float _amount)
    {
        healthBarFill.localScale = new Vector3(1f, _amount, 1f);
    }

    void SetAmmoAmount(int _amount)
    {
        ammoText.text = _amount.ToString();
    }


    // public funcitons for GameEvents
    public void ShowCrosshair(bool b)
    {
        crosshair.SetActive(b);
    }
	
}
