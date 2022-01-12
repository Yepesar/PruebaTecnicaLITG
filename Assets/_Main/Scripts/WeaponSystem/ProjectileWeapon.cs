using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : Weapon
{
    public override void Init()
    {
        if (PoolingSystem.Instance)
        {
            PoolingSystem.Instance.CreatePoolItem(WeaponStats.WeaponProjectilePrefab, WeaponStats.PoolLength, WeaponStats.WeaponProjectilePrefab.name);
        }
        else
        {
            Debug.LogError("Theres not an active pooling system on the scene!");
        }
    }

    public override void Reload()
    {
    }

    public override void Shoot()
    {
       GameObject bullet =  PoolingSystem.Instance.GetAvailableItem(WeaponStats.WeaponProjectilePrefab.name);
       bullet.gameObject.SetActive(true);
       bullet.transform.position = ExitPoint.position;
       Vector3 direction = ExitPoint.position - StartPoint.position;

        if (bullet)
        {
            bullet.GetComponent<Bullet>().Init(WeaponStats);
            bullet.GetComponent<Bullet>().Run(direction);
        }
    }

}
