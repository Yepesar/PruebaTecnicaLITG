using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject hitVFXPrefab;

    private IEnumerator distanceC;
    private BulletStatsModel bulletStats;
    public BulletStatsModel BulletStats { get => bulletStats; set => bulletStats = value; }
    private TrailRenderer trailRenderer;

    public abstract void Run(Vector3 direction);
    public abstract void Stop();

    #region ClassMethods
   public void Init(SOWeaponStats weaponStats)
    {
        bulletStats = weaponStats.Stats.BulletStats;
        this.bulletStats.Damage = weaponStats.Stats.Damage;
        this.bulletStats.Range = weaponStats.Stats.Range;

        trailRenderer = GetComponent<TrailRenderer>();

        if (hitVFXPrefab)
        {
            PoolingSystem.Instance.CreatePoolItem(hitVFXPrefab, weaponStats.PoolLength, hitVFXPrefab.name);
        }

        if (distanceC == null)
        {
            distanceC = ChecktRange();
            StartCoroutine(distanceC);
        }

        ClearTrail();
    }

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
            ActivateHitVFX(transform.position);
        }

        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Stop();

        if (hitVFXPrefab)
        {
            ActivateHitVFX(transform.position);
        }

        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    public void ClearTrail()
    {
        if (trailRenderer)
        {
            trailRenderer.Clear();
        }
    }

    public void ActivateHitVFX(Vector3 pos)
    {
        GameObject hitVFX = PoolingSystem.Instance.GetAvailableItem(hitVFXPrefab.name);
        hitVFX.transform.position = pos;
        hitVFX.GetComponent<ParticleSystem>().Play();
    }
    #endregion
}
