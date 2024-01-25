public class IdlingState : GroundedState
{
    public IdlingState(IStateSwitcherTSK3 stateSwitcher, StateMachineData data, CharacterTSK3 character) : base(stateSwitcher, data, character)
    {
    }

    public override void Enter()
    {
        base.Enter();

        View.StartIdling();

        Data.Speed = 0;
    }

    public override void Exit()
    {
        base.Exit();

        View.StopIdling();
    }

    public override void Update()
    {
        base.Update();

        if (IsHorizontalInputZero())
            return;

        if (Data.IsSprintKeyPressed)
            StateSwitcher.SwitchState<SprintingState>();
        else if (Data.IsWalkKeyPressed)
            StateSwitcher.SwitchState<WalkingState>();
        else
            StateSwitcher.SwitchState<RunningState>();
    }
}
