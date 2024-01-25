using UnityEngine;

public class WorkState : ContiniosState
{
    public WorkState(IStateSwitcher stateSwitcher, NPCStatesData statesData, float workDuration) : base(stateSwitcher, statesData, workDuration) 
    {
        _stateTransitions = new()
        {
            new(CanTransitionToNext, typeof(MoveToTargetState))
        };
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter to WorkState");
    }

    public override void Exit()
    {
        _statesData.CurrentMovementTarget = _statesData.SleepPoint;
        Debug.Log("Exit from WorkState");
    }
}
