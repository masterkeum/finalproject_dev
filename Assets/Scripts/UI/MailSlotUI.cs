using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MailSlotUI : MonoBehaviour
{
    public GameObject openedMail;
    public GameObject closedMail;
    public TextMeshProUGUI message;

    private int mailIndex;
    private bool isOpened;
    private void Start()
    {
        UpdateUI();
        SetIndex();
    }

    public void OpenMail()
    {
        UIManager.Instance.ShowUI<UIInsideMail>();
        if(!isOpened) { isOpened = true; }
        UpdateUI();
    }
    public void UpdateUI()
    {
        if (isOpened) 
        {
            openedMail.SetActive(true);
            closedMail.SetActive(false);
        }
        else 
        {
            openedMail.SetActive(false);
            closedMail.SetActive(true);
        }
    }
    public void SetIndex()
    {
        mailIndex = transform.GetSiblingIndex();
    }


}
