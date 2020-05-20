using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject portal;

    [Header("Monster Create Info")]
    private float createTime = 0.5f;    //몬스터 생성 시간

    public int curMonster;
    public int maxMonster;
    public int genCount;                //한 왜이브에 생성된 오브젝트의 수
    public int genCountLimit;           //한 웨이브에 생성될수 있는 오브젝트의 수

    [Header("Object Pool")]
    private int maxPool = 60;           //오브젝트 풀내 오브젝트의 수
    public Dictionary<string, List<GameObject>> monsterPools;

    List<GenInfomation> curWaveMonsterList = new List<GenInfomation>();

    void Awake()
    {
        gameManager = GameManager.Get();
        gameManager.SetGenInfomation();
        CreatePooling();
    }

    void Start()
    {
        portal = transform.GetChild(0).gameObject;

        curMonster = 0;
        maxMonster = 60;
        genCount = 0;
    }

    public void ResetGenInfo()
    {
        genCount = 0;
        genCountLimit = 0;
        curWaveMonsterList.Clear();

        curWaveMonsterList = gameManager.GenInfoMation[GameManager.instance.curStage][GameManager.instance.curWave];

        foreach (var genInfo in curWaveMonsterList)
        {
            genCountLimit += genInfo.GenCount;
        }
    }

    public IEnumerator CreateMonster()
    {
        while (!gameManager.isGameOver && !gameManager.isWaveEnd)
        {
            if (genCount < genCountLimit)
            {
                yield return new WaitForSeconds(createTime);

                for (int i = 0; i < curWaveMonsterList.Count; ++i)
                {
                    var monster = Getmonster(curWaveMonsterList[i].MonsterName);
                    var monsterDamage = monster.GetComponent<MonsterDamage>();

                    monsterDamage.isDie = false;
                    monsterDamage.SetHpBar(curWaveMonsterList[i].Hp);

                    SetToUnit(monster, curWaveMonsterList[i].Speed, curWaveMonsterList[i].Soul);
                    curMonster += 1;
                    genCount += 1;
                }
            }
            else
            {
                if (gameManager.curWave == 6)
                {
                    gameManager.stageEnd = true;
                }

                yield return null;
            }
        }
    }

    public void SetToUnit(GameObject unitObj, int speed, int soul)
    {
        unitObj.transform.position = transform.position;
        unitObj.transform.rotation = transform.rotation;
        unitObj.SetActive(true);
        unitObj.GetComponent<MonsterMove>().Init(speed, soul);
    }

    public GameObject Getmonster(string monsterName)
    {
        var list = monsterPools[monsterName];

        for (int i = 0; i < list.Count; ++i)
        {
            if (!list[i].activeInHierarchy)
            {
                return list[i];
            }
        }

        return null;
    }

    public void CreatePooling()
    {
        GameObject objectPools = new GameObject("MonsterPooling");

        monsterPools = new Dictionary<string, List<GameObject>>();
        gameManager.LoadMonsterPrefabs();

        for (int i = 0; i < Type.Monster.Max; ++i)
        {
            var monsterName = Type.Monster.ToString(i);

            List<GameObject> monsterPool = new List<GameObject>();

            for (int j = 0; j < maxPool; ++j)
            {
                var monster = Instantiate<GameObject>(gameManager.monsterPrefabs[i], objectPools.transform) as GameObject;

                monsterPool.Add(monster);
                monster.SetActive(false);
            }

            monsterPools.Add(monsterName, monsterPool);
        }
    }
}
