using UnityEngine;

public static class ConfigReader
{
    public static StatsBulletConfig ReadBulletConfig(string _bulletConfigPath)
    {
        return Resources.Load<StatsBulletConfig>(_bulletConfigPath);
    }

    public static StatsWeaponConfig ReadWeaponConfig(string _weaponConfigPath)
    {
        return Resources.Load<StatsWeaponConfig>(_weaponConfigPath);
    }
}
