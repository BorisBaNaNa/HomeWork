using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BallDissolver : MonoBehaviour
{
    public Color BaseColor => _mainMat.GetColor(_colorPropName);

    [SerializeField] private Collider _collider;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private string _colorPropName;
    [SerializeField] private string _trashholdPropName;
    [SerializeField] private float _trashholdMin = 0f;
    [SerializeField] private float _trashholdMax = 2f;
    [SerializeField] private float _dissolveDur = 1f;

    private GameManager _gameManager;
    private Material _mainMat;
    private Tween _activateAnim;
    private int _colorPropId;
    private int _trashholdPropId;

    private void Awake()
    {
        InitMaterial();
        _gameManager = AllServices.GetService_<GameManager>();
    }

    private void OnValidate()
    {
        if (_collider == null)
            _collider = GetComponent<Collider>();
        if (_mesh == null)
            _mesh = GetComponent<MeshRenderer>();
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        _gameManager.BallWasActivated(BaseColor);
        SetActive(false);
    }

    private void OnDestroy()
    {
        _activateAnim?.Kill();
    }

    public void SetActive(bool active, Action OnAnimComlete = null)
    {
        _activateAnim?.Kill();

        if (active)
        {
            gameObject.SetActive(true);
            _mainMat.SetFloat(_trashholdPropId, _trashholdMax);
            _activateAnim = DoFade(_trashholdMin, () =>
            {
                _collider.enabled = true;
                OnAnimComlete?.Invoke();
            });
        }
        else
        {
            _collider.enabled = false;
            _mainMat.SetFloat(_trashholdPropId, _trashholdMin);
            _activateAnim = DoFade(_trashholdMax, () =>
            {
                gameObject.SetActive(false);
                OnAnimComlete?.Invoke();
            });
        }
    }

    public void SetColor(Color newColor)
    {
        _mainMat.SetColor(_colorPropId, newColor);
    }

    private void InitMaterial()
    {
        _mainMat = _mesh.material;

        _colorPropId = Shader.PropertyToID(_colorPropName);
        _trashholdPropId = Shader.PropertyToID(_trashholdPropName);
    }

    private Tween DoFade(float endVal, Action onCompleteAction)
    {
        return _mainMat.DOFloat(endVal, _trashholdPropId, _dissolveDur)
            .OnComplete(() => onCompleteAction?.Invoke());
    }
}
