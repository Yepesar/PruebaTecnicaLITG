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
    [SerializeField] private float weaponRange = 0f;
    [SerializeField] private int damage;
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
    public float WeaponRange { get => weaponRange; set => weaponRange = value; }
}

[System.Serializable]
public class BulletStatsModel
{
    [SerializeField] private AnimationCurve bulletDrop;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletHeight;

    private float bulletRange = 1f;
    private int damage;

    public AnimationCurve BulletDrop { get => bulletDrop; set => bulletDrop = value; }
    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
    public float BulletHeight { get => bulletHeight; set => bulletHeight = value; }
    public float BulletRange { get => bulletRange; set => bulletRange = value; }
    public int Damage { get => damage; set => damage = value; }
}
