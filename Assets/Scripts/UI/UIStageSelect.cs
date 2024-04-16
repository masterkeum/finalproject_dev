using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

//public class StageSlot
//{
//    public int stageIndex;
//}
public class UIStageSelect : UIBase
{
    [SerializeField] private StageSlotUI[] _stageSlots;
    [SerializeField] private ScrollRect scrollbar;

    [Header("SelectedStage")]
    private StageSlotUI _curStage;

    private void OnEnable()
    {
        // TODO : 현재 선택된 스테이지 셀렉트 상태
        int tmpSelect = GameManager.Instance.accountInfo.selectedStageId % 100 - 1;
        SelectCurSlot(tmpSelect);

        LockSlot(GameManager.Instance.accountInfo.clearStageId);
        scrollbar.verticalNormalizedPosition = 1.0f - (float)tmpSelect / 4;
    }


    public void OnSelectButton()
    {
        GameManager.Instance.SelectStage(_curStage.index);
        GameManager.Instance.UpdateUI();
        gameObject.SetActive(false);
        SoundManager.Instance.PlaySound("ConfirmUI_1", 1f);
    }

    public void OnCancelButton()
    {
        gameObject.SetActive(false);
        SoundManager.Instance.PlaySound("ButtonClickUI_1", 1f);
    }

    public void SelectCurSlot(int index)
    {
        int tmpClearStageIndex = GameManager.Instance.accountInfo.clearStageId % 100;
        //Debug.Log($"{index} <= {tmpClearStageIndex}");
        if (index <= tmpClearStageIndex)
        {
            _curStage = _stageSlots[index];
            for (int i = 0; i < _stageSlots.Length; i++)
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
            SoundManager.Instance.PlaySound("NextPageUI_1", 1f);
        }
    }

    private void LockSlot(int clearStageId)
    {
        int open = clearStageId % 100;
        for (int i = 0; i < _stageSlots.Length; i++)
        {
            if (i <= open)
            {
                _stageSlots[i].GetComponent<StageSlotUI>().LockedStage(false);
            }
            else
            {
                _stageSlots[i].GetComponent<StageSlotUI>().LockedStage(true);
            }

        }

    }
}
