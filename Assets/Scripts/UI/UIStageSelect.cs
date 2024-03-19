using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class StageSlot
//{
//    public int stageIndex;
//}
public class UIStageSelect : UIBase
{
    [SerializeField] private StageSlotUI[] _stageSlots;

    [Header("SelectedStage")]
    private StageSlotUI _curStage;

    public OutGameHUD outHUD;

    private void Start()
    {
        outHUD = GameObject.Find("OutGameHUD").GetComponent<OutGameHUD>();
    }



    public void OnSelectButton()
    {
        outHUD.SelectedStageId = _curStage.index;
    }
    public void OnCancelButton()
    {
        gameObject.SetActive(false);
    }
    

    public void SelectCurSlot(int index)
    {
        _curStage = _stageSlots[index];
        for(int i = 0; i < _stageSlots.Length; i++)
        {
            if (_curStage == _stageSlots[i])
            {
                _stageSlots[i].isCurStage = true;
            }
            else
            {
                _stageSlots[i].isCurStage = false;
            }
            _stageSlots[i].UpdateMark();
        }

    }

}
