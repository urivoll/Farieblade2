using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Sound : MonoBehaviour
{
    public static AudioMixerGroup soundMixer;
    public static AudioMixerGroup musicMixer;
    public static AudioMixerGroup ambMixer;
    public static AudioMixerGroup voiceMixer;
    [SerializeField] private AudioMixerGroup soundMixerPrefub;
    [SerializeField] private AudioMixerGroup musicMixerPrefub;
    [SerializeField] private AudioMixerGroup ambMixerPrefub;
    [SerializeField] private AudioMixerGroup voiceMixerPrefub;
    public static float soundLevel;
    public static float musicLevel;
    public static float voiceLevel;
    public static float ambLevel;

    public static AudioSource voice;
    public static AudioSource sound;
    public static AudioSource amb;
    [SerializeField] private AudioSource soundPrefab;
    [SerializeField] private AudioSource voicePrefab;
    [SerializeField] private AudioSource ambPrefab;
    private void Awake()
    {
        voice = voicePrefab;
        sound = soundPrefab;
        amb = ambPrefab;
        soundMixer = soundMixerPrefub;
        musicMixer = musicMixerPrefub;
        voiceMixer = voiceMixerPrefub;
        ambMixer = ambMixerPrefub;
    }
    private void Start()
    {
        soundMixer.audioMixer.SetFloat("soundLevel", Mathf.Lerp(-40, 0, soundLevel));
        musicMixer.audioMixer.SetFloat("musicLevel", Mathf.Lerp(-40, 0, musicLevel));
        voiceMixer.audioMixer.SetFloat("voiceLevel", Mathf.Lerp(-40, 0, voiceLevel));
        ambMixer.audioMixer.SetFloat("ambLevel", Mathf.Lerp(-40, 0, ambLevel));
    }
}
