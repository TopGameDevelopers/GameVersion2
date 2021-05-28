using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public AudioMixerGroup audioMixerGroup;

    public void ChangeMusicVolume(float volume)
    {
        audioMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, volume));
    }

    public void ChangeSoundsVolume(float volume)
    {
        audioMixerGroup.audioMixer.SetFloat("SoundsVolume", Mathf.Lerp(-80, 0, volume));
    }
}
