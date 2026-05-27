using System;
using UnityEngine;
public enum RuntimeState
{
    Buttle,
    BossBattle,
    Pause,
    GameOver,
    Victory
}
public class StateManager : MonoBehaviour
{
    static public StateManager instance;
    public RuntimeState CurrentState { get; private set; }
    public event Action<RuntimeState> OnStateChanged;
    private void Awake()
    {
        SetState(CurrentState);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void SetState(RuntimeState state)
    {
        if(CurrentState == state)
        {
            return;
        }
        CurrentState = state;
        OnStateChanged?.Invoke(CurrentState);
    }
}
