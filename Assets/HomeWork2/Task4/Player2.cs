using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2 : MonoBehaviour, IRestartable
{
    public delegate void OnLvlUpEvent(float maxHp, float curHP, int curLvl);
    public delegate void OnHpEditedEvent(float maxHp, float curHP);

    public event OnLvlUpEvent OnLevelUp;
    public event OnHpEditedEvent OnHpEdited;
    public event Action OnDead;

    [SerializeField] private float _startMaxHP;
    [SerializeField] private float _onLvlupStatsMult = 1.1f;
    [SerializeField] private int _startLvl = 1;

    private float _maxHP;
    private float _curHP;
    private int _curLvl;
    private InputActions _inputActions;

    public void Construct(InputActions inputActions)
    {
        _inputActions = inputActions;
        InitializeInput_Kostil();
    }

    private void Awake()
    {
        Restart();
    }

    private void OnDestroy()
    {
        _inputActions.Player.EditHP.Disable();
        _inputActions.Player.EditLvl.Disable();

        _inputActions.Player.EditHP.performed -= EditHP_performed;
        _inputActions.Player.EditLvl.performed -= EditLvl_performed;
    }

    public void KillMe()
    {
        //animation, sounds and tralala...
        OnDead?.Invoke();
    }

    public void Restart()
    {
        //animation, sounds and tralala...
        _curHP = _maxHP = _startMaxHP;
        _curLvl = _startLvl;
        // Nu tipo escho kakoe-to sobitie vizivaem
        OnLevelUp?.Invoke(_maxHP, _curHP, _curLvl);
    }

    private void LvlUpAction()
    {
        ++_curLvl;
        _maxHP = Mathf.Round(_maxHP * _onLvlupStatsMult);
        _curHP = Mathf.Round(_curHP * _onLvlupStatsMult);
        OnLevelUp?.Invoke(_maxHP, _curHP, _curLvl);
    }

    private void EditHpEction(float editValue)
    {
        float newHp = _curHP + editValue;
        _curHP = Mathf.Clamp(newHp, 0, _maxHP);

        OnHpEdited?.Invoke(_maxHP, _curHP);
        if (_curHP <= 0)
            KillMe();
    }

    private void InitializeInput_Kostil()
    {
        _inputActions.Player.EditHP.Enable();
        _inputActions.Player.EditLvl.Enable();

        _inputActions.Player.EditHP.performed += EditHP_performed;
        _inputActions.Player.EditLvl.performed += EditLvl_performed;
    }

    private void EditLvl_performed(InputAction.CallbackContext _)
    {
        LvlUpAction();
    }

    private void EditHP_performed(InputAction.CallbackContext context)
    {
        float positivOrNegativeVal = context.ReadValue<float>();
        EditHpEction(10 * positivOrNegativeVal);
    }
}
