using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public Action OnNeedRestart;

    [SerializeField] private TextMeshProUGUI _playerHpText;
    [SerializeField] private TextMeshProUGUI _playerLvlText;
    [SerializeField] private DefeatWndController _defeatWnd;

    private void Awake()
    {
        _defeatWnd.OnResetBtnPress = OnResetBtnPressed;
    }

    public void ShowPlayerHP(float maxHP, float curHP)
    {
        _playerHpText.text = $"Current HP: {curHP}|{maxHP}";
    }

    public void ShowPlayerLvl(int lvl)
    {
        _playerLvlText.text = $"Current Lvl: {lvl}";
    }

    public void ShowDefeatWnd() => _defeatWnd.Show();

    private void OnResetBtnPressed()
    {
        // animation, effects pfff booom vaaaau!!!
        OnNeedRestart?.Invoke();
    }
}
