using UnityEngine;

public class CoinsCounterController : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _counterText;
    private void Awake()
    {
        if ( _counterText == null)
        {
            Debug.LogError("Counter text is not asigned ", this);
            Destroy(gameObject);
            return;
        }
    }
    private void Start()
    {
        CoinsManager.instance.OnCoinsChanged += OnChangeCoins;
        OnChangeCoins(CoinsManager.instance.Coins);
    }
    private void OnDisable()
    {
        if ( CoinsManager.instance != null)
        {
        CoinsManager.instance.OnCoinsChanged -= OnChangeCoins;
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
