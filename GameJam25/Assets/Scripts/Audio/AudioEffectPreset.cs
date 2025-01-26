using UnityEngine;

[CreateAssetMenu(fileName = "AudioEffectPreset", menuName = "Audio Management/Audio Effect Preset")]
public class AudioEffectPreset : ScriptableObject
{
    [Range(-20f, 10f)] public float gain = 0f;
    [Range(0.5f, 2f)] public float pitch = 1f;
    [Range(0.1f, 20f)] public float reverb = 0.1f;
    [Range(10f, 22000f)] public float lowpass = 22000f;
    [Range(10f, 22000f)] public float highpass = 10f;
    [Range(0f, 1f)] public float distortion = 0f;
    [Range(0f, 1f)] public float chorus = 0f;
}
