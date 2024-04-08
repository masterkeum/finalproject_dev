using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class DataManager : SingletoneBase<DataManager>
{
    [ReadOnly, SerializeField] private string _pidStr;

    public bool IsReady { get; private set; } = false;
    JsonSerializerSettings settings = new()
    {
        FloatFormatHandling = FloatFormatHandling.String,
        Converters = { new StringEnumConverter() }
    };

    // 데이터 파일 경로 Assets/Resources/DataTables/DataTable.json
    private string DataFilePath = "DataTables/DataTable";

    public Dictionary<string, int> _InitParam;

    public Dictionary<int, CharacterInfo> characterInfoDict;
    public Dictionary<int, StageList> stageListDict;
    public Dictionary<int, List<StageInfoTable>> stageInfoDict;
    public Dictionary<int, PlayerLevel> playerLevelDict;
    public Dictionary<int, PlayerIngameLevel> playerIngameLevelDict;
    public Dictionary<int, SkillTable> skillTableDict;
    public Dictionary<int, ItemTable> itemTableDict;
    public Dictionary<int, LevelGacha> levelGachaDict;
    public Dictionary<int, Shop> shopDict;
    public Dictionary<int, Mimiclevel> mimiclevelDict;

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
        //DataTable jsonData = JsonUtility.FromJson<DataTable>(json);
        DataTable jsonData = JsonConvert.DeserializeObject<DataTable>(json, settings);

        // init 데이터
        _InitParam = new Dictionary<string, int>();
        foreach (InitializeParam initparam in jsonData.InitializeParam)
        {
            _InitParam.Add(initparam.code, initparam.param1);
        }

        // 캐릭터 정보
        characterInfoDict = new Dictionary<int, CharacterInfo>();
        foreach (CharacterInfo characterInfo in jsonData.CharacterInfo)
        {
            characterInfoDict.Add(characterInfo.uid, characterInfo);
            //characterInfo.monsterLevelData = new Dictionary<int, MonsterLevel>();
        }
        foreach (MonsterLevel monsterLevel in jsonData.MonsterLevel)
        {
            if (characterInfoDict.ContainsKey(monsterLevel.uid))
            {
                characterInfoDict[monsterLevel.uid].monsterLevelData.Add(monsterLevel.level, monsterLevel);
            }
            else
            {
                Debug.LogError($"monsterLevel 데이터 불량 {monsterLevel.uid}");
            }
        }
        Debug.Log("케릭터 정보 로드 완료");

        // 스테이지 정보
        stageListDict = new Dictionary<int, StageList>();
        foreach (StageList stageList in jsonData.StageList)
        {
            stageListDict.Add(stageList.stageId, stageList);
        }
        Debug.Log("스테이지 리스트 정보 로드 완료");

        // 스테이지 세부 정보
        stageInfoDict = new Dictionary<int, List<StageInfoTable>>();
        foreach (StageInfoTable stageInfoTable in jsonData.StageInfoTable)
        {
            int stageId = stageInfoTable.stageId;
            if (stageInfoDict.ContainsKey(stageId))
            {
                stageInfoDict[stageId].Add(stageInfoTable);
            }
            else
            {
                // 새로 생성
                stageInfoDict.Add(stageId, new List<StageInfoTable>());
                stageInfoDict[stageId].Add(stageInfoTable);
            }
        }
        Debug.Log("스테이지 정보 로드 완료");
        // 계정레벨
        playerLevelDict = new Dictionary<int, PlayerLevel>();
        foreach (PlayerLevel playerLevel in jsonData.PlayerLevel)
        {
            playerLevelDict.Add(playerLevel.level, playerLevel);
        }

        // 인게임 유저 레벨 정보
        playerIngameLevelDict = new Dictionary<int, PlayerIngameLevel>();
        foreach (PlayerIngameLevel playerIngameLevel in jsonData.PlayerIngameLevel)
        {
            playerIngameLevelDict.Add(playerIngameLevel.level, playerIngameLevel);
        }
        Debug.Log("유저 레벨 정보 로드 완료");

        //미믹레벨
        mimiclevelDict = new Dictionary<int, Mimiclevel>();
        foreach(Mimiclevel mimiclevel in jsonData.Mimiclevel)
        {
            mimiclevelDict.Add(mimiclevel.mimiclevel, mimiclevel);
        }

        // 스킬 정보
        skillTableDict = new Dictionary<int, SkillTable>();
        foreach (SkillTable skillTable in jsonData.SkillTable)
        {
            if (skillTable.isEnable == true)
                skillTableDict.Add(skillTable.skillId, skillTable);
        }

        //아이템정보
        itemTableDict = new Dictionary<int, ItemTable>();
        foreach (ItemTable itemTable in jsonData.ItemTable)
        {
            itemTableDict.Add(itemTable.itemId, itemTable);
        }
        Debug.Log("아이템데이터 로드 완료");

        //아이템가챠 정보
        levelGachaDict = new Dictionary<int, LevelGacha>();
        foreach (LevelGacha levelGacha in jsonData.LevelGacha)
        {
            levelGachaDict.Add(levelGacha.level, levelGacha);
        }
        Debug.Log("가챠확률 정보 로드 완료");

        // 상점 상품 정보
        shopDict = new Dictionary<int, Shop>();
        foreach (Shop shop in jsonData.Shop)
        {
            shopDict.Add(shop.packageId, shop);
        }


        // GC에서 언제 가져갈지 모르니 jsonData를 명시적으로 null 로 만들거나 destroy 하고싶다.
        jsonData = null;
        IsReady = true;
    }


    public CharacterInfo GetCharacterInfo(int uid)
    {
        if (characterInfoDict.ContainsKey(uid))
            return characterInfoDict[uid];
        else
            return null;
    }

    public List<StageInfoTable> GetStageInfo(int stageId)
    {
        if (stageInfoDict.ContainsKey(stageId))
            return stageInfoDict[stageId];
        else
            return null;
    }

    public PlayerLevel GetPlayerLevel(int level)
    {
        if (playerLevelDict.ContainsKey(level))
            return playerLevelDict[level];
        else return null;
    }

    public PlayerIngameLevel GetPlayerIngameLevel(int level)
    {
        if (playerIngameLevelDict.ContainsKey(level))
            return playerIngameLevelDict[level];
        else return null;
    }

    public SkillTable GetSkillTable(int skillId)
    {
        if (skillTableDict.ContainsKey(skillId))
            return skillTableDict[skillId];
        else return null;
    }

    public ItemTable GetItemTable(int itemId)
    {
        if (itemTableDict.ContainsKey(itemId))
            return itemTableDict[itemId];
        else return null;
    }

    public LevelGacha GetLevelGacha(int level)
    {
        if (levelGachaDict.ContainsKey(level))
            return levelGachaDict[level];
        else return null;
    }

    public Shop GetShop(int packageId)
    {
        if(shopDict.ContainsKey(packageId))
            return shopDict[packageId];
        else return null;
    }

    public Mimiclevel GetMimicLevel(int level)
    {
        if(mimiclevelDict.ContainsKey(level))
            return mimiclevelDict[level];
        else return null;
    }
}

