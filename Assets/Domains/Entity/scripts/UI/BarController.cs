using System;
using UnityEngine;
using TMPro;
public class BarController : MonoBehaviour
{
    [SerializeField] private GameObject _barToChange;
    [SerializeField] private GameObject _barHolder;
    [SerializeField] private TextMeshProUGUI _barText;
    private float _barToChangeLimitWidth;
    private bool _isText;
    private void Start()
    {
        bool error = false;
        if (_barToChange == null)
        {
            Debug.LogError($"Null Reference to {nameof(_barToChange)} in the script {nameof(BarController)}");
            enabled = false;
            error = true;
        }
        if (_barHolder == null)
        {
            Debug.LogError($"Null Reference to {nameof(_barHolder)} in the script {nameof(BarController)}");
            enabled = false;
            error = true;
        }
        if (error) 
        {
            return;
        }
        _isText = _barText != null;
        RectTransform rect = _barHolder.GetComponent<RectTransform>();
        _barToChangeLimitWidth = rect.sizeDelta.x;
    }

    public void ChangeBar(int value, int max)
    {
        float ratio = (float)value / max;
        float rightOffset = _barToChangeLimitWidth - _barToChangeLimitWidth*ratio;
        RectTransform rect = _barToChange.GetComponent<RectTransform>();
        Vector2 offsetMax = rect.offsetMax;
        offsetMax.x = -rightOffset;
        rect.offsetMax = offsetMax;
        if (_isText)
        {
            ChangeBarText($"{value}/{max}");
        }
    }
    private void ChangeBarText(string barText)
    {
        _barText.text = barText;
    }
}
