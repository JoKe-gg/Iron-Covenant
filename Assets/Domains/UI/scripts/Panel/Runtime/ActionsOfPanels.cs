using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionsOfPanels : MonoBehaviour
{
    [SerializeField] private SaveManagerSO _saveManagerSO;
    [SerializeField] private GameObject panel;
    private int? _sceneIndex = null;
    public void RestartArena()
    {
        if (_sceneIndex != null) return;
        _sceneIndex = SceneManager.GetActiveScene().buildIndex;
        PauseManager.instance.Reset();
        _saveManagerSO.SaveGame();
        SceneManager.LoadScene(_sceneIndex.Value);
    }

    public void GoToMainMenu()
    {
        if (_sceneIndex != null) return;
        _sceneIndex = 0;
        PauseManager.instance.Reset();
        _saveManagerSO.SaveGame();
        SceneManager.LoadScene(_sceneIndex.Value);
    }
    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    public void Resume()
    {
        PauseManager.instance.SetPause(false);
        if (panel != null)
        panel.SetActive(false);
    }
}
