using TMPro;
using UnityEngine;

public class UI2Btn : UIBase
{
    public TextMeshProUGUI messageText;

    private System.Action onOKCallback;
    private System.Action onCancelCallback;

    public void SetPopup(string message, System.Action onOK, System.Action onCancel = null)
    {
        Debug.Log("SetPopup");
        messageText.text = message;
        onOKCallback = onOK;
        onCancelCallback = onCancel;
    }

    // OK 버튼 클릭 시 실행될 함수
    public void OnOKButtonClick()
    {
        Debug.Log("OnOKButtonClick");
        onOKCallback?.Invoke(); // null 체크 후 콜백 실행
        gameObject.SetActive(false);
    }

    // Cancel 버튼 클릭 시 실행될 함수
    public void OnCancelButtonClick()
    {
        Debug.Log("OnCancelButtonClick");
        onCancelCallback?.Invoke(); // null 체크 후 콜백 실행
        gameObject.SetActive(false);
    }
}
