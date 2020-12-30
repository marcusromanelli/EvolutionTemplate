using System.Collections.Generic;

public interface IRecordsController
{
    bool HasUnlockedElementType(ElementType type);

    void AddNewType();

    ElementStatistics RetrieveStatistics(ElementType type);

    void AddSpawnCount(ElementType type);

    void AddCoinFarmCount(ElementType type, int quantity);

    List<ElementStatistics> GetStatistics();
}
