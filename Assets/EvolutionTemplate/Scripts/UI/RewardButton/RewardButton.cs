using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RewardButton : MonoBehaviour {

    [SerializeField]
    private Button _button;

    [SerializeField]
    private Text _label;

    [SerializeField]
    private float _rewardCooldown; //In seconds

    [SerializeField]
    private CurrencyType _currencyType;

    [SerializeField]
    private int _rewardValue;

    [Inject]
    IEventHandler _eventHandler;

    private float _lastRewardTime;
    private bool _adsReady;

    private void Update()
    {
        UpdateStatus();
    }

    [Inject]
    public void Construct(IEventHandler eventHandler)
    {
        _eventHandler = eventHandler;

        _eventHandler.onEventTrigger += HandleTrigger;
    }

    private void HandleTrigger(EventType eventType, params object[] param)
    {
        switch(eventType)
        {
            default:
                break;
            case EventType.AdsReady:
                _adsReady = true;
                break;
            case EventType.RewardVideoPlayed:
                PayReward();
                break;
        }
    }

    private bool CanReward()
    {
        return _adsReady == true && Time.time - _lastRewardTime > _rewardCooldown;
    }

    private void UpdateStatus()
    {
        if(_adsReady == false)
            _button.gameObject.SetActive(false);
        else
        {

            _button.gameObject.SetActive(true);
            _button.interactable = CanReward();
        }

        UpdateLabel();
    }
    private void UpdateLabel()
    {
        string finaltext;

        if(CanReward())
        {
            finaltext = string.Concat("You won ", GetReward(), "!\nWatch video to retrieve!");
        }
        else
        {
            finaltext= string.Concat("Next reward in \n", GetTimeForNextReward(), " seconds.");
        }

        _label.text = finaltext;
    }

    private string GetReward()
    {
        return string.Concat(_rewardValue, " ", _currencyType.ToString());
    }

    private float GetTimeForNextReward()
    {
        return Mathf.Abs(Mathf.Ceil(Time.time - _rewardCooldown));
    }

    private void PayReward()
    {
        _lastRewardTime = Time.time;

        _eventHandler.TriggerEvent(EventType.EarnCurrency, _currencyType, _rewardValue);
    }
}
