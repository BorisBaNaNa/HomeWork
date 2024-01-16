using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLooking : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Camera _cameraMain;
    [SerializeField] private Transform _head;

    [Header("Stats")]
    [SerializeField, Range(0.001f, 1f)] private float _mouseSensitivity = 0.5f;
    [SerializeField] private float _verticalAngleViewMax;

    private InputActions _inputActions;
    private float _curVerticalAngle;

    public void Construct(InputActions inputActions)
    {
        _inputActions = inputActions;
    }

    private void Awake()
    {
        InitCamera();

        _inputActions.Player.Look.Enable();
    }

    private void Update()
    {
        Vector2 mouseDelta = _inputActions.Player.Look.ReadValue<Vector2>();
        ApplyVerticalRotation(mouseDelta.y);
        ApplyHorizontalRotation(mouseDelta.x);
    }

    private void OnDestroy()
    {
        _inputActions.Player.Look.Disable();
    }

    private void OnValidate()
    {
        if (_cameraMain == null) _cameraMain = Camera.main;
    }

    private void InitCamera()
    {
        _cameraMain.transform.SetParent(_head);
        _cameraMain.transform.localPosition = Vector3.zero;
        _cameraMain.transform.rotation = Quaternion.identity;
    }

    private void ApplyHorizontalRotation(float mouseDeltaX)
    {
        if (Mathf.Approximately(mouseDeltaX, 0))
            return;

        Vector3 eulerRotation = new(0f, mouseDeltaX * _mouseSensitivity, 0f);
        transform.Rotate(eulerRotation);
    }

    private void ApplyVerticalRotation(float mouseDeltaY)
    {
        if (Mathf.Approximately(mouseDeltaY, 0f))
            return;

        _curVerticalAngle -= mouseDeltaY * _mouseSensitivity;
        _curVerticalAngle = Mathf.Clamp(_curVerticalAngle, -_verticalAngleViewMax, _verticalAngleViewMax);
        _head.transform.localRotation = Quaternion.Euler(_curVerticalAngle, 0f, 0f);
    }
}
