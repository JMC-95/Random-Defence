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
        LoadCharacterPrefabs();
    }

    public void LoadCharacterPrefabs()
    {
        characterPrefabs = new GameObject[Type.Character.Max];

        for (int i = 0; i < Type.Character.Max; ++i)
        {
            characterPrefabs[i] = Resources.Load(bagicPath + Type.Character.ToString(i)) as GameObject;
        }
    }

    public void CreateCharacter()
    {
        var ran = Random.Range(0, 8);
        Instantiate(characterPrefabs[ran], new Vector3(-3.0f, 0, 0), transform.localRotation);
    }

    public void CombineCharacter_1()
    {
        var nID = UIManager.instance.resultID;
        Instantiate(characterPrefabs[nID], new Vector3(-3.0f, 0, 0), transform.localRotation);
    }

    public void CombineCharacter_2()
    {
        var nID2 = UIManager.instance.resultID2;
        Instantiate(characterPrefabs[nID2], new Vector3(-3.0f, 0, 0), transform.localRotation);
    }
}
