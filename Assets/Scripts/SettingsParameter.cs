using KorYmeLibrary.SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SettingsParameter : MonoBehaviour, IDataSaveable<GameData>
{
    float _musicVolumeToSave;
    float _sfxVolumeToSave;
    bool _vibrationToSave;

    Slider _slider;
    Toggle _toggle;


    private void Start()
    {
        _slider = GetComponent<Slider>();
        _toggle = GetComponent<Toggle>();
    }

    public void ChangeMusicVolume(Slider slider)
    {
        float _musicVolumeToSave = slider.value / 100;
        AudioManager.Instance.music._source.volume = _musicVolumeToSave;
    }

    public void ChangeSFXVolume(Slider slider)
    {
        float _sfxVolumeToSave = slider.value / 100;
        AudioManager.Instance.sfxVolume = _sfxVolumeToSave;
    }

    public void ChangeVibration(Toggle toggle)
    {
        DataManager.Instance.VibrationEnabled = toggle.isOn;
    }
    
    public void InitializeData()
    {
        if (_slider != null)
        {
            switch (gameObject.name)
            {
                case "MusicManager":
                    _slider.value = AudioManager.Instance.musicVolume;
                    break;
                case "SoundManager":
                    _slider.value = AudioManager.Instance.sfxVolume;
                    break;
                default:
                    break;
            }
        }

        if (_toggle != null)
            _toggle.isOn = DataManager.Instance.VibrationEnabled;
    }

    public void LoadData(GameData gameData)
    {
        if (_slider != null)
        {
            switch (gameObject.name)
            {
                case "MusicManager":
                    _slider.value = gameData.VolumeMusic;
                    AudioManager.Instance.musicVolume = gameData.VolumeMusic;
                    break;
                case "SoundManager":
                    _slider.value = gameData.VolumeSFX;
                    AudioManager.Instance.sfxVolume = gameData.VolumeSFX;
                    break;
                default:
                    break;
            }
        }

        if (_toggle != null)
            _toggle.isOn = gameData.VibrationEnabled;
    }

    public void SaveData(ref GameData gameData)
    {
        if (_slider != null)
        {
            switch (gameObject.name)
            {
                case "MusicManager":
                    gameData.VolumeMusic = _musicVolumeToSave;
                    break;
                case "SoundManager":
                    gameData.VolumeSFX = _sfxVolumeToSave;
                    break;
                default:
                    break;
            }
        }

        if (_toggle != null)
            gameData.VibrationEnabled = _vibrationToSave;
    }
}
