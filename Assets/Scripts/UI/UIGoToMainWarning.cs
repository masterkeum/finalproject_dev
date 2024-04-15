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
