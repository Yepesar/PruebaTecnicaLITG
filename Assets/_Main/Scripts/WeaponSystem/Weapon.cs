using UnityEngine;

public abstract class Weapon: MonoBehaviour
{
    [SerializeField] private SOWeaponStats weaponStats;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform exitPoint;

    public SOWeaponStats WeaponStats { get => weaponStats; set => weaponStats = value; }
    public Transform StartPoint { get => startPoint; set => startPoint = value; }
    public Transform ExitPoint { get => exitPoint; set => exitPoint = value; }

    public abstract void Init();
    public abstract void Shoot();
    public abstract void Reload();
}
