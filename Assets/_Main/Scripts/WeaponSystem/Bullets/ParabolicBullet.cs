using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicBullet : Bullet
{
    private IEnumerator coroutine;
    private Vector3 direction;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    #region Inheritance Methods
    public override void Run(Vector3 direction)
    {
        ClearTrail();

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

    public override void Stop()
    {
        ClearTrail();

        rb.velocity = Vector3.zero;

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = null;
        gameObject.SetActive(false);

    }
    #endregion

    private IEnumerator DropControl()
    {       
        while (true)
        {                    
            float dropRate = BulletStats.BulletDrop.Evaluate(Time.deltaTime);
            rb.AddForce(Physics.gravity * dropRate,ForceMode.Acceleration);

            yield return null;
        }
    }   
}
