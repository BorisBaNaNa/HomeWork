
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boostraper : MonoBehaviour
{
    [SerializeField] private CharacterLooking _characterLooking;
    [SerializeField] private CharacterMooving _characterMooving;

    private InputActions _inputActions;

    private void Awake()
    {
        InitInput();

        InitPlayer();
    }

    private void InitInput() => _inputActions = new();

    private void InitPlayer()
    {
        _characterLooking.Construct(_inputActions);
        _characterMooving.Construct(_inputActions);
    }
}
