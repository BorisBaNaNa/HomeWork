using UnityEngine;

[CreateAssetMenu(fileName = "NewStatsWeaponConfig", menuName = "Weapons/StatsConfig", order = 0)]
public class StatsWeaponConfig : ScriptableObject
{
    public float Damage = 10f;
    public float ShootDelay = 0.1f;
    public float ReloadDuration = 2f;
    public int MaxMagazineAmmo = 9;
    public int MaxReserveAmmo = 50;
    public int BaseShotBulletCount = 1;
    public int MultiShotBulletCount = 3;
    public bool IsInfinityAmmo = false;
}
