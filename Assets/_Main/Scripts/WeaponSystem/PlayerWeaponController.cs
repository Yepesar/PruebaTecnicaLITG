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
    private bool canShoot = true;
    private bool canSwitchWeapon = true;

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
                canShoot = true;
            }

            if (canShoot)
            {
                actualWeapon.Shoot();
                CinemachineShake.Instance.ShakeCamera(actualWeapon.WeaponStats.ShakeFrequency, actualWeapon.WeaponStats.ShakeIntensity, actualWeapon.WeaponStats.ShakeDuration);
                canShoot = false;
            }
        }       
    }

    private void ChangeWeapon(Weapon newWeapon)
    {
        if (actualWeapon)
        {
            Rigidbody oldWeaponRB = actualWeapon.gameObject.GetComponent<Rigidbody>();
            oldWeaponRB.gameObject.transform.parent = null;
            oldWeaponRB.isKinematic = false;
            oldWeaponRB.useGravity = true;
            oldWeaponRB.AddForce(transform.forward * 0.5f, ForceMode.Impulse);
            oldWeaponRB.GetComponent<Collider>().enabled = true;
            actualWeapon = null;          
        }
        
        Rigidbody newWeaponRB = newWeapon.gameObject.GetComponent<Rigidbody>();
        newWeaponRB.isKinematic = true;
        newWeaponRB.useGravity = true;
        newWeapon.gameObject.transform.position = weaponPoint.position;
        newWeapon.transform.parent = weaponPoint;
        newWeapon.transform.rotation = weaponPoint.rotation;
        newWeapon.GetComponent<Collider>().enabled = false;

        actualWeapon = newWeapon;
        actualWeapon.Init();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            if (input.interact && canSwitchWeapon)
            {
                ChangeWeapon(other.gameObject.GetComponent<Weapon>());
                canSwitchWeapon = false;
                StartCoroutine(SwitchCooldown());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            if (input.interact && canSwitchWeapon)
            {
                ChangeWeapon(other.gameObject.GetComponent<Weapon>());
                canSwitchWeapon = false;
                StartCoroutine(SwitchCooldown());
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            if (input.interact && canSwitchWeapon)
            {
                ChangeWeapon(collision.gameObject.GetComponent<Weapon>());
                canSwitchWeapon = false;
                StartCoroutine(SwitchCooldown());
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            if (input.interact && canSwitchWeapon)
            {
                ChangeWeapon(collision.gameObject.GetComponent<Weapon>());
                canSwitchWeapon = false;
                StartCoroutine(SwitchCooldown());
            }
        }
    }

    private IEnumerator SwitchCooldown()
    {
        yield return new WaitForSeconds(0.2f);
        canSwitchWeapon = true;
    }
}
