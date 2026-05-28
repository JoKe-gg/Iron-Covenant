using UnityEngine;

public class BossInstantiating : MonoBehaviour
{
    [SerializeField] private BossBarController _bossBar;
    private EnemyHealth _enemyHealth;
    private void Start()
    {
        _bossBar = GameManager.instance.BossBarController;
        if (_bossBar == null)
        {
            Debug.LogError($"Null reference to {nameof(_bossBar)} in the script {nameof(BossInstantiating)}");
            return;
        }
        _enemyHealth = GetComponent<EnemyHealth>();
        SetBossBar();
    }
    private void SetBossBar()
    {
        _bossBar.gameObject.SetActive(true);
        _bossBar.SubscribeToEvent(_enemyHealth);
    }
}
