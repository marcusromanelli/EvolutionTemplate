using System;
using UnityEngine;

[Serializable]
public struct FactoryTemplates {
    [SerializeField]
    private EvolutionElement _evolutionElementPrefab;
    public EvolutionElement EvolutionElementPrefab
    {
        get
        {
            return _evolutionElementPrefab;
        }
    }

    [SerializeField]
    public ElementPoop _poopPrefab;
    public ElementPoop PoopPrefab
    {
        get
        {
            return _poopPrefab;
        }
    }

    [SerializeField]
    public StoreItemObject _storeObjectPrefab;
    public StoreItemObject StoreObjectPrefab
    {
        get
        {
            return _storeObjectPrefab;
        }
    }

    [SerializeField]
    public SkinObject _skinObjectPrefab;
    public SkinObject SkinObjectPrefab
    {
        get
        {
            return _skinObjectPrefab;
        }
    }

    [SerializeField]
    public ElementSprite _boxPrefab;
    public ElementSprite BoxPrefab
    {
        get
        {
            return _boxPrefab;
        }
    }

    [SerializeField]
    public StatsItem _statsObjectPrefab;
    public StatsItem StatsObjectPrefab
    {
        get
        {
            return _statsObjectPrefab;
        }
    }
}
