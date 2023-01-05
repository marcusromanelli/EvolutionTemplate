using UnityEngine;
using UnityEngine.Advertisements;
using Zenject;

public class AdController : MonoBehaviour, IUnityAdsListener {
    [SerializeField]
    private string _rewardVideoId = "rewardedVideo";
    [SerializeField]
    private string _gameId = "3928719";
#if UNITY_IOS
    [SerializeField]
    private string _gameId = "3928718";
#endif
    [SerializeField]
    private bool testMode = true;

    [Inject]
    IEventHandler _eventHandler;

    void Start()
    {
        Advertisement.AddListener(this);

        Advertisement.Initialize(_gameId, testMode);
    }

    [Inject]
    public void Construct(IEventHandler eventHandler)
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
                _eventHandler.RewardVideoPlayed();
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
            _eventHandler.AdsReady();
    }


    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }
}
