using UnityEngine;

public class OperateCanvasElements : MonoBehaviour
{
    [SerializeField] private GameObject[] _panelsToDisableOnStart;

    private void Start()
    {
        if (_panelsToDisableOnStart.Length > 0)
        {
            foreach(GameObject panel in _panelsToDisableOnStart)
            {
                panel.SetActive(false);
            }
        }
    }
}
