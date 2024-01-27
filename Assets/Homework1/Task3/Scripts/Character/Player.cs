using UnityEngine;

public class Player : MonoBehaviour, ISocialable
{
    [SerializeField] private float _startRespect;

    private float _curRespect;

    private void Awake()
    {
        _curRespect = _startRespect;
    }

    public float GetRespect() => _curRespect;

    public void UpRespect(float upVal)
    {
        _curRespect += upVal;
    }

    public void DownRespect(float upVal)
    {
        _curRespect -= upVal;
    }
}