using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class WardrobeElement : MonoBehaviour {

    [SerializeField]
    Transform _elementPivot;

    GameLibrary _gameLibrary;
    ElementSprite _elementSprite;

    [Inject]
    void Constructor(GameLibrary gameLibrary)
    {
        _gameLibrary = gameLibrary;

        Initialize();
    }

    public void Initialize()
    {
        ElementProperty baseElement = _gameLibrary.ElementSettings[0];

        InstantiateObject(baseElement.Prefab);
    }
    private void InstantiateObject(ElementSprite prefab)
    {
        ElementSprite newObject = Instantiate(prefab, _elementPivot);
        newObject.transform.localPosition = Vector3.zero;

        newObject.Setup(_gameLibrary);

        _elementSprite = newObject;
    }

    public void SetSkin(SkinType type)
    {
        _elementSprite.SetSkin(type);
    }
}
