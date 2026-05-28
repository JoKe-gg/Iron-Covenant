using UnityEngine;

public class CoinsCounterControllerMainMenu : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _counterText;
    private void Awake()
    {
        if (_counterText == null)
        {
            Debug.LogError("Counter text is not asigned ", this);
            Destroy(gameObject);
            return;
        }
    }
    private void OnEnable()
    {   
        if (CoinsManagerMainMenu.instance != null)
        {
            OnChangeCoins(CoinsManagerMainMenu.instance.Coins);
            CoinsManagerMainMenu.instance.OnCoinsChanged += OnChangeCoins;
        }
    }
    private void OnDisable()
    {
        if (CoinsManagerMainMenu.instance != null)
        {
            CoinsManagerMainMenu.instance.OnCoinsChanged -= OnChangeCoins;
        }
    }
    private void OnChangeCoins(int coins)
    {
        ChangeText(coins);
    }
    private void ChangeText(int coins)
    {
        _counterText.text = coins.ToString();
    }
}
