
using System.Collections.Generic;
using System;

public class DataTable
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

    public Dictionary<int, MonsterLevel> monsterLevelData = new Dictionary<int, MonsterLevel>(); // 키값은 레벨
}


[Serializable]
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

[Serializable]
public class StageInfoTable
{
    public int stageId;
    public int monsterId;
    public int level;
    public string characterType;
    public int genTimeStart;
    public int genTimeEnd;
    public int genAmount;
    public int genTime;
    public int genMax;
}