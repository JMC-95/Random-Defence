using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    private MonsterSpawner monsterSpawnerScript;


    private Button skip;
    public Text waveCount;
    public Text genCount;
    public Text timer;

    private float time;

    void Start()
    {
        gameManager = GameManager.Get();
        monsterSpawnerScript = GameObject.Find("MonsterSpawner").GetComponent<MonsterSpawner>();

        skip = GameObject.Find("Canvas").transform.Find("Skip").GetComponent<Button>();
        waveCount = GameObject.Find("WaveCount").GetComponent<Text>();
        genCount = GameObject.Find("GenCount").GetComponent<Text>();
        timer = GameObject.Find("Timer").GetComponent<Text>();

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
        var curWave = gameManager.curWave;
        var maxWave = gameManager.maxWave;
        var curGen = monsterSpawnerScript.curMonster;
        var maxGen = monsterSpawnerScript.maxMonster;

        waveCount.text = curWave.ToString("00") + " / " + maxWave.ToString("00");
        genCount.text = curGen.ToString("00") + " / " + maxGen.ToString("00");
        timer.text = "00 : " + time.ToString("00");
    }

    void UpdateBtn()
    {
        if (time < 30) skip.gameObject.SetActive(true);
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

    public void MonsterSpawn()
    {
        time = 45;
        gameManager.StartWave();

        if (gameManager.stageEnd) gameManager.StartStage();
    }
}
