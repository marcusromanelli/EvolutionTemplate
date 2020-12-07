using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class NewLevelUnlockedScreen : DefaultScreen {
    [SerializeField]
    private Button _shareButton;
    [SerializeField]
    private Button _okButton;
    [SerializeField]
    private Image _elementImage;
    [SerializeField]
    private Text _nameLabel;

    EventHandler _eventHandler;
    GameLibrary _gameLibrary;

    [Inject]
    void Constructor(EventHandler eventHandler, GameLibrary gameLibrary)
    {
        _eventHandler = eventHandler;
        _gameLibrary = gameLibrary;

        _eventHandler.onEventTrigger += HandleEvent;

        Initialize();
    }

    protected override void Initialize()
    {
        base.Initialize();

        _shareButton.onClick.AddListener(ShareScreen);
    }

    private void ShareScreen()
    {
        SocialController.ShareScreen();
    }

    private void HandleEvent(EventType type, params object[] param)
    {
        if(type != EventType.NewLevelUnlocked)
            return;

        if(param.Length != 1)
            throw new Exception("New Level Unlocked event type expects one arguments: ElementType.");

        ElementType elementType = (ElementType)param[0];

        ShowWindow(elementType);
    }

    private void ShowWindow(ElementType type)
    {
        OpenWindow();

        ElementProperty element = _gameLibrary.GetLevelSettings(type);
        _nameLabel.text = element.Name;
        _elementImage.sprite = element.Sprite;
    }
}
