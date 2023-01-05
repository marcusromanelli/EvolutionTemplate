using System;
using UnityEngine;
using Zenject;

/// <summary>
/// The EventHandler is a simple way to achieve communication between UI, Data Structures to the game itself without using fixed links between classes.
/// </summary>
public class EventHandler : IEventHandler
{
    public event OnEventTrigger _onLoggedOut;
    public event OnEventTrigger onLoggedOut
    {
        add
        {
            _onLoggedOut += value;
        }

        remove
        {
            _onLoggedOut -= value;
        }
    }

    public event OnEventTrigger _onLoggedIn;
    public event OnEventTrigger onLoggedIn
    {
        add
        {
            _onLoggedIn += value;
        }

        remove
        {
            _onLoggedIn -= value;
        }
    }

    public event OnEventTrigger<CurrencyType, int> _onSpendCurrency;
    public event OnEventTrigger<CurrencyType, int> onSpendCurrency
    {
        add
        {
            _onSpendCurrency += value;
        }

        remove
        {
            _onSpendCurrency -= value;
        }
    }

    public event OnEventTrigger<CurrencyType, int> _onEarnCurrency;
    public event OnEventTrigger<CurrencyType, int> onEarnCurrency
    {
        add
        {
            _onEarnCurrency += value;
        }

        remove
        {
            _onEarnCurrency -= value;
        }
    }

    public event OnEventTrigger<ElementType, CurrencyType, int> _onFarmCurrency;
    public event OnEventTrigger<ElementType, CurrencyType, int> onFarmCurrency
    {
        add
        {
            _onFarmCurrency += value;
        }

        remove
        {
            _onFarmCurrency -= value;
        }
    }

    public event OnEventTrigger _onSpawnCrate;
    public event OnEventTrigger onSpawnCrate
    {
        add
        {
            _onSpawnCrate += value;
        }

        remove
        {
            _onSpawnCrate -= value;
        }
    }

    public event OnEventTrigger<ElementType> _onSpawnElement;
    public event OnEventTrigger<ElementType> onSpawnElement
    {
        add
        {
            _onSpawnElement += value;
        }

        remove
        {
            _onSpawnElement -= value;
        }
    }

    public event OnEventTrigger<ElementType> _onNewElementLevelUnlocked;
    public event OnEventTrigger<ElementType> onNewElementLevelUnlocked
    {
        add
        {
            _onNewElementLevelUnlocked += value;
        }

        remove
        {
            _onNewElementLevelUnlocked -= value;
        }
    }

    public event OnEventTrigger<ElementType> _onUpgradeElementLevel;
    public event OnEventTrigger<ElementType> onUpgradeElementLevel
    {
        add
        {
            _onUpgradeElementLevel += value;
        }

        remove
        {
            _onUpgradeElementLevel -= value;
        }
    }

    public event OnEventTrigger<SkinType> _onChangeSkin;
    public event OnEventTrigger<SkinType> onChangeSkin
    {
        add
        {
            _onChangeSkin += value;
        }

        remove
        {
            _onChangeSkin -= value;
        }
    }

    public event OnEventTrigger<SkinType> _onPurchasedSkin;
    public event OnEventTrigger<SkinType> onPurchasedSkin
    {
        add
        {
            _onPurchasedSkin += value;
        }

        remove
        {
            _onPurchasedSkin -= value;
        }
    }

    public event OnEventTrigger _onAdsReady;
    public event OnEventTrigger onAdsReady
    {
        add
        {
            _onAdsReady += value;
        }

        remove
        {
            _onAdsReady -= value;
        }
    }

    public event OnEventTrigger _onRewardVideoPlayed;
    public event OnEventTrigger onRewardVideoPlayed
    {
        add
        {
            _onRewardVideoPlayed += value;
        }

        remove
        {
            _onRewardVideoPlayed -= value;
        }
    }

    public void Dispose()
    {
    }

    public void Initialize()
    {
    }

    public void Tick()
    {
    }


    //public void TriggerEvent(EventType type, params object[] param)
    //{
        //Debug.Log("Triggered " + type.ToString());
        //_onEventTrigger(type, param);
    //}

    public void SpendCurrency(CurrencyType currencyType, int amount)
    {
        _onSpendCurrency?.Invoke(currencyType, amount);
    }

