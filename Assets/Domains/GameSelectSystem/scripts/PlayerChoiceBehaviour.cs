using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerChoiceBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _purchasePanel;
    [SerializeField] private GameObject _blockedPanel;
    [SerializeField] private TextMeshProUGUI _priceText;
    private PlayerChoicesFiller _choicesFiller;
    private int _price;
    private int _id;

    private Sprite _playerSprite;
    private List<InfoTextData> _infoTextDatas;
    private string _titleText;
    private UiInfoPanelSetter _uiInfoPanelSetter;

    private void Start()
    {
        if (_purchasePanel == null)
        {
            Destroy(gameObject);
        }
    }

    public void SetInfoPanelData(Sprite playerSprite, List<InfoTextData> infoTextDatas, string titleText, UiInfoPanelSetter uiInfoPanelSetter)
    {
        _playerSprite = playerSprite;
        _infoTextDatas = infoTextDatas;
        _titleText = titleText;
        _uiInfoPanelSetter = uiInfoPanelSetter;
    }
    public void SetPurchasePanel(bool isPurchased, bool isUnlocked, PlayerChoicesFiller playerChoicesFiller, int amountToPurchase, int id)
    {
        if (_purchasePanel != null)
        {
            _purchasePanel.SetActive(!isPurchased);
            Debug.Log($"PurchasePanel {id} active: {_purchasePanel.activeSelf}, isPurchased: {isPurchased}", _purchasePanel);
        }
        else
        {
            Destroy(gameObject);
        }
        if (_blockedPanel != null)
        {
            _blockedPanel.SetActive(!isUnlocked);
        }
        else
        {
            Destroy(gameObject);
        }
        if (_priceText != null)
        {
            _priceText.text = "Buy: " + amountToPurchase;
        }
        _price = amountToPurchase;
        _choicesFiller = playerChoicesFiller;
        _id = id;
    }
    public void PurchasePlayer()
    {
        if (CoinsManagerMainMenu.instance != null)
        {
            if (CoinsManagerMainMenu.instance.TrySpendCoins(_price))
            {
                _choicesFiller.PurchasePlayer(_id);
                _purchasePanel.SetActive(false);
            }
        }
    }
    public void HoverInfoSet()
    {
        _uiInfoPanelSetter.Initialize(_infoTextDatas, _titleText, _playerSprite);
    }
}
