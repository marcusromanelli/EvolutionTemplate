using System;
using UnityEngine;
using Zenject;

/// <summary>
/// Farm Controller is responsible for spawning elements every a certain period time
/// </summary>
public class FarmController : IInitializable, ITickable, IDisposable {

    [Inject]
    FarmSettings _farmSettings;

    [Inject]
    EvolutionElement.Factory _elementFactory;

    [Inject]
    IEventHandler _eventHandler;

    private float _lastSpawnTime;
    private int _unopenedCrates;
    private bool isLoggedIn;

    public void Dispose()
    {
    }

    public void Initialize()
    {
        _lastSpawnTime = Time.time;

        _eventHandler.onLoggedIn += HandleLogin;
        _eventHandler.onLoggedIn += HandleLogout;

        _eventHandler.onSpawnElement += HandleSpawnElement;

        SpawnCrate();
    }

    public void Tick()
    {
        CheckSpawnTime();
    }

    private void CheckSpawnTime()
    {
        if(!CanSpawn())
            return;

        SpawnCrate();
    }

    private void SpawnCrate(ElementType type = ElementType.White)
    {
        EvolutionElement element = _elementFactory.Create();

        element.Setup(GetRandomPosition(), type);
        element.onFarmCoin += CollectFarming;
        element.onUpgraded += HandleUpgrade;

        _lastSpawnTime = Time.time;

        _unopenedCrates++;

        _eventHandler.SpawnCrate();
    }

    private bool CanSpawn()
    {
        var hasCooldown = Time.time - _lastSpawnTime >= _farmSettings.ElementSpawnTime;
        var hasAvailableCratesSlot = _unopenedCrates <= _farmSettings.MaxAmountOfAwaitingCrates;

        return isLoggedIn && hasCooldown && hasAvailableCratesSlot;
    }

    private Vector3 GetRandomPosition()
    {
        Rect centerPosition = _farmSettings.SpawnArea;

        float halfWidth = _farmSettings.SpawnArea.width / 2;

        float halfHeight = _farmSettings.SpawnArea.height / 2;

        float randomX = UnityEngine.Random.Range(-halfWidth, halfWidth);
        float randomY = UnityEngine.Random.Range(-halfHeight, halfHeight);

        Vector3 position = new Vector3(centerPosition.x, centerPosition.y, 0);
        position.x += randomX;
        position.y += randomY;

        return position;
    }

    private void CollectFarming(ElementType type, int coinQuantity)
    {
        _eventHandler.FarmCurrency(type, CurrencyType.Coin, coinQuantity);
    }

    private void HandleUpgrade(ElementType type)
    {
        _eventHandler.UpgradeElementLevel(type);
    }
    private void HandleSpawnElement(ElementType elementType)
    {
        _unopenedCrates--;

        _unopenedCrates = Mathf.Clamp(_unopenedCrates, 0, _unopenedCrates);
    }

    private void HandleLogin()
    {
        isLoggedIn = true;
    }

    private void HandleLogout()
    {
        isLoggedIn = false;
        _unopenedCrates = 0;
        _lastSpawnTime = 0;
    }
}
