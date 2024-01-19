using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterShooting : MonoBehaviour
{
    [SerializeField] private Transform _weaponPoint;

    private InputActions _inputActions;
    private WeaponsController _weaponController;
    private Camera _cameraMain;

    public void Construct(InputActions inputActions, WeaponsController weaponController)
    {
        InitInput(inputActions);

        _weaponController = weaponController;
        _weaponController.Init(_weaponPoint);
        _cameraMain = Camera.main;
    }

    private void OnDestroy()
    {
        _inputActions.Player.Shoot.Disable();
        _inputActions.Player.Reload.Disable();
        _inputActions.Player.SwichWeaponMod.Disable();
        _inputActions.Player.SwapWeapon.Disable();

        _inputActions.Player.Shoot.performed -= Shoot;
        _inputActions.Player.Reload.performed -= Reload;
        _inputActions.Player.SwichWeaponMod.performed -= SwichWeaponMode;
        _inputActions.Player.SwapWeapon.performed -= SwapWeapon;
    }

    private void InitInput(InputActions inputActions)
    {
        _inputActions = inputActions;

        _inputActions.Player.Shoot.Enable();
        _inputActions.Player.Reload.Enable();
        _inputActions.Player.SwichWeaponMod.Enable();
        _inputActions.Player.SwapWeapon.Enable();

        _inputActions.Player.Shoot.performed += Shoot;
        _inputActions.Player.Reload.performed += Reload;
        _inputActions.Player.SwichWeaponMod.performed += SwichWeaponMode;
        _inputActions.Player.SwapWeapon.performed += SwapWeapon;
    }

    private void Shoot(InputAction.CallbackContext _)
    {
        Vector2 mousePos = _inputActions.Helpers.MousePos.ReadValue<Vector2>();
        Ray cameraRay = _cameraMain.ScreenPointToRay(mousePos);
        if (Physics.Raycast(cameraRay, out var hitInfo, float.PositiveInfinity))
        {
            _weaponController.CurWeapon.Shoot(hitInfo.point);
        }
    }

    private void Reload(InputAction.CallbackContext _) => _weaponController.CurWeapon.Reload();

    private void SwichWeaponMode(InputAction.CallbackContext _) => _weaponController.CurWeapon.SetNextWeaponMode();

    private void SwapWeapon(InputAction.CallbackContext context)
    {
        float scrollDir = context.ReadValue<float>();
        if (scrollDir >= 0)
            _weaponController.SetNextWeapon();
        else
            _weaponController.SetPrevWeapon();
    }
}
