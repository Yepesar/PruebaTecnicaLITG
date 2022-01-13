using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponStats", menuName = "Weapon/NewWeaponStats")]
public class SOWeaponStats : ScriptableObject
{
    [SerializeField] private string profileName;
    [SerializeField] private GameObject weaponProjectilePrefab;
    [SerializeField] private int poolLength = 0;
    [SerializeField] private WeaponStatsModel stats;

    public string ProfileName { get => profileName; set => profileName = value; }
    public GameObject WeaponProjectilePrefab { get => weaponProjectilePrefab; set => weaponProjectilePrefab = value; }
    public WeaponStatsModel Stats { get => stats; set => stats = value; }
    public int PoolLength { get => poolLength; set => poolLength = value; }
}

public enum WeaponType {Projectile, Atractor, Divider }
[System.Serializable]
public class WeaponStatsModel
{
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [SerializeField] private BulletStatsModel bulletStats;
    [SerializeField] private int maxAmmo;
    [SerializeField] private float shootRate;
    [SerializeField] private float shootTime;

    public WeaponType WeaponType { get => weaponType; set => weaponType = value; }
    public int Damage { get => damage; set => damage = value; }
    public int MaxAmmo { get => maxAmmo; set => maxAmmo = value; }
    public float ShootRate { get => shootRate; set => shootRate = value; }
    public float ShootTime { get => shootTime; set => shootTime = value; }
    public BulletStatsModel BulletStats { get => bulletStats; set => bulletStats = value; }
    public float Range { get => range; set => range = value; }
}

[System.Serializable]
public class BulletStatsModel
{
    [SerializeField] private float bulletSpeed;

    [Space]

    [Header("Projectile settings")]
    [SerializeField] private AnimationCurve bulletDrop;
    
    [Space]

    [Header("Attractor settings")]
    [SerializeField] private float minOrbitSpeed;
    [SerializeField] private float maxOrbitSpeed;

    [SerializeField] private float minOrbitRadius;
    [SerializeField] private float maxOrbitRadius;

    [SerializeField] private float minRadiusSpeed;
    [SerializeField] private float maxRadiusSpeed;

    [SerializeField] private int maxOrbitingObjects;
    [SerializeField] private float captureRange;
    [SerializeField] private LayerMask[] targetLayers;

    private float range;
    private int damage;

    public AnimationCurve BulletDrop { get => bulletDrop; set => bulletDrop = value; }
    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
    public int Damage { get => damage; set => damage = value; }
    public float CaptureRange { get => captureRange; set => captureRange = value; }
    public LayerMask[] TargetLayers { get => targetLayers; set => targetLayers = value; }
    public float MinOrbitSpeed { get => minOrbitSpeed; set => minOrbitSpeed = value; }
    public float MaxOrbitSpeed { get => maxOrbitSpeed; set => maxOrbitSpeed = value; }
    public float MinOrbitRadius { get => minOrbitRadius; set => minOrbitRadius = value; }
    public float MaxOrbitRadius { get => maxOrbitRadius; set => maxOrbitRadius = value; }
    public float MinRadiusSpeed { get => minRadiusSpeed; set => minRadiusSpeed = value; }
    public float MaxRadiusSpeed { get => maxRadiusSpeed; set => maxRadiusSpeed = value; }
    public int MaxOrbitingObjects { get => maxOrbitingObjects; set => maxOrbitingObjects = value; }
    public float Range { get => range; set => range = value; }
}
