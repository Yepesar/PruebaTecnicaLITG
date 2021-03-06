using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon: MonoBehaviour
{
    [SerializeField] private SOWeaponStats weaponStats;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform exitPoint;

    public SOWeaponStats WeaponStats { get => weaponStats; set => weaponStats = value; }
    public Transform StartPoint { get => startPoint; set => startPoint = value; }
    public Transform ExitPoint { get => exitPoint; set => exitPoint = value; }

    public  void Init()
    {
        CreatePool();
    }

    public  void Shoot()
    {
        GameObject bullet = PoolingSystem.Instance.GetAvailableItem(WeaponStats.WeaponProjectilePrefab.name);
        bullet.gameObject.SetActive(true);
        bullet.transform.position = ExitPoint.position;
        Vector3 direction = ExitPoint.position - StartPoint.position;

        if (bullet)
        {
            bullet.GetComponent<Bullet>().Init(WeaponStats);
            bullet.GetComponent<Bullet>().Run(direction);
        }

        StartCoroutine(ShootMuzzle());
    }

    private void CreatePool()
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

    private IEnumerator ShootMuzzle()
    {
        GameObject l = startPoint.GetChild(0).gameObject;
        if (l)
        {
            l.gameObject.SetActive(true);
        }       
        yield return new WaitForSeconds(0.1f);

        if (l)
        {
            l.gameObject.SetActive(false);
        }
    }
}
