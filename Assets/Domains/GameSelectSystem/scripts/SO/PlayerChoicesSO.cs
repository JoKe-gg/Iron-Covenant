using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class PlayerChoiceData
{
    public int PlayerId;
    public Sprite PlayerSprite;
    public Sprite WeaponSprite;
    public string PlayerName;
    public int Price;
}

[CreateAssetMenu(fileName = "NewPlayerChoicesSO", menuName = "Scriptable Objects/PlayerChoicesSO")]
public class PlayerChoicesSO : ScriptableObject
{
    [SerializeField] private PlayerChoiceData _defaultPlayerChoicesData;
    [SerializeField] private PlayerChoiceData[] _playerChoicesData;
    public PlayerChoiceData[] PlayerChoicesData => _playerChoicesData;
    public PlayerChoiceData DefaultPlayerChoiceData => _defaultPlayerChoicesData;
}
