using Zenject;

public class StoreScreen : DefaultScreen {
    GameLibrary _gameLibrary;
    StoreItemObject.Factory _storeItemFactory;

    [Inject]
    void Constructor(GameLibrary gameLibrary, StoreItemObject.Factory storeItemFactory)
    {
        _gameLibrary = gameLibrary;
        _storeItemFactory = storeItemFactory;
    }

    protected override void Initialize()
    {
        base.Initialize();

        foreach(StoreItem item in _gameLibrary.StoreData)
        {
            StoreItemObject obj = _storeItemFactory.Create();
            obj.transform.SetParent(_contentsContainer.transform);
            obj.Setup(item);
        }
    }
}
