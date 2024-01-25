public class JumpingState : AirbornState
{
    private readonly JumpingStateConfig _config;

    public JumpingState(IStateSwitcherTSK3 stateSwitcher, StateMachineData data, CharacterTSK3 character) : base(stateSwitcher, data, character)
        => _config = character.Config.AirbornStateConfig.JumpingStateConfig;

    public override void Enter()
    {
        base.Enter();

        View.StartJumping();

        Data.YVelocity = _config.StartYVelocity;
    }

    public override void Exit()
    {
        base.Exit();

        View.StopJumping();
    }

    public override void Update()
    {
        base.Update();

        if (Data.YVelocity <= 0)
            StateSwitcher.SwitchState<FallingState>();
    }
}
