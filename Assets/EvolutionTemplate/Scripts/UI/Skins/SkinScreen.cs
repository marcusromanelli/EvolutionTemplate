using UnityEngine;
using Zenject;

public class SkinScreen : DefaultScreen {
    [SerializeField]
    private WardrobeElement _wardrobeElement;


    IPlayerDataController _playerData;
    IEventHandler _eventHandler;
    GameLibrary _gameLibrary;
    SkinObject.Factory _skinObjectFactory;
    SkinType _currentTryOnSkin;

    [Inject]
    void Constructor(GameLibrary gameLibrary, SkinObject.Factory storeItemFactory, IPlayerDataController playerData, IEventHandler eventHandler)
    {
        _gameLibrary = gameLibrary;
        _skinObjectFactory = storeItemFactory;
        _playerData = playerData;
        _eventHandler = eventHandler;
    }

    protected override void Initialize()
    {
        base.Initialize();

        foreach(SkinItem item in _gameLibrary.SkinData)
        {
            bool hasSkin = _playerData.HasSkin(item.Type);

            SkinObject obj = _skinObjectFactory.Create();
            obj.transform.SetParent(_contentsContainer.transform);

            obj.Setup(this, item, hasSkin);
        }
    }
    public override void OpenWindow()
    {
        SkinType currentSkin = _playerData.GetCurrentSkin();
        TrySkin(currentSkin);

        base.OpenWindow();
    }

    public void InteractUseSkin(SkinType type)
    {
        _currentTryOnSkin = type;
        SkinType currentSkin = _playerData.GetCurrentSkin();

        if(_playerData.HasSkin(type))
        {
            bool isUsingSkin = currentSkin == type;
            SkinType destinySkin;

            if(!isUsingSkin)
                destinySkin = type;
            else
                destinySkin = SkinType.None;


            _eventHandler.ChangeSkin(destinySkin);
            TrySkin(destinySkin);
        }
        else
            _wardrobeElement.SetSkin(type);
    }

    public void PurchaseSkin(SkinType type)
    {
        if(_playerData.HasSkin(type))
        {
            Debug.Log(" Already had " + type.ToString());
            return;
        }

        if(!_playerData.CanPurchaseSkin(type))
        {
            PopUp.Show("Not enough diamonds!", "Warning", "Ok");
            Debug.Log("Cannot Purchase Skin!");
            return;
        }

        _playerData.PurchaseSkins(type);
    }

    private void TrySkin(SkinType skin)
    {
        _wardrobeElement.SetSkin(skin);
    }
}
