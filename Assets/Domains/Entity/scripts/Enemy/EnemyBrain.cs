using System;
using UnityEngine;
public enum EnemyState
{
    buttle,
    idle,
}
public class EnemyBrain : MonoBehaviour
{
    private EnemyState _currentState = EnemyState.idle;
    public EnemyState CurrentState => _currentState;
    public event Action<EnemyState> OnStateChanged;
    private void OnEnable()
    {
        _currentState = EnemyState.idle;
        OnStateChanged?.Invoke(_currentState);
    }
    private void OnDisable()
    {
    }
    public void ChangeState(EnemyState state)
    {
        SetState(state);
    }
    private void SetState(EnemyState state)
    {
        _currentState = state;
        OnStateChanged?.Invoke(_currentState);
    }
}

