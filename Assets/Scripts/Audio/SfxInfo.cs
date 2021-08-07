using System;
using UnityEngine;

[Serializable]
public class SfxInfo
{
    [HideInInspector]
    [SerializeField] private string name;

    public AudioClip clip;
    public SfxType sfxType;

    [Range(0, 1)]
    public float Volume =  1f;

    public void OnValidate()
    {
        name = sfxType.ToString();
    }
}
