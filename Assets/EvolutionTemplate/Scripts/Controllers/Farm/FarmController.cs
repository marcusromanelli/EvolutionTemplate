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
    EventHandler _eventHandler;

    private float lastSpawnTime;

    public void Dispose()
    {
    }

    public void Initialize()
    {
        lastSpawnTime = Time.time;

        SpawnElement();
    }

    public void Tick()
    {
        CheckSpawnTime();
    }

    private void CheckSpawnTime()
    {
        if(!CanSpawn())
            return;

        SpawnElement();
    }

    private void SpawnElement(ElementType type = ElementType.White)
    {
        EvolutionElement element = _elementFactory.Create();

        element.Setup(GetRandomPosition(), type);
        element.onFarmCoin += CollectFarming;
        element.onUpgraded += HandleUpgrade;

        lastSpawnTime = Time.time;

        _eventHandler.TriggerEvent(EventType.Spawn, type);
    }

    private bool CanSpawn()
    {
        return Time.time - lastSpawnTime >= _farmSettings.ElementSpawnTime;
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
        _eventHandler.TriggerEvent(EventType.FarmCoin, type, coinQuantity);
    }
    private void HandleUpgrade(ElementType type)
    {
        _eventHandler.TriggerEvent(EventType.UpgradeLevel, type);
    }
}
