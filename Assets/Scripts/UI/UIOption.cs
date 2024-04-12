using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIOption : UIBase
{
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI BGMText;
    public TextMeshProUGUI SFXText;

    public Slider volumeSlider;
    public Slider BGMSlider;
    public Slider SFXSlider;

    private float oldBGM;
    private float oldSFX;

    private void Awake()
    {
        BGMSlider.value = SoundManager.Instance.musicAudioSource.volume;
        SFXSlider.value = SoundManager.Instance.soundEffectVolume;
    }
    private void Update()
    {
        if (oldBGM != BGMSlider.value)
        {
            SoundManager.Instance.musicAudioSource.volume = BGMSlider.value;
            oldBGM = BGMSlider.value;
            GameManager.Instance.accountInfo.bgmVolume = oldBGM;
        }
        if (oldSFX != SFXSlider.value)
        {
            SoundManager.Instance.soundEffectVolume = SFXSlider.value;
            oldSFX = SFXSlider.value;
            GameManager.Instance.accountInfo.sfxVolume = oldSFX;
        }
    }

    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
