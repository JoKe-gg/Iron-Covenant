using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class ChildSortingLayer : MonoBehaviour
{
    [SerializeField] private int _offset = 1;
    [SerializeField] private int _multiplier = -100;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _multiplier = SortingLayerUpdateMultiplier.multiplier;
    }

    private void LateUpdate()
    {
        _spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.root.position.y * _multiplier) + _offset;
    }
}
