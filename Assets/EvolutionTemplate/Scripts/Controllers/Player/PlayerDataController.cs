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

        _eventHandler.onEventTrigger += OnEventTriggered;

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

        _eventHandler.TriggerEvent(EventType.SpendCurrency, skinData.CurrencyType, skinData.Price);

        _eventHandler.TriggerEvent(EventType.PurchasedSkin, skin);
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


    private void OnEventTriggered(EventType eventType, params object[] param)
    {
        try
        {
            ElementType elementType;
            CurrencyType currencyType;
            int amount;

            switch(eventType)
            {
                case EventType.SpendCurrency:
                    CheckArgumentSize(2, GenerateExceptionMessage(EventType.SpendCurrency, new string[] { "CurrencyType", "int" }), param);

                    currencyType = (CurrencyType)param[0];
                    amount = (int)param[1];

                    _wallet.Spend(currencyType, amount);
                    break;
                case EventType.EarnCurrency:
                    CheckArgumentSize(2, GenerateExceptionMessage(EventType.EarnCurrency, new string[] { "CurrencyType", "int" }), param);

                    currencyType = (CurrencyType)param[0];
                    amount = (int)param[1];

                    _wallet.Earn(currencyType, amount);
                    break;
                case EventType.FarmCurrency:
                    CheckArgumentSize(3, GenerateExceptionMessage(EventType.FarmCurrency, new string[] { "ElementType", "CurrencyType", "int" }), param);

                    elementType = (ElementType)param[0];
                    currencyType = (CurrencyType)param[1];
                    amount = (int)param[2];

                    _wallet.Earn(currencyType, amount);
                    _records.AddCoinFarmCount(elementType, amount);
                    break;
                case EventType.Spawn:
                    CheckArgumentSize(1, GenerateExceptionMessage(EventType.Spawn, new string[] { "ElementType" }), param);

                    elementType = (ElementType)param[0];

                    _records.AddSpawnCount(elementType);
                    break;
                case EventType.NewLevelUnlocked:
                    _records.AddNewType();
                    break;
                case EventType.UpgradeLevel:
                    CheckArgumentSize(1, GenerateExceptionMessage(EventType.UpgradeLevel, new string[] { "ElementType" }), param);

                    elementType = (ElementType)param[0];

                    HandleUpgradeLevel((ElementType)param[0]);
                    break;
                case EventType.ChangeSkin:
                    CheckArgumentSize(1, GenerateExceptionMessage(EventType.ChangeSkin, new string[] { "SkinType" }), param);

                    SkinType skinType = (SkinType)param[0];

                    _wardrobe.SetCurrentSkin(skinType);
                    break;
            }
        }
        catch(Exception exception)
        {
            Debug.LogError("Could not handle event " + eventType.ToString() + ": " + exception.Message);
        }
    }

    private string GenerateExceptionMessage(EventType eventName, params string[] argumentNames)
    {
        string argumentSize = argumentNames.Length.ToString();
        string concatArgumentNames = string.Join(", ", argumentNames);

        return string.Concat(eventName.ToString(), " event type expects ", argumentSize, " arguments: ", concatArgumentNames);
    }
    private void CheckArgumentSize(int desiredType, string errorMessage, params object[] param)
    {
        if(param.Length != desiredType)
            throw new Exception(errorMessage);
    }


    private void HandleUpgradeLevel(ElementType type)
    {
        if(!_records.HasUnlockedElementType(type))
            _eventHandler.TriggerEvent(EventType.NewLevelUnlocked, type);

        _records.AddSpawnCount(type);
    }
}
