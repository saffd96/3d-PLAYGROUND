using System;
using UnityEngine;

[Serializable]
public class MusicInfo
{
    [HideInInspector]
    [SerializeField] private string name;

    public AudioClip[] clips;
    public MusicType musicType;

    public void OnValidate()
    {
        name = musicType.ToString();
    }
}
