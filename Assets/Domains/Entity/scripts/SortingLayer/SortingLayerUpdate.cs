using UnityEngine;

public class SortingLayerUpdate : MonoBehaviour
{
    private int _multiplier = -100;
    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if( _spriteRenderer == null )
        {
            enabled = false;
        }
        _multiplier = SortingLayerUpdateMultiplier.multiplier;
    }
    private void LateUpdate()
    {
        float y = transform.position.y;
        UpdateSortingLayer(y);
    }
    private void UpdateSortingLayer(float y)
    {
        _spriteRenderer.sortingOrder = Mathf.RoundToInt(_multiplier*y);
    }
}
