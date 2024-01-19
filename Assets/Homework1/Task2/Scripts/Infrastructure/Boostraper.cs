using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Boostraper : MonoBehaviour, IService
{
    // Ну так-то можно спавнить конечно, однако и так слишком усложнил всё
    [Header("Character")]
    [SerializeField] private CharacterLooking _characterLooking;
    [SerializeField] private CharacterMooving _characterMooving;
    [SerializeField] private CharacterShooting _characterShooting;
    [SerializeField] private CharacterInteraction _characterInteraction;
    [SerializeField] private Player _player;

    [Header("Traider")]
    [SerializeField] private TraiderNPC _traderNPC;

    [Header("Enemy")]
    [SerializeField] private Enemy[] _enemies;

    [Header("Weapons")]
    [SerializeField] private WeaponPrefToConfPath[] _weaponsCreateInfo;

    [Header("Bullets")]
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _bulletsParent;
    [SerializeField] private string _bulletConfigPath;

    [Header("UI")]
    [SerializeField] private UIOutputer _outputer;

    private InputActions _inputActions;

    private void Awake()
    {
        InitServices();

        InitInput();
        InitPlayer();
        InitTraider();
        InitEnemies();
    }

    private void OnDestroy()
    {
        _inputActions.Helpers.Disable();
    }

    private void InitInput()
    {
        _inputActions = new();
        _inputActions.Helpers.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void InitPlayer()
    {
        _characterLooking.Construct(_inputActions);
        _characterMooving.Construct(_inputActions);
        _characterInteraction.Construct(_inputActions);

        WeaponsController weaponsController = new(InitWeapons());
        _characterShooting.Construct(_inputActions, weaponsController);
    }

    private Weapon[] InitWeapons()
    {
        Weapon[] weapons = new Weapon[_weaponsCreateInfo.Length];
        for (int i = 0; i < _weaponsCreateInfo.Length; i++)
            weapons[i] = CreateWeapon(_weaponsCreateInfo[i]);

        return weapons;
    }

    private Weapon CreateWeapon(WeaponPrefToConfPath weaponInfo)
    {
        StatsWeaponConfig statsWeapon = ConfigReader.ReadWeaponConfig(weaponInfo.WapoinConfigPath);
        AmmoCounter ammoCounter = new(statsWeapon);
        IShootImplamentation[] shootImplements = CreateShootImplementation(ammoCounter, statsWeapon);

        Weapon weapon = Instantiate(weaponInfo.WeaponPrefab);
        weapon.Construct(shootImplements, ammoCounter, statsWeapon.Damage, statsWeapon.ShootDelay);
        return weapon;
    }

    private IShootImplamentation[] CreateShootImplementation(AmmoCounter ammoCounter, StatsWeaponConfig statsWeapon) => new IShootImplamentation[]
    {
        new OneShotImplement(ammoCounter, statsWeapon.BaseShotBulletCount),
        new MultiShotImplement(ammoCounter, statsWeapon.MultiShotBulletCount),
    };

    private void InitTraider()
    {
        ITradeImplementation[] strategies = CreateTradeStategies();
        _traderNPC.Construct(strategies, _player);
    }

    private ITradeImplementation[] CreateTradeStategies() => new ITradeImplementation[]
    {
        new NotTradingStrat(),
        new FruitsTradingStrat(),
        new ArmorTradingStrat()
    };

    private void InitEnemies()
    {
        foreach (var enemy in _enemies)
        {
            enemy.Construct(_player);
        }
    }

    private void InitServices()
    {
        AllServices.RegisterService_(this);
        AllServices.RegisterService_(_outputer);
        InitBulletFactory();
    }

    private void InitBulletFactory()
    {
        StatsBulletConfig statsBullet = ConfigReader.ReadBulletConfig(_bulletConfigPath);
        BulletFactory bulletFactory = new(_bulletPrefab, _bulletsParent, statsBullet);
        AllServices.RegisterService_(bulletFactory);
    }
}
