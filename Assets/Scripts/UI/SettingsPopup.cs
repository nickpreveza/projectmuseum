using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : UIPopup
{
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider audioSlider;
    [SerializeField] Slider musicSlider;

    public override void Setup()
    {
        if (AudioManager.Instance != null)
        {
            float masterVolume;
            AudioManager.Instance.masterMixer.GetFloat("masterVolume", out masterVolume);
            masterSlider.value = masterVolume;

            float audioVolume;
            AudioManager.Instance.masterMixer.GetFloat("audioVolume", out audioVolume);
            audioSlider.value = audioVolume;

            float musicVolume;
            AudioManager.Instance.masterMixer.GetFloat("musicVolume", out musicVolume);
            musicSlider.value = musicVolume;
        }

    }

    public void SetMasterVolume(float volume)
    {
        AudioManager.Instance.masterMixer.SetFloat("masterVolume", volume);
    }

    public void SetAudiovolume(float volume)
    {
        AudioManager.Instance.masterMixer.SetFloat("audioVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        AudioManager.Instance.masterMixer.SetFloat("musicVolume", volume);
    }

}
