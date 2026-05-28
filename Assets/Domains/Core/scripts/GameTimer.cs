using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance { get; private set; }

    public float ElapsedTime { get; private set; } = 0f;

    private bool _isRunning = true;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        PlayerSpawnManager.CurrentPlayer.GetComponent<PlayerHealth>().OnPlayerDied += StopTimer;
    }

    private void Update()
    {
        if (!_isRunning)
            return;

        ElapsedTime += Time.deltaTime;
    }

    public void StopTimer()
    {
        _isRunning = false;
    }
    public void Reset()
    {
        ElapsedTime = 0f;
    }
}
