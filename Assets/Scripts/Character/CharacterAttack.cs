using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    private EffectManager effectManager;

    private List<GameObject> collMonsters = new List<GameObject>();

    private GameObject monster;

    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private CharacterInfomation playerInfo;
    private MonsterDamage monsterDamage;

    private float fillAmount;
    private float atkSpeed;
    private float delay;
    public float fTime;

    void Start()
    {
        effectManager = EffectManager.instance;

        anim = GetComponentInParent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInfo = GetComponentInParent<CharacterInfomation>();

        atkSpeed = playerInfo.fAspeed;
        delay = 1 / atkSpeed;
        fTime = 0.0f;
    }

    void Update()
    {
        fTime += Time.deltaTime;

        if (collMonsters.Count > 0)
        {
            GameObject target = collMonsters[0];
            monster = target;
            monsterDamage = target.GetComponent<MonsterDamage>();

            if (transform.parent.tag == "Player")
            {
                if (target != null && fTime > delay)
                {
                    anim.SetBool("Attack", true);
                }
                else
                {
                    anim.SetBool("Attack", false);
                }

                for (int i = 0; i < collMonsters.Count; ++i)
                {
                    if (!collMonsters[i].activeInHierarchy)
                    {
                        collMonsters.Remove(collMonsters[i]);
                    }
                }
            }
        }
        else
        {
            anim.SetBool("Attack", false);
        }
    }

    public void OnAtkRange()
    {
        spriteRenderer.enabled = true;
    }

    public void OffAtkRange()
    {
        spriteRenderer.enabled = false;
    }

    public void AttackEffect()
    {
        int playerID = playerInfo.nID + 1;
        Vector3 leftPos = new Vector3(-1, 1, 1);
        Vector3 rightPos = new Vector3(1, 1, 1);

        if (playerID == (int)eHero.Archer)
        {
            var atkEffect = effectManager.GetEffect("ArcherFire");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                effectManager.SetToEffect(atkEffect, monster.transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                effectManager.SetToEffect(atkEffect, monster.transform);
            }
        }
        else if (playerID == (int)eHero.Assassin)
        {
            var atkEffect = effectManager.GetEffect("AssassinSlash");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                atkEffect.transform.localScale = rightPos;
                effectManager.SetToEffect(atkEffect, transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                atkEffect.transform.localScale = leftPos;
                effectManager.SetToEffect(atkEffect, transform);
            }
        }
        else if (playerID == (int)eHero.Avenger)
        {
            var atkEffect = effectManager.GetEffect("AvengerSlash");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                atkEffect.transform.localScale = rightPos;
                effectManager.SetToEffect(atkEffect, transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                atkEffect.transform.localScale = leftPos;
                effectManager.SetToEffect(atkEffect, transform);
            }
        }
        else if (playerID == (int)eHero.Bard)
        {
            var atkEffect = effectManager.GetEffect("BardFire");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                effectManager.SetToEffect(atkEffect, monster.transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                effectManager.SetToEffect(atkEffect, monster.transform);
            }
        }
        else if (playerID == (int)eHero.Berserker)
        {
            var atkEffect = effectManager.GetEffect("BerserkerSlash");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                atkEffect.transform.localScale = rightPos;
                effectManager.SetToEffect(atkEffect, transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                atkEffect.transform.localScale = leftPos;
                effectManager.SetToEffect(atkEffect, transform);
            }
        }
        else if (playerID == (int)eHero.Bishop)
        {
            var atkEffect = effectManager.GetEffect("BishopFire");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                effectManager.SetToEffect(atkEffect, monster.transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                effectManager.SetToEffect(atkEffect, monster.transform);
            }
        }
        else if (playerID == (int)eHero.Blackguards)
        {
            var atkEffect = effectManager.GetEffect("BlackguardsSlash");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                atkEffect.transform.localScale = new Vector3(1, 1, 1);
                effectManager.SetToEffect(atkEffect, transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                atkEffect.transform.localScale = new Vector3(1, -1, 1);
                effectManager.SetToEffect(atkEffect, transform);
            }
        }
        else if (playerID == (int)eHero.Crusaders)
        {
            var atkEffect = effectManager.GetEffect("CrusadersSlash");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                atkEffect.transform.localScale = new Vector3(1, 1, 1);
                effectManager.SetToEffect(atkEffect, transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                atkEffect.transform.localScale = new Vector3(1, -1, 1);
                effectManager.SetToEffect(atkEffect, transform);
            }
        }
        else if (playerID == (int)eHero.DragonKnight)
        {
            var atkEffect = effectManager.GetEffect("DragonKnightSlash");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                effectManager.SetToEffect(atkEffect, transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                effectManager.SetToEffect(atkEffect, transform);
            }
        }
        else if (playerID == (int)eHero.Engineer)
        {
            var atkEffect = effectManager.GetEffect("EngineerFire");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                effectManager.SetToEffect(atkEffect, monster.transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                effectManager.SetToEffect(atkEffect, monster.transform);
            }
        }
        else if (playerID == (int)eHero.Fighter)
        {
            var atkEffect = effectManager.GetEffect("FighterSlash");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                atkEffect.transform.localScale = rightPos;
                effectManager.SetToEffect(atkEffect, transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                atkEffect.transform.localScale = leftPos;
                effectManager.SetToEffect(atkEffect, transform);
            }
        }
        else if (playerID == (int)eHero.Grappler)
        {
            var atkEffect = effectManager.GetEffect("GrapplerSlash");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                atkEffect.transform.localScale = rightPos;
                effectManager.SetToEffect(atkEffect, transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                atkEffect.transform.localScale = leftPos;
                effectManager.SetToEffect(atkEffect, transform);
            }
        }
        else if (playerID == (int)eHero.Hunter)
        {
            var atkEffect = effectManager.GetEffect("HunterFire");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                atkEffect.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                effectManager.SetToEffect(atkEffect, monster.transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                atkEffect.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
                effectManager.SetToEffect(atkEffect, monster.transform);
            }
        }
        else if (playerID == (int)eHero.Lancer)
        {
            var atkEffect = effectManager.GetEffect("LancerSlash");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                atkEffect.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                effectManager.SetToEffect(atkEffect, transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                atkEffect.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                effectManager.SetToEffect(atkEffect, transform);
            }
        }
        else if (playerID == (int)eHero.Mechanic)
        {
            var atkEffect = effectManager.GetEffect("MechanicFire");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                effectManager.SetToEffect(atkEffect, monster.transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                effectManager.SetToEffect(atkEffect, monster.transform);
            }
        }
        else if (playerID == (int)eHero.Monk)
        {
            var atkEffect = effectManager.GetEffect("MonkFire");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                effectManager.SetToEffect(atkEffect, monster.transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                effectManager.SetToEffect(atkEffect, monster.transform);
            }
        }
        else if (playerID == (int)eHero.Paladin)
        {
            var atkEffect = effectManager.GetEffect("PaladinSlash");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                atkEffect.transform.localScale = rightPos;
                effectManager.SetToEffect(atkEffect, transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                atkEffect.transform.localScale = leftPos;
                effectManager.SetToEffect(atkEffect, transform);
            }
        }
        else if (playerID == (int)eHero.Priest)
        {
            var atkEffect = effectManager.GetEffect("PriestFire");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                effectManager.SetToEffect(atkEffect, monster.transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                effectManager.SetToEffect(atkEffect, monster.transform);
            }
        }
        else if (playerID == (int)eHero.RuneKnight)
        {
            var atkEffect = effectManager.GetEffect("RuneKnightSlash");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                atkEffect.transform.localScale = rightPos;
                effectManager.SetToEffect(atkEffect, transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                atkEffect.transform.localScale = leftPos;
                effectManager.SetToEffect(atkEffect, transform);
            }
        }
        else if (playerID == (int)eHero.Samurai)
        {
            var atkEffect = effectManager.GetEffect("SamuraiSlash");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                atkEffect.transform.localScale = rightPos;
                effectManager.SetToEffect(atkEffect, transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                atkEffect.transform.localScale = leftPos;
                effectManager.SetToEffect(atkEffect, transform);
            }
        }
        else if (playerID == (int)eHero.Slayer)
        {
            var atkEffect = effectManager.GetEffect("SlayerSlash");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                atkEffect.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                effectManager.SetToEffect(atkEffect, transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                atkEffect.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                effectManager.SetToEffect(atkEffect, transform);
            }
        }
        else if (playerID == (int)eHero.Sniper)
        {
            var atkEffect = effectManager.GetEffect("SniperFire");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                effectManager.SetToEffect(atkEffect, monster.transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                effectManager.SetToEffect(atkEffect, monster.transform);
            }
        }
        else if (playerID == (int)eHero.Sorcerer)
        {
            var atkEffect = effectManager.GetEffect("SorcererFire");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                effectManager.SetToEffect(atkEffect, monster.transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                effectManager.SetToEffect(atkEffect, monster.transform);
            }
        }
        else if (playerID == (int)eHero.Thief)
        {
            var atkEffect = effectManager.GetEffect("ThiefSlash");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                atkEffect.transform.localScale = rightPos;
                effectManager.SetToEffect(atkEffect, transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                atkEffect.transform.localScale = leftPos;
                effectManager.SetToEffect(atkEffect, transform);
            }
        }
        else if (playerID == (int)eHero.Valkyrie)
        {
            var atkEffect = effectManager.GetEffect("ValkyrieSlash");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                atkEffect.transform.localScale = rightPos;
                effectManager.SetToEffect(atkEffect, transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                atkEffect.transform.localScale = leftPos;
                effectManager.SetToEffect(atkEffect, transform);
            }
        }
        else if (playerID == (int)eHero.Warrior)
        {
            var atkEffect = effectManager.GetEffect("WarriorSlash");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                atkEffect.transform.localScale = rightPos;
                effectManager.SetToEffect(atkEffect, transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                atkEffect.transform.localScale = leftPos;
                effectManager.SetToEffect(atkEffect, transform);
            }
        }
        else if (playerID == (int)eHero.Wizard)
        {
            var atkEffect = effectManager.GetEffect("WizardFire");

            if (monster.transform.position.x < transform.parent.position.x)
            {
                transform.parent.localScale = new Vector3(2, 2, -1);
                effectManager.SetToEffect(atkEffect, monster.transform);
            }
            else
            {
                transform.parent.localScale = new Vector3(-2, 2, -1);
                effectManager.SetToEffect(atkEffect, monster.transform);
            }
        }
    }

    public void MonsterHit()
    {
        //몬스터의 체력이 줄어든다.
        monsterDamage.curHp -= playerInfo.nPower;
        monsterDamage.hpBarImage.fillAmount = monsterDamage.curHp / (float)monsterDamage.initHp;

        //몬스터의 체력 비율에 따라 색상이 바뀐다.
        fillAmount = monsterDamage.hpBarImage.fillAmount;

        if (0.4 < fillAmount && fillAmount < 1) monsterDamage.hpBarImage.color = new Color(255, 255, 0);
        else if (fillAmount <= 0.4) monsterDamage.hpBarImage.color = new Color(255, 0, 0);

        //피격시 몬스터의 색상이 바뀐다.
        if (monsterDamage.curHp > 0) monsterDamage.skin.material.color = Color.red;

        //체력이 0이하면 사라진다.
        if (monsterDamage.curHp <= 0)
        {
            monsterDamage.GetSoul();
            monsterDamage.Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            collMonsters.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (GameObject monster in collMonsters)
        {
            if (monster == collision.gameObject)
            {
                collMonsters.Remove(monster);
                break;
            }
        }
    }
}
