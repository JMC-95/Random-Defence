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
    Gunner,
    //2성
    Priest = 9,
    Samurai,
    Engineer,
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
    End
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

    public void GetTable()
    {
    }
}

public class DataController : MonoBehaviour
{
    public static DataController instance = null;

    public HeroData[] heroData;

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
    }

    public void LoadHeroData()
    {
        string path = Application.dataPath + "/Scripts/JsonData/HeroData.json";
        string jsonData = File.ReadAllText(path);

        heroData = JsonConvert.DeserializeObject<HeroData[]>(jsonData);
    }
}
