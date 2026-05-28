using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class WeaponTransform : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _parentSpriteRenderer;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private bool _parentFlipped;
    private Vector2 _localPosition;
    private void Start()
    {
        if(_parentSpriteRenderer == null)
        {
            _parentSpriteRenderer = GetComponentInParent<SpriteRenderer>();
        }
        if(_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        _localPosition = transform.localPosition;
    }
    private void LateUpdate()
    {
        _parentFlipped = _parentSpriteRenderer.flipX;
        Rotating();
        Flip();
        ChangeOffset();
    }
    private void Flip()
    {
        
        _spriteRenderer.flipX = _parentFlipped;
    }
    private void ChangeOffset()
    {
        transform.localPosition = new Vector2(_localPosition.x * (IsFlipped() ? -1 : 1), _localPosition.y);
    }
    public bool IsFlipped()
    {
        return _spriteRenderer.flipX;
    }
    private void Rotating()
    {
        Vector2 screen = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(screen.x, screen.y, 0f));
        Vector3 playerPosition = transform.position;
        Vector2 dir = mouseWorldPos - playerPosition;

        float angleDeg = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - (IsFlipped() ? 180 : 0);

        transform.rotation = Quaternion.Euler(0, 0, angleDeg);
    }
    public Quaternion GetRotation()
    {
        return transform.rotation;
    } 
}
