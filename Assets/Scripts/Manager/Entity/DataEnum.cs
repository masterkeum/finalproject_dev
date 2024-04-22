using System;

public enum GameState
{
    None,
    Intro,
    Loading,
    Main,
    IngameStart,
    IngameEnd,
    DropDie,
    KillDie,
}

public enum CharacterType
{
    None,
    Player,
    Monster,
    BossMonster
}

public enum ItemCategory
{
    None,
    Resource,
    Equipment
}

public enum ItemType
{
    None,
    Gold,
    Gem,
    Core,
    Energy,
    Timeticket,
    Weapon,
    Armor,
    Gloves,
    Boots,
    Helmet,
    Accessories
}

public enum ItemOptions
{
    Hp,
    Dp,
    Ap,
    MoveSpeed,
    CriticalHit,
    HpGen
}

public enum ItemGrade
{
    None,
    Normal,
    Magic,
    Elite,
    Rare,
    Epic,
    Legendary
}

public enum SkillApplyType
{
    Active,
    Awaken,
    Passive
}

public enum SkillTargetType
{
    // active
    Single,
    RandomSingle,
    RandomPos,
    AOE,
    Around,
    FixedDirection,
    RandomArea,

    // passive
    AddProjectile,
    DotHeal,
    AddHP,
    AddDamage,
    CollectableRange,
    AddDef,
}

public enum SkillRangeType
{
    None,
    Range,
    FixedRange,
    Rotating,
    Normal,
}


public enum InfoGraphic
{
    None,
    Main,
    Inventory,
    Shop,
    Ingame,
    Skill,
}

public enum AdsStates
{
    AllFree,
    Defeated,
    Clear,
    Free,
    Reroll,
}

