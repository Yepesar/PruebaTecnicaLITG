using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using StarterAssets;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField] private Transform weaponPoint;

    private StarterAssetsInputs input;
    private Weapon actualWeapon;
    private float fireTimer;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<StarterAssetsInputs>();

        Weapon w = weaponPoint.GetComponentInChildren<Weapon>();
        if (w)
        {
            actualWeapon = w;
            actualWeapon.Init();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (input.shoot && actualWeapon)
        {
            if (Time.time - fireTimer > actualWeapon.WeaponStats.Stats.ShootTime/ actualWeapon.WeaponStats.Stats.ShootRate)
            {
                fireTimer = Time.time;
                actualWeapon.Shoot();
            }
        }       
    }
}
