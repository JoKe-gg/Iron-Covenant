using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BarController))]
public class BossBarController : MonoBehaviour
{
    private EnemyHealth _enemyHealth;
    private BarController _barController;
    private void Start()
    {
        _barController = GetComponent<BarController>();
        gameObject.SetActive(false);
    }
    private void ChangeBossBar(int health, int maxHealth)
    {
        if (health == 0)
        {
            gameObject.SetActive(false);
            return;
        }
        _barController.ChangeBar(health, maxHealth);
    }
    public void SubscribeToEvent(EnemyHealth enemyHealth)
    {
        _enemyHealth = enemyHealth;
        _enemyHealth.OnHealthChanged += ChangeBossBar;
        StartCoroutine(UpdateHP());
    }

    private IEnumerator UpdateHP()
    {
        yield return new WaitForEndOfFrame();
        _enemyHealth.UpdateHP();
    }
    private void OnDisable()
    {
        if (_enemyHealth != null)
        _enemyHealth.OnHealthChanged -= ChangeBossBar;
    }
}
