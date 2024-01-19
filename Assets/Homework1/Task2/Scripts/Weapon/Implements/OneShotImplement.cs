using UnityEngine;
using UnityEditor;

public class OneShotImplement : BaseShotImplement
{
    public OneShotImplement(AmmoCounter ammoCounter, int shotBulletCount) : base(ammoCounter, shotBulletCount) { }

    public override void Shoot(Transform _firePoint, Vector3 targetPosition, float damage)
    {
        if (!_ammoCounter.TryDecrementAmmo(_shotBulletCount))
            return;

        _bulletFactory.Build(_firePoint.position, targetPosition, damage);
    }
}
