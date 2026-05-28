using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(AttractToPlayer))]
public class Gem : MonoBehaviour, IPoolable
{
    private int _expForTaking = 1;
    private AttractToPlayer _attractToPlayer;
    private GemPool _pool;
    private void Awake()
    {
        _attractToPlayer = GetComponent<AttractToPlayer>();
    }
    private void OnEnable()
    {
        StateManager.instance.OnStateChanged += OnStateChanged;
    }
    private void OnDisable()
    {
        StateManager.instance.OnStateChanged -= OnStateChanged;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.TryGetComponent(out PlayerLevelSystem playerLevelSystem))
            {
                playerLevelSystem.PurchaseEXP(_expForTaking);
                _pool.ReturnGem(this);
            }
        }
    }
    private void OnStateChanged(RuntimeState state)
    {
        switch (state)
        {
            case RuntimeState.Victory:
                _pool.ReturnGem(this);
                break;
            case RuntimeState.GameOver:
                _pool.ReturnGem(this);
                break;
            default:
                break;
        }
    }
    public void Initialize(int expAmount, Vector2 pos, GemPool gemPool)
    {
        transform.position = pos;
        SetExpForTaking(expAmount);
        _pool = gemPool;
    }
    private void SetExpForTaking(int value)
    {
        _expForTaking = value;
    }
    public void OnSpawnFromPool()
    {
        _attractToPlayer.SetAttraction(null);
    }
    public void SetAttraction(Transform transform)
    {
        _attractToPlayer.SetAttraction(transform);
    }
    public void OnReturnToPool()
    {
        _attractToPlayer.SetAttraction(null);
    }
}
