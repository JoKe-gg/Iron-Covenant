using System.Collections;
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
    private void Start()
    {
        if (_purchasePanel == null)
        {
            Destroy(gameObject);
        }
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
}
