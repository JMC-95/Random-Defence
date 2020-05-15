using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    private GameManager gameManager;

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
        while (!GameManager.instance.isGameOver && !GameManager.instance.isWaveEnd)
        {
            if (genCount < genCountLimit)
            {
                yield return new WaitForSeconds(createTime);

                for (int i = 0; i < curWaveMonsterList.Count; ++i)
                {
                    var monster = Getmonster(curWaveMonsterList[i].MonsterName);

                    SetToUnit(monster, curWaveMonsterList[i].Gold);
                    curMonster += 1;
                    genCount += 1;
                }
            }
            else
            {
                if (GameManager.instance.curWave == 5)
                {
                    GameManager.instance.stageEnd = true;
                }

                yield return null;
            }
        }
    }

    public void SetToUnit(GameObject unitObj, int gold)
    {
        unitObj.transform.position = transform.position;
        unitObj.transform.rotation = transform.rotation;
        unitObj.SetActive(true);
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
        GameObject objectPools = new GameObject("ObjectPools");

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
