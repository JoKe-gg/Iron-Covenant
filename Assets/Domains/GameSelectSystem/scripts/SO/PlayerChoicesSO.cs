using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class PlayerChoiceData
{
    public int PlayerId;
    public Sprite PlayerSprite;
    public Sprite WeaponSprite;
    public Sprite WeaponSpriteOnGame;
    public Sprite PlayerSpriteOnGame;
    public string PlayerName;
    public int Price;

    public PlayerBasicStatsSO basicStats;
    public WeaponStatsSO weaponStats;
}

[CreateAssetMenu(fileName = "NewPlayerChoicesSO", menuName = "Scriptable Objects/PlayerChoicesSO")]
public class PlayerChoicesSO : ScriptableObject
{
    [SerializeField] private PlayerChoiceData _defaultPlayerChoicesData;
    [SerializeField] private PlayerChoiceData[] _playerChoicesData;
    public PlayerChoiceData[] PlayerChoicesData => _playerChoicesData;
    public PlayerChoiceData DefaultPlayerChoiceData => _defaultPlayerChoicesData;
}
