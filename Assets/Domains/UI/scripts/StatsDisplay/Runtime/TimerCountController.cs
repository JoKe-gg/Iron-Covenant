using UnityEngine;

public class TimerCountController : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _timerCounterText;
    [SerializeField] private string PlaceHolderText = "1:00";

    private void Start()
    {
        if (_timerCounterText == null)
        {
            Debug.LogError($"Null reference to {nameof(_timerCounterText)} int the script {nameof(TimerCountController)}");
            Destroy(this);
            return;
        }
        UpdateTimerCounter();
    }
    private void LateUpdate()
    {
        UpdateTimerCounter();
    }
    private void UpdateTimerCounter()
    {
        float timer = GameTimer.Instance.ElapsedTime;
        int seconds = Mathf.RoundToInt(timer % 60);
        int minutes = (int)(timer / 60);
        string textOfTimerCounter = PlaceHolderText;
        if (minutes > 0)
        {
            textOfTimerCounter = $"{minutes}:";
        }
        else
        {
            textOfTimerCounter = string.Empty;
        }
        if (seconds < 10)
        {
            textOfTimerCounter += $"0{seconds}";
        }
        else
        {
            textOfTimerCounter += seconds;
        }
        _timerCounterText.text = textOfTimerCounter;
    }
}
