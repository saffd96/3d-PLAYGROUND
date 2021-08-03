using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioSettings audioSettings;
    
    private AudioClip currentClip;

    public float MusicVolume => bgmSource.volume;
    public float SfxVolume => sfxSource.volume;

    private void Start()
    {
        LoadValues();
        bgmSource.loop = false;
    }

    private void Update()
    {
        if (bgmSource.isPlaying) return;

        do
        {
            currentClip = audioSettings.GetRandomMusic();
        }
        while (currentClip == bgmSource.clip);

        bgmSource.clip = currentClip;
        bgmSource.Play();
    }

    public void PLaySfx(SfxType sfxType, Transform transform = null)
    {
        var audioClip = GetAudioClip(sfxType);

        if (audioClip != null)
        {
            sfxSource.PlayOneShot(audioClip);
        }
    }

    public void SetMusicVolume(float volume)
    {
        bgmSource.volume = volume;
        PlayerPrefs.SetFloat(PrefsConstants.MusicPrefsKey, volume);
    }

    public void SetSfxVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat(PrefsConstants.SfxPrefsKey, volume);
    }
    
    private void LoadValues()
    {
        var musicValue = PlayerPrefs.GetFloat(PrefsConstants.MusicPrefsKey);
        bgmSource.volume = musicValue;

        var sfxValue = PlayerPrefs.GetFloat(PrefsConstants.SfxPrefsKey);
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
