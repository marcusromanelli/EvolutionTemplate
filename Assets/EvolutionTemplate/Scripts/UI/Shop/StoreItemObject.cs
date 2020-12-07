using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StoreItemObject : MonoBehaviour
{
    [SerializeField]
    public Image _itemIconSprite;

    [SerializeField]
    public Text _itemDescriptionLabel;

    [SerializeField]
    public Button _purchaseButton;

    [SerializeField]
    public Text _purchaseButtonLabel;

    private StoreItem _item;

    private void Awake()
    {
        _purchaseButton.onClick.AddListener(() =>
        {
            Purchase();
        });
    }

    public void Setup(StoreItem item)
    {
        _item = item;

        InitializeItem();
    }

    private void InitializeItem()
    {
        _itemIconSprite.sprite = _item.Sprite;
        _itemDescriptionLabel.text = _item.Description;
        _purchaseButtonLabel.text = "Purchase\n$" + _item.Price;
    }

    void Purchase()
    {
        Debug.Log("Purchased: " + _item.Type.ToString());
    }



    public class Factory : PlaceholderFactory<StoreItemObject> {
    }
}
