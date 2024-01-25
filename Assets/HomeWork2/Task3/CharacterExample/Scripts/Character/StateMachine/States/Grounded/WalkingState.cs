public class WalkingState : GroundedState
{
    private readonly RunningStateConfig _config;

    public WalkingState(IStateSwitcherTSK3 stateSwitcher, StateMachineData data, CharacterTSK3 character) : base(stateSwitcher, data, character)
        => _config = character.Config.RunningStateConfig;

    public override void Enter()
    {
        base.Enter();
        //View.StartWalking();

        Data.Speed = _config.WalkSpeed;
    }

    public override void Exit()
    {
        base.Exit();

        //View.StopWalking();
    }

    public override void Update()
    {
        base.Update();

        if (IsHorizontalInputZero())
            StateSwitcher.SwitchState<IdlingState>();
        else if (Data.IsSprintKeyPressed)
            StateSwitcher.SwitchState<SprintingState>();
        else if (!Data.IsWalkKeyPressed)
            StateSwitcher.SwitchState<RunningState>();
    }
}
