using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "SaveManagerSO", menuName = "Scriptable Objects/SaveManagerSO")]
public class SaveManagerSO : ScriptableObject
{
    private HashSet<Savable> _savables = new();
    public event Action OnGameSaved;
    string _path;
    public void Register(Savable savable)
    {
        if (!_savables.Contains(savable))
        {
            _savables.Add(savable);
        }
    }
    public void Unregister(Savable savable)
    {
        if (_savables.Contains(savable))
        {
            _savables.Remove(savable);
        }
    }
    public void SaveGame()
    {
        _path = Application.persistentDataPath + "/save.json";
        DataSave saveData = new DataSave();

        if (File.Exists(_path))
        {
            string savedJson = File.ReadAllText(_path);
            saveData = JsonUtility.FromJson<DataSave>(savedJson);
        }

        _savables.RemoveWhere(s => s == null);
        foreach (var savable in new HashSet<Savable>(_savables)) // iterate a snapshot
        {
            if (savable != null)
                savable.Save(saveData);
        }

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(_path, json);
        OnGameSaved?.Invoke();
    }
    public void LoadGame()
    {
        _path = Application.persistentDataPath + "/save.json";
        DataSave saveData = new DataSave();
        if (File.Exists(_path))
        {
            string json = File.ReadAllText(_path);

            saveData = JsonUtility.FromJson<DataSave>(json);
        }
        foreach (var savable in _savables)
        {
            savable.Load(saveData);
        }
    }
}
