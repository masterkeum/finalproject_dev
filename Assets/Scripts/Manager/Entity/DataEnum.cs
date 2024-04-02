using System;

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
    TimeTicket,
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
