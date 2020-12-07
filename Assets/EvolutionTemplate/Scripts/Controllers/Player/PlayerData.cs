using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// PlayerData manages all the player's personal actifacts: Wallet, Records and Skins.
/// </summary>
[Serializable]
public class PlayerData : IInitializable {

    [SerializeField]
    private ElementsRecords _records;
    public ElementsRecords Records
    {
        get
        {
            return _records;
        }
    }

    [SerializeField]
    private List<SkinType> _purchasedSkins;
    public List<SkinType> PurchasedSkins
    {
        get
        {
            return _purchasedSkins;
        }
    }

    EventHandler _eventHandler;
    GameLibrary _gameLibrary;

    private int _coinAmount = 0;
    public int CoinAmount
    {
        get
        {
            return _coinAmount;
        }
    }

    //TO-DO: Now that I think of, I should rearrange those curency things into one separated struct :think:.
    private int _cashAmount = 0;
    public int CashAmount
    {
        get
        {
            return _cashAmount;
        }
    }

    private SkinType _currentActiveSKin = 0;
    public SkinType CurrentActiveSKin
    {
        get
        {
            return _currentActiveSKin;
        }
    }

    [Inject]
    public void Construct(EventHandler eventHandler, GameLibrary gameLibrary)
    {
        _eventHandler = eventHandler;
        _gameLibrary = gameLibrary;

        _eventHandler.onEventTrigger += OnEventTriggered;

        _records = new ElementsRecords(_eventHandler);
        _purchasedSkins = new List<SkinType>();
    }
    public void Initialize()
    {
    }

    public bool HasSkin(SkinType skin)
    {
        return _purchasedSkins.FindAll(skinType => skinType == skin).Count > 0;
    }

    public bool CanPurchaseSkin(SkinType skin)
    {
        if(HasSkin(skin))
            return false;

        SkinItem skinData = _gameLibrary.GetSkinSettings(skin);

        switch(skinData.CurrencyType)
        {
            case CurrencyType.Cash:
                return _cashAmount >= skinData.Price;
            case CurrencyType.Coin:
                return _coinAmount >= skinData.Price;
        }

        return false;
    }

    public void PurchaseSkins(SkinType skin)
    {
        if(!CanPurchaseSkin(skin))
            return;


        SkinItem skinData = _gameLibrary.GetSkinSettings(skin);

        switch(skinData.CurrencyType)
        {
            case CurrencyType.Cash:
                _eventHandler.TriggerEvent(EventType.SpendCash, skinData.Price);
                break;
            case CurrencyType.Coin:
                _eventHandler.TriggerEvent(EventType.SpendCoin, skinData.Price);
                break;
        }


        _eventHandler.TriggerEvent(EventType.PurchasedSkin, skin);

        _purchasedSkins.Add(skin);
    }

    public bool CanUnlockSkin(SkinType skin)
    {
        if(HasSkin(skin) == true)
            return false;

        SkinItem item = _gameLibrary.GetSkinSettings(skin);

        return _purchasedSkins.FindAll(skinType => skinType == skin).Count > 0;
    }

    public bool HasLevel(ElementType elementType)
    {
        return Records.HasUnlockedElementType(elementType);
    }

    private void OnEventTriggered(EventType eventType, params object[] param)
    {
        try
        {
            switch(eventType)
            {
                case EventType.SpendCash:
                    if(param.Length != 1)
                        throw new Exception("Spend Cash event type expects one arguments: int.");

                    HandleSpendCash((int)param[0]);
                    break;
                case EventType.SpendCoin:
                    if(param.Length != 1)
                        throw new Exception("Spend Cash event type expects one arguments: int.");

                    HandleSpendCoin((int)param[0]);
                    break;
                case EventType.FarmCoin:
                    if(param.Length != 2)
                        throw new Exception("Farm Coin event type expects two arguments: ElementType and int.");

                    HandleFarmCoin((ElementType)param[0], (int)param[1]);
                    break;
                case EventType.Spawn:
                    if(param.Length != 1)
                        throw new Exception("Spawn event type expects one arguments: ElementType.");

                    HandleSpawn((ElementType)param[0]);
                    break;
                case EventType.NewLevelUnlocked:
                    Records.AddNewType();
                    break;
                case EventType.UpgradeLevel:
                    if(param.Length != 1)
                        throw new Exception("UpgradeLevel event type expects one arguments: ElementType.");


                    HandleUpgradeLevel((ElementType)param[0]);
                    break;
                case EventType.ChangeSkin:
                    if(param.Length != 1)
                        throw new Exception("ChangeSkin event type expects one arguments: SkinType.");

                    HandleChangeSkin((SkinType)param[0]);
                    break;
                case EventType.WinCoin:
                    if(param.Length != 1)
                        throw new Exception("Win Coin event type expects one arguments: int.");

                    HandleWinCoin((int)param[0]);
                    break;
                case EventType.WinCash:
                    if(param.Length != 1)
                        throw new Exception("Win Cashevent type expects one arguments: int.");

                    HandleWinCash((int)param[0]);
                    break;
            }
        }
        catch(System.Exception exception)
        {
            Debug.LogError("Could not handle event " + eventType.ToString() + ": " + exception.Message);
        }
    }

    private void HandleSpendCash(int quantity)
    {
        _cashAmount -= quantity;

        if(_cashAmount < 0)
            _cashAmount = 0;
    }
    private void HandleSpendCoin(int quantity)
    {
        _coinAmount -= quantity;

        if(_coinAmount < 0)
            _coinAmount = 0;
    }
    private void HandleFarmCoin(ElementType type, int coinQuantity)
    {
        Records.AddCoinFarmCount(type, coinQuantity);

        _coinAmount += coinQuantity;
    }

    private void HandleSpawn(ElementType type)
    {
        Records.AddSpawnCount(type);
    }

    private void HandleUpgradeLevel(ElementType type)
    {
        if(!Records.HasUnlockedElementType(type))
            _eventHandler.TriggerEvent(EventType.NewLevelUnlocked, type);

        Records.AddSpawnCount(type);
    }

    private void HandleChangeSkin(SkinType skinType)
    {
        _currentActiveSKin = skinType;
    }
    private void HandleWinCoin(int coinQuantity)
    {
        _coinAmount += coinQuantity;
    }
    private void HandleWinCash(int coinQuantity)
    {
        _cashAmount += coinQuantity;
    }
}
