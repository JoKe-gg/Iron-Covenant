using TMPro;
using UnityEngine;
public class UpgradeChoiceBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;

    public void Initialize(UpgradeSO upgradeSO)
    {
        _name.text = CreateName(upgradeSO);
        _description.text = upgradeSO.Description;
    }
    private string CreateName(UpgradeSO upgradeSO)
    {
        return upgradeSO.Name + " " + upgradeSO.Level;
    }
}
