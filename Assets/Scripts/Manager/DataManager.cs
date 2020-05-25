using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public enum eHero
{
    None,
    //1성
    Warrior = 1,
    Archer,
    Wizard,
    Fighter,
    Lancer,
    Monk,
    Thief,
    Mechanic,
    //2성
    Priest = 9,
    Engineer,
    Samurai,
    Assassin,
    Paladin,
    Bard,
    Sorcerer,
    Sniper,
    //3성
    Avenger = 17,
    Valkyrie,
    Grappler,
    Slayer,
    Hunter,
    Berserker,
    RuneKnight,
    //4성
    Crusaders = 24,
    Blackguards,
    Bishop,
    DragonKnight,
    Max
}

[Serializable]
public class HeroData
{
    public int nID { get; set; }
    public string sProperty { get; set; }
    public int nRating { get; set; }
    public string sName { get; set; }
    public int nPower { get; set; }
    public float fAspeed { get; set; }
    public int nSkillPow { get; set; }
    public string sSkilldes { get; set; }
}

[Serializable]
public class CombineData
{
    public int nID { get; set; }
    public string sName { get; set; }
    public int nMaterial1 { get; set; }
    public int nMaterial2 { get; set; }
    public int nMaterial3 { get; set; }
    public int nMaterial4 { get; set; }
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;

    public HeroData[] heroData;
    public CombineData[] combineData;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        LoadHeroData();
        LoadCombineData();
    }

    public void LoadHeroData()
    {
        string path = Application.dataPath + "/Scripts/JsonData/HeroData.json";
        string jsonData = File.ReadAllText(path);

        heroData = JsonConvert.DeserializeObject<HeroData[]>(jsonData);
    }

    public void LoadCombineData()
    {
        string path = Application.dataPath + "/Scripts/JsonData/CombineData.json";
        string jsonData = File.ReadAllText(path);

        combineData = JsonConvert.DeserializeObject<CombineData[]>(jsonData);
    }
}
