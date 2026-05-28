using UnityEngine;

public class TrackButton : MonoBehaviour
{
    [SerializeField] private GameObject _firstButton;
    [SerializeField] private float _moveSpeed;
    private GameObject _buttonToTrack;
    private RectTransform _rectTransform;
    private float _xOffset = 0;
    private void Awake()
    {
        if (_firstButton == null) 
        {
            Debug.Log("First button is not assigned", this);
            Destroy(gameObject);
            return;
        }
        else
        {
            _buttonToTrack = _firstButton;
        }
        _rectTransform = GetComponent<RectTransform>();
        _xOffset = _firstButton.transform.localPosition.x - _rectTransform.position.x;
    }
    private void Update()
    {
        if (_buttonToTrack != null)
        {
            Vector2 target = new Vector2(_rectTransform.position.x, _buttonToTrack.transform.position.y);
            _rectTransform.position = Vector2.MoveTowards(_rectTransform.position, target, _moveSpeed); 
        }
    }

    public void SetButtonToTrack(GameObject ButtonToTrack)
    {
        _buttonToTrack = ButtonToTrack;
    }
}
