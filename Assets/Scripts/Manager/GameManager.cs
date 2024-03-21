using UnityEngine;

public class GameManager : SingletoneBase<GameManager>
{
    [ReadOnly, SerializeField] private string _pidStr;
    public AccountInfo accountInfo;

    // 필요한가?
    public Transform playerParent;
    public Transform parentCanvas;
    private Player player;

    // 씬이 넘어가도 유지할 데이터
    public int stageId { get; set; } // 진입한 스테이지ID 가지고있게
    


    protected override void Init()
    {
        _pidStr = _pid.ToString();
        base.Init();
        // 초기화
        
        


        // 계정 세팅


        stageId = 101; // !NOTE : Test코드
    }
    
    // 받을 메서드
    public void TakePlayer(Player player)
    {
        this.player = player;
    }



}
