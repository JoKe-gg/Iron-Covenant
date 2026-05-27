using System.Collections.Generic;
using UnityEngine;

public class TakingDamageDisplay : MonoBehaviour
{
    private List<TakingDamageDisplayItemSetter> damageDisplayItemSetters = new();
    public void DisplayDamage(int damage, Color panelColor, Vector2 scale)
    {
        DamageDisplayPool poolDamageDisplay = GameManager.instance.DamageDisplayPool;
        TakingDamageDisplayItemSetter displayDamageSetter = poolDamageDisplay.GetDamageDisplay();
        displayDamageSetter.Initialize(GetComponent<RectTransform>(), damage, poolDamageDisplay, panelColor, scale);
        damageDisplayItemSetters.Add(displayDamageSetter);
    }
    private void OnEnable()
    {
        DamageDisplayPool poolDamageDisplay = GameManager.instance.DamageDisplayPool;
        foreach (var item in damageDisplayItemSetters)
        {
            poolDamageDisplay.ReturnToPool(item);
        }
        damageDisplayItemSetters.Clear();
    }
}
