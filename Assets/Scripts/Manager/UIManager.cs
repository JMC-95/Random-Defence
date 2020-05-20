using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;

    private MonsterSpawner monsterSpawnerScript;

    public Button skip;

    public Text soulCount;
    public Text humanCount;
    public Text waveCount;
    public Text genCount;
    public Text timer;

    private float time;

    void Start()
    {
        gameManager = GameManager.Get();

        monsterSpawnerScript = GameObject.Find("MonsterSpawner").GetComponent<MonsterSpawner>();

        time = 45;
    }

    void Update()
    {
        UpdateUI();
        UpdateBtn();
        UpdateTime();
    }

    void UpdateUI()
    {
        var curSoul = gameManager.soul;
        var curWave = gameManager.curWave;
        var maxWave = gameManager.maxWave;
        var curGen = monsterSpawnerScript.curMonster;
        var maxGen = monsterSpawnerScript.maxMonster;

        soulCount.text = curSoul.ToString();
        //humanCount.text = gameManager.gold.ToString("00") + " / 50";
        waveCount.text = curWave.ToString("00") + " / " + maxWave.ToString("00");
        genCount.text = curGen.ToString("00") + " / " + maxGen.ToString("00");
        timer.text = "00 : " + time.ToString("00");
    }

    void UpdateBtn()
    {
        if (gameManager.curWave < 6)
        {
            //if (time < 30) skip.gameObject.SetActive(true);
            if (time < 45) skip.gameObject.SetActive(true);
            else skip.gameObject.SetActive(false);
        }
        else skip.gameObject.SetActive(false);
    }

    void UpdateTime()
    {
        time -= Time.deltaTime;

        if (time < 0)
        {
            time = 45;
            gameManager.StartWave();

            if (gameManager.stageEnd) gameManager.StartStage();
        }
    }

    public void Skip()
    {
        time = 45;
        gameManager.StartWave();

        if (gameManager.stageEnd) gameManager.StartStage();
    }
}
