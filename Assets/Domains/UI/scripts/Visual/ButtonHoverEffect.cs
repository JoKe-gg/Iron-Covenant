using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Hover effect data")]
    [SerializeField] private Vector2 _scaleToChange;
    [SerializeField] private float _speedOfChangingScale = 1f;
    private RectTransform _rectTransform;
    private Vector3 _startScale;
    private Vector3 _currentRequiredScale;
    bool _toChange = false;
    bool _isAbleToHover = true;
    private void Awake()
    {
        if (!TryGetComponent(out RectTransform rectTransform))
        {
            
        }
        else
        {
            _rectTransform = rectTransform;
        }
        _startScale = _rectTransform.localScale;
    }
    private void Update()
    {
        if (_toChange && _isAbleToHover)
        {
            if (Vector2.Distance(_rectTransform.localScale, _currentRequiredScale) <= 0.1f)
            {
                _rectTransform.localScale = _currentRequiredScale;
                _toChange = false;
            }
            else
            {
                _rectTransform.localScale = Vector2.MoveTowards(_rectTransform.localScale, _currentRequiredScale, _speedOfChangingScale);
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _toChange = true;
        _currentRequiredScale = _scaleToChange;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _toChange = true;
        _currentRequiredScale = _startScale;
    }
    public void SetEnableHover(bool enable)
    {
        _isAbleToHover = enable;
    }
}
