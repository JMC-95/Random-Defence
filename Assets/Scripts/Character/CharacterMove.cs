using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMove : MonoBehaviour
{
    private CharacterAttack characterAtk;
    private Animator anim;
    private Vector2 targetPos;          //캐릭터가 이동할 위치(Position)

    private float speed;

    void Start()
    {
        characterAtk = GetComponentInChildren<CharacterAttack>();
        anim = GetComponent<Animator>();
        targetPos = transform.position;

        speed = 4.0f;
    }

    void Update()
    {
        if ((Vector2)transform.position != targetPos)
        {
            var playerPos = transform.position;
            transform.position = Vector2.MoveTowards(playerPos, targetPos, speed * Time.deltaTime);
        }
        else anim.SetBool("Walk", false);
    }

    public void Select()
    {
        characterAtk.OnAtkRange();
    }

    public void Move()
    {
        anim.SetBool("Walk", true);

        var playerPos = transform.position;
        targetPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        characterAtk.OffAtkRange();

        if (playerPos.x < targetPos.x) transform.localScale = new Vector3(-2, 2, -1);
        else transform.localScale = new Vector3(2, 2, -1);
    }

    public void MonsterHit()
    {
        characterAtk.MonsterHit();
    }
}
