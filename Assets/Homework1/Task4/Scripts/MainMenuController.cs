using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private BallsInstaller _ballsInstaller;

    [Header("UI")]
    [SerializeField] private GameObject _mainBackground;
    [SerializeField] private RectTransform _mainMenuWnd;
    [SerializeField] private CanvasGroup _menuControls;
    [SerializeField] private Button _startBtn;
    [SerializeField] private Button _openCloseBtn;
    [SerializeField] private Toggle _allClickModeTgl;
    [SerializeField] private Toggle _oneColorClickModeTgl;
    [SerializeField] private ToggleGroup _colorsGroup;
    [SerializeField] private Toggle _whiteTgl;
    [SerializeField] private Toggle _blueTgl;
    [SerializeField] private Toggle _redTgl;

    private GameManager _gameManager;
    private GameLogics _startLogic;
    private Color _startColor;

    private void Awake()
    {
        _gameManager = AllServices.GetService_<GameManager>();

        InitUiElems();
    }

    private void OnDestroy()
    {
        RemoveAllListeners();
    }

    public void SetAtiveMainMenu(bool active)
    {
        if (active)
            OpenMainMenu();
        else
            CloseMainMenu();
    }

    private void InitUiElems()
    {
        _startBtn.onClick.AddListener(StartGame);
        _openCloseBtn.onClick.AddListener(() => SetAtiveMainMenu(!_mainBackground.activeSelf));

        _allClickModeTgl.onValueChanged.AddListener(AllClickModeTglActive);
        _oneColorClickModeTgl.onValueChanged.AddListener(OneColorClickModeTglActive);

        _whiteTgl.onValueChanged.AddListener(active => ColorToggleActive(_whiteTgl, active));
        _blueTgl.onValueChanged.AddListener(active => ColorToggleActive(_blueTgl, active));
        _redTgl.onValueChanged.AddListener(active => ColorToggleActive(_redTgl, active));
    }

    private void RemoveAllListeners()
    {
        _startBtn.onClick.RemoveAllListeners();
        _openCloseBtn.onClick.RemoveAllListeners();

        _allClickModeTgl.onValueChanged.RemoveAllListeners();
        _oneColorClickModeTgl.onValueChanged.RemoveAllListeners();

        _whiteTgl.onValueChanged.RemoveAllListeners();
        _blueTgl.onValueChanged.RemoveAllListeners();
        _redTgl.onValueChanged.RemoveAllListeners();
    }

    private void StartGame()
    {
        _gameManager.SetGameLogic(_startLogic);
        _gameManager.SetColorForWin(_startColor);

        CloseMainMenu();
        List<BallDissolver> balls = _ballsInstaller.SpawnBalls();
        _gameManager.InitGame(balls);
    }

    private void AllClickModeTglActive(bool active)
    {
        if (!active)
            return;

        _startLogic = GameLogics.AllClickForWinLogic;
    }

    private void OneColorClickModeTglActive(bool active)
    {
        _colorsGroup.gameObject.SetActive(active);

        if (active)
        {
            _startLogic = GameLogics.OneColorClickForWinLogic;
            _startColor = _colorsGroup.GetFirstActiveToggle().transform.GetComponentInChildren<Image>().color;
        }
    }

    private void ColorToggleActive(Toggle tgl, bool active)
    {
        if (!active)
            return;

        _startColor = tgl.transform.GetComponentInChildren<Image>().color;
    }

    private void OpenMainMenu()
    {
        _mainBackground.SetActive(true);
        _mainMenuWnd.anchoredPosition = new(-_mainMenuWnd.sizeDelta.x, _mainMenuWnd.localPosition.y);

        _mainMenuWnd.DOAnchorPosX(0, 0.5f)
            .OnComplete(() =>
            {
                _menuControls.interactable = true;
            });
    }

    private void CloseMainMenu()
    {
        _menuControls.interactable = false;
        _mainBackground.SetActive(false);
        _mainMenuWnd.anchoredPosition = new(0, _mainMenuWnd.localPosition.y);

        _mainMenuWnd.DOAnchorPosX(-_mainMenuWnd.sizeDelta.x, 0.5f);
    }
}
