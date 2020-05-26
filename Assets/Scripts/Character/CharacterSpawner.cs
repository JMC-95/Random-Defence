using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    private GameManager gameManager;
    private EffectManager effectManager;

    private CharacterSelect characterSelect;

    [Header("Character Resources")]
    [SerializeField] public GameObject[] characterPrefabs;
    public string bagicPath = "Prefabs/Player/";

    [Header("Object pool")]
    private int maxPool = 30;
    public Dictionary<string, List<GameObject>> characterPools;

    List<Transform> effectTransform = new List<Transform>();
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
        effectManager = EffectManager.instance;

        characterSelect = gameManager.GetComponent<CharacterSelect>();

        curCharacterCount = 0;
        maxCharacterCount = 50;

        CreateCharacter();
    }

    public void CreateCharacter()
    {
        if (curCharacterCount < maxCharacterCount)
        {
            int randHeroNum1 = Random.Range(0, 8);
            int randHeroNum2 = Random.Range(0, 8);
            string heroName1 = Type.Character.ToString(randHeroNum1);
            string heroName2 = Type.Character.ToString(randHeroNum2);

            //캐릭터 생성
            SetCreateUnit(GetCharacter(heroName1), 0, 2);
            SetCreateUnit(GetCharacter(heroName2), 2, 4);
            curCharacterCount += 2;

            //이펙트 생성
            for (int i = 0; i < effectTransform.Count; i++)
            {
                var effectObj = effectManager.GetEffect("RespawnCircle");
                effectManager.SetToEffect(effectObj, effectTransform[i]);
            }

            effectTransform.Clear();
        }
    }

    public void CombineCharacter_1()
    {
        if (curCharacterCount < maxCharacterCount)
        {
            int combineID = UIManager.instance.resultID;
            string combineName = Type.Character.ToString(combineID);
            var effectObj = effectManager.GetEffect("CombineBlast");

            //캐릭터 생성
            SetCombineUnit(GetCharacter(combineName));
            curCharacterCount += 1;

            //이펙트 생성
            effectManager.SetToEffect(effectObj, GetCharacter(combineName).transform);
        }
    }

    public void CombineCharacter_2()
    {
        if (curCharacterCount < maxCharacterCount)
        {
            int combineID = UIManager.instance.resultID2;
            string combineName = Type.Character.ToString(combineID);
            var effectObj = effectManager.GetEffect("CombineBlast");

            //캐릭터 생성
            SetCombineUnit(GetCharacter(combineName));
            curCharacterCount += 1;

            //이펙트 생성
            effectManager.SetToEffect(effectObj, GetCharacter(combineName).transform);
        }
    }

    public void SetCreateUnit(GameObject unitObj, int min, int max)
    {
        int ranPos = Random.Range(min, max);

        unitObj.transform.position = randomPos[ranPos];
        unitObj.SetActive(true);
        effectTransform.Add(unitObj.transform);
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
