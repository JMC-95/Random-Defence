using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct GenInfomation
{
    public GenInfomation(int monsterNumber, int hp, int gold, int genCount)
    {
        MonsterName = Type.Monster.ToString(monsterNumber);
        Hp = hp;
        Gold = gold;
        GenCount = genCount;
    }

    public string MonsterName;
    public int Hp;
    public int Gold;
    public int GenCount;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public GameObject monsterSpawner;
    public MonsterSpawner monsterSpawnerScript;
    public UIManager uiManagerScript;

    public int monsterType;
    public int waveCount = 0;
    public int lifeCount = 10;
    public int gold;

    public float pastTime = 0.0f;
    public float waveDelay = 5.0f;

    public bool isWaveEnd = true;
    public bool isGameOver = false;
    public bool isGameVictory = false;

    [Header("Monster Resources")]
    public GameObject[] monsterPrefabs;
    public string bagicPath = "Prefabs/Monster/";

    //first key : Stage, second key : Wave
    public Dictionary<int, Dictionary<int, List<GenInfomation>>> GenInfoMation;

    public int curStage;
    public int stageMax;
    public int curWave;
    public int maxWave;

    public bool stageEnd;

    //GenInfomation define
    private Dictionary<int, Dictionary<int, List<GenInfomation>>> GetGenInfo()
    {
        var stage = new Dictionary<int, Dictionary<int, List<GenInfomation>>>();

        //1 Wave
        var firstWave = new Dictionary<int, List<GenInfomation>>();
        //1 Wave - Monster
        var first = new List<GenInfomation>();
        first.Add(new GenInfomation(Type.Monster.Goblin, 100, 10, 1));
        firstWave.Add(1, first);
        //2 Wave - Monster
        var second = new List<GenInfomation>();
        second.Add(new GenInfomation(Type.Monster.Golem, 100, 10, 10));
        firstWave.Add(2, second);
        //3 Wave - Monster
        var third = new List<GenInfomation>();
        third.Add(new GenInfomation(Type.Monster.Kerberos, 100, 10, 10));
        firstWave.Add(3, third);
        //4 Wave - Monster
        var fourth = new List<GenInfomation>();
        fourth.Add(new GenInfomation(Type.Monster.Minotauros, 100, 10, 10));
        firstWave.Add(4, fourth);
        //5 Wave - Monster
        var fifth = new List<GenInfomation>();
        fifth.Add(new GenInfomation(Type.Monster.Troll, 100, 10, 10));
        firstWave.Add(5, fifth);
        //6 Wave - Monster
        var sixth = new List<GenInfomation>();
        sixth.Add(new GenInfomation(Type.Monster.Dragon, 100, 10, 10));
        firstWave.Add(6, sixth);
        stage.Add(0, firstWave);

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
        monsterType = 0;
        waveCount = 0;
        gold = 500;
        curStage = 0;
        curWave = 0;
        stageMax = 3;
        maxWave = 6;
        isWaveEnd = true;
        stageEnd = true;

        var uiManager = GameObject.Find("UIManager");
        uiManagerScript = uiManager.GetComponent<UIManager>();

        monsterSpawner = GameObject.Find("MonsterSpawner");
        monsterSpawnerScript = monsterSpawner.GetComponent<MonsterSpawner>();
    }

    public void UseGold(int cost)
    {
        gold -= cost;
    }

    public void StartStage()
    {
        stageEnd = false;
    }

    public void StartWave()
    {
        curWave += 1;
        pastTime = waveDelay;
    }

    void Update()
    {
        if (isGameOver || isGameVictory)
        {
            return;
        }

        if (!isGameOver && !isWaveEnd)
        {
            if (monsterSpawnerScript.genCount == monsterSpawnerScript.genCountLimit)
            {
                isWaveEnd = true;

                if (curWave > maxWave)
                {
                    curWave = 0;
                    curStage += 1;

                    if (curStage > stageMax)
                    {
                        pastTime = 0.0f;
                        isGameVictory = true;
                    }
                }
            }
        }
        else
        {
            if (!stageEnd)
            {
                if (curWave == 7 && monsterSpawnerScript.curMonster != 0)
                {
                    return;
                }

                pastTime += Time.deltaTime;

                if (pastTime > waveDelay)
                {
                    pastTime = 0.0f;
                    isWaveEnd = false;
                    monsterSpawnerScript.ResetGenInfo();
                    StartCoroutine(monsterSpawnerScript.CreateMonster());
                }
            }
        }
    }
}
