using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
public class UITabSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> _tabs = new();
    [SerializeField] private UnityEvent _changeTabAction = null;
    private int _currentPageId;
    private GameObject _activePanel;
    private void Awake()
    {
        if (_tabs.Count == 0)
        {
            Debug.LogError($"Tabs are not assigned");
            Destroy(this);
            return;
        }
        _currentPageId = 0;
        DeactivateTabs();
    }
    private void OnEnable()
    {
        OpenCurrentPanel();
    }
    public void DeactivateTabs()
    {
        foreach (var tab in _tabs)
        {
            tab.SetActive(false);
        }
        _activePanel = null;
    }

    public void OpenNextPage()
    {
        if (_currentPageId < _tabs.Count - 1)
        {
            _currentPageId ++;
        }
        else
        {
            _currentPageId = 0;
        }
        OpenCurrentPanel();
    }
    public void OpenPreviousPage()
    {
        if (_currentPageId > 0)
        {
            _currentPageId -- ;
        }
        else
        {
            _currentPageId = _tabs.Count-1;
        }
        OpenCurrentPanel();
    }
    public void OpenPanelById(int id)
    {
        _currentPageId = id;
        OpenCurrentPanel();
    }
    private void OpenCurrentPanel()
    {
        if (_activePanel != null){
            _activePanel.SetActive(false);
            _changeTabAction?.Invoke();
        }
        _activePanel = _tabs[_currentPageId];
        _activePanel.SetActive(true);
        if (_activePanel.TryGetComponent(out Tab tab)) 
        {
            tab.Initialize();
        }
    }
}
