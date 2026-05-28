using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FlipUpdate : MonoBehaviour
{
    private SpriteRenderer _parentSpriteRenderer;
    private SpriteRenderer _spriteRenderer;
    private bool _lastFlip = false;
    private void Awake()
    {
        _parentSpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _lastFlip = _parentSpriteRenderer.flipX;
    }
    private void LateUpdate()
    {
        if (_lastFlip != _parentSpriteRenderer.flipX)
        {
            _spriteRenderer.flipX = _parentSpriteRenderer.flipX;
            _lastFlip = _parentSpriteRenderer.flipX;
        }
    }
}
