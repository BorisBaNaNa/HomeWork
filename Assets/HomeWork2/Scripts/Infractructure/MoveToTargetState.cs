using UnityEngine;

public class MoveToTargetState : BaseState<NPCStatesData>
{
    private bool IsMovedToTarget => Vector3.Distance(_characterMovement.GetPositionOnGround(), _statesData.CurrentMovementTarget.position) <= 0.05f;

    private IMovement _characterMovement;
    private bool _isMovedToTarget;

    public MoveToTargetState(IStateSwitcher stateSwitcher, NPCStatesData statesData, IMovement characterMovement) : base(stateSwitcher, statesData)
    {
        _characterMovement = characterMovement;
        _stateTransitions = new()
        {
            new StateTransition<NPCStatesData>(CanTransitionToSleep, typeof(SleepState)),
            new StateTransition<NPCStatesData>(CanTransitionToWork, typeof(WorkState)),
        };
    }

    public override void Enter()
    {
        Debug.Log("Enter to MoveToTargetState");
    }

    public override void Exit()
    {
        Debug.Log("Exit from MoveToTargetState");
    }

    public override void StateAction()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        Vector3 targetPos = _statesData.CurrentMovementTarget.position;
        Vector3 moveDir = targetPos - _characterMovement.GetPositionOnGround();
        _characterMovement.Move(moveDir.normalized);
        _isMovedToTarget = IsMovedToTarget;
    }

    private bool CanTransitionToSleep(NPCStatesData data) =>
        _isMovedToTarget && data.CurrentMovementTarget == data.SleepPoint;

    private bool CanTransitionToWork(NPCStatesData data) =>
        _isMovedToTarget && data.CurrentMovementTarget == data.WorkPoint;
}
