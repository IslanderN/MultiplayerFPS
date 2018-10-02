using UnityEngine;

[System.Serializable]
public class PlayerWeapon
{
    public string name = "Glock";

    public int damage = 10;
    public float range = 100f;

    public float fireRate = 0f;
    public int maxBulletsPerShoot = 10;

    public int bulletsPerShoot = 1;
    

    public int maxBullets = 30;
    [HideInInspector]
    public int bullets;

    public float reloadTime = 1f;

    public GameObject graphics;

    //public bool isInstantiated = false;

    public float spreadFactor = 1f;

    public float force = 1f;

    public float cooldownShooting = 0f;

    public float puttingDownTime = 1f;
    public float puttingOnTime = 1f;

    public PlayerWeapon()
    {
        bullets = maxBullets;
        CheckBulletsPerShoot();
    }

    public void SetMaxBullets()
    {
        bullets = maxBullets;
        CheckBulletsPerShoot();
    }

    private void CheckBulletsPerShoot()
    {
        if (bulletsPerShoot < 1)
        {
            bulletsPerShoot = 1;
        }
        else if (bulletsPerShoot > maxBulletsPerShoot)
        {
            bulletsPerShoot = maxBulletsPerShoot;
        }
    }
}
