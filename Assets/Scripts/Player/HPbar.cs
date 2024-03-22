using UnityEngine;

public class UIhpbar : UIBase
{
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(mainCamera.transform.forward, mainCamera.transform.up);
    }
}
