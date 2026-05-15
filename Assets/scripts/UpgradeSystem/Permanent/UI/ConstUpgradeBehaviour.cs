using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(ButtonHoverEffect))]
public class ConstUpgradeBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private Image _constUpgradeSprite;
    private Button _constUpgradeButton;
    Dictionary<int, UpgradeSO> _upgradeSODictionary = new();
    public int Level { get; private set; } = 1;
    private int _price;

    private void Awake()
    {
        _constUpgradeButton = GetComponent<Button>();
    }
    public void Initialize(List<UpgradeSO> upgradeSOs, int startLevel)
    {
        foreach (var upgradeSO in upgradeSOs)
        {
            _upgradeSODictionary[upgradeSO.Level] = upgradeSO;
        }
        EndInitialize(startLevel);
    }
    public void EndInitialize(int startLevel)
    {
        Level = startLevel + 1;
        SetCurrentLevel();
    }
    private void SetCurrentLevel()
    {
        if (!_upgradeSODictionary.TryGetValue(Level, out UpgradeSO upgradeSO)) 
        { 
            Destroy(gameObject);
            return;
        }
        else
        {
            _nameText.text = upgradeSO.name;
            _price = upgradeSO.Price;
            _priceText.text = _price.ToString();
            _constUpgradeSprite.sprite = upgradeSO.Sprite;
            if (CoinsManagerMainMenu.instance != null)
            {
                SetEnable(CoinsManagerMainMenu.instance.Coins);
            }
        }
            
    }
    private void OnEnable()
    {
        if (CoinsManagerMainMenu.instance != null)
            CoinsManagerMainMenu.instance.OnCoinsChanged += SetEnable;
    }
    private void OnDisable()
    {
        if (CoinsManagerMainMenu.instance != null)
            CoinsManagerMainMenu.instance.OnCoinsChanged -= SetEnable;
    }
    private void SetEnable(int amount)
    {
        bool interactable = amount >= _price;
        _constUpgradeButton.interactable = interactable;
        _constUpgradeSprite.color = interactable ? Color.white : Color.darkGray;
    }
    public void AddUpgrade()
    {
        if (ConstUpgradeManager.instance == null) return;
        if (CoinsManagerMainMenu.instance == null) { return; }
        if (_upgradeSODictionary == null) return;
        UpgradeSO constUpgrade = _upgradeSODictionary[Level];
        CoinsManagerMainMenu.instance.SpendCoins(_price);
        ConstUpgradeManager.instance.AddConstUpgrade(constUpgrade);
        Level++;
        SetCurrentLevel();
    }
}
