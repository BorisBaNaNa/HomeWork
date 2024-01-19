using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMooving : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CharacterController _characterController;

    [Header("Stats")]
    [SerializeField] private float _speed;

    private InputActions _inputActions;

    public void Construct(InputActions inputActions)
    {
        _inputActions = inputActions;
        _inputActions.Player.Move.Enable();
    }

    private void OnValidate()
    {
        if (_characterController == null) _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector2 input = _inputActions.Player.Move.ReadValue<Vector2>();
        _characterController.Move(GetMotion(input));
    }

    private Vector3 GetMotion(Vector2 input)
    {
        Vector2 translate = _speed * Time.deltaTime * input;
        Vector3 motion = transform.TransformDirection(new(translate.x, 0f, translate.y));
        return motion;
    }

    private void OnDestroy()
    {
        _inputActions.Player.Move.Disable();
    }
}
