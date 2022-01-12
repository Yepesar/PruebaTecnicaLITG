using UnityEngine;

public abstract class Weapon: MonoBehaviour
{
    [SerializeField] private SOWeaponStats weaponStats;

    public abstract void Init(Transform weaponPoint);
    public abstract void Shoot(Vector3 direction);
    public abstract void Reload();
}
