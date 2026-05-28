using UnityEngine;

public class DamageDisplayPool : MonoBehaviour
{
    [SerializeField] private TakingDamageDisplayItemSetter _prefab;
    [SerializeField] private int _startAmount;
    [SerializeField] private int _maxAmount;
    private ObjectPool<TakingDamageDisplayItemSetter> _pool;

    public void Awake()
    {
        _pool = new(_prefab, _startAmount, transform, _maxAmount);
    }
    public TakingDamageDisplayItemSetter GetDamageDisplay()
    {
        return _pool.Get();
    }
    public void ReturnToPool(TakingDamageDisplayItemSetter setter)
    {
        _pool.Return(setter);
    }
}
