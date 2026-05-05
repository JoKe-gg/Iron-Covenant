using System;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    //Components
    private EnemyBrain _enemyBrain;
    //Target
    private GameObject _player;
    //Target Urgent settings
    [SerializeField] private float _intervalBetweenUrgentPlayer = 0.1f;
    private float _nextUrgentTime;
    public event Action<Vector2?> OnUrgentTarget;
    private bool _toSense;
    private void Start()
    {
        _player = PlayerSpawnManager.CurrentPlayer.gameObject;
        _enemyBrain = GetComponent<EnemyBrain>();
        if (_player == null)
        {
            enabled = false;
        }
    }
    private void Update()
    {
        if (Time.time >= _nextUrgentTime)
        {
            UrgentTarget();
            _nextUrgentTime = Time.time + _intervalBetweenUrgentPlayer;
            
        }
    }
    void UrgentTarget()
    {
        Vector2? target = _player != null 
            ? _player.transform.position 
            : null;
        _enemyBrain.ChangeState(EnemyState.buttle);
        OnUrgentTarget?.Invoke(target);
        Debug.Log($"Target = {target}");
    }
}
