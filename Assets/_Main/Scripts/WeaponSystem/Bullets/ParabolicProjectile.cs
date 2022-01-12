using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolicProjectile : Bullet
{
    private IEnumerator coroutine;
    private Vector3 direction;

    public override void Run(Vector3 direction)
    {
        if (coroutine == null)
        {
            this.direction = direction;
            coroutine = FollowPath();
            StartCoroutine(FollowPath());
        }
    }

    private IEnumerator FollowPath()
    {
        float totalDistance = (direction.magnitude * BulletStats.BulletRange);
        direction.Normalize();

        float distanceTravelled = 0f;

        Vector3 newPosition = transform.position;

        while (distanceTravelled <= totalDistance)
        {
            Vector3 deltaPath = direction * (BulletStats.BulletSpeed * Time.deltaTime);
            newPosition += deltaPath;
            distanceTravelled += deltaPath.magnitude;

            newPosition.y = BulletStats.BulletHeight * BulletStats.BulletDrop.Evaluate(distanceTravelled / totalDistance);

            transform.position = newPosition;

            yield return null;
        }

        coroutine = null;
    }
}
