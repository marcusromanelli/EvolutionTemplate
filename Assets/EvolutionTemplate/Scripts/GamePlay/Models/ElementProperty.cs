using System;
using UnityEngine;

[Serializable]
public struct ElementProperty {
    [SerializeField]
    private string _name;

    [SerializeField]
    private string _description;

    [SerializeField]
    private ElementType _type;

    [SerializeField]
    private Sprite _sprite;

    [SerializeField]
    private ElementSprite _prefab;

    [SerializeField]
    private int _coinCoolDown;

    [SerializeField]
    private int _coinFarmQuantity;

    [SerializeField]
    private float _walkSpeed;

    [SerializeField]
    private float _flySpeed;

    [SerializeField]
    private float _movementRadius;

    [SerializeField]
    private float _minWanderAwait;

    public string Name
    {
        get
        {
            return _name;
        }

    }
    public string Description
    {
        get
        {
            return _description;
        }

    }

    public ElementType Type
    {
        get
        {
            return _type;
        }

    }

    public Sprite Sprite
    {
        get
        {
            return _sprite;
        }

    }
    

    public ElementSprite Prefab
    {
        get
        {
            return _prefab;
        }

    }

    public int CoinCoolDown
    {
        get
        {
            return _coinCoolDown;
        }

    }

    public int CoinFarmQuantity
    {
        get
        {
            return _coinFarmQuantity;
        }

    }

    public float WalkSpeed
    {
        get
        {
            return _walkSpeed;
        }

    }

    public float FlySpeed
    {
        get
        {
            return _flySpeed;
        }

    }

    public float MovementRadius
    {
        get
        {
            return _movementRadius;
        }

    }

    public float MinWanderAwait
    {
        get
        {
            return _minWanderAwait;
        }

    }

    public float CPS
    {
        get
        {
            return (float)_coinFarmQuantity / (float)_coinCoolDown;
        }

    }


    public ElementType GetNextUpgradeLevel()
    {
        if(CanUpgrade() == false)
            return Type;

        int currentLevel = (int)Type;        

        return (ElementType) (currentLevel + 1);
    }

    public bool CanUpgrade()
    {
        int currentLevel = (int) Type;

        bool isDefined = Enum.IsDefined(typeof(ElementType), currentLevel + 1);
        return isDefined;
    }
}
