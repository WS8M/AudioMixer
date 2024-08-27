using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private const string MasterVolume = nameof(MasterVolume);
    private const string EffectsVolume = nameof(EffectsVolume);
    private const string BackgroundVolume = nameof(BackgroundVolume);
    
    private const float MinVolumeValue = 0.0001f;
    
    [SerializeField] private AudioMixerGroup _mixer;

    private bool _isEnabled;
    private float _currentTotalVolume;

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
    
    public void SetTotalVolume(float value)
    {
        _currentTotalVolume = value;
        
        if(_isEnabled)
            SetVolume(MasterVolume, value);
    } 

    public void SetEffectsVolume(float value) => 
        SetVolume(EffectsVolume, value);
    
    public void SetBackgroundVolume(float value) => 
        SetVolume(BackgroundVolume, value);
    
    private void SetVolume(string parameterName,float value)
    {
        value = value < MinVolumeValue ? MinVolumeValue : value;
        _mixer.audioMixer.SetFloat(parameterName, Mathf.Log10(value) * 20);
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