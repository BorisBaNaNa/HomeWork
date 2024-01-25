using System.Collections.Generic;
using System.Linq;

public class CharacterTSK3StateMachine: IStateSwitcherTSK3
{
    private List<IStateTSK3> _states;
    private IStateTSK3 _currentState;

    public CharacterTSK3StateMachine(CharacterTSK3 character)
    {
        StateMachineData data = new StateMachineData();

        _states = new List<IStateTSK3>()
        {
            new IdlingState(this, data, character),
            new RunningState(this, data, character),
            new SprintingState(this, data, character),
            new WalkingState(this, data, character),
            new JumpingState(this, data, character),
            new FallingState(this, data, character),
        };

        _currentState = _states[0];
        _currentState.Enter();
    }

    public void SwitchState<T>() where T : IStateTSK3
    {
        IStateTSK3 state = _states.FirstOrDefault(state => state is T);

        _currentState?.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public void HandleInput() => _currentState.HandleInput();

    public void Update() => _currentState.Update();
}
