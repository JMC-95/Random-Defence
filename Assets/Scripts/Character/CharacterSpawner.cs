using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    private GameManager gameManager;
    private CharacterSelect characterSelect;

    [Header("Character Resources")]
    [SerializeField] public GameObject[] characterPrefabs;
    public string bagicPath = "Prefabs/Player/";

    [Header("Object pool")]
    private int maxPool = 30;
    public Dictionary<string, List<GameObject>> characterPools;

    private Vector3[] randomPos = { new Vector3(-1.5f, 0, 0), new Vector3(1.5f, 0, 0),
                                    new Vector3(0, -1.5f, 0), new Vector3(0, 1.5f, 0)};

    public int curCharacterCount;
    public int maxCharacterCount;

    void Awake()
    {
        CreatePooling();
    }

    void Start()
    {
        gameManager = GameManager.Get();
        characterSelect = gameManager.GetComponent<CharacterSelect>();

        curCharacterCount = 0;
        maxCharacterCount = 50;

        CreateCharacter();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
        }
    }

    public void CreateCharacter()
    {
        if (curCharacterCount < maxCharacterCount)
        {
            int createRandomHero1 = Random.Range(0, 8);
            int createRandomHero2 = Random.Range(0, 8);
            string heroName1 = Type.Character.ToString(createRandomHero1);
            string heroName2 = Type.Character.ToString(createRandomHero2);

            SetCreateUnit(GetCharacter(heroName1));
            SetCreateUnit(GetCharacter(heroName2));
            curCharacterCount += 2;
        }
    }

    public void CombineCharacter_1()
    {
        if (curCharacterCount < maxCharacterCount)
        {
            int combineID = UIManager.instance.resultID;
            string combineName = Type.Character.ToString(combineID);

            SetCombineUnit(GetCharacter(combineName));
            curCharacterCount += 1;
        }
    }

    public void CombineCharacter_2()
    {
        if (curCharacterCount < maxCharacterCount)
        {
            int combineID = UIManager.instance.resultID2;
            string combineName = Type.Character.ToString(combineID);

            SetCombineUnit(GetCharacter(combineName));
            curCharacterCount += 1;
        }
    }

    public void SetCreateUnit(GameObject unitObj)
    {
        int ranPos = Random.Range(0, 4);

        unitObj.transform.position = randomPos[ranPos];
        unitObj.SetActive(true);
    }

    public void SetCombineUnit(GameObject unitObj)
    {
        unitObj.transform.position = transform.position;
        unitObj.SetActive(true);
    }

    public GameObject GetCharacter(string characterName)
    {
        var list = characterPools[characterName];

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].activeSelf == false)
            {
                return list[i];
            }
        }

        return null;
    }

    public void CreatePooling()
    {
        GameObject objectPools = new GameObject("CharacterPooling");

        characterPools = new Dictionary<string, List<GameObject>>();
        LoadCharacterPrefabs();

        for (int i = 0; i < Type.Character.Max; ++i)
        {
            var characterName = Type.Character.ToString(i);

            List<GameObject> characterPool = new List<GameObject>();

            for (int j = 0; j < maxPool; j++)
            {
                var character = Instantiate<GameObject>(characterPrefabs[i], objectPools.transform) as GameObject;

                characterPool.Add(character);
                character.SetActive(false);
            }

            characterPools.Add(characterName, characterPool);
        }
    }

    public void LoadCharacterPrefabs()
    {
        characterPrefabs = new GameObject[Type.Character.Max];

        for (int i = 0; i < Type.Character.Max; ++i)
        {
            characterPrefabs[i] = Resources.Load(bagicPath + Type.Character.ToString(i)) as GameObject;
        }
    }
}
