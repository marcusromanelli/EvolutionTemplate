using System;
using System.Collections.Generic;
using Zenject;

public class StatsScreen : DefaultScreen {

    StatsItem.Factory _statsFactory;
    IPlayerDataController _playerData;
    GameLibrary _gameLibrary;

    public ElementProperty ElementProperty { get; private set; }

    [Inject]
    void Constructor(IPlayerDataController playerData, StatsItem.Factory statsFactory, GameLibrary gameLibrary)
    {
        _statsFactory = statsFactory;
        _playerData = playerData;
        _gameLibrary = gameLibrary;

        Initialize();
    }

    public override void OpenWindow()
    {
        RenderElements();

        base.OpenWindow();
    }

    protected override void Initialize()
    {
    }

    private void RenderElements()
    {
        if(_contentsContainer.transform.childCount <= 0)
            CreateElements();

        RefreshElements();
    }

    private void CreateElements()
    {

        foreach(ElementType type in Enum.GetValues(typeof(ElementType)))
        {
            StatsItem item = _statsFactory.Create();
            item.transform.SetParent(_contentsContainer.transform);
        }
    }

    private void RefreshElements()
    {
        StatsItem[] statsItems = _contentsContainer.GetComponentsInChildren<StatsItem>();

        foreach(ElementType type in Enum.GetValues(typeof(ElementType)))
        {
            ElementProperty elementProperty = _gameLibrary.GetLevelSettings(type);

            IRecordsController records = _playerData.GetRecords();
            List<ElementStatistics> fullStatistics = records.GetStatistics();
            ElementStatistics statistics = null;

            if((int)type < fullStatistics.Count)
                statistics = fullStatistics[(int)type];

            statsItems[(int)type].Setup(elementProperty, statistics);
        }
    }
}