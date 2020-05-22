using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    GameObject target;
    CharacterMove character;

    int layerMask;
    RaycastHit2D hit;

    bool isSelect = false;

    void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("Player");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0f, layerMask);

            if (hit.collider != null && hit.collider.tag == "Player" && !isSelect)
            {
                target = hit.collider.gameObject;
                character = target.GetComponent<CharacterMove>();

                character.Select();
                isSelect = true;
            }
            else
            {
                if (target != null && isSelect)
                {
                    character.Move();
                    isSelect = false;
                }
            }
        }
    }
}
