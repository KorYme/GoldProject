using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] Sound[] _sounds;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in _sounds)
        {
            s._source = gameObject.AddComponent<AudioSource>();
            s._source.clip = s._clip;
            s._source.volume = s._volume;
            s._source.pitch = s._pitch;

            if (s.name == "music")
            {
                s._source.Play();
            }
        }
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(string name, bool randomizePitch = false, float pitchRange = 1f)
    {
        Sound s = Array.Find(_sounds, sound => sound.name == name);
        if (randomizePitch)
            s._source.pitch = UnityEngine.Random.Range(s._pitch - pitchRange, s._pitch + pitchRange);
        else
            s?._source.Play();

        if (s == null)
        {
            Debug.Log(name + " sound was not found.");
        }
    }

    public void StopSound(string name)
    {
        Sound s = Array.Find(_sounds, sound => sound.name == name);
        s?._source.Stop();

        if (s == null)
        {
            Debug.Log(name + " sound was not found.");
        }
    }
}
