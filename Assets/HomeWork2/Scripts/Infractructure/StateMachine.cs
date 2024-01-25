using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseStateMachine : IStateSwitcher
{
    protected readonly IState[] _states;
    protected IState _currentState;

    public virtual void SwichState<TState>() where TState : IState
    {
        _currentState?.Exit();
        _currentState = _states.FirstOrDefault(state => state is TState);
        _currentState.Enter();
    }

    public void SwichState<TState>(TState newState) where TState : IState
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public virtual void Update()
    {
        _currentState.Update();
    }
}

public interface IStateSwitcher
{
    void SwichState<TState>() where TState : IState;

    void SwichState<TState>(TState newState) where TState : IState;
}

public interface IState
{
    void Enter();

    void Exit();

    void Update();
}

public abstract class BaseState<TData> : IState
{
    protected IStateSwitcher _stateSwitcher;
    protected TData _stateMachineData;
    protected List<StateTransition<TData>> _stateTransitions;

    protected BaseState(IStateSwitcher stateSwitcher, TData stateMachineData, List<StateTransition<TData>> stateTransitions = null)
    {
        _stateSwitcher = stateSwitcher;
        _stateMachineData = stateMachineData;
        _stateTransitions = stateTransitions ?? new();
    }

    public abstract void Enter();

    public abstract void Exit();

    public virtual void Update()
    {
        foreach (var transition in _stateTransitions)
        {
            if (transition.Predicate(_stateMachineData))
            {
                _stateSwitcher.SwichState(transition.State);
                return;
            }
        }
    }

    public void AddTransition(Predicate<TData> predicate, BaseState<TData> nextState)
    {
        StateTransition<TData> newTransition = new(predicate, nextState);
        _stateTransitions.Add(newTransition);
    }

    public void AddTransition(StateTransition<TData> newTransition) =>
        _stateTransitions.Add(newTransition);

    public void RemoveTransition(Predicate<TData> predicate) =>
        _stateTransitions.RemoveAll(transition => transition.Predicate == predicate);
}

public class StateTransition<TData>
{
    public StateTransition(Predicate<TData> predicate, BaseState<TData> state)
    {
        Predicate = predicate;
        State = state;
    }

    public Predicate<TData> Predicate { get; private set; }
    public BaseState<TData> State { get; private set; }
}