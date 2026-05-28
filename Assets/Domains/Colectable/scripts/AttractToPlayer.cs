using UnityEngine;

public class AttractToPlayer : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    private Transform _playerTransform = null;
    public void Update()
    {
        if (_playerTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, _speed);
        }
    }
    public void SetAttraction(Transform playerTransform)
    {
            _playerTransform = playerTransform;
    }
}
