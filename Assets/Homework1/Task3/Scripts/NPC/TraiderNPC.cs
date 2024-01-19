using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraiderNPC : MonoBehaviour, IInteractable, IDamagable
{
    [SerializeField] private float[] _activateStratsTriggers;

    private ISocialable _interactinCharacter;
    private ITradeImplementation[] _tradeStrats;
    private ITradeImplementation _curTradeStrat;

    //  остыль дл€ демонстрации, надоело паритьс€
    private Player _player;

    public void Construct(ITradeImplementation[] tradeStrats, Player player)
    {
        _tradeStrats = tradeStrats;
        _player = player;
    }

    public void Interact(GameObject interactingObj)
    {
        if (!interactingObj.TryGetComponent(out _interactinCharacter))
        {
            Debug.LogError($"Object {interactingObj.name} has not ISocialable methods");
            return;
        }
        if (_activateStratsTriggers.Length < _tradeStrats.Length)
        {
            Debug.LogError($"_activateStratsTriggers less than _tradeStrats");
            return;

        }

        SelectTradeStrat();
        StartTrade();
    }

    private void StartTrade()
    {
        var itemList = _curTradeStrat.GetTradeItemsList();
        if (itemList == null)
        {
            Debug.Log("The merchant does not want to trade with you, raise your respect");
            return;
        }

        string tradeItems = "";
        foreach (var item in itemList)
        {
            tradeItems += $"{item}, ";
        }
        Debug.Log($"The merchant is ready to sell to you: {tradeItems}");
    }

    private void SelectTradeStrat()
    {
        float characterRespect = _interactinCharacter.GetRespect();
        for (int i = _tradeStrats.Length - 1; i >= 0; i--)
        {
            if (characterRespect >= _activateStratsTriggers[i])
            {
                _curTradeStrat = _tradeStrats[i];
                return;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        _player.DownRespect(5f);
    }
}
