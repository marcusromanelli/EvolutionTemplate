using System;

public interface ICurrencyController{   
    int Amount { get; }

    void Earn(int value);
    void Spend(int value);
    bool HasAmount(int value);
}
