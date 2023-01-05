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

    IEventHandler _eventHandler;
    GameLibrary _gameLibrary;

    [Inject]
    void Constructor(IEventHandler eventHandler, GameLibrary gameLibrary)
    {
        _eventHandler = eventHandler;
        _gameLibrary = gameLibrary;

        _eventHandler.onNewElementLevelUnlocked += HandleElementLevelUnlocked;

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

    private void HandleElementLevelUnlocked(ElementType elementType)
    {
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
