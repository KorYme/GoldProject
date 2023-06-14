using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    [Header("AudioSliders")]
    [SerializeField] Slider _musicSlider;
    [SerializeField] Slider _sfxSlider;


    public static AudioManager Instance;

    Sound _music;
    int _nbOfPlayersReflecting = 0;
    float _musicVolume = 0.5f;
    float _sfxVolume = 0.5f;

    [SerializeField] Sound[] _sounds;

    public int NbOfPlayersReflecting { get => _nbOfPlayersReflecting; set => _nbOfPlayersReflecting = value; }

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
                _music = s;
                _music._source.loop = true;
                _music._source.Play();
            }
        }
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(string name, bool randomizePitch = false, float pitchRange = 1f)
    {
        Sound s = Array.Find(_sounds, sound => sound.name == name);
        s._source.volume = _sfxVolume;
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

    public void PlayPlayerReflectSound()
    {
        switch (_nbOfPlayersReflecting)
        {
            case 1:
                PlaySound("PlayerReflect1");
                break;
            case 2:
                PlaySound("PlayerReflect2");
                break;
            case 3:
                PlaySound("PlayerReflect3");
                Debug.Log("3 joueurs");
                break;
            default:
                break;
        }
    }

    public void ChangeMusicVolume(Slider slider)
    {
        _music._source.volume = slider.value/100;
    }

    public void ChangeSFXVolume(Slider slider)
    {
        _sfxVolume = slider.value/100;
    }

    //Init
    //_musicSlider.value = _musicVolume;
    //_sfxSlider.value = _sfxVolume;

    //Save data
    //gameData.VolumeMusic = _musicVolume;
    //gameData.VolumeSFX = _sfxVolume;

    //Load data
    //_musicVolume = gameData.VolumeMusic;
    //_sfxVolume = gameData.VolumeSFX;
    //_musicSlider.value = _musicVolume;
    //_sfxSlider.value = _sfxVolume;
}