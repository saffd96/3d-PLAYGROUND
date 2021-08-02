using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    
    [SerializeField] private AudioSettings audioSettings;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private Text musicText;
    [SerializeField] private Text sfxText;

    [SerializeField] private AudioClip[] musics;
    
    
    private void Start()
    {
        LoadValues();
        bgmSource.loop = false;
    }

    private void Update()
    {
        if (!bgmSource.isPlaying)
        {
            bgmSource.clip = GetRandomMusic();
            bgmSource.Play();
        }
    }

    private AudioClip GetRandomMusic()
    {
        return musics[Random.Range(0, musics.Length)];
    }

    public void PLaySfx(SfxType sfxType, Transform transform = null)
    {
        var audioClip = GetAudioClip(sfxType);

        if (audioClip != null)
        {
            sfxSource.PlayOneShot(audioClip);
        }
    }
    
    

    public void MusicSlider(float volume)
    {
        musicText.text = volume.ToString("0.0");
        bgmSource.volume = volume;
    }

    public void SfxSlider(float volume)
    {
        sfxText.text = volume.ToString("0.0");
        sfxSource.volume = volume;
    }

    public void SaveVolumeSettings()
    {
        var musicValue = musicSlider.value;
        var sfxValue = sfxSlider.value;

        PlayerPrefs.SetFloat("MusicValue", musicValue);
        PlayerPrefs.SetFloat("SfxValue", sfxValue);

        LoadValues();
    }

    private void LoadValues()
    {
        var musicValue = PlayerPrefs.GetFloat("MusicValue");
        musicSlider.value = musicValue;

        var sfxValue = PlayerPrefs.GetFloat("SfxValue");
        sfxSlider.value = sfxValue;

        bgmSource.volume = musicValue;
        sfxSource.volume = sfxValue;
    }

    private AudioSource CreateAudioSource(Transform transform)
    {
        var transformForAudioSource = transform == null ? this.transform : transform;
        var audioSource = transformForAudioSource.gameObject.AddComponent<AudioSource>();

        audioSource.loop = false;
        audioSource.playOnAwake = true;

        return audioSource;
    }

    private AudioClip GetAudioClip(SfxType sfxType)
    {
        return audioSettings.GetAudioClip(sfxType);
    }
}
