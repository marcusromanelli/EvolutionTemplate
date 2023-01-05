using System;
using UnityEngine;
using Zenject;

/// <summary>
/// 'EvolutionElement' is the whole foundation of the game. It represents a possible digivolvable element. (A cow, rabbid, human, etc)
/// It was made to be a generic behavioural controller. It controls the coin/poop generation, the Movement component and the ElementSprite component.
/// </summary>
public class EvolutionElement : MonoBehaviour {

    public delegate void OnFarmCoin(ElementType type, int quantity);
    public event OnFarmCoin onFarmCoin;

    public delegate void OnUpgraded(ElementType newType);
    public event OnUpgraded onUpgraded;

    //Exposed
    [SerializeField]
    private ElementProperty _properties;

    public ElementProperty Properties
    {
        get
        {
            return _properties;
        }
    }


    [SerializeField]
    private ElementComponents _components;

    public ElementComponents Components
    {
        get
        {
            return _components;
        }
    }


    //Injections
    IInputController _inputController;
    GameLibrary _gameLibrary;
    IEventHandler _eventHandler;
    ElementPoop.Factory _poopFactory;
    IPlayerDataController _playerData;

    //Internal
    private ElementStatus _status;
    private EvolutionElement _CollidingWithEqual;
    private ElementMovement _movement;
    private float _lastFarmedCoinTime;
    private ElementSprite _elementSprite;

    private void Awake()
    {
        _movement = GetComponent<ElementMovement>();
    }

    [Inject]
    public void Construct(IInputController inputController, GameLibrary gameLibrary, ElementPoop.Factory poopFactory, IEventHandler eventHandler, IPlayerDataController playerData)
    {
        _inputController = inputController;
        _gameLibrary = gameLibrary;
        _poopFactory = poopFactory;
        _eventHandler = eventHandler;
        _playerData = playerData;


        _eventHandler.onChangeSkin += HandleChangeSkin;
    }

    public void Setup(Vector3 position, ElementType type = ElementType.White)
    {
        UpdatePosition(position);

        SetLevel(type);

        SetStatus(ElementStatus.Locked);
    }

    public void Upgrade()
    {
        bool canUpgrade = _properties.CanUpgrade();

        if(canUpgrade == false)
            return;

        ElementType nextLevel = _properties.GetNextUpgradeLevel();

        SetLevel(nextLevel);
    }

    public ElementStatus GetStatus()
    {
        return _status;
    }
    public void SetStatus(ElementStatus newStatus)
    {
        _status = newStatus;

        RefreshSprite();
    }

    private void OnMouseDown()
    {
        Interact();
    }

    private void OnMouseUp()
    {
        FinishInteraction();
    }

    private void Update()
    {
        UpdateDragPosition();
        CheckCoinFarming();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(GetStatus() != ElementStatus.Selected)
            return;

        EvolutionElement newElement = IsColliderSameElement(collision);

        if(newElement != null)
            _CollidingWithEqual = newElement;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(GetStatus() != ElementStatus.Selected)
            return;

        EvolutionElement newElement = IsColliderSameElement(collision);

        if(newElement != null && _CollidingWithEqual == newElement)
            _CollidingWithEqual = null;
    }

    private void Interact()
    {
        switch(_status)
        {

            case ElementStatus.Locked:
                Unlock();
                break;
            case ElementStatus.Idle:
                ToggleDrag(true);
                break;
            case ElementStatus.Selected:
                //Something broke here D:
                break;
        }
    }

    private void Unlock()
    {
        _eventHandler.SpawnElement(_properties.Type);
        SetStatus(ElementStatus.Idle);
    }

    private void FinishInteraction()
    {
        if(_status != ElementStatus.Selected)
            return;

        ToggleDrag(false);
    }


    private void UpdateDragPosition()
    {
        if(GetStatus() != ElementStatus.Selected)
            return;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(_inputController.GetClickPosition());

        UpdatePosition(mousePosition);
    }


    private void UpdatePosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    private void ToggleDrag(bool value)
    {
        if(value == true)
        {
            SetSelected();
        }
        else
        {
            Unselect();
        }
    }

    private EvolutionElement IsColliderSameElement(Collision2D collision)
    {
        EvolutionElement element = collision.gameObject.GetComponent<EvolutionElement>();

        if(element == null || element.Properties.Type != Properties.Type || element.GetStatus() != ElementStatus.Idle)
            return null;

        return element;
    }

    private void SetSelected()
    {
        SetStatus(ElementStatus.Selected);

        transform.SetAsLastSibling();

        transform.SetAsLastSibling();
        _components.Rigidbody.useFullKinematicContacts = true;

        _movement.SetSelected();
    }

    private void Unselect()
    {
        if(_CollidingWithEqual == null || Properties.CanUpgrade() == false)
        {
            transform.SetSiblingIndex(transform.GetSiblingIndex() - 1);
            _components.Rigidbody.Sleep();
            _components.Rigidbody.useFullKinematicContacts = false;
            _CollidingWithEqual = null;

            _movement.UnSelect();
            return;
        }

        Merge();
    }

    private void Merge()
    {
        _CollidingWithEqual.Upgrade();

        Destroy(gameObject);
    }

    private void SetLevel(ElementType newLevel)
    {
        ElementProperty nextSettings = _gameLibrary.GetLevelSettings(newLevel);

        UpdateProperties(nextSettings);
    }
    private void UpdateProperties(ElementProperty newProperties)
    {
        if(onUpgraded != null)
            onUpgraded(newProperties.Type);

        _properties = newProperties;

        RefreshSprite();
    }

    private ElementSprite LoadPrefab()
    {
        if(GetStatus() == ElementStatus.Locked)
            return _gameLibrary.FactoryTemplates.BoxPrefab;

        return _properties.Prefab;
    }

    private void RefreshSprite()
    {
        EraseExistingObject();

        ElementSprite spr = LoadPrefab();

        InstantiateObject(spr);
    }

    private void CheckCoinFarming()
    {
        if(GetStatus() != ElementStatus.Idle)
            return;

        if(Time.time - _lastFarmedCoinTime < Properties.CoinCoolDown)
            return;

        if(onFarmCoin != null)
            onFarmCoin(Properties.Type, Properties.CoinFarmQuantity);

        _lastFarmedCoinTime = Time.time;

        SpawnPoop();
    }

    private void SpawnPoop()
    {
        ElementPoop poop = _poopFactory.Create();

        poop.transform.position = transform.position;
        poop.transform.SetSiblingIndex(transform.GetSiblingIndex() - 1);
    }

    private void InstantiateObject(ElementSprite prefab)
    {
        ElementSprite newObject = Instantiate(prefab, _components.ElementPivot);
        newObject.Setup(_gameLibrary);
        newObject.transform.localPosition = Vector3.zero;

        _elementSprite = newObject;

        CheckSkin();
    }

    private void CheckSkin()
    {
        SkinType currentSkin = _playerData.GetCurrentSkin();

        _elementSprite.SetSkin(currentSkin);
    }

    private void EraseExistingObject()
    {
        if(_elementSprite == null)
            return;

        Destroy(_elementSprite.gameObject);
    }

    private void HandleChangeSkin(SkinType skinType)
    {
        _elementSprite.SetSkin(skinType);
    }

    public class Factory : PlaceholderFactory<EvolutionElement> {
    }
}
