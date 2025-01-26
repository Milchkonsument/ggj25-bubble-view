using UnityEngine;

[CreateAssetMenu(fileName = "AudioEffectPreset", menuName = "Audio Management/Audio Effect Preset")]
public class AudioEffectPreset : ScriptableObject
{
    [Range(0f, 10f)] public float gain = 3.3f;
    [Range(0.5f, 2f)] public float pitch = 1f;
    [Range(0f, 1f)] public float reverb = 1f;
    [Range(10f, 22000f)] public float lowpass = 22000f;
    [Range(10f, 22000f)] public float highpass = 10f;
    [Range(0f, 1f)] public float distortion = 0f;
    [Range(0f, 1f)] public float chorus = 0f;
}
