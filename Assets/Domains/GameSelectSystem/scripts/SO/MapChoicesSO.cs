using UnityEngine;
[System.Serializable]
public class MapChoiceData
{
    public int MapId;
    public Sprite MapSprite;
    public string MapName;
}
[CreateAssetMenu(fileName = "MapChoicesSO", menuName = "Scriptable Objects/MapChoicesSO")]
public class MapChoicesSO : ScriptableObject
{
    [SerializeField] private MapChoiceData _defaultChoice;
    [SerializeField] private MapChoiceData[] _mapChoicesData;

    private void OnValidate()
    {
        foreach(var mapChoiceData in _mapChoicesData)
        {
            if(mapChoiceData.MapId <= 0)
            {
                mapChoiceData.MapId = 1;
            }
        }
    }
    public MapChoiceData DefaultChoice => _defaultChoice;
    public MapChoiceData[] MapChoicesData => _mapChoicesData;
}

