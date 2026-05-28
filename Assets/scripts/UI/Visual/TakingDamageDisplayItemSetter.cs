using TMPro;
using UnityEngine;
public class TakingDamageDisplayItemSetter : MonoBehaviour, IPoolable
{
    [Header("Required components")]
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private RectTransform _canvasRectTransform;
    private UnityEngine.UI.Image _image;
    private RectTransform _rectTransform;
    [Header("Movement")]
    [SerializeField] private float _frequency = 0.1f;
    [SerializeField] private float _amplitude = 0.1f;
    [SerializeField] private float _stepX = 0.1f;
    private float _startX = 0;
    [SerializeField] private float _stepY = 0.1f;
    [Header("pool")]
    private DamageDisplayPool _pool;
    private bool _isInitialized = false;
    private Vector2 _startScale = Vector2.one;
    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<UnityEngine.UI.Image>();
    }
    public void Initialize(RectTransform canvasRectTransform, int amountOfDamage, DamageDisplayPool pool, Color panelColor)
    {
        if (_isInitialized == true)
            return;
        _isInitialized = true;
        transform.SetParent(canvasRectTransform.transform, false);
        _damageText.text = $" - {amountOfDamage.ToString()}";
        _canvasRectTransform = canvasRectTransform;
        _rectTransform.anchoredPosition = new Vector3(0, -_canvasRectTransform.rect.height / 2, 0);
        _startX = 0f;
        _pool = pool;
        _image.color = new Color32(0, 0, 0, 0);
        _damageText.color = panelColor;
    }
    public void Initialize(RectTransform canvasRectTransform, int amountOfDamage, DamageDisplayPool pool, Color panelColor, Vector2 scale)
    {
        Initialize(canvasRectTransform, amountOfDamage, pool, panelColor);
        if (scale != Vector2.zero && scale.x > 0 && scale.y > 0)
        {
            _rectTransform.localScale = scale;

        }
    }
    private void FixedUpdate()
    {
        if (_isInitialized != true)
        {
            return;
        }
        if (_rectTransform.anchoredPosition.y <= _canvasRectTransform.rect.height - _stepY*1.5)
        {
            _startX += _stepX;
            float y = _rectTransform.anchoredPosition.y + _stepY;
            float x = Mathf.Cos((_startX + 1) * _frequency) * _amplitude;
            _rectTransform.anchoredPosition = new Vector3(x, y, 0);
        }
        else 
        {
            if (_pool != null)
            {
                    _pool.ReturnToPool(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    public void OnSpawnFromPool()
    {
        return;
    }
    public void OnReturnToPool()
    {
        ResetDisplayDamage();
    }
    public void ResetDisplayDamage()
    {
        _isInitialized = false;

        _startX = 0f;

        _rectTransform.localScale = Vector2.one;

        _damageText.text = string.Empty;
        _damageText.color = Color.white;

        _canvasRectTransform = null;

        _pool = null;
    }
}
