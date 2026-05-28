using UnityEngine;

public class AncoredPositionTransform : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _parentSpriteRenderer;
    [SerializeField] private Collider2D _optionalWeaponCollider;
    private Vector2 _localPosition;
    private float _colliderOffsetX;
    private void Start()
    {
        if (_parentSpriteRenderer == null)
        {
            _parentSpriteRenderer = GetComponentInParent<SpriteRenderer>();
        }
        _localPosition = transform.localPosition;
        if (_optionalWeaponCollider != null)
        {
            _colliderOffsetX = _optionalWeaponCollider.offset.x;
        }
    }
    private void LateUpdate()
    {
        ChangeOffset();
    }
    private void ChangeOffset()
    {
        transform.localPosition = new Vector2(_localPosition.x * (IsParentFlipped() ? -1 : 1), _localPosition.y * (IsParentFlipped() ? -1 : 1));
        
        if (_optionalWeaponCollider != null)
        {
            Vector2 offset = _optionalWeaponCollider.offset;
            offset.x = (IsParentFlipped() ? -1 : 1) * _colliderOffsetX;
            _optionalWeaponCollider.offset = offset;
        }
    }
    public bool IsParentFlipped()
    {
        return _parentSpriteRenderer.flipX;
    }
}
