using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EncyclopediaScreen : DefaultScreen {

    [SerializeField]
    private Button _previousElement;

    [SerializeField]
    private Button _nextElement;

    [SerializeField]
    private Image _elementIcon;

    [SerializeField]
    private Text _elementName;

    [SerializeField]
    private Text _elementDescription;

    [SerializeField]
    private Text _elementCPS;

    [SerializeField]
    private Sprite _interrogationSprite;


    GameLibrary _gameLibrary;
    IPlayerDataController _playerData;
    ElementType currentElement = ElementType.White;

    private void Awake()
    {
        _nextElement.onClick.AddListener(() =>
        {
            NextElement();
        });

        _previousElement.onClick.AddListener(() =>
        {
            PreviousElement();
        });
    }
    [Inject]
    void Constructor(GameLibrary gameLibrary, IPlayerDataController playerData)
    {
        _gameLibrary = gameLibrary;
        _playerData = playerData;

        Initialize();
    }

    public override void OpenWindow()
    {
        currentElement = 0;

        RenderElement();
        base.OpenWindow();
    }

    protected override void Initialize()
    {
        RenderElement();
    }

    private void RenderElement()
    {
        if(_playerData.HasLevel(currentElement))
        {
            ElementProperty properties = _gameLibrary.GetLevelSettings(currentElement);

            _elementIcon.sprite = properties.Sprite;
            _elementName.text = properties.Name;
            _elementDescription.text = properties.Description;
            _elementCPS.text = string.Concat(properties.CPS, " Coins per Second");
        }
        else
        {
            _elementIcon.sprite = _interrogationSprite;
            _elementName.text = "???";
            _elementDescription.text = "???";
            _elementCPS.text = string.Concat("???", " Coins per Second");
        }

        RefreshArrows();
    }

    private void NextElement()
    {
        int nextElement = (int)currentElement;
        nextElement++;

        currentElement = (ElementType)nextElement;

        RenderElement();
    }

    private void PreviousElement()
    {
        if((int)currentElement > 0)
        {
            int nextElement = (int)currentElement;
            nextElement--;

            currentElement = (ElementType)nextElement;
        }

        RenderElement();
    }

    private void RefreshArrows()
    {
        int enumSize = Enum.GetValues(typeof(ElementType)).Length;

        if(currentElement == 0)
        {
            _previousElement.interactable = false;
            _nextElement.interactable = true;
        }
        else if((int)currentElement == enumSize - 1)
        {
            _previousElement.interactable = true;
            _nextElement.interactable = false;
        }
        else
        {
            _previousElement.interactable = true;
            _nextElement.interactable = true;
        }

    }

}
