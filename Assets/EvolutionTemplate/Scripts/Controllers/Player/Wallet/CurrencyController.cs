using System;

public class CurrencyController : ICurrencyController{    

    public int Amount { get => _amount; }


    int _amount = 0;

    public void Earn(int value) => _amount += value;

    public void Spend(int value) => _amount -= value;

    public bool HasAmount(int value) => _amount >= value;
}
