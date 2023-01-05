using System.Collections.Generic;

public class SkinController : ISkinController {
    public SkinType CurrentSkin { get => _currentSkin; }

    private List<SkinType> _purchasedSkins = new List<SkinType>();

    SkinType _currentSkin = SkinType.None;
    GameLibrary _gameLibrary;

    public void Initialize(GameLibrary gameLibrary)
    {
        _gameLibrary = gameLibrary;
    }

    public bool CanUnlockSkin(SkinType skin)
    {
        if(HasSkin(skin) == true)
            return false;

        SkinItem item = _gameLibrary.GetSkinSettings(skin);

        return _purchasedSkins.FindAll(skinType => skinType == skin).Count > 0;
    }

    public bool HasSkin(SkinType skin)
    {
        return _purchasedSkins.FindAll(skinType => skinType == skin).Count > 0;
    }

    public void PurchaseSkin(SkinType skin)
    {
        _purchasedSkins.Add(skin);
    }

    public void SetCurrentSkin(SkinType skin)
    {
        _currentSkin = skin;
    }
}