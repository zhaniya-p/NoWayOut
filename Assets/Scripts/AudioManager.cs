using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;

    [Header("UI Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey("MasterVolume"))
        {
            masterSlider.value = 1f;
            musicSlider.value = 1f;
            sfxSlider.value = 1f;
        }
        else
        {
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }

        SetMasterVolume(masterSlider.value);
        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);
    }

    public void SetMasterVolume(float volume)
    {
        if (volume <= 0.0001f)
            volume = 0.0001f;

        mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        if (volume <= 0.0001f)
            volume = 0.0001f;

        mixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        if (volume <= 0.0001f)
            volume = 0.0001f;

        mixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }
}