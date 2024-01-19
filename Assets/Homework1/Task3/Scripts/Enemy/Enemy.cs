using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private float _maxHp;

    //  остыль дл€ демонстрации, надоело паритьс€
    private Player _player;
    private float _curHP;

    public void Construct(Player player)
    {
        _player = player;
    }

    private void Awake()
    {
        _curHP = _maxHp;
    }

    public void TakeDamage(float damage)
    {
        _curHP -= damage;
        if (_curHP <= 0)
        {
            Destroy(gameObject);
            _player.UpRespect(10);
        }
    }
}
