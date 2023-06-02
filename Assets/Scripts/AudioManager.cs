using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    const string MIXER_MUSIC = "MusicVolume";
    const string MIXER_SFX = "SFXVolume";

    [Header("Sliders")]
    //Must set slider Min value to 0.001 to avoid log(0) error
    [SerializeField] Slider _musicSlider;
    [SerializeField] Slider _sfxSlider;

    [Header("AudioSources and Mixer")]
    [SerializeField] AudioSource _musicSource;
    [SerializeField] AudioSource _sfxSource;
    [SerializeField] AudioMixer _audioMixer;

    [Header("AudioClips")]
    [SerializeField] AudioClip _music;
    [SerializeField] AudioClip _playerMoveStart;
    [SerializeField] AudioClip _playerMoveStop;
    [SerializeField] AudioClip _crateMoveStart;
    [SerializeField] AudioClip _crateMoveStop;
    [SerializeField] AudioClip _playerRotationStart;
    [SerializeField] AudioClip _playerRotationStop;
    [SerializeField] AudioClip _reflectStart;
    [SerializeField] AudioClip _targetLaserRampUp;
    [SerializeField] AudioClip _targetLaserStops;
    [SerializeField] AudioClip _levelCompleted;
    [SerializeField] AudioClip _lensReflectStart;
    [SerializeField] AudioClip _lensReflectStop;


    private void Awake()
    {
        _musicSlider?.onValueChanged.AddListener(ChangeMusicVolume);
        _sfxSlider?.onValueChanged.AddListener(ChangeSFXVolume);
    }

    public void ChangeMusicVolume(float value)
    {
        _audioMixer.SetFloat(MIXER_MUSIC, Mathf.Log(value) * 20);
    }

    public void ChangeSFXVolume(float value)
    {
        _audioMixer.SetFloat(MIXER_SFX, Mathf.Log(value) * 20);
    }

    public void PlayerMoveStarted()
    {
        _sfxSource.PlayOneShot(_playerMoveStart);
    }

    public void PlayerMoveStopped()
    {
        _sfxSource.PlayOneShot(_playerMoveStop);
    }

    public void CrateMoveStarted()
    {
        _sfxSource.PlayOneShot(_crateMoveStart);
    }

    public void CrateMoveStopped()
    {
        _sfxSource.PlayOneShot(_crateMoveStop);
    }

    public void PlayerRotationStarted()
    {
        _sfxSource.PlayOneShot(_playerRotationStart);
    }

    public void PlayerRotationStopped()
    {
        _sfxSource.PlayOneShot(_playerRotationStop);
    }

    public void ReflectionStarted()
    {
        _sfxSource.PlayOneShot(_reflectStart);
    }

    public void TargetLaserHits()
    {
        _sfxSource.PlayOneShot(_targetLaserRampUp);
    }

    public void TargetLaserStop()
    {
        _sfxSource.PlayOneShot(_targetLaserStops);
    }

    public void LevelComplete()
    {
        _sfxSource.PlayOneShot(_levelCompleted);
    }

    public void LensFilterStart()
    {
        
    }   

    public void LensFilterStop()
    {
    }
}
