using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// PlayerData manages all the player's personal actifacts: Wallet, Records and Skins.
/// </summary>
[Serializable]
public class PlayerDataController<RecordControllerClass, SkinControllerClass, CurrencyControllerClass, WalletControllerClass> : IPlayerDataController
    where RecordControllerClass : IRecordsController, new()
    where SkinControllerClass : ISkinController, new()
    where CurrencyControllerClass : ICurrencyController, new()
    where WalletControllerClass : IWalletController<CurrencyControllerClass>, new() {


    RecordControllerClass _records;
    SkinControllerClass _wardrobe;
    IEventHandler _eventHandler;
    WalletControllerClass _wallet;
    GameLibrary _gameLibrary;

    [Inject]
    public void Construct(IEventHandler eventHandler, GameLibrary gameLibrary)
    {
        _eventHandler = eventHandler;
        _gameLibrary = gameLibrary;

        _eventHandler.onSpendCurrency += HandleSpendCurency;
        _eventHandler.onEarnCurrency += HandleEarnCurency;
        _eventHandler.onFarmCurrency += HandleFarmCurency;
        _eventHandler.onSpawnElement += HandleSpawnElement;
        _eventHandler.onNewElementLevelUnlocked += HandleNewElementLevelUnlocked;
        _eventHandler.onUpgradeElementLevel += HandleElementUpgradeLevel;
        _eventHandler.onChangeSkin += HandleChangeSkin;

        _records = new RecordControllerClass();

        _wardrobe = new SkinControllerClass();

        _wallet = new WalletControllerClass();
    }

    public void Initialize()
    {
    }

    public bool HasSkin(SkinType skin)
    {
        return _wardrobe.HasSkin(skin);
    }

    public SkinType GetCurrentSkin()
    {
        return _wardrobe.CurrentSkin;
    }
    public bool CanPurchaseSkin(SkinType skin)
    {
        if(HasSkin(skin))
            return false;

        SkinItem skinData = _gameLibrary.GetSkinSettings(skin);

        return _wallet.CanSpend(skinData.CurrencyType, skinData.Price);
    }

    public void PurchaseSkins(SkinType skin)
    {
        if(!CanPurchaseSkin(skin))
            return;

        _wardrobe.PurchaseSkin(skin);

        SkinItem skinData = _gameLibrary.GetSkinSettings(skin);

        _eventHandler.SpendCurrency(skinData.CurrencyType, skinData.Price);

        _eventHandler.PurchaseSkin(skin);
    }

    public bool CanUnlockSkin(SkinType skin)
    {
        return _wardrobe.CanUnlockSkin(skin);
    }

    public bool HasLevel(ElementType elementType)
    {
        return _records.HasUnlockedElementType(elementType);
    }

    public IRecordsController GetRecords()
    {
        return _records;
    }

    public int GetCurrencyAmount(CurrencyType currency)
    {
        return _wallet.GetAmount(currency);
    }

    private void HandleSpendCurency(CurrencyType currencyType, int amount)
    {
        _wallet.Spend(currencyType, amount);
    }
    private void HandleEarnCurency(CurrencyType currencyType, int amount)
    {
        _wallet.Earn(currencyType, amount);
    }
    private void HandleFarmCurency(ElementType elementType, CurrencyType currencyType, int amount)
    {
        _wallet.Earn(currencyType, amount);
        _records.AddCoinFarmCount(elementType, amount);
    }
    private void HandleSpawnElement(ElementType elementType)
    {
        _records.AddSpawnCount(elementType);
    }
    private void HandleNewElementLevelUnlocked(ElementType elementType)
    {
        _records.AddNewType();
    }
    private void HandleElementUpgradeLevel(ElementType elementType)
    {
        if (!_records.HasUnlockedElementType(elementType))
            _eventHandler.NewElementLevelUnlocked(elementType);

        _records.AddSpawnCount(elementType);
    }
    private void HandleChangeSkin(SkinType skinType)
    {
        _wardrobe.SetCurrentSkin(skinType);
    }

}
