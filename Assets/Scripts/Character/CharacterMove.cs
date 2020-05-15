using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterMove : MonoBehaviour
{
    private CharacterAttack atkRange;   //캐릭터의 공격범위 클래스
    private Animator anim;

    private Vector2 targetPos;          //캐릭터가 이동할 위치(Position)
    private float speed = 4.0f;

    private bool isSelect = false;

    void Start()
    {
        atkRange = GetComponentInChildren<CharacterAttack>();
        anim = GetComponent<Animator>();

        targetPos = transform.position;
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero);

        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null && hit.collider.tag == "Player")
            {
                atkRange.OnAtkRange();
                isSelect = true;
            }
            else
            {
                if (isSelect)
                {
                    anim.SetBool("Walk", true);

                    var playerPos = transform.position;
                    targetPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    atkRange.OffAtkRange();
                    isSelect = false;

                    if (playerPos.x < targetPos.x) transform.localScale = new Vector3(-2, 2, -1);
                    else transform.localScale = new Vector3(2, 2, -1);
                }
            }
        }

        if ((Vector2)transform.position != targetPos)
        {
            var playerPos = transform.position;
            transform.position = Vector2.MoveTowards(playerPos, targetPos, speed * Time.deltaTime);
        }
        else anim.SetBool("Walk", false);
    }
}
