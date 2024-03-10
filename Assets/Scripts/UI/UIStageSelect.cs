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
    private StageSlotUI _selectedStage;

    


   
    public void OnSelectButton()
    {
        _selectedStage = _curStage;
    }
    public void OnCancelButton()
    {
        Destroy(gameObject);
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
