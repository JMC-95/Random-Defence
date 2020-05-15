using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private List<GameObject> collMonsters = new List<GameObject>();

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

    }

    public void OnAtkRange()
    {
        spriteRenderer.enabled = true;
    }

    public void OffAtkRange()
    {
        spriteRenderer.enabled = false;
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
