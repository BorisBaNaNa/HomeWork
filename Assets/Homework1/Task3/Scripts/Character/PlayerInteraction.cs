using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInteraction : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform _head;

    [Header("Stats")]
    [SerializeField] private LayerMask _interactMask;
    [SerializeField] private float _interactDistance = 1f;

    private bool IsInteracting
    {
        get
        {
            return _isInteracting;
        }

        set
        {
            if (_isInteracting == value)
                return;
            _outputer.SetActiveInteractionText(value);
            _isInteracting = value;
        }
    }

    private InputActions _inputActions;
    private UIOutputer _outputer;
    private RaycastHit _interactTarget;
    private bool _isInteracting;

    public void Construct(InputActions inputActions)
    {
        _outputer = AllServices.GetService_<UIOutputer>();
        _inputActions = inputActions;
        _inputActions.Player.Interact.Enable();
        _inputActions.Player.Interact.performed += InteractWithObj;
    }

    private void FixedUpdate()
    {
        IsInteracting = Physics.Raycast(_head.position, _head.forward, out _interactTarget, _interactDistance, _interactMask.value);
    }

    private void OnDestroy()
    {
        _inputActions.Player.Interact.Disable();
        _inputActions.Player.Interact.performed -= InteractWithObj;
    }

    private void InteractWithObj(InputAction.CallbackContext _)
    {
        if (!IsInteracting)
            return;
        if (!_interactTarget.collider.TryGetComponent<IInteractable>(out var interactable))
        {
            Debug.LogError($"Object {_interactTarget.collider.name} has not IInteractable methods");
            return;
        }

        interactable.Interact(gameObject);
    }
}
