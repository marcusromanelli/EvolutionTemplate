using System;
using UnityEngine;

[Serializable]
public struct SkinItem {
    [SerializeField]
    private string _name;
    public string Name
    {
        get
        {
            return _name;
        }
    }

    [SerializeField]
    private SkinType _type;
    public SkinType Type
    {
        get
        {
            return _type;
        }
    }


    //This particular variable cannot be a generic type because Unity's inspector system does not support generic types :(
    [SerializeField]
    private CurrencyType _currencyType;
    public CurrencyType CurrencyType
    {
        get
        {
            return _currencyType;
        }
    }

    [SerializeField]
    private Sprite _sprite;
    public Sprite Sprite
    {
        get
        {
            return _sprite;
        }
    }

    [SerializeField]
    private int _price;
    public int Price
    {
        get
        {
            return _price;
        }
    }
}
