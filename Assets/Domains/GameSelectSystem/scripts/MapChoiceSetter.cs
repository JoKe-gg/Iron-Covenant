using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MapChoiceSetter : MonoBehaviour
{
    [SerializeField] private Image _mapSprite;
    [SerializeField] private TextMeshProUGUI _mapName;
    private int _mapIndex;
    public void SetChoice(Sprite MapSprite, int MapIndex, string MapName)
    {
        _mapSprite.sprite = MapSprite;
        _mapName.text = MapName;
        _mapIndex = MapIndex;
    }
    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_mapIndex);
    }
}
