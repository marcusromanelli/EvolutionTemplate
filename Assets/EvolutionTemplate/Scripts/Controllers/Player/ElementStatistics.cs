public class ElementStatistics {
    private int _amountSpawned = 0;
    public int AmountSpawned
    {
        get
        {
            return _amountSpawned;
        }
    }


    private int _amounCoinFarmed = 0;
    public int AmounCoinFarmed
    {
        get
        {
            return _amounCoinFarmed;
        }
    }

    public void AddSpawnCount()
    {
        _amountSpawned++;
    }
    public void AddCoinFarmCount(int quantity)
    {
        _amounCoinFarmed += quantity;
    }
}
