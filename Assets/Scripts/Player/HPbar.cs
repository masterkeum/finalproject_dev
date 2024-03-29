using UnityEngine;
using UnityEngine.UI;

public class UIhpbar : UIBase
{
    private Camera mainCamera;
    private Slider hpGuageSlider;
    // health 데이터 선언 (플레이어 및 몬스터)
    
    private void Awake()
    {
        mainCamera = Camera.main;
        // health 값 가져오기
        // 데미지 추가하기
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(mainCamera.transform.forward, mainCamera.transform.up);
    }

    private void UpdateHealthUI()
    {
        // hpGuageSlider.value = 'currentHealth'  /   'maxhealth' 
    }
}
