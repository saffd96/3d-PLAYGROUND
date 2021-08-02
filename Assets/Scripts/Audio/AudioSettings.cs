using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = nameof(AudioSettings), menuName = "Audio/AudioSettings")]
public class AudioSettings : ScriptableObject
{
    [Serializable]
    private class SfxInfo
    {
        [HideInInspector]
        [SerializeField] private string name;

        public AudioClip Clip;
        public SfxType SfxType;

        public void OnValidate()
        {
            name = SfxType.ToString();
        }
    }

    private const string Tag = nameof(AudioSettings);

    [SerializeField] private SfxInfo[] sfx;

    private readonly Dictionary<SfxType, SfxInfo> sfxMap = new Dictionary<SfxType, SfxInfo>();

    private void OnEnable()
    {
        FillMap();
    }

    private void OnValidate()
    {
        if (sfx == null) return;

        foreach (var sfxInfo in sfx)
        {
            sfxInfo.OnValidate();
        }
    }

    public AudioClip GetAudioClip(SfxType sfxType)
    {
        return sfxMap.ContainsKey(sfxType) ? sfxMap[sfxType].Clip : null;
    }

    private void FillMap()
    {
        sfxMap.Clear();

        if (sfx == null) return;

        foreach (var sfxInfo in sfx)
        {
            var type = sfxInfo.SfxType;

            if (!sfxMap.ContainsKey(type))
            {
                sfxMap.Add(type,sfxInfo);
            }
            else
            {
                Debug.LogError($"{Tag}, {nameof(FillMap)}: Cannot be more than 1 clip for type '{type}'!");
            }
        }
    }
}
