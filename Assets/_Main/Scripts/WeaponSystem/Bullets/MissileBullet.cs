using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBullet : Bullet
{
    private Transform target;
    private Rigidbody rb;
    private IEnumerator targetC;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (target)
        {
            Vector3 direction = target.position - rb.position;
            direction.Normalize();
            Vector3 rotationAmmount = Vector3.Cross(transform.forward, direction);
            rb.angularVelocity = rotationAmmount * BulletStats.RotationSpeed;
            rb.velocity = transform.forward * BulletStats.BulletSpeed;
        }      
    }

    private IEnumerator GetTarget()
    {
        while (true)
        {
            Collider[] candidates = Physics.OverlapSphere(transform.position, BulletStats.SearchingRange);
            foreach (Collider obj in candidates)
            {
                if (IsTargateable(obj.gameObject.layer))
                {
                    target = obj.transform;
                    break;
                }
            }
          
            yield return null;
        }
    }

    private bool IsTargateable(LayerMask layerMask)
    {
        for (int i = 0; i < BulletStats.MissileTargetMasks.Length; i++)
        {
            if (((1 << layerMask) & BulletStats.MissileTargetMasks[i]) != 0)
            {
                return true;
            }
        }

        return false;
    }

    public override void Run(Vector3 direction)
    {
        if (!rb)
        {
            rb = GetComponent<Rigidbody>();
        }

        rb.AddForce(direction * BulletStats.BulletSpeed, ForceMode.Impulse);
        transform.LookAt(direction - transform.position);

        if (targetC == null && !target)
        {
            targetC = GetTarget();
            StartCoroutine(GetTarget());
        }
    }

    public override void Stop()
    {
        StopAllCoroutines();
        targetC = null;
        target = null;
        gameObject.SetActive(false);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, BulletStats.SearchingRange);
    }
}
