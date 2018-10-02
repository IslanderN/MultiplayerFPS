using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{

    [SerializeField]
    private RectTransform thrusterFuelFill;

    [SerializeField]
    private RectTransform healthBarFill;

    [SerializeField]
    Text ammoText;

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject scoreboard;


    private Player player;
    private PlayerController controller;
    private WeaponManager weaponManager;

    public void SetPlayer(Player _player)
    {
        player = _player;
        controller = _player.GetComponent<PlayerController>();
        weaponManager = _player.GetComponent<WeaponManager>();
    }

    void Start()
    {
        PauseMenu.isOn = false;
        //GameManager.instance.playerUI = this.transform.gameObject;
        //if(GameManager.instance.OnSetUp != null)
        //{
        //    GameManager.instance.OnSetUp.Invoke(this.gameObject);
        //}
    }

    void Update()
    {
        SetFuelAmount(controller.GetThrusterFuelAmount());
        SetHealthAmount(player.GetHealthPct());
        if (weaponManager != null && weaponManager.GetCurrentWeapon() != null)
        {
            SetAmmoAmount(weaponManager.GetCurrentWeapon().bullets);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            scoreboard.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            scoreboard.SetActive(false);
        }
    }

    public void TogglePauseMenu()
    {
        // pauseMenu.SetActive(!pauseMenu.activeSelf);
        // PauseMenu.isOn = pauseMenu.activeSelf;
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
}
