using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsParameter : MonoBehaviour
{
    [SerializeField] Slider _musicSlider, _sfxSlider;
    [SerializeField] Toggle _vibrationToggle, _colorblindToggle;
    bool _isInit = false;

    private void Start()
    {
        if (_musicSlider != null)
        {
            _musicSlider.value = AudioManager.Instance.MusicVolume * 100;
        }
        if (_sfxSlider != null)
        {
            _sfxSlider.value = AudioManager.Instance.SFXVolume * 100;
        }
        if (_vibrationToggle != null)
        {
            _vibrationToggle.isOn = DataManager.Instance.VibrationEnabled;
        }
        if (_colorblindToggle != null)
        {
            _colorblindToggle.isOn = DataManager.Instance.ColorBlindModeEnabled;
        }
        _isInit = true;
    }

    public void MusicSlider()
    {
        if (!_isInit) return;
        AudioManager.Instance.MusicVolume = _musicSlider.value / 100f;
    }

    public void SFXSlider()
    {
        if (!_isInit) return;
        AudioManager.Instance.SFXVolume = _sfxSlider.value / 100f;
    }

    public void VibrationToggle()
    {
        if (!_isInit) return;
        DataManager.Instance.VibrationEnabled = _vibrationToggle.isOn;
    }

    public void ColorblindToggle()
    {
        if (!_isInit) return;
        DataManager.Instance.ColorBlindModeEnabled = _colorblindToggle.isOn;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
