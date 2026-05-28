using TMPro;
using UnityEngine;

public class InitializableEnemyKilledAmount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _enemyNameText;
    [SerializeField] private TextMeshProUGUI _enemyKilledAmounts;

    public void Initialize(string enemy, int amount)
    {
        if (_enemyNameText != null)
            _enemyNameText.text = enemy;
        if (_enemyKilledAmounts != null)
            _enemyKilledAmounts.text = "amount : " + amount;
    }
}
