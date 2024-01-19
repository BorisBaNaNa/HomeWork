using System;
using System.Collections;
using UnityEngine;

public class AmmoCounter
{
    public bool IsReadyToShoot => _curMagazineAmmo > 0 && !_isReload || _isInfinityAmmo;

    private readonly Boostraper _coroutiner;
    private readonly UIOutputer _outputer;
    private float _reloadDuration;
    private int _maxMagazineAmmo;
    private int _maxReserveAmmo;
    private int _curMagazineAmmo;
    private int _curReserveAmmo;
    private bool _isReload;
    private bool _isInfinityAmmo;

    public AmmoCounter(StatsWeaponConfig statsWeapon)
    {
        _coroutiner = AllServices.GetService_<Boostraper>();
        _outputer = AllServices.GetService_<UIOutputer>();

        _reloadDuration = statsWeapon.ReloadDuration;
        _maxMagazineAmmo = statsWeapon.MaxMagazineAmmo;
        _maxReserveAmmo = statsWeapon.MaxReserveAmmo;
        _isInfinityAmmo = statsWeapon.IsInfinityAmmo;

        _curMagazineAmmo = _maxMagazineAmmo;
        _curReserveAmmo = _maxReserveAmmo;
    }

    public bool TryDecrementAmmo(int count)
    {
        if (_isInfinityAmmo)
            return true;

        if (count > _curMagazineAmmo)
            return false;

        _curMagazineAmmo -= count;
        OutAmmoCount();
        return true;
    }

    public bool TryReload()
    {
        if (_curReserveAmmo == 0)
        {
            Debug.Log("No reserve ammo!");
            return false;
        }
        if (_isReload)
        {
            Debug.Log("It's already recharging!");
            return false;
        }
        if (_curMagazineAmmo == _maxMagazineAmmo)
        {
            Debug.Log("Magazine is full");
            return false;
        }

        _coroutiner.StartCoroutine(ReloadRoutine());
        return true;
    }

    public void OutAmmoCount()
    {
        _outputer.OutAmmoCount(GetAmmoInfo());
    }

    private IEnumerator ReloadRoutine()
    {
        Debug.Log("Start Reloading...");
        _isReload = true;
        yield return new WaitForSeconds(_reloadDuration);

        int takeAmmoCount = Math.Min(_maxMagazineAmmo - _curMagazineAmmo, _curReserveAmmo);
        _curReserveAmmo -= takeAmmoCount;
        _curMagazineAmmo += takeAmmoCount;
        _isReload = false;
        OutAmmoCount();
        Debug.Log("Reload completed");
    }

    private string GetAmmoInfo()
    {
        if (_isInfinityAmmo)
            return "∞/∞";
        return string.Format("{0,2:D2}/{1,2:D2}", _curMagazineAmmo, _curReserveAmmo);
    }
}
