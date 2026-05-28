using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PausePanelController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private bool isAbleToOpenPause = true;
    private void Awake()
    {
        if (panel == null)
        {
            Debug.LogError("Panel is not assigned", this);
            Destroy(gameObject);
            return;
        }
    }
    private void Start()
    {

        if (PauseManager.instance != null)
        {
            PauseManager.instance.OnPauseStatusChanged += OnPauseTriggered;
        }
    }

    private void OnDisable()
    {
        if (PauseManager.instance != null)
        {
            PauseManager.instance.OnPauseStatusChanged -= OnPauseTriggered;
        }
    }
    public void BlockPause()
    {
        isAbleToOpenPause = false;
        panel.SetActive(false);
    }
    private void OnPauseTriggered(bool value)
    {
        if (!isAbleToOpenPause) { return; }
        panel.SetActive(value);
    }
}
