public class UIChangeProfile : UIBase
{
    private ProfileSlotUI[] profileSlot;
    private ProfileSlotUI selectedSlot;

    public void Init()
    {
        for (int i = 0; i < profileSlot.Length; i++)
        {
            profileSlot[i].Init();
            UpdateUI();
        }

    }

    private void OnEnable()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {

    }
}

