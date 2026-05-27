using System;
using UnityEngine;

[Serializable]
public class PlayerBasicStatsData
{
    [SerializeField] private int _hP;
    [SerializeField] private int _xP;
    [SerializeField] private int _resistance;
    public int HP => _hP;
    public int XP => _xP;
    public int Resistance => _resistance;
}
