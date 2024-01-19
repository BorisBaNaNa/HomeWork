
using System;
using UnityEngine;

[Serializable]
public class WeaponPrefToConfPath
{
    [field: SerializeField] public Weapon WeaponPrefab { get; private set; }
    [field: SerializeField] public string WapoinConfigPath { get; private set; }
}