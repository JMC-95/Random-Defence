using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSelect : MonoBehaviour
{
    public GameObject target;
    public CharacterMove character;

    private int layerMask;
    private RaycastHit2D hit;

    public bool isSelect = false;

    void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("Player");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !UIManager.instance.isCombineTable)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0f, layerMask);

                if (hit.collider != null && hit.collider.tag == "Player" && !isSelect)
                {
                    target = hit.collider.gameObject;
                    character = target.GetComponent<CharacterMove>();

                    UIManager.instance.UpdateInfo();
                    character.Select();
                    isSelect = true;
                }
                else
                {
                    if (target != null && isSelect)
                    {
                        UIManager.instance.UpdateInfoExit();
                        character.Move();
                        isSelect = false;
                    }
                }
            }
        }
    }
}
