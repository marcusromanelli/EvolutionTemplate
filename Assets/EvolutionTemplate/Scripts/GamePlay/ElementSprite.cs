using UnityEngine;

/// <summary>
/// This is a separate controller to handle 'evolution'. Every evolution comes in a separated Prefab with it's own elements.
/// It was made this way to allow for easy pivot customization (to match skins correctly independent of the sprite).
/// This controller also sets the Element's skin.
/// </summary>
public class ElementSprite : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer _skinRenderer;

    GameLibrary _gameLibrary;

    public void Setup(GameLibrary gameLibrary)
    {
        _gameLibrary = gameLibrary;
    }

    public void SetSkin(SkinType type)
    {
        if(_skinRenderer == null)
            return;

        SkinItem skin = _gameLibrary.GetSkinSettings(type);

        _skinRenderer.sprite = skin.Sprite;
    }
}
