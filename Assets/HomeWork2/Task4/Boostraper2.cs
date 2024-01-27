using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Boostraper2 : MonoBehaviour, IService
{
    [SerializeField] private UIController _uIController;

    [Header("Character")]
    [SerializeField] private CharacterLooking _characterLooking;
    [SerializeField] private CharacterMooving _characterMooving;
    [SerializeField] private CharacterInteraction _characterInteraction;
    [SerializeField] private Player2 _player;

    private InputActions _inputActions;
    private LevelManagerUIMediator _levelManagerUIMediator;
    private PlayerUIMediator _playerUIMediator;
    private LevelManager _levelManager;

    private void Awake()
    {
        InitInput();
        InitPlayer();
        InitLevelManager();
        InitMediators();
    }

    private void InitMediators()
    {
        _levelManagerUIMediator = new(_uIController, _levelManager);
        _playerUIMediator = new(_player, _uIController);
    }

    private void InitLevelManager()
    {
        IRestartable[] restartables = GetRestartables();
        _levelManager = new(restartables);
    }

    private IRestartable[] GetRestartables() => new IRestartable[]
    {
        _player,
    };

    private void OnDestroy()
    {
        _inputActions.Helpers.Disable();
    }

    private void InitInput()
    {
        _inputActions = new();
        _inputActions.Helpers.Enable();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void InitPlayer()
    {
        _characterLooking.Construct(_inputActions);
        _characterMooving.Construct(_inputActions);
        _characterInteraction.Construct(_inputActions);
        _player.Construct(_inputActions);
    }
}
