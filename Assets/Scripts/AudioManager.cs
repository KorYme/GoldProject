using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Events;
using KorYmeLibrary.SaveSystem;

public class AudioManager : MonoBehaviour, IDataSaveable<GameData>
{
    [Header("Parameters")]
    [SerializeField] Slider _musicSlider;
    [SerializeField] Slider _sfxSlider;
    [SerializeField] Toggle _vibrationToggle;


    public static AudioManager Instance;

    public Sound music;
    int _nbOfPlayersReflecting = 0;
    public float musicVolume;
    public float sfxVolume;
    bool _vibration;

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

        sfxVolume = 0.5f;
        musicVolume = 0.5f;
        _vibration = true;

        foreach (Sound s in _sounds)
        {
            s._source = gameObject.AddComponent<AudioSource>();
            s._source.clip = s._clip;
            s._source.volume = s._volume;
            s._source.pitch = s._pitch;

            if (s.name == "music")
            {
                music = s;
                music._source.loop = true;
                music._source.Play();
            }
        }
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(string name, bool randomizePitch = false, float pitchRange = 1f)
    {
        Sound s = Array.Find(_sounds, sound => sound.name == name);
        s._source.volume = sfxVolume * s._volume;
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

    

    //DATA

    public void InitializeData()
    {
        //_musicSlider.value = musicVolume;
        //_sfxSlider.value = sfxVolume;
        //_vibrationToggle.isOn = _vibration;
    }

    public void LoadData(GameData gameData)
    {
        
    }

    public void SaveData(ref GameData gameData)
    {
        
    
    }
}