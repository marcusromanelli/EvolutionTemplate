using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Text))]
public class CurrencyLabel : MonoBehaviour
{
    [SerializeField]
    private CurrencyType type;

    [Inject]
    PlayerData _playerData;
    
    private Text _label;

    private void Awake()
    {
        _label = GetComponent<Text>();
    }
    void Update()
    {
        CheckData();
    }

    void CheckData()
    {
        string text = "";

        switch(type)
        {

            case CurrencyType.Cash:
                text = _playerData.CashAmount.ToString();
                break;
            case CurrencyType.Coin:
                text = _playerData.CoinAmount.ToString();
                break;
        }

        _label.text = text;

    }
}
