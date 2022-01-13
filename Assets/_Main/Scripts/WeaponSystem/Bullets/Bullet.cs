using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject hitVFXPrefab;

    private IEnumerator distanceC;
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
        this.bulletStats.Damage = weaponStats.Stats.Damage;
        this.bulletStats.Range = weaponStats.Stats.Range;

        if (distanceC == null)
        {
            distanceC = ChecktRange();
            StartCoroutine(distanceC);
        }
    }

    public abstract void Run(Vector3 direction);
    public abstract void Stop();

    private IEnumerator ChecktRange()
    {
        Vector3 startPos = transform.position;
        while (true)
        {
            float distance = Vector3.Distance(transform.position, startPos);
            if (distance >= bulletStats.Range)
            {
                Stop();
                distanceC = null;
                break;
            }

            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {       
        Stop();

        if (hitVFXPrefab)
        {
            GameObject hitVFX = PoolingSystem.Instance.GetAvailableItem(hitVFXPrefab.name);
            hitVFX.transform.position = transform.position;
            hitVFX.GetComponent<ParticleSystem>().Play();
        }

        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Stop();

        if (hitVFXPrefab)
        {
            GameObject hitVFX = PoolingSystem.Instance.GetAvailableItem(hitVFXPrefab.name);

            hitVFX.transform.position = transform.position;
            hitVFX.GetComponent<ParticleSystem>().Play();
        }

        StopAllCoroutines();
        gameObject.SetActive(false);
    }
}
