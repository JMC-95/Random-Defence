using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    private List<GameObject> collMonsters = new List<GameObject>();

    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private MonsterDamage monsterDamage;

    private float fillAmount;
    private float fTime = 0;
    private float delay = 0;
    public float atkSpeed = 1f;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        fTime += Time.deltaTime;

        if (collMonsters.Count > 0)
        {
            GameObject target = collMonsters[0];
            monsterDamage = target.GetComponent<MonsterDamage>();

            if (transform.parent.tag == "Player")
            {
                delay = 1 / atkSpeed;

                if (target != null && fTime > delay)
                {
                    //몬스터의 위치에 따라서 캐릭터의 공격 방향이 결정된다.
                    if (target.transform.position.x < transform.parent.position.x)
                        transform.parent.localScale = new Vector3(2, 2, -1);
                    else
                        transform.parent.localScale = new Vector3(-2, 2, -1);

                    fTime = 0.0f;
                    anim.SetBool("Attack", true);
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

    public void MonsterHit()
    {
        //몬스터의 체력이 줄어든다.
        monsterDamage.curHp -= 20;
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
