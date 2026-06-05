using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSpawnSO", menuName = "Scriptable Objects/CharacterSpawnSO")]
public class CharacterSpawnSO : ScriptableObject
{
    [SerializeField] private List<GameObject> _characterSpawnList; 
    public List<GameObject> CharacterSpawnList => _characterSpawnList;
}
