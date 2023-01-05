using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DefaultScreen : MonoBehaviour {
    [SerializeField]
    protected GameObject _mainContainer;

    [SerializeField]
    protected GameObject _contentsContainer;

    [SerializeField]
    protected Button _closeButton;

    void Start()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        _closeButton.onClick.AddListener(() => { CloseWindow(); });
    }

    public virtual void OpenWindow()
    {
        _mainContainer.SetActive(true);
    }

    public virtual void CloseWindow()
    {
        _mainContainer.SetActive(false);
    }
}
