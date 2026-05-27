using UnityEngine;

[CreateAssetMenu(fileName = "BasicStatsEnemySO", menuName = "Scriptable Objects/NewBasicStatsEnemySO")]
public class BasicStatsEnemySO : ScriptableObject
{
    [SerializeField] private string _name;
    public new string name { get { return _name; } }
    public BasicStatsEnemy basicStats;
    private void OnValidate()
    {
        basicStats.OnValidate();
    }
}
