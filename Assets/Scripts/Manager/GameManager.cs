using System;
using System.IO;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState
{
    None,
    Intro,
    Loading,
    Main,
    IngameStart,
    IngameEnd,
}

public class GameManager : SingletoneBase<GameManager>
{
    [ReadOnly, SerializeField] private string _pidStr;
    public GameState gameState { get; private set; }
    string filePath = Application.persistentDataPath + "/";

    public AccountInfo accountInfo;
    public event Action updateUIAction; // UI 업데이트 콜
    public Player player { get; private set; }

    // 씬이 넘어가도 유지할 데이터
    public int stageId { get; set; } // 진입한 스테이지ID 가지고있게

    protected override void Init()
    {
        _pidStr = _pid.ToString();
        base.Init();

        // 버전 체크

        // 초기화
        CheckAccount();

        // 계정 세팅
        stageId = 101; // !NOTE : Test코드
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
        LoadGame(PlayerPrefs.GetString("AID"));
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
        //string jsonData = accountInfo.Serialize();
        //File.WriteAllText("save.json", jsonData);
    }

    private AccountInfo LoadGame(string aid)
    {
        // 불러오기
        //파일이 있으면 로드
        if (File.Exists(filePath + aid + ".json"))
        {
            string FromJsonData = File.ReadAllText(filePath);
            accountInfo = JsonUtility.FromJson<AccountInfo>(FromJsonData);
        }
        else
        {
            // 없으면 신규 유저
            accountInfo = new AccountInfo(aid, aid.Substring(0, 6));
        }
        return accountInfo;
    }
}
