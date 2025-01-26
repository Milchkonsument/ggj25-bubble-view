using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioEffectPreset normal;
    public List<AudioEffectPresetByBubbleType> effectPresets = new();
    public AudioMixerGroup mixerGroup;

    public AudioEffectPreset currentlyActivePreset = null;

    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindFirstObjectByType<AudioManager>();
            }

            return _instance;
        }
    }

    private void Awake ()
    {
        ApplyEffectPreset(normal);
    }

    public void SetMixerEffectsByBubbleType (BubbleObjectBase.BubbleType bubbleType)
    {
        currentlyActivePreset = FindPresetByType(bubbleType);
        ApplyEffectPreset(currentlyActivePreset);
    }

    public void ResetMixerEffects()
    {
        currentlyActivePreset = normal;
        ApplyEffectPreset(currentlyActivePreset);
    }

    private AudioEffectPreset FindPresetByType (BubbleObjectBase.BubbleType type) => effectPresets.FirstOrDefault(e => e.type == type).preset;

    private void ApplyEffectPreset(AudioEffectPreset preset)
    {
        mixerGroup.audioMixer.SetFloat("volume", preset.gain);
        mixerGroup.audioMixer.SetFloat("lowpass", preset.lowpass);
        mixerGroup.audioMixer.SetFloat("highpass", preset.highpass);
        mixerGroup.audioMixer.SetFloat("distortion", preset.distortion);
        mixerGroup.audioMixer.SetFloat("pitch", preset.pitch);
        mixerGroup.audioMixer.SetFloat("chorus", preset.chorus);
        mixerGroup.audioMixer.SetFloat("reverb", preset.reverb);
    }
}

[Serializable]
public class AudioEffectPresetByBubbleType
{
    public BubbleObjectBase.BubbleType type;
    public AudioEffectPreset preset;
}
