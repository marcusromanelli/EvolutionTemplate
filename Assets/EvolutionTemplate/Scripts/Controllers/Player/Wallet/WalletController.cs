using System;
using System.Collections.Generic;

public class WalletController<CurrencyControllerClass> : IWalletController<CurrencyControllerClass>
    where CurrencyControllerClass : ICurrencyController, new(){
    public Dictionary<CurrencyType, CurrencyControllerClass> Wallet { get => _wallet; }

    Dictionary<CurrencyType, CurrencyControllerClass> _wallet = new Dictionary<CurrencyType, CurrencyControllerClass>();

    public bool CanSpend(CurrencyType currency, int amount)
    {
        CurrencyControllerClass currencyController = GetCurrencyController(currency);

        return currencyController.HasAmount(amount);
    }

    public void Earn(CurrencyType currency, int amount)
    {
        CurrencyControllerClass currencyController = GetCurrencyController(currency);

        currencyController.Earn(amount);
    }

    public int GetAmount(CurrencyType currency)
    {
        CurrencyControllerClass currencyController = GetCurrencyController(currency);

        return currencyController.Amount;
    }

    public void Spend(CurrencyType currency, int amount)
    {
        CurrencyControllerClass currencyController = GetCurrencyController(currency);

        if(currencyController.HasAmount(amount))
            currencyController.Spend(amount);
    }

    private CurrencyControllerClass GetCurrencyController(CurrencyType type)
    {
        if(_wallet.ContainsKey(type) == false)
            InitializeCurrencyController(type);

        return _wallet[type];
    }

    private void InitializeCurrencyController(CurrencyType type)
    {
        if(_wallet.ContainsKey(type) == true)
            return;

        _wallet.Add(type, new CurrencyControllerClass());
    }
}
