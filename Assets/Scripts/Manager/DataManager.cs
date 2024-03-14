using System;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletoneBase<DataManager>
{
    [ReadOnly, SerializeField] private string _pidStr;

    // 데이터 파일 경로 Assets/Resources/DataTables/DataTable.json
    private string DataFilePath = "DataTables/DataTable";

    public Dictionary<int, CharacterInfo> characterInfoDict;
    public Dictionary<int, List<StageInfoTable>> StageInfoDict;
    public Dictionary<int, PlayerIngameLevel> PlayerIngameLevelDict;
    public Dictionary<int, SkillTable> SkillTableDict;

    protected override void Init()
    {
        _pidStr = _pid.ToString();
        base.Init();
        LoadJsonData();
    }

    private void LoadJsonData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(DataFilePath);
        if (jsonFile != null)
        {
            //string jsonString = jsonFile.text;
            ConvertJsonToDictionary(jsonFile.text);
        }
        else
        {
            Debug.LogError("Failed to load JSON file at path: " + DataFilePath);
        }
    }

    public void ConvertJsonToDictionary(string json)
    {
        DataTable jsonData = JsonUtility.FromJson<DataTable>(json);

        characterInfoDict = new Dictionary<int, CharacterInfo>();
        StageInfoDict = new Dictionary<int, List<StageInfoTable>>();
        PlayerIngameLevelDict = new Dictionary<int, PlayerIngameLevel>();

        // 캐릭터 정보
        foreach (CharacterInfo characterInfo in jsonData.CharacterInfo)
        {
            characterInfoDict.Add(characterInfo.uid, characterInfo);
            //characterInfo.monsterLevelData = new Dictionary<int, MonsterLevel>();
        }
        foreach (MonsterLevel monsterLevel in jsonData.MonsterLevel)
        {
            if (characterInfoDict.ContainsKey(monsterLevel.uid))
            {
                characterInfoDict[monsterLevel.uid].monsterLevelData.Add(monsterLevel.level, monsterLevel);
            }
            else
            {
                Debug.LogError($"monsterLevel 데이터 불량 {monsterLevel.uid}");
            }
        }
        Debug.Log("케릭터 정보 로드 완료");

        // 스테이지 정보
        foreach (StageInfoTable stageInfoTable in jsonData.StageInfoTable)
        {
            int stageId = stageInfoTable.stageId;
            if (StageInfoDict.ContainsKey(stageId))
            {
                StageInfoDict[stageId].Add(stageInfoTable);
            }
            else
            {
                // 새로 생성
                StageInfoDict.Add(stageId, new List<StageInfoTable>());
                StageInfoDict[stageId].Add(stageInfoTable);
            }
        }
        Debug.Log("스테이지 정보 로드 완료");

        // 인게임 유저 레벨 정보

        foreach (PlayerIngameLevel playerIngameLevel in jsonData.PlayerIngameLevel)
        {
            PlayerIngameLevelDict.Add(playerIngameLevel.level, playerIngameLevel);
        }
        Debug.Log("유저 인게임 레벨 정보 로드 완료");

        // 스킬 정보

        foreach (SkillTable skillTable in jsonData.SkillTable)
        {
            SkillTableDict.Add(skillTable.sID, skillTable);
        }

        // GC에서 언제 가져갈지 모르니 jsonData를 명시적으로 null 로 만들거나 destroy 하고싶다.
        jsonData = null;
    }


    public CharacterInfo GetCharacterInfo(int uid)
    {
        if (characterInfoDict.ContainsKey(uid))
            return characterInfoDict[uid];
        else
            return null;
    }

    public List<StageInfoTable> GetStageInfo(int stageId)
    {
        if (StageInfoDict.ContainsKey(stageId))
            return StageInfoDict[stageId];
        else
            return null;
    }

    public PlayerIngameLevel GetPlayerIngameLevel(int level)
    {
        if (PlayerIngameLevelDict.ContainsKey(level))
            return PlayerIngameLevelDict[level];
        else return null;
    }

    public SkillTable GetSkillTable(int sID)
    {
        if(SkillTableDict.ContainsKey(sID))
            return SkillTableDict[sID];
        else return null;
    }


}

