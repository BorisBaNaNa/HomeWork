using UnityEngine;

public class WeaponsController
{
    public Weapon CurWeapon => _curWeapon;

    private Weapon[] _weapons;
    private Weapon _curWeapon;
    private int _curWeaponId;

    public WeaponsController(Weapon[] weapons)
    {
        _weapons = weapons;
        _curWeaponId = 0;
    }

    public void Init(Transform weaponPoint)
    {
        foreach (var weapon in _weapons)
            InitWeapon(weapon, weaponPoint);
        _curWeapon = _weapons[_curWeaponId];
        _curWeapon.Show();
    }

    public void SetNextWeapon()
    {
        _curWeaponId = (_curWeaponId + 1) % _weapons.Length;
        SetNewCurWeapon();
    }

    public void SetPrevWeapon()
    {
        _curWeaponId = (_curWeaponId - 1 + _weapons.Length) % _weapons.Length;
        SetNewCurWeapon();
    }

    private void SetNewCurWeapon()
    {
        if (_curWeapon != null)
            _curWeapon.Hide();
        _curWeapon = _weapons[_curWeaponId];
        _curWeapon.Show();
    }

    private void InitWeapon(Weapon weapon, Transform weaponPoint)
    {
        weapon.transform.SetParent(weaponPoint);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        weapon.Hide();
    }
}