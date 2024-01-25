using System;
using System.Collections;
using System.Linq;

public abstract class BaseStateMachine : IStateSwitcher
{
    protected IState[] _states;
    protected IState _currentState;

    public virtual void SwichState(Type newStateType)
    {
        if (!typeof(IState).IsAssignableFrom(newStateType))
            throw new ArgumentException($"newStateType({newStateType.Name}) is not inherited from IState");

        _currentState?.Exit();
        _currentState = _states.FirstOrDefault(state => newStateType.IsEquivalentTo(state.GetType()));
        _currentState.Enter();
    }

    public void SetStartState<TState>() where TState : IState
    {
        _currentState = _states.FirstOrDefault(state => state is TState);
        _currentState.Enter();
    }

    public virtual void Update()
    {
        _currentState?.Update();
    }
}
