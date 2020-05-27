using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GenInfomation
{
    public GenInfomation(int monsterNumber, int hp, int speed, int soul, int genCount)
    {
        MonsterName = Type.Monster.ToString(monsterNumber);
        Hp = hp;
        Speed = speed;
        Soul = soul;
        GenCount = genCount;
    }

    public string MonsterName;
    public int Hp;
    public int Speed;
    public int Soul;
    public int GenCount;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject monsterSpawner;

    public MonsterSpawner monsterSpawnerScript;

    public int soul;

    public bool isWaveEnd = true;
    public bool isGameOver = false;
    public bool isGameVictory = false;

    [Header("Monster Resources")]
    public GameObject[] monsterPrefabs;
    public string bagicPath = "Prefabs/Monster/";

    //first key : Stage, second key : Wave
    public Dictionary<int, Dictionary<int, List<GenInfomation>>> GenInfoMation;

    public int curStage;
    public int maxStage;
    public int curWave;
    public int maxWave;

    public bool stageEnd;
    public bool isSummon;

    //GenInfomation define
    private Dictionary<int, Dictionary<int, List<GenInfomation>>> GetGenInfo()
    {
        var stage = new Dictionary<int, Dictionary<int, List<GenInfomation>>>();

        //1 Wave
        var firstWave = new Dictionary<int, List<GenInfomation>>();
        //1 Wave - Monster
        var first = new List<GenInfomation>();
        first.Add(new GenInfomation(Type.Monster.Goblin, 60, 3, 50, 30));
        firstWave.Add(1, first);
        //2 Wave - Monster
        var second = new List<GenInfomation>();
        second.Add(new GenInfomation(Type.Monster.Golem, 150, 3, 100, 30));
        firstWave.Add(2, second);
        //3 Wave - Monster
        var third = new List<GenInfomation>();
        third.Add(new GenInfomation(Type.Monster.Kerberos, 300, 3, 150, 30));
        firstWave.Add(3, third);
        //4 Wave - Monster
        var fourth = new List<GenInfomation>();
        fourth.Add(new GenInfomation(Type.Monster.Minotauros, 500, 3, 200, 30));
        firstWave.Add(4, fourth);
        //5 Wave - Monster
        var fifth = new List<GenInfomation>();
        fifth.Add(new GenInfomation(Type.Monster.Troll, 1000, 3, 300, 30));
        firstWave.Add(5, fifth);
        //6 Wave - Monster
        var sixth = new List<GenInfomation>();
        sixth.Add(new GenInfomation(Type.Monster.Dragon, 10000, 3, 1000, 1));
        firstWave.Add(6, sixth);
        stage.Add(1, firstWave);

        return stage;
    }

    static public GameManager Get()
    {
        return instance;
    }

    public void LoadMonsterPrefabs()
    {
        monsterPrefabs = new GameObject[Type.Monster.Max];

        for (int i = 0; i < Type.Monster.Max; ++i)
        {
            monsterPrefabs[i] = Resources.Load(bagicPath + Type.Monster.ToString(i)) as GameObject;
        }
    }

    public void SetGenInfomation()
    {
        GenInfoMation = GetGenInfo();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    public void Start()
    {
        soul = 200;
        curStage = 1;
        maxStage = 3;
        curWave = 0;
        maxWave = 6;
        isSummon = true;
        isWaveEnd = true;
        stageEnd = true;

        monsterSpawner = GameObject.Find("MonsterSpawner");
        monsterSpawnerScript = monsterSpawner.GetComponent<MonsterSpawner>();
    }

    public void UseSoul(int cost)
    {
        soul -= cost;
    }

    public void StartStage()
    {
        stageEnd = false;
    }

    public void StartWave()
    {
        curWave += 1;
        isSummon = true;
    }

    void FixedUpdate()
    {
        if (isGameOver || isGameVictory)
        {
            return;
        }

        if ((monsterSpawnerScript.curMonster > monsterSpawnerScript.maxMonster) ||
            (curWave == 6 && UIManager.instance.time < 0))
        {
            isGameOver = true;
        }

        if (!isGameOver && !isWaveEnd && !isSummon)
        {
            monsterSpawnerScript.portal.SetActive(true);

            if (monsterSpawnerScript.genCount == monsterSpawnerScript.genCountLimit)
            {
                isWaveEnd = true;

                if (isWaveEnd) monsterSpawnerScript.portal.SetActive(false);
            }
        }
        else
        {
            if (!stageEnd)
            {
                //if (curWave == 6 && monsterSpawnerScript.curMonster != 0)
                //{
                //    return;
                //}

                if (curWave == 6 && isSummon)
                {
                    UIManager.instance.BossText.gameObject.SetActive(true);
                }

                if (isSummon)
                {
                    isWaveEnd = false;
                    isSummon = false;
                    monsterSpawnerScript.ResetGenInfo();
                    StartCoroutine(monsterSpawnerScript.CreateMonster());
                }
            }

            if (curWave == maxWave && monsterSpawnerScript.genCount != 0 &&
                monsterSpawnerScript.curMonster == 0)
            {
                curWave = 0;
                curStage += 1;
                isGameVictory = true;
            }
        }
    }
}
