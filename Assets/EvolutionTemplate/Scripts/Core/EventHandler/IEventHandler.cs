using System;
using Zenject;

public delegate void OnEventTrigger<T0, T1, T2, T3>(T0 param0, T1 param1, T2 param2, T3 param3);
public delegate void OnEventTrigger<T0, T1, T2>(T0 param0, T1 param1, T2 param2);
public delegate void OnEventTrigger<T0, T1>(T0 param0, T1 param1);
public delegate void OnEventTrigger<T0>(T0 param0);
public delegate void OnEventTrigger();


public interface IEventHandler : IInitializable, ITickable, IDisposable
{
    public event OnEventTrigger<CurrencyType, int> onSpendCurrency;
    public event OnEventTrigger<CurrencyType, int> onEarnCurrency;
    public event OnEventTrigger<ElementType, CurrencyType, int> onFarmCurrency;
    public event OnEventTrigger onSpawnCrate;
    public event OnEventTrigger<ElementType> onSpawnElement;
    public event OnEventTrigger<ElementType> onNewElementLevelUnlocked;
    public event OnEventTrigger<ElementType> onUpgradeElementLevel;
    public event OnEventTrigger<SkinType> onChangeSkin;
    public event OnEventTrigger<SkinType> onPurchasedSkin;
    public event OnEventTrigger onAdsReady;
    public event OnEventTrigger onRewardVideoPlayed;
    public event OnEventTrigger onLoggedIn;
    public event OnEventTrigger onLoggedOut;

    public void SpendCurrency(CurrencyType currencyType, int amount);
    public void EarnCurrency(CurrencyType currencyType, int amount);
    public void FarmCurrency(ElementType elementType, CurrencyType currencyType, int amount);
    public void SpawnCrate();
    public void SpawnElement(ElementType elementType);
    public void NewElementLevelUnlocked(ElementType elementType);
    public void UpgradeElementLevel(ElementType elementType);
    public void ChangeSkin(SkinType skin);
    public void PurchaseSkin(SkinType skin);
    public void AdsReady();
    public void RewardVideoPlayed();
    public void LogIn();
    public void LogOut();
}
