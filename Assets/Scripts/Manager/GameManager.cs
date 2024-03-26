using System;
using System.IO;
using UnityEngine;



public class GameManager : SingletoneBase<GameManager>
{
    // global
    [ReadOnly, SerializeField] private string _pidStr;
    public GameState gameState { get; private set; }
    string saveFilePath;

    private int _maxActionPoint;
    private float _regenActionPointTime;
    public int stageId { get; set; } // 진입한 스테이지ID 가지고있게

    // 사용자
    public AccountInfo accountInfo;


    // 게임
    public event Action updateUIAction; // UI 업데이트 콜

    public Player player { get; private set; }


    protected override void Init()
    {
        _pidStr = _pid.ToString();
        base.Init();
        // 게임 세팅
        InitParam();

        // TODO : 버전 체크. 에셋번들이나 어드레서블 들어가면


        // 계정 정보 세팅
        CheckAccount();

        // 출석체크

        // 행동력 회복
        CalcActionPoint();

    }

    private void InitParam()
    {
        // DataManager 살아있는지, 구동 됐는지 체크 어떻게 하지?
        while (true)
        {
            if (DataManager.Instance.IsReady)
            {
                _maxActionPoint = DataManager.Instance._InitParam["MaxActionPoint"];
                _regenActionPointTime = DataManager.Instance._InitParam["RegenActionPointTime"];
                break;
            }
        }
    }

    private void CalcActionPoint()
    {
        /*
        실행 타이밍
            게임 시작
            인게임 종료 후 아웃게임 나오는 경우
        */
        float timeElapsed = UtilityKit.GetCurrentTime() - accountInfo.lastUpdateTime; // 시간차이
        if (timeElapsed >= _regenActionPointTime)
        {
            int plusActionPoint = Mathf.FloorToInt(timeElapsed / _regenActionPointTime); // 8분당 1씩 회복
            // 행동력 회복
            if (accountInfo.actionPoint < _maxActionPoint)
            {
                accountInfo.actionPoint += plusActionPoint;
                if (accountInfo.actionPoint < _maxActionPoint)
                {
                    // 마지막 업데이트 시간 갱신
                    accountInfo.lastUpdateTime += plusActionPoint * _regenActionPointTime;
                    return;
                }
            }
            accountInfo.lastUpdateTime = UtilityKit.GetCurrentTime(); // 현재시간으로 덮어씌우기
        }
    }

    private void CheckAccount()
    {
        // 계정 있는지 확인 임시로 PlayerPrefs 사용
        // 나중에 구글 붙이면 구글쪽으로 로직 변경해야함
        if (!PlayerPrefs.HasKey("AID"))
        {
            // 새 계정 생성
            Guid guid = Guid.NewGuid();
            PlayerPrefs.SetString("AID", guid.ToString());
        }

        saveFilePath = Application.persistentDataPath + "/" + PlayerPrefs.GetString("AID") + ".json";
        Debug.Log(saveFilePath);
        accountInfo = LoadGame(PlayerPrefs.GetString("AID"));
        stageId = accountInfo.selectedStageId;
    }


    private void Update()
    {
        if (UIManager.Instance.popupUICount > 0)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("게임종료");
        this.SaveGame();
    }
    private void OnApplicationPause(bool pause)
    {
        // 포커스아웃
        if (pause)
        {
            Debug.Log("일시정지");
            this.SaveGame();
        }
    }

    public void Clear()
    {
        updateUIAction = null;
    }

    public void UpdateUI()
    {
        updateUIAction?.Invoke();
    }

    // 받을 메서드
    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void SetState(GameState state)
    {
        gameState = state;
    }


    private void SaveGame()
    {
        string jsonData = JsonUtility.ToJson(accountInfo);
        File.WriteAllText(saveFilePath, jsonData);
    }

    private AccountInfo LoadGame(string aid)
    {
        // 불러오기
        //파일이 있으면 로드
        if (File.Exists(saveFilePath))
        {
            string FromJsonData = File.ReadAllText(saveFilePath);
            return JsonUtility.FromJson<AccountInfo>(FromJsonData);
        }
        else
        {
            // 없으면 신규 유저
            return accountInfo = new AccountInfo(aid, aid[..8]);
        }
    }
}
