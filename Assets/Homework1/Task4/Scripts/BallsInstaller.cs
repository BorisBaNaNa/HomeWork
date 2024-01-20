using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallsInstaller : MonoBehaviour
{
    [SerializeField] private BallDissolver _ballPrefab;
    [SerializeField] private Transform _ballsParent;
    [SerializeField] private Color[] _colors;
    [SerializeField] private float _spawnRadius = 7f;
    [SerializeField] private float _minBallsDistance = 1.1f;
    [SerializeField] private int _spawnCount = 10;

    private readonly List<BallDissolver> _spawnedBalls = new();

    public List<BallDissolver> SpawnBalls()
    {
        if (_spawnedBalls.Count > 0)
        {
            foreach (var spawnBall in _spawnedBalls)
                spawnBall.SetActive(false, () => Destroy(spawnBall.gameObject));
            _spawnedBalls.Clear();
        }

        for (int i = 0; i < _spawnCount; i++)
        {
            Vector3 randomPoint = GetUniqueRandomPos();
            BallDissolver newBall = Instantiate(_ballPrefab, randomPoint, Quaternion.identity, _ballsParent);
            newBall.SetColor(GetRandomColor());
            newBall.SetActive(true);
            _spawnedBalls.Add(newBall);
        }
        return _spawnedBalls;
    }

    private Vector3 GetUniqueRandomPos()
    {
        bool isNotUniquePos;
        Vector3 randomPoint;
        do
        {
            randomPoint = GetRandomPoint();
            isNotUniquePos = _spawnedBalls.Any(spawnObj => Vector3.Distance(randomPoint, spawnObj.transform.position) <= _minBallsDistance);
        } while (isNotUniquePos);

        return randomPoint;
    }

    private Vector3 GetRandomPoint()
    {
        Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        float randomDistance = Random.Range(0f, _spawnRadius);
        Vector3 randomPoint = randomDir * randomDistance;
        randomPoint.y = 0.5f;
        return randomPoint;
    }

    private Color GetRandomColor()
    {
        int randomId = Random.Range(0, _colors.Length);
        return _colors[randomId];
    }
}