using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject hitVFXPrefab;

    private BulletStatsModel bulletStats;
    public BulletStatsModel BulletStats { get => bulletStats; set => bulletStats = value; }
    
    private void Start()
    {
        if (hitVFXPrefab)
        {
            PoolingSystem.Instance.CreatePoolItem(hitVFXPrefab, 5, hitVFXPrefab.name);
        }       
    }

    public void Init(SOWeaponStats weaponStats)
    {
        bulletStats = weaponStats.Stats.BulletStats;
        bulletStats.BulletRange = weaponStats.Stats.WeaponRange;
        this.bulletStats.Damage = weaponStats.Stats.Damage;
    }

    public abstract void Run(Vector3 direction);

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hitVFX = PoolingSystem.Instance.GetAvailableItem(hitVFXPrefab.name);

        if (hitVFX)
        {
            hitVFX.transform.position = transform.position;
            hitVFX.GetComponent<ParticleSystem>().Play();
        }

        gameObject.SetActive(false);
    }
}
