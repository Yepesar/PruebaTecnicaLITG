using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtractorBullet : Bullet
{
    private Rigidbody rb;
    private int atractedObjectsCount;
    private List<GameObject> atractedObjects = new List<GameObject>();
    private List<OrbitProfile> orbiters = new List<OrbitProfile>();
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
        for (int i = 0; i < orbiters.Count; i++)
        {
            orbiters[i].Orbit();
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

                    OrbitProfile orbiter = new OrbitProfile(transform, obj.transform, BulletStats.MinOrbitSpeed, BulletStats.MaxOrbitSpeed, BulletStats.MinOrbitRadius, BulletStats.MaxOrbitRadius, BulletStats.MinRadiusSpeed, BulletStats.MaxRadiusSpeed);
                    orbiters.Add(orbiter);

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
        for (int i = 0; i < BulletStats.AttractorTargetLayers.Length; i++)
        {
            if (((1 << layerMask) & BulletStats.AttractorTargetLayers[i]) != 0)
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

        atractedObjects.Clear();
        orbiters.Clear();
    }

    public override void Stop()
    {
        ReleaseObjects();
        rb.velocity = Vector3.zero;
        orbiters.Clear();
        atractedObjects.Clear();
        StopAllCoroutines();
        attractC = null;
        gameObject.SetActive(false);
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

public class OrbitProfile
{
    private Vector3 axis;
    private Transform center;
    private Transform orbiter;
    private float orbitSpeed;
    private float orbitRadius;
    private float radiusSpeed;

    public OrbitProfile(Transform center,Transform orbiter, float minOrbitSpeed, float maxOrbitSpeed, float minOrbitRadius,float maxOrbitRadius, float minRadiusSpeed, float maxRadiusSpeed)
    {
        this.orbiter = orbiter;
        this.center = center;

        orbitSpeed = Random.Range(minOrbitSpeed,maxOrbitSpeed);
        orbitRadius = Random.Range(minOrbitRadius, maxOrbitRadius);
        radiusSpeed = Random.Range(minRadiusSpeed, maxRadiusSpeed);

        int rand = Random.Range(1, 6);

        if (rand == 1)
        {
            axis = Vector3.up;
        }
        else if (rand == 2)
        {
            axis = Vector3.down;
        }
        else if (rand == 3)
        {
            axis = Vector3.right;
        }
        else if (rand == 4)
        {
            axis = Vector3.left;
        }
        else if (rand == 5)
        {
            axis = Vector3.forward;
        }
        else if (rand == 6)
        {
            axis = Vector3.back;
        }
    }

    public void Orbit()
    {
        orbiter.RotateAround(center.position ,axis, orbitSpeed * Time.deltaTime);
        Vector3 desirePos = (orbiter.position - center.position).normalized * orbitRadius + center.position;
        orbiter.position = Vector3.MoveTowards(orbiter.transform.position, desirePos, Time.deltaTime * radiusSpeed);
    }
}
