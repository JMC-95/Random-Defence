using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfomation : MonoBehaviour
{
    private HeroData hero;

    public int nID;
    public string sProperty;
    public int nRating;
    public string sName;
    public int nPower;
    public float fAspeed;
    public int nSkillPow;
    public string sSkilldes;
    public int nLevel;
    public int nSell;

    void Start()
    {
        hero = DataController.instance.heroData[nID];

        sProperty = hero.sProperty;
        nRating = hero.nRating;
        sName = hero.sName;
        nPower = hero.nPower;
        fAspeed = hero.fAspeed;
        nSkillPow = hero.nSkillPow;
        sSkilldes = hero.sSkilldes;
        nLevel = 1;
    }
}
