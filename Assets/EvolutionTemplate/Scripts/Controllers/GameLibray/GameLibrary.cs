using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameLibrary is responsible for maintaining a direct asset reference for consuption.
/// It's used by GameLibraryScriptable.
/// </summary>
[Serializable]
public struct GameLibrary {

    [SerializeField]
    private FactoryTemplates _factoryTemplates;
    public FactoryTemplates FactoryTemplates
    {
        get
        {
            return _factoryTemplates;
        }
    }

    [SerializeField]
    private List<ElementProperty> _elementSettings;
    public List<ElementProperty> ElementSettings
    {
        get
        {
            return _elementSettings;
        }
    }

    [SerializeField]
    public List<StoreItem> _storeData;
    public List<StoreItem> StoreData
    {
        get
        {
            return _storeData;
        }
    }

    [SerializeField]
    private List<SkinItem> _skinData;
    public List<SkinItem> SkinData
    {
        get
        {
            return _skinData;
        }
    }

    public ElementProperty GetLevelSettings(ElementType type)
    {
        return ElementSettings.Find(elementProperty => elementProperty.Type == type);
    }

    public SkinItem GetSkinSettings(SkinType type)
    {
        return SkinData.Find(skinData => skinData.Type == type);
    }
}