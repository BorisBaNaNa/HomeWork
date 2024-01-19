using UnityEditor;
using UnityEngine;

public class MultiShotImplement : BaseShotImplement
{
    public MultiShotImplement(AmmoCounter ammoCounter, int shotBulletCount) : base(ammoCounter, shotBulletCount) { }

    public override void Shoot(Transform _firePoint, Vector3 targetPosition, float damage)
    {
        if (!_ammoCounter.TryDecrementAmmo(_shotBulletCount))
            return;

        SpawnBulletWithRotation(_firePoint, targetPosition, damage);
    }

    private void SpawnBulletWithRotation(Transform _firePoint, Vector3 targetPosition, float damage)
    {
        float offset = 0.03f;// По хорошему тоже бы в конфиг пихать но мне лень делать отдельный
        float rotateAngle = 360 / _shotBulletCount;
        Quaternion rotate = Quaternion.Euler(Vector3.forward * rotateAngle);
        Vector3 spawnLocPos = Vector3.left * offset
            ;
        for (int i = 0; i < _shotBulletCount; i++)
        {
            Vector3 spawnWorldPos = _firePoint.TransformPoint(spawnLocPos);
            _bulletFactory.Build(spawnWorldPos, targetPosition, damage);
            spawnLocPos = rotate * spawnLocPos;
        }
    }
}

