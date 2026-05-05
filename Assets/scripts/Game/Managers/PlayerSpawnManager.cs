using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _playerPrefabs;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _currentIndex;

    public static GameObject CurrentPlayer { get; private set; }

    private void Awake()
    {
        _currentIndex = CurrentArenaHolderManager.Instance.currentPlayerID;
        if (GameObject.FindGameObjectWithTag("Player") == null)
        CurrentPlayer = Instantiate(_playerPrefabs[_currentIndex], _spawnPoint.position, Quaternion.identity);
    }
}
