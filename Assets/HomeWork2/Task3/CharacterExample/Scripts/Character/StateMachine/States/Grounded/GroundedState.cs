using UnityEngine;
using UnityEngine.InputSystem;

public abstract class GroundedState : MovementState
{
    private readonly GroundChecker _groundChecker;

    public GroundedState(IStateSwitcherTSK3 stateSwitcher, StateMachineData data, CharacterTSK3 character) : base(stateSwitcher, data, character)
        => _groundChecker = character.GroundChecker;

    public override void Enter()
    {
        base.Enter();

        View.StartGrounded();
    }

    public override void Exit()
    {
        base.Exit();

        View.StopGrounded();
    }

    public override void Update()
    {
        base.Update();

        if (_groundChecker.IsTouches == false)
            StateSwitcher.SwitchState<FallingState>();
    }

    protected override void AddInputActionCallbacks()
    {
        base.AddInputActionCallbacks();
        Input.Movement.Jump.started += OnJumpKeyPressed;
        // in airborn state not allowed
        Input.Movement.Walk.started += OnWalkKeyPressed;
        Input.Movement.Sprint.started += OnSprintKeyPressed;
        Input.Movement.Sprint.canceled += OnSprintKeyReleased;
    }

    protected override void RemoveInputActionCallbacks()
    {
        base.RemoveInputActionCallbacks();
        Input.Movement.Jump.started -= OnJumpKeyPressed;
        Input.Movement.Walk.started -= OnWalkKeyPressed;
        Input.Movement.Sprint.started -= OnSprintKeyPressed;
        Input.Movement.Sprint.canceled -= OnSprintKeyReleased;
    }

    private void OnJumpKeyPressed(InputAction.CallbackContext obj) => StateSwitcher.SwitchState<JumpingState>();
    private void OnSprintKeyPressed(InputAction.CallbackContext _) { Data.IsSprintKeyPressed = true; Data.IsWalkKeyPressed = false; }
    private void OnSprintKeyReleased(InputAction.CallbackContext _) { Data.IsSprintKeyPressed = false; }
    private void OnWalkKeyPressed(InputAction.CallbackContext _) { Data.IsSprintKeyPressed = false; Data.IsWalkKeyPressed = !Data.IsWalkKeyPressed; }
}
