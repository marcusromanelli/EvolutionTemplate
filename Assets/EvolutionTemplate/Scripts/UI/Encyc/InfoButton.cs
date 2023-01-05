using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[Serializable]
public class InfoButton : MonoBehaviour {
    [SerializeField]
    private GameObject _exclamationPoint;

    [SerializeField]
    private Button _button;

    Animation _animation;
    IEventHandler _eventHandler;

    void Start()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(() => { HideExclamation(); });

        HideExclamation();
    }

    [Inject]
    void Constructor(IEventHandler eventHandler)
    {
        _eventHandler = eventHandler;

        _eventHandler.onNewElementLevelUnlocked += OnNewElementLevelUnlocked;
    }

    private void OnNewElementLevelUnlocked(ElementType elementType)
    {
        ShowExclamation();
    }

    private void HideExclamation()
    {
        _exclamationPoint.SetActive(false);
    }

    private void ShowExclamation()
    {
        _exclamationPoint.SetActive(true);
    }
}
