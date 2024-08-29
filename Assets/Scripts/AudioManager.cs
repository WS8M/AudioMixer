using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private const float MinVolumeValue = 0.0001f;
    
    private const string MasterVolume = nameof(MasterVolume);
    private const string EffectsVolume = nameof(EffectsVolume);
    private const string BackgroundVolume = nameof(BackgroundVolume);
    
    [SerializeField] private AudioMixerGroup _mixer;
    [SerializeField] private VolumeSlider _masterSlider;
    [SerializeField] private VolumeSlider _effectsSlider;
    [SerializeField] private VolumeSlider _backgroundSlider;
    
    private bool _isEnabled;
    private float _currentTotalVolume;

    private void OnEnable()
    {
        _masterSlider.VolumeChanged += SetTotalVolume;
        _effectsSlider.VolumeChanged += SetEffectsVolume;
        _backgroundSlider.VolumeChanged += SetBackgroundVolume;
    }

    private void OnDisable()
    {
        _masterSlider.VolumeChanged -= SetTotalVolume;
        _effectsSlider.VolumeChanged -= SetEffectsVolume;
        _backgroundSlider.VolumeChanged -= SetBackgroundVolume;
    }

    private void Awake()
    {
        _isEnabled = true;
        _currentTotalVolume = 1;
        
        SetTotalVolume(1);
        SetBackgroundVolume(1);
        SetEffectsVolume(1);
    }
    
    public void SetMute()
    {
        if(_isEnabled)
            TurnOffAudio();
        else
            TurnOnAudion();
    }
    
    private void SetTotalVolume(float value)
    {
        _currentTotalVolume = value;
        
        if(_isEnabled)
            SetVolume(MasterVolume, value);
    } 

    private void SetEffectsVolume(float value) => 
        SetVolume(EffectsVolume, value);
    
    private void SetBackgroundVolume(float value) => 
        SetVolume(BackgroundVolume, value);

    private void SetVolume(string parameterName, float value)
    {
        value = value < MinVolumeValue ? MinVolumeValue : value;
        value = Mathf.Log10(value) * 20;
        _mixer.audioMixer.SetFloat(parameterName, value);
    }

    private void TurnOnAudion()
    {
        _isEnabled = true;
        SetTotalVolume(_currentTotalVolume);
    }
    
    private void TurnOffAudio()
    {
        SetVolume(MasterVolume,0);
        _isEnabled = false;
    }
}