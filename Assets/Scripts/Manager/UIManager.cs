using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    private GameManager gameManager;

    private CharacterSelect characterSelect;
    private MonsterSpawner monsterSpawnerScript;

    public GameObject content;
    public GameObject playerInfoObj;

    public Button combine;
    public Button rating2;
    public Button rating3;
    public Button rating4;
    public Button exit;
    public Button skip;

    public Text soulCount;
    public Text humanCount;
    public Text waveCount;
    public Text genCount;
    public Text timer;

    public Image combineImage;

    private int resNum = 9;
    private int matNum = 1;
    private int rank2 = 9;
    private int rank3 = 17;
    private int rank4 = 24;
    private float time;

    public bool isCombine;

    Vector2 normalTab = new Vector2(45, 20);
    Vector2 selectTab = new Vector2(45, 25);

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

    void Start()
    {
        gameManager = GameManager.Get();

        characterSelect = gameManager.GetComponent<CharacterSelect>();
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
            if (time < 30) skip.gameObject.SetActive(true);
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

    public void UpdateInfo()
    {
        var player = characterSelect.target.GetComponent<CharacterInfomation>();
        var playerRank = playerInfoObj.transform.GetChild(1);

        playerInfoObj.gameObject.SetActive(true);
        playerInfoObj.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + player.sName);
        playerInfoObj.transform.GetChild(2).GetComponent<Text>().text = "Lv. " + player.nLevel.ToString();
        playerInfoObj.transform.GetChild(3).GetComponent<Text>().text = player.sName;
        playerInfoObj.transform.GetChild(4).GetComponent<Text>().text = "공격력 : " + player.nPower.ToString();
        playerInfoObj.transform.GetChild(5).GetComponent<Text>().text = "공격속도 : " + player.fAspeed.ToString();
        playerInfoObj.transform.GetChild(6).GetComponentInChildren<Text>().text = player.nSell.ToString();

        if (-1 < player.nID && player.nID < 8)           //1성   
        {
            playerInfoObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/1성");
            playerRank.GetChild(0).gameObject.SetActive(true);
            playerRank.GetChild(1).gameObject.SetActive(false);
            playerRank.GetChild(2).gameObject.SetActive(false);
            playerRank.GetChild(3).gameObject.SetActive(false);
        }
        else if (7 < player.nID && player.nID < 16)     //2성
        {
            playerInfoObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/2성");
            playerRank.GetChild(0).gameObject.SetActive(true);
            playerRank.GetChild(1).gameObject.SetActive(true);
            playerRank.GetChild(2).gameObject.SetActive(false);
            playerRank.GetChild(3).gameObject.SetActive(false);
        }
        else if (15 < player.nID && player.nID < 23)    //3성
        {
            playerInfoObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/3성");
            playerRank.GetChild(0).gameObject.SetActive(true);
            playerRank.GetChild(1).gameObject.SetActive(true);
            playerRank.GetChild(2).gameObject.SetActive(true);
            playerRank.GetChild(3).gameObject.SetActive(false);
        }
        else                                            //4성
        {
            playerInfoObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/4성");
            playerRank.GetChild(0).gameObject.SetActive(true);
            playerRank.GetChild(1).gameObject.SetActive(true);
            playerRank.GetChild(2).gameObject.SetActive(true);
            playerRank.GetChild(3).gameObject.SetActive(true);
        }

        //if()
    }

    public void UpdateInfoExit()
    {
        playerInfoObj.gameObject.SetActive(false);
    }

    public void Sell()
    {
        var player = characterSelect.target.GetComponent<CharacterInfomation>();

        gameManager.UseGold(-player.nSell);
        characterSelect.isSelect = false;
        playerInfoObj.gameObject.SetActive(false);
        characterSelect.target.gameObject.SetActive(false);
    }

    public void Skip()
    {
        time = 45;
        gameManager.StartWave();

        if (gameManager.stageEnd) gameManager.StartStage();
    }

    public void ClickCombine()
    {
        combineImage.gameObject.SetActive(true);
        isCombine = true;

        //이미지
        for (int i = (int)eHero.Priest; i < (int)eHero.Avenger; i++)
        {
            var combineRes = DataController.instance.combineData[i - resNum];
            var combineBase = DataController.instance.heroData[combineRes.nMaterial1 - matNum];
            var combineMat2 = DataController.instance.heroData[combineRes.nMaterial2 - matNum];
            var contents = content.transform.GetChild(i - rank2);

            contents.GetChild(5).gameObject.SetActive(false);
            contents.GetChild(6).gameObject.SetActive(false);
            contents.GetChild(7).gameObject.SetActive(false);
            contents.GetChild(8).gameObject.SetActive(false);
            contents.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/2성");
            contents.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + combineRes.sName);
            contents.GetChild(0).GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
            contents.GetChild(0).GetComponentInChildren<Text>().text = combineRes.sName;
            contents.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/1성");
            contents.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + combineBase.sName);
            contents.GetChild(2).GetComponentInChildren<Text>().color = new Color32(130, 70, 50, 255);
            contents.GetChild(2).GetComponentInChildren<Text>().text = combineBase.sName;
            contents.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/1성");
            contents.GetChild(4).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + combineMat2.sName);
            contents.GetChild(4).GetComponentInChildren<Text>().color = new Color32(130, 70, 50, 255);
            contents.GetChild(4).GetComponentInChildren<Text>().text = combineMat2.sName;
        }
    }

    public void ExitCombine()
    {
        combineImage.gameObject.SetActive(false);
        isCombine = false;
    }

    public void ClickRating2()
    {
        //2성 Tab
        rating2.GetComponent<RectTransform>().anchoredPosition = new Vector2(-210, 23);
        rating2.GetComponent<RectTransform>().sizeDelta = selectTab;
        rating3.GetComponent<RectTransform>().anchoredPosition = new Vector2(-165, 18);
        rating3.GetComponent<RectTransform>().sizeDelta = normalTab;
        rating4.GetComponent<RectTransform>().anchoredPosition = new Vector2(-120, 18);
        rating4.GetComponent<RectTransform>().sizeDelta = normalTab;

        //2성 조합표
        content.transform.GetChild(4).gameObject.SetActive(true);
        content.transform.GetChild(5).gameObject.SetActive(true);
        content.transform.GetChild(6).gameObject.SetActive(true);
        content.transform.GetChild(7).gameObject.SetActive(true);

        //이미지
        for (int i = (int)eHero.Priest; i < (int)eHero.Avenger; i++)
        {
            var combineRes = DataController.instance.combineData[i - resNum];
            var combineBase = DataController.instance.heroData[combineRes.nMaterial1 - matNum];
            var combineMat2 = DataController.instance.heroData[combineRes.nMaterial2 - matNum];
            var contents = content.transform.GetChild(i - rank2);

            contents.GetChild(5).gameObject.SetActive(false);
            contents.GetChild(6).gameObject.SetActive(false);
            contents.GetChild(7).gameObject.SetActive(false);
            contents.GetChild(8).gameObject.SetActive(false);
            contents.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/2성");
            contents.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + combineRes.sName);
            contents.GetChild(0).GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
            contents.GetChild(0).GetComponentInChildren<Text>().text = combineRes.sName;
            contents.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/1성");
            contents.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + combineBase.sName);
            contents.GetChild(2).GetComponentInChildren<Text>().color = new Color32(130, 70, 50, 255);
            contents.GetChild(2).GetComponentInChildren<Text>().text = combineBase.sName;
            contents.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/1성");
            contents.GetChild(4).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + combineMat2.sName);
            contents.GetChild(4).GetComponentInChildren<Text>().color = new Color32(130, 70, 50, 255);
            contents.GetChild(4).GetComponentInChildren<Text>().text = combineMat2.sName;
        }
    }

    public void ClickRating3()
    {
        //3성 Tab
        rating2.GetComponent<RectTransform>().anchoredPosition = new Vector2(-210, 18);
        rating2.GetComponent<RectTransform>().sizeDelta = normalTab;
        rating3.GetComponent<RectTransform>().anchoredPosition = new Vector2(-165, 23);
        rating3.GetComponent<RectTransform>().sizeDelta = selectTab;
        rating4.GetComponent<RectTransform>().anchoredPosition = new Vector2(-120, 18);
        rating4.GetComponent<RectTransform>().sizeDelta = normalTab;

        //3성 조합표
        content.transform.GetChild(4).gameObject.SetActive(true);
        content.transform.GetChild(5).gameObject.SetActive(true);
        content.transform.GetChild(6).gameObject.SetActive(true);
        content.transform.GetChild(7).gameObject.SetActive(false);

        //이미지
        for (int i = (int)eHero.Avenger; i < (int)eHero.Crusaders; i++)
        {
            var combineRes = DataController.instance.combineData[i - resNum];
            var combineBase = DataController.instance.heroData[combineRes.nMaterial1 - matNum];
            var combineMat2 = DataController.instance.heroData[combineRes.nMaterial2 - matNum];
            var combineMat3 = DataController.instance.heroData[combineRes.nMaterial3 - matNum];
            var contents = content.transform.GetChild(i - rank3);

            contents.GetChild(5).gameObject.SetActive(true);
            contents.GetChild(6).gameObject.SetActive(true);
            contents.GetChild(7).gameObject.SetActive(false);
            contents.GetChild(8).gameObject.SetActive(false);
            contents.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/3성");
            contents.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + combineRes.sName);
            contents.GetChild(0).GetComponentInChildren<Text>().color = new Color32(230, 230, 180, 255);
            contents.GetChild(0).GetComponentInChildren<Text>().text = combineRes.sName;
            contents.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/2성");
            contents.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + combineBase.sName);
            contents.GetChild(2).GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
            contents.GetChild(2).GetComponentInChildren<Text>().text = combineBase.sName;
            contents.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/2성");
            contents.GetChild(4).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + combineMat2.sName);
            contents.GetChild(4).GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
            contents.GetChild(4).GetComponentInChildren<Text>().text = combineMat2.sName;
            contents.GetChild(5).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/1성");
            contents.GetChild(6).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + combineMat3.sName);
            contents.GetChild(6).GetComponentInChildren<Text>().color = new Color32(130, 70, 50, 255);
            contents.GetChild(6).GetComponentInChildren<Text>().text = combineMat3.sName;
        }
    }

    public void ClickRating4()
    {
        //4성 Tab
        rating2.GetComponent<RectTransform>().anchoredPosition = new Vector2(-210, 18);
        rating2.GetComponent<RectTransform>().sizeDelta = normalTab;
        rating3.GetComponent<RectTransform>().anchoredPosition = new Vector2(-165, 18);
        rating3.GetComponent<RectTransform>().sizeDelta = normalTab;
        rating4.GetComponent<RectTransform>().anchoredPosition = new Vector2(-120, 23);
        rating4.GetComponent<RectTransform>().sizeDelta = selectTab;

        //4성 조합표
        content.transform.GetChild(4).gameObject.SetActive(false);
        content.transform.GetChild(5).gameObject.SetActive(false);
        content.transform.GetChild(6).gameObject.SetActive(false);
        content.transform.GetChild(7).gameObject.SetActive(false);

        //이미지
        for (int i = (int)eHero.Crusaders; i < (int)eHero.Max; i++)
        {
            var combineRes = DataController.instance.combineData[i - resNum];
            var combineBase = DataController.instance.heroData[combineRes.nMaterial1 - matNum];
            var combineMat2 = DataController.instance.heroData[combineRes.nMaterial2 - matNum];
            var combineMat3 = DataController.instance.heroData[combineRes.nMaterial3 - matNum];
            var combineMat4 = DataController.instance.heroData[combineRes.nMaterial4 - matNum];
            var contents = content.transform.GetChild(i - rank4);

            contents.GetChild(5).gameObject.SetActive(true);
            contents.GetChild(6).gameObject.SetActive(true);
            contents.GetChild(7).gameObject.SetActive(true);
            contents.GetChild(8).gameObject.SetActive(true);
            contents.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/4성");
            contents.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + combineRes.sName);
            contents.GetChild(0).GetComponentInChildren<Text>().color = new Color32(100, 200, 120, 255);
            contents.GetChild(0).GetComponentInChildren<Text>().text = combineRes.sName;
            contents.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/3성");
            contents.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + combineBase.sName);
            contents.GetChild(2).GetComponentInChildren<Text>().color = new Color32(230, 230, 180, 255);
            contents.GetChild(2).GetComponentInChildren<Text>().text = combineBase.sName;
            contents.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/3성");
            contents.GetChild(4).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + combineMat2.sName);
            contents.GetChild(4).GetComponentInChildren<Text>().color = new Color32(230, 230, 180, 255);
            contents.GetChild(4).GetComponentInChildren<Text>().text = combineMat2.sName;
            contents.GetChild(5).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/2성");
            contents.GetChild(6).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + combineMat3.sName);
            contents.GetChild(6).GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
            contents.GetChild(6).GetComponentInChildren<Text>().text = combineMat3.sName;
            contents.GetChild(7).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/1성");
            contents.GetChild(8).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + combineMat4.sName);
            contents.GetChild(8).GetComponentInChildren<Text>().color = new Color32(130, 70, 50, 255);
            contents.GetChild(8).GetComponentInChildren<Text>().text = combineMat4.sName;
        }
    }
}
