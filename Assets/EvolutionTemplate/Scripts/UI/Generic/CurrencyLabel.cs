using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Text))]
public class CurrencyLabel : MonoBehaviour
{
    [SerializeField]
    private CurrencyType type;

    [Inject]
    IPlayerDataController _playerData;
    
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
        string text = _playerData.GetCurrencyAmount(type).ToString();

        _label.text = text;

    }
}
