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

    void Start()
    {
        hero = DataController.instance.heroData[nID];

        sProperty = hero.sProperty;
        nRating = hero.nRating;
        sName = hero.sProperty;
        nPower = hero.nPower;
        fAspeed = hero.fAspeed;
        nSkillPow = hero.nSkillPow;
        sSkilldes = hero.sSkilldes;
    }
}
