using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public abstract class Movable : MonoBehaviour
{
    [Header("Basic Movable Data")]
    protected Rigidbody2D _rigidBody;
    protected virtual void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
}
