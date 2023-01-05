using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SkinObject : MonoBehaviour {
    [SerializeField]
    public Image _itemIconSprite;

    [SerializeField]
    public Button _testButton;

    [SerializeField]
    public Text _testButtonLabel;

    [SerializeField]
    public Button _purchaseButton;


    private SkinItem _skinItem;
    private SkinScreen _skinScreen;
    private IEventHandler _eventHandler;
    private bool _isSkinEquipped;
    private bool _hasSkin;

    [Inject]
    void Constructor(IEventHandler eventHandler)
    {
        _eventHandler = eventHandler;

        _eventHandler.onChangeSkin += HandleChangeSkin;
        _eventHandler.onPurchasedSkin += HandlePurchaseSkin;
    }

    private void Awake()
    {
        _purchaseButton.onClick.AddListener(() =>
        {
            _skinScreen.PurchaseSkin(_skinItem.Type);
        });

        _testButton.onClick.AddListener(() =>
        {
            _skinScreen.InteractUseSkin(_skinItem.Type);
        });
    }

    public void Setup(SkinScreen screen, SkinItem item, bool hasSkin)
    {
        _skinItem = item;
        _skinScreen = screen;
        _hasSkin = hasSkin;

        InitializeSkin(hasSkin);
    }

    private void InitializeSkin(bool hasSkin)
    {
        _itemIconSprite.sprite = _skinItem.Sprite;

        _purchaseButton.gameObject.SetActive(!hasSkin);

        RefreshLabel();
    }
    private void HandlePurchaseSkin(SkinType skinType)
    {
        if(skinType == _skinItem.Type)
            Setup(_skinScreen, _skinItem, true);
    }
    private void HandleChangeSkin(SkinType skinType)
    {
        _isSkinEquipped = skinType == _skinItem.Type;

        RefreshLabel();
    }

    private void RefreshLabel()
    {
        string finalString = "";

        if(_isSkinEquipped)
            finalString = "Remove";
        else if(_hasSkin)
            finalString = "Use";
        else
            finalString = "Test";

        _testButtonLabel.text = finalString;
    }

    public class Factory : PlaceholderFactory<SkinObject> {
    }
}
