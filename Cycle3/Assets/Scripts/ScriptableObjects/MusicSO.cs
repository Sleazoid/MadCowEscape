using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class MusicSO : ScriptableObject
{
    public string artist;
    public string songName;
    public bool showInfo;
    public float volume = 0.836f;
    public AudioClip audio;
}

