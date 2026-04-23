using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class SoundManager : MonoSingleton<SoundManager>
{
    private List<AudioSource> _sources;
    [SerializeField] private int spawnAudioSourceCount = 5;
    [SerializeField] private int maxSourceCount = 5;

    [SerializeField] AudioMixerGroup mixerGroup;
    private GameObject sfxPlayer;

    private void Awake()
    {
        _sources = new List<AudioSource>();

        sfxPlayer = new GameObject() { name = "SFX Player" };
        sfxPlayer.transform.SetParent(transform);

        for (int i = 0; i < spawnAudioSourceCount; i++)
        {
            var item = sfxPlayer.AddComponent<AudioSource>();
            item.playOnAwake = false;
            item.loop = false;
            item.volume = 0.5f;
            item.outputAudioMixerGroup = mixerGroup;
            _sources.Add(item);
        }
    }

    public SoundSO PlaySFX(SoundSO soundData)
    {
        if (soundData.clip == null)
        {
            Debug.LogError("SoundData does not have a valid AudioClip.");
            return null;
        }

        if (TryGetValue(_sources, out AudioSource source))
        {
            source.clip = soundData.clip;
            source.volume = soundData.volume; // 이름 수정
            source.pitch = soundData.pitch;   // 피치 설정
            source.Play();
        }
        else if (_sources.Count < maxSourceCount) // 최대 개수 제한
        {
            var newSource = sfxPlayer.AddComponent<AudioSource>();
            newSource.playOnAwake = false;
            newSource.loop = false;
            newSource.outputAudioMixerGroup = mixerGroup;
            _sources.Add(newSource);

            newSource.clip = soundData.clip;
            newSource.volume = soundData.volume; // 이름 수정
            newSource.pitch = soundData.pitch;   // 피치 설정
            newSource.Play();
        }
        else
        {
            Debug.LogWarning("No available AudioSource and maximum limit reached.");
        }

        return soundData;
    }

    private static bool TryGetValue(List<AudioSource> list, out AudioSource value)
    {
        foreach (var item in list)
        {
            if (item.isPlaying) continue;

            value = item;
            return true;
        }

        value = null;
        return false;
    }
}