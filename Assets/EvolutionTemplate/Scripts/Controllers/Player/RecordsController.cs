using System;
using System.Collections.Generic;

public class RecordsController : IRecordsController {
    private List<ElementStatistics> _statistics;
    
    public RecordsController()
    {
        _statistics = new List<ElementStatistics>();
        AddNewType(); //Add default for "level 1" element
    }
    public bool HasUnlockedElementType(ElementType type)
    {
        if(_statistics.Count - 1 < (int)type)
            return false;

        return true;
    }

    public void AddNewType()
    {
        _statistics.Add(new ElementStatistics());
    }

    public ElementStatistics RetrieveStatistics(ElementType type)
    {
        if(!HasUnlockedElementType(type))
            return new ElementStatistics();

        return _statistics[(int)type];
    }

    public void AddSpawnCount(ElementType type)
    {
        if(!HasUnlockedElementType(type))
            throw new Exception("Type " + type.ToString() + " has not been unlocked.");

        ElementStatistics elementStatistics = RetrieveStatistics(type);

        elementStatistics.AddSpawnCount();
    }
    public void AddCoinFarmCount(ElementType type, int quantity)
    {
        if(!HasUnlockedElementType(type))
            throw new Exception("Type " + type.ToString() + " has not been unlocked.");

        ElementStatistics elementStatistics = RetrieveStatistics(type);

        elementStatistics.AddCoinFarmCount(quantity);
    }

    public List<ElementStatistics> GetStatistics()
    {
        return _statistics;
    }
}
