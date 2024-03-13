using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameHUD : MonoBehaviour
{
    public TextMeshProUGUI killText;
    public TextMeshProUGUI goldText;
    public Slider expSlider;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI curLevelText;

    public void OnPauseButton()
    {
        UIManager.Instance.ShowUI<UIPause>();
    }
}
