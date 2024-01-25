using UnityEngine;

public class SleepState : ContiniosState
{
    public SleepState(IStateSwitcher stateSwitcher, NPCStatesData statesData, float sleepDuration) : base(stateSwitcher, statesData, sleepDuration)
    {
        _stateTransitions = new()
        {
            new(CanTransitionToNext, typeof(MoveToTargetState))
        };
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter to SleepState");
    }

    public override void Exit()
    {
        _statesData.CurrentMovementTarget = _statesData.WorkPoint;
        Debug.Log("Exit from SleepState");
    }
}
