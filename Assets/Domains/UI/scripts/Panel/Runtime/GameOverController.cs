using UnityEngine;

public class GameOverController : MonoBehaviour
{
    [SerializeField]private GameObject _gameOverPanel;
    [SerializeField]private GameObject _victoryPanel;
    [SerializeField] private PausePanelController _pausePanelController;
    private void Start()
    {
        PlayerSpawnManager.CurrentPlayer.GetComponent<PlayerHealth>().OnPlayerDied += OnPlayerDied;
        _gameOverPanel.SetActive(false);
    }
    private void OnEnable()
    {
        StateManager.instance.OnStateChanged += OnRuntimeStateChanged;
    }
    private void OnRuntimeStateChanged(RuntimeState state)
    {
        switch(state)
        {
            case RuntimeState.Victory:
                OnPlayerWom();
                break;
            case RuntimeState.GameOver:
                OnPlayerDied();
                break;
            default:
                break;
        }
    }
    private void OnPlayerDied()
    {
        _pausePanelController.BlockPause();
        _gameOverPanel.SetActive(true);
    }
    private void OnPlayerWom()
    {
        _pausePanelController.BlockPause();
        _victoryPanel.SetActive(true);
    }
}
