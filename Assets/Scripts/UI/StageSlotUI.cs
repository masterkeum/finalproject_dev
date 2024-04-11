using UnityEngine;
using UnityEngine.UI;

public class StageSlotUI : MonoBehaviour
{
    [SerializeField] private Image _stageBanner;
    [SerializeField] private GameObject _selectMark;

    public int index;

    public bool isCurStage;

    public void UpdateMark()
    {
        _selectMark.SetActive(isCurStage);
    }


}
