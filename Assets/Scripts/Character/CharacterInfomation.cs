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
        hero = DataManager.instance.heroData[nID];

        if (0 <= hero.nID && hero.nID < 8)
        {
            sProperty = hero.sProperty;
            nRating = hero.nRating;
            sName = hero.sName;
            nPower = hero.nPower + UIManager.instance.powerUp1;
            fAspeed = hero.fAspeed;
            nSkillPow = hero.nSkillPow;
            sSkilldes = hero.sSkilldes;
            nLevel = 1 + UIManager.instance.levelUp1;
        }
        else if (8 <= hero.nID && hero.nID < 16)
        {
            sProperty = hero.sProperty;
            nRating = hero.nRating;
            sName = hero.sName;
            nPower = hero.nPower + UIManager.instance.powerUp2;
            fAspeed = hero.fAspeed;
            nSkillPow = hero.nSkillPow;
            sSkilldes = hero.sSkilldes;
            nLevel = 1 + UIManager.instance.levelUp2;
        }
        else if (16 <= hero.nID && hero.nID < 23)
        {
            sProperty = hero.sProperty;
            nRating = hero.nRating;
            sName = hero.sName;
            nPower = hero.nPower + UIManager.instance.powerUp3;
            fAspeed = hero.fAspeed;
            nSkillPow = hero.nSkillPow;
            sSkilldes = hero.sSkilldes;
            nLevel = 1 + UIManager.instance.levelUp3;
        }
        else
        {
            sProperty = hero.sProperty;
            nRating = hero.nRating;
            sName = hero.sName;
            nPower = hero.nPower + UIManager.instance.powerUp4;
            fAspeed = hero.fAspeed;
            nSkillPow = hero.nSkillPow;
            sSkilldes = hero.sSkilldes;
            nLevel = 1 + UIManager.instance.levelUp4;
        }
    }
}
