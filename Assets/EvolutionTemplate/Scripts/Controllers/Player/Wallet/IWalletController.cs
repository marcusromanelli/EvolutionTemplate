using System;
using System.Collections.Generic;

public interface IWalletController<CurrencyController>
    where CurrencyController : ICurrencyController, new() {
    Dictionary<CurrencyType, CurrencyController> Wallet { get; }

    void Earn(CurrencyType currency, int amount);

    void Spend(CurrencyType currency, int amount);

    bool CanSpend(CurrencyType currency, int amount);

    int GetAmount(CurrencyType currency);
}
