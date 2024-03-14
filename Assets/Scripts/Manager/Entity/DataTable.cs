
using System.Collections.Generic;
using System;
using System.Numerics;

public class DataTable
{
    public List<CharacterInfo> CharacterInfo;
    public List<MonsterLevel> MonsterLevel;
    public List<StageInfoTable> StageInfoTable;
    public List<PlayerIngameLevel> PlayerIngameLevel;
    public List<SkillTable> SkillTable;
}



[Serializable]
public class CharacterInfo
{
    public int uid;
    public CharacterType characterType;
    public string name;
    public int minLevel;
    public int maxLevel;
    public int hp;
    public int ap;
    public float? sensoryRange;
    public float attackRange;
    public float attackSpeed;
    public float moveSpeed;
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
    public int genTimeStart;
    public int genTimeEnd;
    public int genAmount;
    public int genTime;
    public int genMax;
    public int[] genPosVecter3;
}

[Serializable]
public class PlayerIngameLevel
{
    public int level;
    public int exp;
    public int totalExp;
}

[Serializable]
public class SkillTable
{
    public int sID;
    public string skill;
    public string skillType;
    public string subskill;
    public int dMG;
    public int dMGPersec;
    public int dMGPerEffect;
    public int perDMG;
    public int probabilityPerDMG;
    public int genNumPerSec;
    public int levelAdjustmentNum;
    public int levelAdjustmentper;
    public float paralyzeSec;
    public float duration;
    public float durationIncreasePer;
    public float coolDownTime;
    public float sizeEnhancementPer;
    public float rangeExpansionPer;
    public string skillStatsExplanation;
    public string skillExplanation;
}