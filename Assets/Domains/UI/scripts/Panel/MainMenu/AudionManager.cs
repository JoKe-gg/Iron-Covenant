using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Savable
{
    [SerializeField] private AudioMixer _audioMixer;
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        SaveManager.LoadGame();
    }
    public override void Load(DataSave dataSave)
    {
        foreach (var audio in dataSave.AudioMixerGroupList)
        {
            _audioMixer.SetFloat(audio.Name, audio.Volume);
        }
    }
    public override void Save(DataSave dataSave)
    {
        return;
    }
}
