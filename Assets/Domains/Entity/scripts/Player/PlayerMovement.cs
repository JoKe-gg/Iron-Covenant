using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : Movable
{
    [SerializeField] private float _movementSpeed;
    private SpriteRenderer _spriteRenderer;
    private PlayerActionMap input;
    private bool _isMoving = true;
    private Vector3 _lastFrameMousePosition;
    protected override void Awake()
    {
        base.Awake();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        input = new PlayerActionMap();
    }
    void Start()
    {
        if (_rigidBody == null)
        {
            enabled = false;
            Debug.LogError("Null reference to Rigidbody2D");
            return;
        }
        if(_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        Vector2 screen = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(screen.x, screen.y, 0f));
        _lastFrameMousePosition = transform.position;
    }

    void FixedUpdate()
    {
        Movement();
    }
    private void LateUpdate()
    {
        Flip();
    }
    private void OnEnable() => input.Enable();
    private void OnDisable() => input.Disable();
    private void Flip()
    {
        Vector2 screen = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(screen.x, screen.y, 0f));
        if (mouseWorldPos != _lastFrameMousePosition)
        {
            Vector3 playerPosition = transform.position;
            bool flip = mouseWorldPos.x < playerPosition.x;
            _spriteRenderer.flipX = flip;
            _lastFrameMousePosition = mouseWorldPos;
        }
    }

    private void Movement()
    {
        if (!_isMoving) { return; }
        float dirX = 0;
        float dirY = 0;
        if (input.Player.right.IsPressed())
        {
            dirX++;
        }
        if (input.Player.left.IsPressed()) 
        {
            dirX--;
        }
        if (input.Player.up.IsPressed())
        {
            dirY++;
        }
        if (input.Player.down.IsPressed()) 
        { 
            dirY--;
        }
        Vector2 dir = new Vector2(dirX, dirY).normalized;
        _rigidBody.linearVelocity = dir * _movementSpeed;
    }
}
