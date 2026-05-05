using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : Movable
{
    //Components
    private SpriteRenderer _spriteRenderer;
    private Enemy _enemy;
    private EnemyBrain _enemyBrain;
    private BasicStatsEnemySO _basicStatsEnemySO;
    private EnemySensor _enemySensor;
    //Movement
    private float _movementSpeed = 1.0f;
    private bool _isABleToMove = true;
    //KnockBak
    private bool _toKnockBack = false;
    private float _knockBackSpeed = 8f;
    //Stun
    [SerializeField] private float _stunTime = 0.1f;
    private Coroutine _stunCoroutine;
    //
    private Vector2? _target;
    //Flip
    private Vector2 _lastFramePosition;
    protected override void Awake()
    {
        base.Awake();
        _enemyBrain = GetComponent<EnemyBrain>();
        _enemy = GetComponent<Enemy>();
        _enemySensor = GetComponent<EnemySensor>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        if ( _rigidBody == null)
        {
            enabled = false;
        }
        _basicStatsEnemySO = _enemy.BasicStatsEnemySO;
        _movementSpeed = _basicStatsEnemySO.basicStats.BasicMovementSpeed;
        _lastFramePosition = transform.position;
    }
    public void Stun(float stunTime)
    {
        if (_stunCoroutine != null)
        {
            StopCoroutine(_stunCoroutine);
        }

        _isABleToMove = false;
        _stunCoroutine = StartCoroutine(Stunning(stunTime));
    }
    private void UrgentTarget(Vector2? newTarget)
    {
        _target = newTarget;
    }
    private IEnumerator Stunning(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        _isABleToMove = true;
    }
    public void KnockBack(float KnockBackSpeed) 
    {
        _toKnockBack = true;
        _knockBackSpeed = KnockBackSpeed;
    }
    private void FixedUpdate()
    {
        if (!_isABleToMove)
        {
            return;
        }
    }
    private void Update()
    {
        if (!_isABleToMove )
        {
            return;
        }
        MoveToPlayer();
        Flip();
    }
    void OnEnable()
    {
        if (_enemyBrain != null )
        _enemyBrain.OnStateChanged += SetMovementState;
        if (_enemySensor != null)
        _enemySensor.OnUrgentTarget += UrgentTarget;
    }
    void OnDisable()
    {
        if (_enemyBrain != null)
            _enemyBrain.OnStateChanged -= SetMovementState;
        if (_enemySensor != null)
            _enemySensor.OnUrgentTarget -= UrgentTarget;
    }
    
    private void SetMovementState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.buttle:
                _isABleToMove = true;
                break;
            case EnemyState.idle:
                _isABleToMove = false;
                break;
            default:
                break;
        }
    }
    private void MoveToPlayer()
    {
        Vector2 target = Vector2.zero;
        if (_target == null)
        {
            _rigidBody.linearVelocity = Vector3.zero;
            return;
        }
        else
        {
            target = _target.Value;
        }
        Vector2 targetDir = -((Vector2)transform.position - target).normalized;
        if (_toKnockBack)
        {
            targetDir *= -1; 
            Stun(_stunTime);
            _rigidBody.linearVelocity = targetDir * _knockBackSpeed;
            _toKnockBack = false;
            return;
        }
        _rigidBody.linearVelocity = targetDir * _movementSpeed;
    }
    private void Flip()
    {
        if (_lastFramePosition.x == transform.position.x) { return; }
        _spriteRenderer.flipX = transform.position.x < _lastFramePosition.x;
        _lastFramePosition = transform.position;
    }
    public void ResetMovement()
    {
        _toKnockBack = false;
        _isABleToMove = true;
        _knockBackSpeed = 0;
        _stunCoroutine = null;
    }
}