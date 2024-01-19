using UnityEngine;

public interface IShootImplamentation
{
    void Shoot(Transform _firePoint, Vector3 targetPosition, float damage);
}