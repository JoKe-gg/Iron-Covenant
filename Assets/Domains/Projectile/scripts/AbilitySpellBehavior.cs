using System.Collections;
using UnityEngine;

public class AbilitySpellBehavior : Projectile, IPoolable
{
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _startScale;
    private float? _speedToChange;
    ProjectilePool _abilitySpellPool = null;

    private void Awake()
    {
        _startScale = transform.localScale;
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if (_speedToChange != null)
        {
            transform.localScale = new Vector2(transform.localScale.x + _speedToChange.Value * Time.deltaTime, transform.localScale.x + _speedToChange.Value * Time.deltaTime);
        }
    }
    public void OnSpawnFromPool()
    {

    }
    public void OnReturnToPool()
    {
        transform.localScale = _startScale;
        _rigidbody.linearVelocity = Vector2.zero;
        _speedToChange = null;
    }
    public void Initialize(float speedToChange, float scaleMultiplier, Vector2 pos, Transform playerTransform, float _speed, ProjectilePool pool, float timeToRemove, bool flipX)
    {
        _speedToChange = speedToChange;
        _spriteRenderer.flipX = flipX;
        _abilitySpellPool = pool;

        float angleDeg = playerTransform.rotation.eulerAngles.z;
        float rad = (angleDeg) * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

        transform.position = pos;

        _rigidbody.linearVelocity = dir * (flipX ? -_speed : _speed);
        transform.localScale = new Vector2(_startScale.x * scaleMultiplier, _startScale.y * scaleMultiplier);
        StartCoroutine(RemoveAfterTime(timeToRemove));

    }
    private IEnumerator RemoveAfterTime(float sec)
    {
        yield return new WaitForSeconds(sec);
        if (_abilitySpellPool != null)
        {
            _abilitySpellPool.ReturnProjectile(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
