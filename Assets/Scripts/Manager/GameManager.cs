using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class GameManager : SingletoneBase<GameManager>
{
    // global
    [ReadOnly, SerializeField] private string _pidStr;

    public GameState gameState { get; private set; }
    string saveFilePath;

    public int _maxActionPoint { get; private set; }
    public float _regenActionPointTime { get; private set; }
    public int _combatActionPoint { get; private set; }
    public int stageId { get; set; } // 진입한 스테이지ID 가지고있게


    // 사용자
    public AccountInfo accountInfo;

    // 게임
    public event Action updateUIAction; // UI 업데이트 콜

    public Player player { get; private set; }


    JsonSerializerSettings settings = new()
    {
        FloatFormatHandling = FloatFormatHandling.String,
        Converters = { new StringEnumConverter() }
    };

    protected override void Init()
    {
        _pidStr = _pid.ToString();
        base.Init();
        // 데이터 로드 이후 루틴
        StartCoroutine(WaitForData());
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

        // TODO : 안드로이드도 저장이 잘 되는지 확인 필요
        saveFilePath = Application.persistentDataPath + "/" + PlayerPrefs.GetString("AID") + ".json";
        Debug.Log(saveFilePath);
        accountInfo = LoadGame(PlayerPrefs.GetString("AID"));
        stageId = accountInfo.selectedStageId;
    }

    IEnumerator WaitForData()
    {
        // A 인스턴스의 데이터가 준비될 때까지 대기
        while (!DataManager.Instance.IsReady)
        {
            Debug.LogWarning("Data Loading...");
            yield return null; // 다음 프레임까지 대기
        }

        // 게임 세팅
        InitParam();

        // 계정 세팅
        CheckAccount();
        // TODO : 버전 체크. 에셋번들이나 어드레서블 들어가면
    }

    private void InitParam()
    {
        _maxActionPoint = DataManager.Instance._InitParam["MaxActionPoint"];
        _regenActionPointTime = DataManager.Instance._InitParam["RegenActionPointTime"];
        _combatActionPoint = DataManager.Instance._InitParam["CombatActionPoint"];
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
                accountInfo.AddActionPoint(plusActionPoint);
                if (accountInfo.actionPoint < _maxActionPoint)
                {
                    // 마지막 업데이트 시간 갱신
                    accountInfo.AddUpdateTime(plusActionPoint * _regenActionPointTime);
                    return;
                }
            }
            accountInfo.AddUpdateTime(); // 현재시간으로 덮어씌우기
        }
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

    public void SaveGame()
    {
        if (saveFilePath != null)
        {
            string jsonData = JsonConvert.SerializeObject(accountInfo, Formatting.Indented, settings);
            File.WriteAllText(saveFilePath, jsonData);
            //Debug.Log("Save : " + jsonData);
        }
    }

    private AccountInfo LoadGame(string aid)
    {
        // 불러오기
        //파일이 있으면 로드
        if (File.Exists(saveFilePath))
        {
            string FromJsonData = File.ReadAllText(saveFilePath);
            if (FromJsonData == "null")
            {
                return new AccountInfo(aid, aid.Substring(9, 4));
            }
            return JsonConvert.DeserializeObject<AccountInfo>(FromJsonData, settings);
        }
        else
        {
            // 없으면 신규 유저
            return new AccountInfo(aid, aid.Substring(9, 4));
        }
    }


    #region InGameScene

    public void InGameSceneProcess()
    {
        SetState(GameState.IngameStart);
        accountInfo.AddActionPoint(-_combatActionPoint);

        SaveGame();
        UpdateUI();
    }

    #endregion

    #region MainScene
    public void MainSceneProcess()
    {
        SetState(GameState.Main);
        // 행동력 회복
        CalcActionPoint();

        // 출석체크
        SaveGame();
        UpdateUI();
    }

    #endregion
}
