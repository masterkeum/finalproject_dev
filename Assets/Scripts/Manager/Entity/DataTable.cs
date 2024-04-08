using System;
using System.Collections.Generic;

public class DataTable
{
    public List<InitializeParam> InitializeParam;
    public List<CharacterInfo> CharacterInfo;
    public List<MonsterLevel> MonsterLevel;
    public List<StageList> StageList;
    public List<StageInfoTable> StageInfoTable;
    public List<PlayerLevel> PlayerLevel;
    public List<PlayerIngameLevel> PlayerIngameLevel;
    public List<SkillTable> SkillTable;
    public List<ItemTable> ItemTable;
    public List<LevelGacha> LevelGacha;
    public List<Shop> Shop;
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
    public int? rewardAmount;
}

[Serializable]
public class StageList
{
    public int stageId;
    public string levelPrefab;
    public string levelLight;
    public string BGSprite; // 스프라이트의 경로
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
    public float[] genPosVecter3;
}

[Serializable]
public class PlayerLevel
{
    public int level;
    public int exp;
    public int totalExp;
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
    public int skillGroup;
    public string skillName;
    public string skillDesc;
    public string taskDesc;
    public bool isEnable;
    public int poolAmount;
    public int level;
    public int maxLevel;
    public int nextSkillId;
    public int reqSkillId;
    public SkillApplyType applyType;
    public SkillTargetType targetType;
    public SkillRangeType rangeType;
    public float coolDownTime;
    public float castDelay;
    public int attackDamage;
    public string projectileType;
    public int projectileSpeed;
    public int projectileCount;
    public int projectilePenetration;
    public int addDef;
    public int addHP;
    public int regenHP;
    public string imageAddress;
    public string prefabAsset;
    public string prefabAddress;
    public string prefabAfterEffect;
}

[Serializable]
public class ItemTable
{
    public int itemId;
    public ItemCategory itemCategory;
    public ItemType itemType;
    public string nameAlias;
    public string descAlias;
    public ItemGrade grade;
    public int maxStack;
    public string imageFile;
    public int minHp;
    public int maxHp;
    public int minDp;
    public int maxDp;
    public int minAp;
    public int maxAp;
    public float minMovespeed;
    public float maxMovespeed;
    public float minCriticalHit;
    public float maxCriticalHit;
    public int minHpGen;
    public int maxHpGen;
    public int getGold;
    public int getExp;
}

[Serializable]
public class LevelGacha
{
    public int level;
    public float normal;
    public float magic;
    public float elite;
    public float rare;
    public float epic;
    public float legendary;
}

[Serializable]
public class Shop
{
    public int packageId;
    public string packageDesc;
    public int currencyID;
    public int price;
    public ItemType itemType1;
    public int itemId1;
    public int amount1;
    public ItemType itemType2;
    public int? itemId2;
    public int? amount2;
    public ItemType itemType3;
    public int? itemId3;
    public int? amount3;
}