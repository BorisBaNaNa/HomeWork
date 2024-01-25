using System;
using System.Collections.Generic;

public abstract class BaseState<TData> : IState
{
    protected IStateSwitcher _stateSwitcher;
    protected TData _statesData;
    protected List<StateTransition<TData>> _stateTransitions;

    protected BaseState(IStateSwitcher stateSwitcher, TData statesData)
    {
        _stateSwitcher = stateSwitcher;
        _statesData = statesData;
    }

    public abstract void Enter();

    public abstract void Exit();

    public abstract void StateAction();

    public void Update()
    {
        StateAction();
        CheckTransitions();
    }

    public void AddTransition(Predicate<TData> predicate, Type stateType)
    {
        StateTransition<TData> newTransition = new(predicate, stateType);
        _stateTransitions.Add(newTransition);
    }

    public void AddTransition(StateTransition<TData> newTransition) =>
        _stateTransitions.Add(newTransition);

    public void RemoveTransition(Predicate<TData> predicate) =>
        _stateTransitions.RemoveAll(transition => transition.Predicate == predicate);

    private void CheckTransitions()
    {
        foreach (var transition in _stateTransitions)
        {
            if (transition.Predicate(_statesData))
            {
                _stateSwitcher.SwichState(transition.StateType);
                return;
            }
        }
    }
}
