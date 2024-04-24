using UnityEngine;
using UnityEngine.UI;

public class StageSlotUI : MonoBehaviour
{
    [SerializeField] private Image _stageBanner;
    [SerializeField] private GameObject _selectMark;
    [SerializeField] private GameObject _imageLock;

    public int index;

    public bool isCurStage;

    public void LockedStage(bool isLock)
    {
        if (isLock)
        {
            _imageLock.SetActive(true);
        }
        else
        {
            _imageLock.SetActive(false);
        }
    }

    public void UpdateMark()
    {
        _selectMark.SetActive(isCurStage);
    }

}
