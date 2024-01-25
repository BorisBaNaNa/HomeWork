using System;
using UnityEngine;

[Serializable]
public class RunningStateConfig
{
    [SerializeField, Range(0, 10)] private float _runSpeed = 5f;
    [SerializeField, Range(0, 10)] private float _walkSpeed = 2f;
    [SerializeField, Range(0, 10)] private float _sprintSpeed = 8f;

    public float RunSpeed => _runSpeed;
    public float WalkSpeed => _walkSpeed;
    public float SprintSpeed => _sprintSpeed;
}
