using System;

public class StateTransition<TData>
{
    public StateTransition(Predicate<TData> predicate, Type stateType)
    {
        Predicate = predicate;
        StateType = stateType;
    }

    public Predicate<TData> Predicate { get; private set; }
    public Type StateType { get; private set; }
}