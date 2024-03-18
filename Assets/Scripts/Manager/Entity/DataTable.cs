
using System.Collections.Generic;
using System;

public class DataTable
{
    public List<CharacterInfo> CharacterInfo;
    public List<MonsterLevel> MonsterLevel;
    public List<StageInfoTable> StageInfoTable;
    public List<PlayerIngameLevel> PlayerIngameLevel;
    public List<SkillTable> SkillTable;
    public List<ItemTable> ItemTable;
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
    public float[] genPosVecter3;
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
    public int skillId;
    public string skill;
    public string applyType;
    public string skillType;
    public int subskill;
    public int level;
    public int maxLevel;
    public int nextSkill;
    public int damage;
    public int damagePersec;
    public int damagePerEffect;
    public int perDamage;
    public int probabilityPerDamage;
    public int genNumPerSec;
    public int levelAdjustmentNum;
    public int levelAdjustmentper;
    public float paralyzeSec;
    public float duration;
    public float durationIncreasePer;
    public float coolDownTime;
    public float sizeEnhancementPer;
    public float rangeExpansionPer;
    public float acquisitionIncreasePer;
    public string skillStatsExplanation;
    public string skillExplanation;
    public string imageAddress;
}
[Serializable]
public class ItemTable
{
    public int itemId;
    public string itemCategory;
    public string itemType;
    public string nameAlias;
    public string descAlias;
    public int maxStack;
    public string ImageFile;
    public int categoryTextId;
    public int hp;
    public int dp;
    public int ap;
    public int speed;
}