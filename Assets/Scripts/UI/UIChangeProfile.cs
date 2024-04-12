using UnityEngine;

public class UIChangeProfile : UIBase
{

    [SerializeField] private ProfileSlotUI[] profileSlot;
    [SerializeField] private ProfileSlotUI selectedSlot;

    //public void Init()
    //{
    //    for (int i = 0; i < profileSlot.Length; i++)
    //    {
    //        profileSlot[i].Init();
    //        UpdateUI();
    //    }

    //}



    private void OnEnable()
    {
        AccountInfo accountInfo = GameManager.Instance.accountInfo;
        selectedSlot.index = accountInfo.selectedProfileIndex;
        for (int i = 0; i < profileSlot.Length; i++)
        {
            profileSlot[i].selectedMark.SetActive(false);
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
       
        for (int i = 0; i < profileSlot.Length; i++)
        {
            if (profileSlot[i].index == selectedSlot.index )
            {
                profileSlot[i].selectedMark.SetActive(true);
            }
            else
            {
                profileSlot[i].selectedMark.SetActive(false);
            }
        }
        selectedSlot.profileImage.sprite = profileSlot[selectedSlot.index].profileImage.sprite;

    }

    public void OnProfileClick(int index)
    {
        selectedSlot.index = profileSlot[index].index;
        UpdateUI();
    }

    public void OnConfirmButton()
    {
        GameManager.Instance.accountInfo.selectedProfileIndex = selectedSlot.index;
        GameManager.Instance.UpdateUI();
        gameObject.SetActive(false);
    }
}

