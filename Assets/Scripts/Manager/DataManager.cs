using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : SingletoneBase<DataManager>
{
    [ReadOnly, SerializeField] private string _pidStr;

    // 데이터 파일 경로
    private string unitDataFilePath = "DataTable/UnitTable.csv";
    public Dictionary<int, UnitData> itemDataDictionary { get; private set; }

    protected override void Init()
    {
        _pidStr = _pid.ToString();
        base.Init();


        /*
            데이터 로드해서 세팅
        
            NOTE: 데이터를 어떻게 관리할 것인가.
            1. Json 으로
            2. CSV to SO
            3. csvHelper 등 외부 ?
            4. https://bravenewmethod.com/2014/09/13/lightweight-csv-reader-for-unity/

        */

        // 데이터 관리
        //ReadUnitData();

        // 프리팹 관리
    }


    private void ReadUnitData()
    {
        itemDataDictionary = new Dictionary<int, UnitData>();
        TextAsset TextFile = Resources.Load<TextAsset>(unitDataFilePath);




        //string[] lines = File.ReadAllLines(unitDataFilePath);
        //for (int i = 3; i < lines.Length; i++)
        //{
        //    string[] fields = lines[i].Split(',');
        //    int uid = int.Parse(fields[0]);
        //    string character = fields[1];
        //    string nameAlias = fields[2];
        //    string descAlias = fields[3];
        //    int HP = int.Parse(fields[4]);
        //    int atk = int.Parse(fields[5]);
        //    string prefabFileName = fields[6];
        //    string prefabFilePath = fields[7];
        //    int defaultCapacity = int.Parse(fields[8]);
        //    int maxSize = int.Parse(fields[9]);

        //    UnitData unitData = new UnitData(uid, character, nameAlias, descAlias, HP, atk, prefabFileName, prefabFilePath, defaultCapacity, maxSize);

        //    itemDataDictionary.Add(uid, unitData);
        //}
    }

}

public class UnitData
{
    public int uid;
    public string character;
    public string nameAlias;
    public string descAlias;
    public int HP;
    public int atk;
    public string prefabFileName;
    public string prefabFilePath;
    public int defaultCapacity;
    public int maxSize;

    public UnitData(int _uid, string _character, string _nameAlias, string _descAlias, int _HP, int _atk, string _prefabFileName, string _prefabFilePath, int _defaultCapacity, int _maxSize)
    {
        uid = _uid;
        character = _character;
        nameAlias = _nameAlias;
        descAlias = _descAlias;
        HP = _HP;
        atk = _atk;
        prefabFileName = _prefabFileName;
        prefabFilePath = _prefabFilePath;
        defaultCapacity = _defaultCapacity;
        maxSize = _maxSize;
    }
}
