public class SprintingState : GroundedState
{
    private readonly RunningStateConfig _config;

    public SprintingState(IStateSwitcherTSK3 stateSwitcher, StateMachineData data, CharacterTSK3 character) : base(stateSwitcher, data, character)
        => _config = character.Config.RunningStateConfig;

    public override void Enter()
    {
        base.Enter();
        //View.StartSprint();

        Data.Speed = _config.SprintSpeed;
    }

    public override void Exit()
    {
        base.Exit();

        //View.StopSprint();
    }

    public override void Update()
    {
        base.Update();

        if (IsHorizontalInputZero())
            StateSwitcher.SwitchState<IdlingState>();
        else if (Data.IsWalkKeyPressed)
            StateSwitcher.SwitchState<WalkingState>();
        else if (!Data.IsSprintKeyPressed)
            StateSwitcher.SwitchState<RunningState>();
    }
}
