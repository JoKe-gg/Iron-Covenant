using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicBaseSO", menuName = "Scriptable Objects/MusicBaseSO")]
public class MusicBase : ScriptableObject
{
    [SerializeField] private List<MusicData> musicData;
    public List<MusicData> MusicData { get { return musicData; } }
}
[Serializable]
public class MusicData
{
    [SerializeField] private int _id;
    [SerializeField] private AudioClip _clip;
    public int Id => _id;
    public AudioClip Clip => _clip;
}