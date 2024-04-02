using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class DailyAdmobButtonUI : MonoBehaviour
{
    Button button;
    [SerializeField]
    private string admobCode;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ShowAdmob);    
    }

    private void ShowAdmob()
    {
        AdmobManager.Instance?.RunRewardedAd(SuccessCallback,admobCode,ErrorCallback);
    }

    private void SuccessCallback()
    {
        //TODO
        // 성공 코드 넣으면 됨.
        // 보상 주고
    }
    private void ErrorCallback()
    {
        //TODO 
        // 여기는 에러 떴을 때 콜백 에러 메세지 팝업? 이런거 넣어주면 될 것 같고.
    }
}