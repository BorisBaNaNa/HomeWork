using UnityEngine;

public abstract class BaseShotImplement : IShootImplamentation
{
    protected readonly BulletFactory _bulletFactory;
    protected readonly AmmoCounter _ammoCounter;
    protected readonly int _shotBulletCount;

    public BaseShotImplement(AmmoCounter ammoCounter, int shotBulletCount)
    {
        _ammoCounter = ammoCounter;
        _shotBulletCount = shotBulletCount;

        _bulletFactory = AllServices.GetService_<BulletFactory>();
    }

    public abstract void Shoot(Transform _firePoint, Vector3 targetPosition, float damage);
}
