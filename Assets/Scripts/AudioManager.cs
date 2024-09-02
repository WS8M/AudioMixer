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
        _masterSlider.Changed += SetVolume;
        _effectsSlider.Changed += SetVolume;
        _backgroundSlider.Changed += SetVolume;
    }

    private void OnDisable()
    {
        _masterSlider.Changed -= SetVolume;
        _effectsSlider.Changed -= SetVolume;
        _backgroundSlider.Changed -= SetVolume;
    }

    private void Start()
    {
        _isEnabled = true;
        _currentTotalVolume = 1;
        
        _masterSlider.Init(MasterVolume);
        _effectsSlider.Init(EffectsVolume);
        _backgroundSlider.Init(BackgroundVolume);
    }
    
    public void SetMute()
    {
        if(_isEnabled)
            TurnOffAudio();
        else
            TurnOnAudion();
    }

    private void SetVolume(string parameterName, float value)
    {
        if (parameterName == MasterVolume)
        {
            _currentTotalVolume = value;
            if (_isEnabled == false)
                return;
        }
        
        value = value < MinVolumeValue ? MinVolumeValue : value;
        value = Mathf.Log10(value) * 20;
        _mixer.audioMixer.SetFloat(parameterName, value);
    }

    private void TurnOnAudion()
    {
        _isEnabled = true;
        SetVolume(MasterVolume, _currentTotalVolume);
    }
    
    private void TurnOffAudio()
    {
        SetVolume(MasterVolume,0);
        _isEnabled = false;
    }
}