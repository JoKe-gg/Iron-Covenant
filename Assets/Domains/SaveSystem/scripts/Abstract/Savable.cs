using System.Collections;
using UnityEngine;

public abstract class Savable : MonoBehaviour
{
    [SerializeField] private SaveManagerSO _saveManagerSO;
    public SaveManagerSO SaveManager => _saveManagerSO;
    public abstract void Load(DataSave saveData);
    public abstract void Save(DataSave saveData);
    protected virtual void Awake()
    {
        if (_saveManagerSO == null)
        {
            Debug.LogError($"Null reference to Save manager ", this);
            Destroy(gameObject);
            return;
        }
        _saveManagerSO.Register(this);
    }
    protected virtual void OnEnable()
    {
        _saveManagerSO.Register(this);
    }
    protected virtual void OnDisable()
    {
        _saveManagerSO.Unregister(this);
    }
}
