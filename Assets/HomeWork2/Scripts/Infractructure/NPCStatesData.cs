using UnityEngine;

public class NPCStatesData
{
    public Transform CurrentMovementTarget;

    public Transform SleepPoint { get; private set; }
    public Transform WorkPoint { get; private set; }

    public NPCStatesData(Transform sleepPoint, Transform workPoint)
    {
        SleepPoint = sleepPoint;
        WorkPoint = workPoint;
    }
}
