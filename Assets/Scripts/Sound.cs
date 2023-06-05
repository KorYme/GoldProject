using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip _clip;

    [Range(0f, 1f)]
    public float _volume;
    [Range(.1f, 3f)]
    public float _pitch;

    [HideInInspector]
    public AudioSource _source;
    
}
