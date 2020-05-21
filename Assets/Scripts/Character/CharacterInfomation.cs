using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfomation : MonoBehaviour
{
    DataController d;
    public HeroData Knight;
    public int nID;
    public string sProperty;
    public int nRating;
    public string sName;
    public int nPower;
    public float fAspeed;
    public int nSkillPow;
    public string sSkilldes;

    void Start()
    {
        var hTb = DataController.instance.heroData[0];

        foreach(var h in hTb.GetTable())

        d = DataController.instance;
        Knight = DataController.instance.heroData[nID];
        sProperty = Knight.sProperty;
        sName = Knight.sName;
    }
}
