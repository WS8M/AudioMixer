using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    private Slider _slider;
    private string _mixerParameterName;
    public event Action<string,float> Changed;

    public void Init(string mixerParameterName, float volume = 1)
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(OnValueChanged);
        
        if (String.IsNullOrWhiteSpace(mixerParameterName))
            throw new NullReferenceException();

        _mixerParameterName = mixerParameterName;
        _slider.value = volume;
    }
    
    private void OnDisable() => 
        _slider.onValueChanged.RemoveListener(OnValueChanged);
    
    private void OnValueChanged(float value)
    {
        Changed?.Invoke(_mixerParameterName, value);
    }
}