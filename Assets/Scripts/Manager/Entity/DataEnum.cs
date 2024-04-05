public enum GameState
{
    None,
    Intro,
    Loading,
    Main,
    IngameStart,
    IngameEnd,
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
    Single,
    RandomSingle,
    RandomPos,
    AOE,
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
