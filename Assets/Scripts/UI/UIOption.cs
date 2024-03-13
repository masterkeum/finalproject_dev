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

   
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}
