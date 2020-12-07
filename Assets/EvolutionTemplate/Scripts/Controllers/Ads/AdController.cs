using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

public class AdController : MonoBehaviour, IUnityAdsListener {
    [SerializeField]
    private string _rewardVideoId = "rewardedVideo";
    [SerializeField]
    private string _androidGameId = "3928719";
    [SerializeField]
    private string _iOSGameId = "3928718";
    [SerializeField]
    private bool testMode = true;

    [Inject]
    EventHandler _eventHandler;

    void Start()
    {
        Advertisement.AddListener(this);

#if UNITY_ANDROID
        Advertisement.Initialize(_androidGameId, testMode);
#elif UNITY_IOS
        Advertisement.Initialize(_iOSGameId, testMode);
#endif
    }

    [Inject]
    public void Construct(EventHandler eventHandler)
    {
        _eventHandler = eventHandler;
    }

    public void ShowRewardedVideo()
    {
        if(Advertisement.IsReady(_rewardVideoId))
        {
            Advertisement.Show(_rewardVideoId);
        }
        else
        {
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch(showResult)
        {
            case ShowResult.Finished:
                _eventHandler.TriggerEvent(EventType.RewardVideoPlayed);
                break;
            case ShowResult.Skipped:
            case ShowResult.Failed:
                Debug.LogWarning("The ad did not finish.");
                break;
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        if(placementId == _rewardVideoId)
            _eventHandler.TriggerEvent(EventType.AdsReady);
    }


    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }
}
