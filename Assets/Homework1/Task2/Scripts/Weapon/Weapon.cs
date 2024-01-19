using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;

    private AmmoCounter _ammoCounter;
    private float _damage;
    private float _shootDelay;

    private IShootImplamentation[] _shootImplementations;
    private IShootImplamentation _curShootImplement;
    private float _lastShootTime;
    private int _curImplementId;

    public void Construct(IShootImplamentation[] shootImplementations, AmmoCounter ammoCounter, float damage, float shootDelay)
    {
        _shootImplementations = shootImplementations;
        _curImplementId = 0;
        _curShootImplement = _shootImplementations[_curImplementId];

        _ammoCounter = ammoCounter;
        _damage = damage;
        _shootDelay = shootDelay;
    }

    public void Shoot(Vector3 targetPosition)
    {
        if (Time.time - _lastShootTime <= _shootDelay || !_ammoCounter.IsReadyToShoot)
            return;

        _lastShootTime = Time.time;
        _curShootImplement.Shoot(_firePoint, targetPosition, _damage);
    }

    public void Reload()
    {
        _ammoCounter.TryReload();
    }

    public void SetNextWeaponMode()
    {
        _curImplementId = (_curImplementId + 1) % _shootImplementations.Length;
        _curShootImplement = _shootImplementations[_curImplementId];
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _ammoCounter.OutAmmoCount();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
