using System.Collections.Generic;
using UnityEngine;

public class MapChoiceBehaviour : MonoBehaviour
{
    private Sprite _playerSprite;
    private List<InfoTextData> _infoTextDatas;
    private string _titleText;
    private UiInfoPanelSetter _uiInfoPanelSetter;

    public void SetInfoPanelData(Sprite MapSprite, List<InfoTextData> infoTextDatas, string titleText, UiInfoPanelSetter uiInfoPanelSetter)
    {
        _playerSprite = MapSprite;
        _infoTextDatas = infoTextDatas;
        _titleText = titleText;
        _uiInfoPanelSetter = uiInfoPanelSetter;
    }
    public void HoverInfoSet()
    {
        _uiInfoPanelSetter.Initialize(_infoTextDatas, _titleText, _playerSprite);
    }
}
