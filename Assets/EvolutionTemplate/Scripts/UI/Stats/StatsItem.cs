using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StatsItem : MonoBehaviour {
    [SerializeField]
    public Text _itemDescriptionLabel;

    [SerializeField]
    public Text _farmedValue;

    [SerializeField]
    public Text _spawnedValue;


    public void Setup(ElementProperty element, ElementStatistics statistic)
    {
        string finalDescription;
        string finalCoinFarmedCount;
        string finalSpawnedCount;

        if(statistic == null)
        {
            finalDescription = "???";
            finalCoinFarmedCount = "0";
            finalSpawnedCount = "0";
        }
        else
        {
            finalDescription = element.Name;
            finalCoinFarmedCount = statistic.AmounCoinFarmed.ToString();
            finalSpawnedCount = statistic.AmountSpawned.ToString();
        }

        _itemDescriptionLabel.text = finalDescription;
        _farmedValue.text = finalCoinFarmedCount;
        _spawnedValue.text = finalSpawnedCount;
    }


    public class Factory : PlaceholderFactory<StatsItem> {
    }
}