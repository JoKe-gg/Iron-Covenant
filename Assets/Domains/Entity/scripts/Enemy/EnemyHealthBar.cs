using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private EnemyHealth _health;
    private void OnEnable()
    {
        if (_health == null )
        {
            return;
        }
        _health.OnHealthChanged += HandleChangeUIHealth;
    }
    private void OnDisable()
    {
        if (_health == null)
        {
            return;
        }
        _health.OnHealthChanged -= HandleChangeUIHealth;
    }
    public void HandleChangeUIHealth(int health, int maxHealth)
    {
        float percent = (float)health / maxHealth;
        _fillImage.fillAmount = Mathf.Clamp01(percent);
    }
}
