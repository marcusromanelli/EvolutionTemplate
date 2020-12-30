using System;
using Zenject;


public interface IPlayerDataController : IInitializable {
    SkinType GetCurrentSkin();
    int GetCurrencyAmount(CurrencyType currency);

    bool HasSkin(SkinType skin);


    bool CanPurchaseSkin(SkinType skin);

    void PurchaseSkins(SkinType skin);

    bool CanUnlockSkin(SkinType skin);

    bool HasLevel(ElementType elementType);

    IRecordsController GetRecords();
}
