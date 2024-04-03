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

    private void Awake()
    {
        BGMSlider.value = SoundManager.Instance.musicAudioSource.volume;
        SFXSlider.value = SoundManager.Instance.soundEffectVolume;
    }
    private void Update()
    {
        SoundManager.Instance.musicAudioSource.volume = BGMSlider.value;
        SoundManager.Instance.soundEffectVolume = SFXSlider.value;
    }
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
