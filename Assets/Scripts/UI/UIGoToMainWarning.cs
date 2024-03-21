using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGoToMainWarning : UIBase
{

    public void OnYesbutton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnCancelButton()
    {
        gameObject.SetActive(false);
    }
}
