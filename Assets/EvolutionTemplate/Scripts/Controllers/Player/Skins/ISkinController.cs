public interface ISkinController
{
    SkinType CurrentSkin { get; }

    bool HasSkin(SkinType skin);
    bool CanUnlockSkin(SkinType skin);
    void PurchaseSkin(SkinType skin);
    void SetCurrentSkin(SkinType skin);
}
