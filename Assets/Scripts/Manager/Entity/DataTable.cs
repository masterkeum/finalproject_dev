
using System;
using System.Collections.Generic;


public class DataTable
{
    public List<InitializeParam> InitializeParam;
    public List<CharacterInfo> CharacterInfo;
    public List<MonsterLevel> MonsterLevel;
    public List<StageList> StageList;
    public List<StageInfoTable> StageInfoTable;
    public List<PlayerIngameLevel> PlayerIngameLevel;
    public List<SkillTable> SkillTable;
    public List<ItemTable> ItemTable;
    public List<LevelGacha> LevelGacha;
}

[Serializable]
public class InitializeParam
{
    public string initType;
    public string code;
    public int param1;
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
    public int attackPower;
    public float sensoryRange;
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
public class StageList
{
    public int stageId;
    public string BGSprite; // 스프라이트의 경로
}

[Serializable]
public class StageInfoTable
{
    public int stageId;
    public int monsterId;
    public int level;
    public CharacterType characterType;
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
    public string skillName;
    public string applyType;
    public string skillType;
    public int nextSkill;
    public int subskill;
    public int level;
    public int maxLevel;

    public int projectileCount;
    public int projectileSpeed;
    public float coolDownTime;

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
    public float sizeEnhancementPer;
    public float rangeExpansionPer;
    public float acquisitionIncreasePer;
    public string skillStatsExplanation;
    public string skillExplanation;

    public string imageAddress;
    public string prefabAddress;
    public string prefabFlashAddress;

    public float lastAttackTime;
}
[Serializable]
public class ItemTable
{
    public int itemId;
    public string itemCategory;
    public string itemType;
    public string nameAlias;
    public string descAlias;
    public string grade;
    public int maxStack;
    public string ImageFile;
    public int categoryTextId;
    public int minHp;
    public int maxHp;
    public int minDp;
    public int maxDp;
    public int maxAp;
    public int minAp;
    public float minMovespeed;
    public float maxMovespeed;
    public float minCriticalHit;
    public float maxCriticalHit;
    public float minHpGen;
    public float maxHpGen;
}

[Serializable]
public class LevelGacha
{
    public int level;
    public float normal;
    public float magic;
    public float elite;
    public float rare;
    public float Epic;
    public float Legendary;
}