using Gley.Localization.Internal;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletoneBase<DataManager>
{
    [ReadOnly, SerializeField] private string _pidStr;

    // 데이터 파일 경로 Assets/Resources/DataTables/DataTable.json
    private string DataFilePath = "DataTables/DataTable";

    public Dictionary<int, CharacterInfo> characterInfoDict;
    public Dictionary<int, MonsterLevel> monsterLevelDict;

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
        Root jsonData = JsonUtility.FromJson<Root>(json);

        characterInfoDict = new Dictionary<int, CharacterInfo>();
        monsterLevelDict = new Dictionary<int, MonsterLevel>();

        foreach (CharacterInfo characterInfo in jsonData.CharacterInfo)
        {
            characterInfoDict.Add(characterInfo.uid, characterInfo);
            Debug.Log(characterInfo.uid);
        }
        foreach (MonsterLevel monsterLevel in jsonData.MonsterLevel)
        {
            characterInfoDict[monsterLevel.uid].monsterLevelData.Add(monsterLevel);
        }

    }
}


public class Root
{
    public List<CharacterInfo> CharacterInfo;
    public List<MonsterLevel> MonsterLevel;
    public List<StageInfoTable> StageInfoTable;
}


[Serializable]
public class CharacterInfo
{
    public int uid;
    public string characterType;
    public string name;
    public int minLevel;
    public int maxLevel;
    public int hp;
    public int ap;
    public int? sensoryRange;
    public int attackRange;
    public int attackSpeed;
    public int moveSpeed;
    public int defaultSkill;
    public string prefabFile;

    public List<MonsterLevel> monsterLevelData;
}


[System.Serializable]
public class MonsterLevel
{
    public int uid;
    public int level;
    public int addHP;
    public int addAP;
    public int gold;
    public int exp;
    public string rewardType;
    public int? rewardID;
}

public class StageInfoTable
{
    public int stage;
    public int uid;
    public int level;
    public string characterType;
    public int genTimeStart;
    public int genTimeEnd;
    public int genAmount;
    public int genTime;
    public int genMax;
}