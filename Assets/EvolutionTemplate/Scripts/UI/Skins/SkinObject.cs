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
    private EventHandler _eventHandler;
    private bool _isSkinEquipped;
    private bool _hasSkin;

    [Inject]
    void Constructor(EventHandler eventHandler)
    {
        _eventHandler = eventHandler;

        _eventHandler.onEventTrigger += HandleEvent;
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
    private void HandleEvent(EventType eventType, params object[] param)
    {
        SkinType type;

        switch(eventType)
        {
            default:
                break;
            case EventType.ChangeSkin:
                if(param.Length != 1)
                    throw new Exception("Change Skin event type expects one arguments: SkinType.");

                type = (SkinType)param[0];

                _isSkinEquipped = type == _skinItem.Type;

                RefreshLabel();
                    break;
            case EventType.PurchasedSkin:
                if(param.Length != 1)
                    throw new Exception("Purchased Skin event type expects one arguments: SkinType.");

                type = (SkinType)param[0];

                if(type == _skinItem.Type)
                    Setup(_skinScreen, _skinItem, true);
                break;
        }

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
