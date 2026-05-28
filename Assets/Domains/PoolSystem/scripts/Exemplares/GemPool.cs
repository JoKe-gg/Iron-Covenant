using UnityEngine;

public class GemPool : MonoBehaviour
{
    [SerializeField] private Gem _prefab;
    [SerializeField] private int _startCount = 30;
    [SerializeField] private int _maxGems = 300;
    private ObjectPool<Gem> _gemPool;
    private void Start()
    {
        _gemPool = new ObjectPool<Gem>(_prefab, _startCount, transform, _maxGems);
    }
    public Gem GetGem()
    {
        return _gemPool.Get();
    }
    public void ReturnGem(Gem gem)
    {
        _gemPool.Return(gem);
    }
}