public class FallingState : AirbornState
{
    private readonly GroundChecker _groundChecker;

    public FallingState(IStateSwitcherTSK3 stateSwitcher, StateMachineData data, CharacterTSK3 character) : base(stateSwitcher, data, character)
        => _groundChecker = character.GroundChecker;

    public override void Enter()
    {
        base.Enter();

        View.StartFalling();
    }

    public override void Exit()
    {
        base.Exit();

        View.StopFalling();
    }

    public override void Update()
    {
        base.Update();

        if (_groundChecker.IsTouches)
        {
            Data.YVelocity = 0;

            if (IsHorizontalInputZero())
                StateSwitcher.SwitchState<IdlingState>();
            else if (Data.IsSprintKeyPressed)
                StateSwitcher.SwitchState<SprintingState>();
            else if (Data.IsWalkKeyPressed)
                StateSwitcher.SwitchState<WalkingState>();
            else
                StateSwitcher.SwitchState<RunningState>();
        }
    }
}
