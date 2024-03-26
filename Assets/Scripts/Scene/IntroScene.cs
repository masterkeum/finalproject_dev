using UnityEngine;

public class IntroScene : MonoBehaviour
{
    [SerializeField] private GameObject chestMonster;
    private Animator chestAnim;

    protected static readonly int OnLoad = Animator.StringToHash("OnLoad");
    protected static readonly int Ready = Animator.StringToHash("Ready");
    private UILoading loadingUI;

    private void Awake()
    {
        Time.timeScale = 1.0f;
        _ = DataManager.Instance;
        _ = GameManager.Instance;
        UIManager.Instance.Clear();
    }

    private void Start()
    {
        GenObject();

        // TODO : 받아온것으로 프로그래스바 조절하게 수정해야함
        loadingUI = UIManager.Instance.ShowUI<UILoading>();
    }

    private void Update()
    {
        if (loadingUI.IsReady)
        {
            chestAnim.SetBool(Ready, true);
        }
    }

    private void GenObject()
    {
        Instantiate(Resources.Load<GameObject>("Prefabs/IntroScene/IntroField"));
        chestMonster = Instantiate(Resources.Load<GameObject>("Prefabs/IntroScene/IntroChestMonster"));
        chestAnim = chestMonster.GetComponent<Animator>();

        chestAnim.SetBool(OnLoad, true);
    }
}

