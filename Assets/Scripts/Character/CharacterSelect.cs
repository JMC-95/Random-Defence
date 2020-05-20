using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    GameObject target;
    CharacterMove character;

    bool isSelect = false;

    void Update()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Player");

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0f, layerMask);

        if (Input.GetMouseButtonDown(0))
        {
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
