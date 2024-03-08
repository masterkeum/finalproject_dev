using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;



public class CSVReader : MonoBehaviour
{
    // 테이블에 맞게 수정
    /*
    https://gist.github.com/kennir/77a216141ca8fb9efacaf52cebe43307

    */

    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };
    
    public static List<Dictionary<string, object>> Read(string file)
    {
        var list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load(file) as TextAsset;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {

            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }
}


/*
    TODO : 하위 내용은 차후 파일 분할

*/


public enum QuestType
{
    None,
    MonsterHunt,
    PlayerLevel,
}

public enum QuestState
{
    NotStarted,
    InProgress,
    ObjectiveCompleted,
    Completed
}

[Serializable]
public class Quest
{
    public int questId;
    public string questName;
    public string questDesc;
    public string goalDesc;
    public QuestType type; // QuestType이 정의된 enum이 필요합니다.
    public int checkId;
    public int checkGoal;
    public int rewardExp;
    public int rewardItem;
    public int rewardCount;
    public string prefabFile;
    public string atlasFile;
    public string atlasName;
    public string iconName;
    public string rewardAtlasName;
    public string rewardIconName;

    public QuestState state;

    public Quest(int questId, string questName, string questDesc, string goalDesc, QuestType type, int checkId, int checkGoal, int rewardExp, int rewardItem, int rewardCount, string prefabFile, string atlasFile, string atlasName, string iconName, string rewardAtlasName, string rewardIconName)
    {
        this.questId = questId;
        this.questName = questName;
        this.questDesc = questDesc;
        this.goalDesc = goalDesc;
        this.type = type;
        this.checkId = checkId;
        this.checkGoal = checkGoal;
        this.rewardExp = rewardExp;
        this.rewardItem = rewardItem;
        this.rewardCount = rewardCount;
        this.prefabFile = prefabFile;
        this.atlasFile = atlasFile;
        this.atlasName = atlasName;
        this.iconName = iconName;
        this.rewardAtlasName = rewardAtlasName;
        this.rewardIconName = rewardIconName;
    }
}