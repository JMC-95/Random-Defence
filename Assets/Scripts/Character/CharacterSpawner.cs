using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    private GameManager gameManager;

    [Header("Character Resources")]
    public GameObject[] characterPrefabs;
    public string bagicPath = "Prefabs/Player/";

    void Start()
    {
        characterPrefabs = new GameObject[Type.Character.Max];

        for (int i = 0; i < Type.Character.Max; ++i)
        {
            characterPrefabs[i] = Resources.Load(bagicPath + Type.Character.ToString(i)) as GameObject;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(characterPrefabs[0], transform.position, transform.localRotation);
            Instantiate(characterPrefabs[1], transform.position, transform.localRotation);
        }
    }
}
