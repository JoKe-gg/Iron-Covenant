using System;
using UnityEngine;
using UnityEngine.AdaptivePerformance.Provider;
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
    //Temp
    private float interval = 0.5f;
    private float nextTimeTick = 0;
    private void OnEnable()
    {
        _currentState = EnemyState.idle;
        OnStateChanged?.Invoke(_currentState);
        nextTimeTick = Time.time;
    }
    private void OnDisable()
    {
    }
    private void Update()
    {
        if (Time.time > nextTimeTick)
        {
            Debug.Log($"Current state of {gameObject.name} is {_currentState}");
            nextTimeTick = Time.time + interval;
        }
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

