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

public enum ItemType
{
    Gold,
    Gem,
    Core,
    Weapon,
    Armor,
    Gloves,
    Boots,
    Helmet,
    Accessorries
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
