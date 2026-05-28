using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChoiceSetter : MonoBehaviour
{
    [SerializeField] private Image _weaponSprite;
    [SerializeField] private Image _playerSprite;
    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private GameObject _purchasePanel;
    private int _playerIndex;
    public void SetChoice(Sprite weaponSprite, Sprite playerSprite, int playerIndex, string PlayerName)
    {
        _weaponSprite.sprite = weaponSprite;
        _playerSprite.sprite = playerSprite;
        _playerName.text = PlayerName;
        _playerIndex = playerIndex;
    }
    public void ChoosePlayer()
    {
        CurrentArenaHolderManager.Instance.SetCurrentPlayerID(_playerIndex);
        MainMenuActions canvasGlobalActions = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<MainMenuActions>();
        canvasGlobalActions.OpenMapSelectPanel();
        canvasGlobalActions.ClosePlayerSelectPanel();
    }
}
