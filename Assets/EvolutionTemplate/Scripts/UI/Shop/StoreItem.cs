using System;
using UnityEngine;

[Serializable]
public struct StoreItem {
    [SerializeField]
    private string _uid;
    public string Uid
    {
        get
        {
            return _uid;
        }
    }

    [SerializeField]
    private StoreItemType _type;
    public StoreItemType Type
    {
        get
        {
            return _type;
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
    private string _description;
    public string Description
    {
        get
        {
            return _description;
        }
    }

    [SerializeField]
    private float _price;
    public float Price
    {
        get
        {
            return _price;
        }
    }
}
