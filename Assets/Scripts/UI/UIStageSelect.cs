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


    private void Start()
    {
        // TODO : 현재 선택된 스테이지 셀렉트 상태
    }

    public void OnSelectButton()
    {
        GameManager.Instance.stageId = _curStage.index;
        GameManager.Instance.UpdateUI();
        gameObject.SetActive(false);
    }
    public void OnCancelButton()
    {
        gameObject.SetActive(false);
    }


    public void SelectCurSlot(int index)
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

    }

}