    //private void OnEventTriggered(EventType eventType, params object[] param)
    //{
    //    try
    //    {
    //        ElementType elementType;
    //        CurrencyType currencyType;
    //        int amount;

    //        switch (eventType)
    //        {
    //            case EventType.SpendCurrency:
    //                CheckArgumentSize(2, GenerateExceptionMessage(EventType.SpendCurrency, new string[] { "CurrencyType", "int" }), param);

    //                currencyType = (CurrencyType)param[0];
    //                amount = (int)param[1];

    //                _wallet.Spend(currencyType, amount);
    //                break;
    //            case EventType.EarnCurrency:
    //                CheckArgumentSize(2, GenerateExceptionMessage(EventType.EarnCurrency, new string[] { "CurrencyType", "int" }), param);

    //                currencyType = (CurrencyType)param[0];
    //                amount = (int)param[1];

    //                _wallet.Earn(currencyType, amount);
    //                break;
    //            case EventType.FarmCurrency:
    //                CheckArgumentSize(3, GenerateExceptionMessage(EventType.FarmCurrency, new string[] { "ElementType", "CurrencyType", "int" }), param);

    //                elementType = (ElementType)param[0];
    //                currencyType = (CurrencyType)param[1];
    //                amount = (int)param[2];

    //                _wallet.Earn(currencyType, amount);
    //                _records.AddCoinFarmCount(elementType, amount);
    //                break;
    //            case EventType.SpawnCrate:
    //                CheckArgumentSize(1, GenerateExceptionMessage(EventType.SpawnCrate, new string[] { "ElementType" }), param);

    //                elementType = (ElementType)param[0];

    //                _records.AddSpawnCount(elementType);
    //                break;
    //            case EventType.SpawnElement:
    //                CheckArgumentSize(1, GenerateExceptionMessage(EventType.SpawnElement, new string[] { "ElementType" }), param);

    //                elementType = (ElementType)param[0];

    //                _records.AddSpawnCount(elementType);
    //                break;
    //            case EventType.NewLevelUnlocked:
    //                _records.AddNewType();
    //                break;
    //            case EventType.UpgradeLevel:
    //                CheckArgumentSize(1, GenerateExceptionMessage(EventType.UpgradeLevel, new string[] { "ElementType" }), param);

    //                elementType = (ElementType)param[0];

    //                HandleUpgradeLevel((ElementType)param[0]);
    //                break;
    //            case EventType.ChangeSkin:
    //                CheckArgumentSize(1, GenerateExceptionMessage(EventType.ChangeSkin, new string[] { "SkinType" }), param);

    //                SkinType skinType = (SkinType)param[0];

    //                _wardrobe.SetCurrentSkin(skinType);
    //                break;
    //        }
    //    }
    //    catch (Exception exception)
    //    {
    //        Debug.LogError("Could not handle event " + eventType.ToString() + ": " + exception.Message);
    //    }
    //}

    public void EarnCurrency(CurrencyType currencyType, int amount)
    {
        _onEarnCurrency?.Invoke(currencyType, amount);
    }

    public void FarmCurrency(ElementType elementType, CurrencyType currencyType, int amount)
    {
        _onFarmCurrency?.Invoke(elementType, currencyType, amount);
    }

    public void SpawnCrate()
    {
        _onSpawnCrate?.Invoke();
    }

    public void SpawnElement(ElementType elementType)
    {
        _onSpawnElement?.Invoke(elementType);
    }

    public void NewElementLevelUnlocked(ElementType elementType)
    {
        _onNewElementLevelUnlocked?.Invoke(elementType);
    }

    public void UpgradeElementLevel(ElementType elementType)
    {
        _onUpgradeElementLevel?.Invoke(elementType);
    }

    public void ChangeSkin(SkinType skin)
    {
        _onChangeSkin?.Invoke(skin);
    }

    public void AdsReady()
    {
        _onAdsReady?.Invoke();
    }

    public void RewardVideoPlayed()
    {
        _onRewardVideoPlayed?.Invoke();
    }

    public void PurchaseSkin(SkinType skin)
    {
        _onPurchasedSkin?.Invoke(skin);
    }

    public void LogIn()
    {
        _onLoggedIn?.Invoke();
    }

    public void LogOut()
    {
        _onLoggedOut?.Invoke();
    }
}
