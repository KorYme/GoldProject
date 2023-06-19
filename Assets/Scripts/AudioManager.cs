using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Events;
using KorYmeLibrary.SaveSystem;

public class AudioManager : MonoBehaviour, IDataSaveable<GameData>
{
    [SerializeField] Sound[] _sounds;

    [HideInInspector] public Sound Music;
    float _musicVolume;
    public float MusicVolume
    {
        get => _musicVolume;
        set
        {
            _musicVolume = value;
            Music._source.volume = value;
        }
    }
    float _sfxVolume;
    public float SFXVolume
    {
        get => _sfxVolume; 
        set => _sfxVolume = value;
    }
    int _nbOfPlayersReflecting = 0;
    public int NbOfPlayersReflecting { get => _nbOfPlayersReflecting; set => _nbOfPlayersReflecting = value; }

    public static AudioManager Instance;
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

            if (s.name == "Music")
            {
                Music = s;
                Music._source.loop = true;
                Music._source.volume = MusicVolume;
                Music._source.Play();
            }
        }
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(string name, bool randomizePitch = false, float pitchRange = 1f)
    {
        Sound s = Array.Find(_sounds, sound => sound.name == name);
        s._source.volume = _sfxVolume * s._volume;
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
                break;
            default:
                break;
        }
    }

    public void InitializeData()
    {
        _sfxVolume = 0.5f;
        _musicVolume = 0.5f;
    }

    public void LoadData(GameData gameData)
    {
        _sfxVolume = gameData.VolumeSFX;
        _musicVolume = gameData.VolumeMusic;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.VolumeSFX = _sfxVolume;
        gameData.VolumeMusic = _musicVolume;
    }
}