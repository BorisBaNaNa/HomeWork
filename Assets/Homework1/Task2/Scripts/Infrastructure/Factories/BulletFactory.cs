using UnityEngine;

public class BulletFactory : IService
{
    private Bullet _bulletPrefab;
    private Transform _bulletsParent;
    private StatsBulletConfig _bulletConfig;

    public BulletFactory(Bullet bulletPrefab, Transform bulletsParent, StatsBulletConfig bulletConfig)
    {
        _bulletPrefab = bulletPrefab;
        _bulletsParent = bulletsParent;
        _bulletConfig = bulletConfig;
    }

    public Bullet Build(Vector3 position, Vector3 targetPosition, float damage)
    {
        Vector3 shootDir = (targetPosition - position).normalized;
        Quaternion rotation = Quaternion.LookRotation(shootDir);

        Bullet bullet = Object.Instantiate(_bulletPrefab, position, rotation, _bulletsParent);
        bullet.Construct(_bulletConfig, damage);
        return bullet;
    }
}
