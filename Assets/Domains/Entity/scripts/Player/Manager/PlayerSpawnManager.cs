using UnityEngine;
using System.Collections.Generic;
public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private CharacterSpawnSO _characterSpawnSO;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _currentIndex;

    public static GameObject CurrentPlayer { get; private set; }

    private void Awake()
    {
        _currentIndex = CurrentArenaHolderManager.Instance.currentPlayerID;
        if (GameObject.FindGameObjectWithTag("Player") == null)
        CurrentPlayer = Instantiate(_characterSpawnSO.CharacterSpawnList[_currentIndex], _spawnPoint.position, Quaternion.identity);
    }
}
