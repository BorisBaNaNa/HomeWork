using System;
using UnityEngine;
using UnityEngine.UI;

public class DefeatWndController : MonoBehaviour
{
    public Action OnResetBtnPress;

    [SerializeField] private Button _restartBtn;

    private void Awake()
    {
        _restartBtn.onClick.AddListener(OnResetBtnPressed);
    }

    public void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);

    private void OnResetBtnPressed()
    {
        OnResetBtnPress?.Invoke();
        Hide();
    }
}
