using UnityEngine;

public abstract class ContiniosState : BaseState<NPCStatesData>
{
    private float _actionDuration;
    private float _currectActionDuration;

    protected ContiniosState(IStateSwitcher stateSwitcher, NPCStatesData statesData, float actionDuration) : base(stateSwitcher, statesData)
    {
        _actionDuration = actionDuration;
    }

    public override void Enter()
    {
        _currectActionDuration = 0;
    }

    public override void StateAction()
    {
        _currectActionDuration += Time.deltaTime;
    }

    protected virtual bool CanTransitionToNext(NPCStatesData _)
    {
        return _currectActionDuration >= _actionDuration;
    }
}