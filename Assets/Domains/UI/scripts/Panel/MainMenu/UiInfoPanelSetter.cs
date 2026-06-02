using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class InfoTextData
{
    public string Text { get; private set; }
    public TextType Type { get; private set; }
    public TextAlignmentOptions Align { get; private set; }

    public InfoTextData(string text, TextType type = TextType.Paragraph, TextAlignmentOptions align = TextAlignmentOptions.MidlineLeft)
    {
        Text = text;
        Type = type;
        Align = align;
    }
}
public enum TextType
{
    Title1,
    Title2,
    Paragraph,
}

public class UiInfoPanelSetter : MonoBehaviour
{
    [Header("UI refs")]
    [SerializeField] private RectTransform _descriptionTransform;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _stubImage;
    [Header("Prefab description item")]
    [SerializeField] private TextMeshProUGUI _textPrefab;

    private Dictionary<TextType, Func<InfoTextData, Transform, TextMeshProUGUI>> _factory;

    private void Awake()
    {
        InitializeFactory();
    }
    private void Start()
    {
        List<InfoTextData> infoTextDatas = new List<InfoTextData>()
        {
            {new("Title", TextType.Title1, TextAlignmentOptions.Center) },
            {new("Descroption", TextType.Paragraph)},
        };
        Initialize(infoTextDatas, "Type", _stubImage);
    }
    private void InitializeFactory()
    {
        _factory = new()
        {
            {TextType.Title1, (text, transform) => SetText(text, 45, transform, FontStyles.Bold)},
            {TextType.Title2, (text, transform) => SetText(text, 38, transform, FontStyles.Normal)},
            {TextType.Paragraph, (text, transform) => SetText(text, 30, transform, FontStyles.Normal)},
        };
    }
    private TextMeshProUGUI SetText(InfoTextData infoTextData, float fontSize, Transform transform, FontStyles fontStyle)
    {
        TextMeshProUGUI text = Instantiate(_textPrefab, transform, false);
        text.text = infoTextData.Text;
        text.fontSize = fontSize;
        text.fontStyle = fontStyle;
        text.alignment = infoTextData.Align;
        return text;
    }
    private void ClearUI()
    {
        foreach (Transform child in _descriptionTransform)
        {
            Destroy(child.gameObject);
        }
    }
    public void Initialize(List<InfoTextData> infoTextData, string titleText, Sprite sprite)
    {
        ClearUI();
        _image.sprite = sprite;
        if (_factory != null)
        {
            foreach (var info in infoTextData)
            {
                if (info != null)
                {
                    TextMeshProUGUI text = _factory[info.Type](info, _descriptionTransform);
                    RectTransform rect = text.GetComponent<RectTransform>();
                }
            }
        }
        if (_titleText != null)
        {
            _titleText.text = titleText;
        }
    }
}
