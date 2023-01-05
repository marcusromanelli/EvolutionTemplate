using System;

using UnityEngine;
using UnityEngine.UI;



public class PopUp : Singleton<PopUp> {

    [SerializeField]
    private GameObject _mainContainer;
    public GameObject MainContainer
    {
        get
        {
            return _mainContainer;
        }
    }
    [SerializeField]
    private Text _titleLabel;
    public Text TitleLabel
    {
        get
        {
            return _titleLabel;
        }
    }

    [SerializeField]
    private Text _descriptionLabel;
    public Text DescriptionLabel
    {
        get
        {
            return _descriptionLabel;
        }
    }


    [SerializeField]
    private Button _leftButton;
    public Button LeftButton
    {
        get
        {
            return _leftButton;
        }
    }



    [SerializeField]
    private Text _leftButtonLabel;
    public Text LeftButtonLabel
    {
        get
        {
            return _leftButtonLabel;
        }
    }


    [SerializeField]
    private Button _rightButton;
    public Button RightButton
    {
        get
        {
            return _leftButton;
        }
    }

    [SerializeField]
    private Text _rightButtonLabel;
    public Text RightButtonLabel
    {
        get
        {
            return _rightButtonLabel;
        }
    }



    public static void Close()
    {
        Instance.CloseWindow();
    }

    public static void Show(string description, string title, string leftButtonlabel = "Ok", Action leftButtonCallback = null, string rightButtonLabel = null, Action rightButtonCallback = null)
    {
        Instance.show(description, title, leftButtonlabel, leftButtonCallback, rightButtonLabel, rightButtonCallback);
    }
    public void show(string description, string title, string leftButtonlabel = "Ok", Action leftButtonCallback = null, string rightButtonLabel = null, Action rightButtonCallback = null)
    {

        _descriptionLabel.text = description;
        _titleLabel.text = title;


        if(leftButtonCallback != null || leftButtonlabel != null)
        {
            _leftButton.gameObject.SetActive(true);

            _leftButton.onClick.RemoveAllListeners();
            _leftButton.onClick.AddListener(() =>
            {
                Close();

                if(leftButtonCallback != null)
                    leftButtonCallback();
            });


            _leftButtonLabel.text = leftButtonlabel;
        }
        else
        {
            _leftButton.gameObject.SetActive(false);
        }

        if(rightButtonCallback != null || rightButtonLabel != null)
        {
            _rightButton.gameObject.SetActive(true);

            _rightButton.onClick.RemoveAllListeners();
            _rightButton.onClick.AddListener(() =>
            {
                Close();

                if(rightButtonCallback != null)
                    rightButtonCallback();
            });


            _rightButtonLabel.text = rightButtonLabel;
        }
        else
        {
            _rightButton.gameObject.SetActive(false);
        }

        OpenWindow();

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
