using UnityEngine;

public class UpgradePanelController : MonoBehaviour
{
    [SerializeField] private GameObject UpgradePanel;
    private void Awake()
    {
        if (UpgradePanel == null)
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        PlayerSpawnManager.CurrentPlayer.GetComponent<PlayerLevelSystem>().OnLevelUP += OpenPanel;
    }
    private void OpenPanel(int level)
    {
        PauseManager.instance.SetPause(true, false);
        UpgradePanel.SetActive(true);
    }
}
