using UnityEngine;

public class RunningState : GroundedState
{
    private readonly RunningStateConfig _config;

    public RunningState(IStateSwitcherTSK3 stateSwitcher, StateMachineData data, CharacterTSK3 character) : base(stateSwitcher, data, character)
        => _config = character.Config.RunningStateConfig;

    public override void Enter()
    {
        base.Enter();

        View.StartRunning();

        Data.Speed = _config.RunSpeed;
    }

    public override void Exit()
    {
        base.Exit();

        View.StopRunning();
    }

    public override void Update()
    {
        base.Update();

        if (IsHorizontalInputZero())
            StateSwitcher.SwitchState<IdlingState>();
        else if (Data.IsSprintKeyPressed)
            StateSwitcher.SwitchState<SprintingState>();
        else if (Data.IsWalkKeyPressed)
            StateSwitcher.SwitchState<WalkingState>();
    }
}
