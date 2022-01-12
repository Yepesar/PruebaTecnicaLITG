using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponStats", menuName = "Weapon/NewWeaponStats")]
public class SOWeaponStats : ScriptableObject
{
    [SerializeField] private string profileName;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private GameObject weaponProjectilePrefab;
    [SerializeField] private WeaponStatsModel weaponStats;

    public string ProfileName { get => profileName; set => profileName = value; }
    public GameObject WeaponPrefab { get => weaponPrefab; set => weaponPrefab = value; }
    public GameObject WeaponProjectilePrefab { get => weaponProjectilePrefab; set => weaponProjectilePrefab = value; }
    public WeaponStatsModel WeaponStats { get => weaponStats; set => weaponStats = value; }
}

public enum WeaponType {Projectile, Atractor, Divider }
[System.Serializable]
public class WeaponStatsModel
{
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private int damage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int maxAmmo;
    [SerializeField] private float shootRate;
    [SerializeField] private float shootTime;

    public WeaponType WeaponType { get => weaponType; set => weaponType = value; }
    public int Damage { get => damage; set => damage = value; }
    public int MaxAmmo { get => maxAmmo; set => maxAmmo = value; }
    public float ShootRate { get => shootRate; set => shootRate = value; }
    public float ShootTime { get => shootTime; set => shootTime = value; }
    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }
}
