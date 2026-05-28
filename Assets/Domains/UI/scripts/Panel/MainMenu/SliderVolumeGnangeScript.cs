using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderVolumeGnangeScript : MonoBehaviour
{
    private AudioMixer _audioMixer;
    private string _exposedParameter;
    
    private Slider _slider;
    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }
    public void Initialize(AudioMixer audioMixer,  float value, string exposedParameter)
    {
        _exposedParameter = exposedParameter;
        _audioMixer = audioMixer;
        _slider.value = value;
    }
    public void OnSliderChange() 
    {
        float volumeDb = (_slider.value * 100) - 80;
        _audioMixer.SetFloat(_exposedParameter, volumeDb);
    }
}
