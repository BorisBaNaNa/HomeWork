using UnityEngine;

public class Player : MonoBehaviour, ISocialable
{
    [SerializeField] private float _startRespect;

    private UIOutputer _outputer;
    private float _curRespect;

    private void Awake()
    {
        _outputer = AllServices.GetService_<UIOutputer>();

        _curRespect = _startRespect;
        OutCurRespect();
    }

    public float GetRespect() => _curRespect;

    public void UpRespect(float upVal)
    {
        _curRespect += upVal;
        OutCurRespect();
    }

    public void DownRespect(float upVal)
    {
        _curRespect -= upVal;
        OutCurRespect();
    }

    private void OutCurRespect()
    {
        _outputer.OutRespectCount($"Current respect: {_curRespect}");
    }
}