using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class EffectForButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TrackButton _trackButton;
    [SerializeField] private Vector2 _scaleToChange;
    [SerializeField] private float _speedOfChangingScale = 1.0f;
    private RectTransform _rectTransform;
    private Vector2 _currentRequiredScale = new Vector2(1, 1);
    private bool ToChange = false;
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        if (ToChange)
        {
            if (Vector2.Distance(_rectTransform.localScale, _currentRequiredScale) <= 0.1f)
            {
                _rectTransform.localScale = _currentRequiredScale;
                ToChange = false;
            }
            else
            {
                _rectTransform.localScale = Vector2.MoveTowards(_rectTransform.localScale, _currentRequiredScale, _speedOfChangingScale);
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _trackButton.SetButtonToTrack(gameObject);
        ToChange = true;
        _currentRequiredScale = _scaleToChange;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _currentRequiredScale = new Vector2(1, 1);
        ToChange = true ;
    }
}
