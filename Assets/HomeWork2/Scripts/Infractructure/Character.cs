using UnityEngine;

public class Character : MonoBehaviour, IMovement
{
    [Header("Components")]
    [SerializeField] private CharacterController _characterController;

    [Header("Stats")]
    [SerializeField] private Transform _workPoint;
    [SerializeField] private Transform _sleepPoint;
    [SerializeField] private float _speed;
    [SerializeField] private float _sleepDuration = 5f;
    [SerializeField] private float _workDuration = 8f;

    private NPCStateMachine _npcStateMachine;

    private void Awake()
    {
        _npcStateMachine = new(this, (_workPoint, _sleepPoint), (_sleepDuration, _workDuration));
        _npcStateMachine.SetStartState<SleepState>();
    }

    private void OnValidate()
    {
        if (_characterController == null)
            _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _npcStateMachine.Update();
    }

    public void Move(Vector3 dir)
    {
        _characterController.Move(_speed * Time.deltaTime * dir);
    }

    public Vector3 GetPositionOnGround() => new(transform.position.x, transform.position.y - _characterController.height * 0.5f - _characterController.skinWidth, transform.position.z);
}
