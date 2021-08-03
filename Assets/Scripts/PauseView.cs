using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PauseView : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration;

    [Header("Settings")]
    [SerializeField] private float volumeMultiplier = 100f;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private Text musicText;
    [SerializeField] private Text sfxText;

    private Tweener tweenAnimation;

    private void Start()
    {
        canvasGroup.alpha = 0;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        var musicValue = musicSlider.value / volumeMultiplier;
        AudioManager.Instance.SetMusicVolume(musicValue);
        musicText.text = $"{GetMusicVolume():0.0}";

        var sfxValue = sfxSlider.value / volumeMultiplier;
        AudioManager.Instance.SetSfxVolume(sfxValue);
        sfxText.text = $"{GetSfxVolume():0.0}";
    }

    public void Show()
    {
        SetVolume();

        gameObject.SetActive(true);
        tweenAnimation?.Kill();
        canvasGroup.DOFade(1, fadeDuration).SetUpdate(true);
    }

    public void Hide()
    {
        tweenAnimation?.Kill();
        canvasGroup.DOFade(0, fadeDuration).SetUpdate(true).OnComplete(() => gameObject.SetActive(true));
    }

    private float GetSfxVolume()
    {
        return AudioManager.Instance.SfxVolume * volumeMultiplier;
    }

    private float GetMusicVolume()
    {
        return AudioManager.Instance.MusicVolume * volumeMultiplier;
    }

    private void SetVolume()
    {
        musicSlider.minValue = 0;
        musicSlider.maxValue = volumeMultiplier;
        musicSlider.value = GetMusicVolume();

        sfxSlider.minValue = 0;
        sfxSlider.maxValue = volumeMultiplier;
        sfxSlider.value = GetSfxVolume();
    }
}
