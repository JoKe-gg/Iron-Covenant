using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;
    public event System.Action<bool> OnPauseStatusChanged;
    private PlayerActionMap _input;
    public bool isPaused { get; private set;}
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _input = new PlayerActionMap();
    }

    private void OnEnable() => _input?.Enable();
    private void OnDisable() => _input?.Disable();
    private void Update()
    {

        if (_input.Player.pause.triggered)
        {
            SetPause(!isPaused);
        }
    }
    public void SetPause(bool value, bool callInvoke = true)
    {
        isPaused = value;
        if (callInvoke)
        {
            OnPauseStatusChanged?.Invoke(isPaused);
        }
        Time.timeScale = isPaused ? 0 : 1;
    }
    public void Reset()
    {
        SetPause(false);
    }
}
