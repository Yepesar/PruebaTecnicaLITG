using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicProjectile : Bullet
{
    private IEnumerator coroutine;
    private Vector3 direction;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void Run(Vector3 direction)
    {
        if (coroutine == null)
        {
            if (!rb)
            {
                rb = GetComponent<Rigidbody>();
            }

            rb.velocity = Vector3.zero;

            this.direction = direction;
            rb.AddForce(direction * BulletStats.BulletSpeed, ForceMode.Impulse);
            coroutine = DropControl();
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator DropControl()
    {       
        while (true)
        {                    
            float dropRate = BulletStats.BulletDrop.Evaluate(Time.deltaTime);
            rb.AddForce(Physics.gravity * dropRate,ForceMode.Acceleration);

            yield return null;
        }
    }

    public override void Stop()
    {
        rb.velocity = Vector3.zero;
        StopCoroutine(coroutine);
        coroutine = null;
    }
}
