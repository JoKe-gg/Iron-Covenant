using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class LevelCountController : MonoBehaviour
{
    [SerializeField] private PlayerLevelSystem _playerLevelSystem;
    [SerializeField] private TextMeshProUGUI _levelCountTMP;
    [SerializeField] private string _levelCountText = "Level: ";

    private void Start()
    {

        _playerLevelSystem = PlayerSpawnManager.CurrentPlayer.GetComponent<PlayerLevelSystem>();
        _playerLevelSystem.OnLevelUP += UpdateLevelCount;
    }

    private void OnDisable()
    {
        if (_playerLevelSystem != null)
            _playerLevelSystem.OnLevelUP -= UpdateLevelCount;
    }
    private void UpdateLevelCount(int level)
    {
        _levelCountTMP.text = _levelCountText + level;
    }
}
