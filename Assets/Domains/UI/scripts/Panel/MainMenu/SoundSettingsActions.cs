using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;

[Serializable]
public class SoundSliderData
{
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private string _exposedParameter;
    public Slider SoundSlider => _soundSlider;
    public string ExposedParameter => _exposedParameter;
}
public class SoundSettingsActions : Savable
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private List<SoundSliderData> _sliders = new();
    private const float _defaultVolumeDB = 0f;
    protected override void Awake()
    {
        base.Awake();
        bool error = false;
        if (_sliders.Count == 0)
        {
            error = true;
            Debug.LogWarning($"Sliders are not assigned in the script {nameof(_sliders)}");
        }
        if (error)
        {
            Destroy(gameObject);
            return;
        }
    }
    public void SetAudionSettings()
    {
        SaveManager.LoadGame();
    }
    public override void Load(DataSave saveData)
    {
        foreach (var slider in _sliders)
        {
            bool isSaved = false;
            foreach (var audioMixerGroup in saveData.AudioMixerGroupList)
            {
                if (slider.ExposedParameter == audioMixerGroup.Name)
                {
                    isSaved = true;
                    SetSlider(audioMixerGroup.Volume, slider);
                    _audioMixer.SetFloat(slider.ExposedParameter, audioMixerGroup.Volume);
                    break;
                }
            }

            if (!isSaved)
            {
                SetSlider(_defaultVolumeDB, slider);
                _audioMixer.SetFloat(slider.ExposedParameter, _defaultVolumeDB);
            }
        }
    }
    public override void Save(DataSave saveData)
    {
        List<AudioMixerGroupSaveData> audioMixerGroupSaveDatas = new();

        foreach (var slider in _sliders)
        {
            if (slider != null 
                && slider.SoundSlider != null 
                && slider.ExposedParameter != null
                && slider.ExposedParameter != string.Empty)
            {
                _audioMixer.GetFloat(slider.ExposedParameter, out float valueDB);
                AudioMixerGroupSaveData audioMixerGroupSaveData = new(slider.ExposedParameter, valueDB);
                audioMixerGroupSaveDatas.Add(audioMixerGroupSaveData);
            }
        }
        saveData.SetAudioMixerGroupsValue(audioMixerGroupSaveDatas);
    }
    private void SetSlider(float valueDB, SoundSliderData slider)
    {
        if (slider != null){
            float value = (valueDB + 80) / 100;
            if (slider.ExposedParameter != null && slider.SoundSlider.isActiveAndEnabled)
            slider.SoundSlider.GetComponent<SliderVolumeGnangeScript>().Initialize(_audioMixer, value, slider.ExposedParameter);
        }
    }
}
