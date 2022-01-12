using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtractorBullet : Bullet
{
    private Rigidbody rb;
    private int atractedObjectsCount;
    private List<GameObject> atractedObjects = new List<GameObject>();
    private IEnumerator attractC;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void Run(Vector3 direction)
    {
        if (!rb)
        {
            rb = GetComponent<Rigidbody>();
        }

        rb.AddForce(direction * BulletStats.BulletSpeed, ForceMode.Impulse);

        if (attractC == null && atractedObjectsCount < BulletStats.MaxOrbitingObjects)
        {
            attractC = Attract();
            StartCoroutine(attractC);
        }
    }

    private void Update()
    {
        if (atractedObjects.Count > 0)
        {
            Orbit();
        }
    }

    private void Orbit()
    {
        for (int i = 0; i < atractedObjects.Count; i++)
        {
            atractedObjects[i].transform.RotateAround(transform.position, Vector3.right, BulletStats.OrbitingSpeed * Time.deltaTime);
            Vector3 desirePos = (atractedObjects[i].transform.position - transform.position).normalized * BulletStats.OrbitingRadius + transform.position;
            atractedObjects[i].transform.position = Vector3.MoveTowards(atractedObjects[i].transform.position, desirePos, Time.deltaTime * BulletStats.OrbitingSpeed);
        }
    }

    private IEnumerator Attract()
    {
        while (true)
        {
            Collider[] candidates = Physics.OverlapSphere(transform.position, BulletStats.CaptureRange);
            foreach (Collider obj in candidates)
            {
                if (IsTargateable(obj.gameObject.layer) && !ItemOnPool(obj.gameObject))
                {
                    atractedObjects.Add(obj.gameObject);
                    atractedObjectsCount++;

                    Rigidbody rb = obj.gameObject.GetComponent<Rigidbody>();

                    if (rb)
                    {
                        rb.useGravity = false;
                    }
                }
            }

            if (atractedObjectsCount > BulletStats.MaxOrbitingObjects)
            {
                attractC = null;
                break;
            }

            yield return new WaitForEndOfFrame();
        }       
    }

    private bool IsTargateable(LayerMask layerMask)
    {
        for (int i = 0; i < BulletStats.TargetLayers.Length; i++)
        {
            if (((1 << layerMask) & BulletStats.TargetLayers[i]) != 0)
            {
                return true;
            }
        }

        return false;
    }

    private void ReleaseObjects()
    {
        for (int i = 0; i < atractedObjects.Count; i++)
        {
            Rigidbody rb = atractedObjects[i].GetComponent<Rigidbody>();
            if (rb)
            {
                rb.useGravity = true;
            }
        }
    }

    public override void Stop()
    {
        rb.velocity = Vector3.zero;
        atractedObjects.Clear();
        StopAllCoroutines();
        attractC = null;
    }

    private bool ItemOnPool(GameObject obj)
    {
        for (int i = 0; i < atractedObjects.Count; i++)
        {
            if (atractedObjects[i] == obj)
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, BulletStats.CaptureRange);
    }
}
