using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    private GameManager gameManager;

    private SelectObject selectObjectScript;
    private CharacterSpawner characterSpawnerScript;
    private MonsterSpawner monsterSpawnerScript;

    public GameObject contentObj;
    public GameObject playerInfoObj;
    public GameObject selectCombineObj;
    public GameObject upgradeObj;
    public GameObject shopObj;
    public GameObject spawnerObj;
    public GameObject gameClear;
    public GameObject gameOver;
    GameObject[] playerObj;
    GameObject[] playersObj;

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
    public Text BossText;

    public Image combineImage;

    //캐릭터 강화
    public int levelUp1;
    public int powerUp1;
    public int levelUp2;
    public int powerUp2;
    public int levelUp3;
    public int powerUp3;
    public int levelUp4;
    public int powerUp4;

    //UI
    private int resNum = 9;
    private int matNum = 1;
    private int rank2 = 9;
    private int rank3 = 17;
    private int rank4 = 24;
    public int resultID;
    public int resultID2;

    private float changeAlpha = 0.01f;
    public float time;
    public float pastTime = 0.0f;
    public float bossDelay = 6.0f;

    public bool isCombineTable = false;
    private bool isCombineBase = false;
    //CombineButton
    private bool isSameMat = false;
    private bool isSameMat_1 = false;
    private bool isSameMat_2 = false;
    private bool isSameMat_3 = false;
    private bool isMaterial = false;
    private bool isMaterial_1 = false;

    Vector2 normalTab = new Vector2(45, 20);
    Vector2 selectTab = new Vector2(45, 25);

    //List
    List<GameObject> material2Obj = new List<GameObject>(); //조합 재료2의 게임 오브젝트를 저장해두는 리스트
    List<GameObject> material3Obj = new List<GameObject>(); //조합 재료3의 게임 오브젝트를 저장해두는 리스트
    List<GameObject> material4Obj = new List<GameObject>(); //조합 재료4의 게임 오브젝트를 저장해두는 리스트

    List<int> resIDList = new List<int>();                  //조합 결과의 ID를 저장해두는 리스트
    List<int> matIDList = new List<int>();                  //조합 재료의 ID를 저장해두는 리스트

    List<string> resList = new List<string>();              //조합 결과의 이름을 저장해두는 리스트
    List<string> baseList = new List<string>();             //조합 베이스의 이름을 저장해두는 리스트
    List<string> mat2List = new List<string>();             //조합 재료2의 이름을 저장해두는 리스트
    List<string> mat3List = new List<string>();             //조합 재료3의 이름을 저장해두는 리스트
    List<string> mat4List = new List<string>();             //조합 재료4의 이름을 저장해두는 리스트

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

        selectObjectScript = gameManager.GetComponent<SelectObject>();
        characterSpawnerScript = GameObject.Find("CharacterSpawner").GetComponent<CharacterSpawner>();
        monsterSpawnerScript = GameObject.Find("MonsterSpawner").GetComponent<MonsterSpawner>();

        gameClear.SetActive(false);
        gameOver.SetActive(false);
        time = 60;
    }

    void UpdateUI()
    {
        if (!gameManager.isGameVictory || !gameManager.isGameOver)
        {
            int curSoul = gameManager.soul;
            int curWave = gameManager.curWave;
            int maxWave = gameManager.maxWave;
            int curGenCharacter = characterSpawnerScript.curCharacterCount;
            int maxGenCharacter = characterSpawnerScript.maxCharacterCount;
            int curGen = monsterSpawnerScript.curMonster;
            int maxGen = monsterSpawnerScript.maxMonster;

            soulCount.text = curSoul.ToString();
            waveCount.text = curWave.ToString("00") + " / " + maxWave.ToString("00");
            humanCount.text = curGenCharacter.ToString("00") + " / " + maxGenCharacter.ToString("00");
            genCount.text = curGen.ToString("00") + " / " + maxGen.ToString("00");
            timer.text = "00 : " + time.ToString("00");
        }
    }

    void UpdateBtn()
    {
        if (!gameManager.isGameVictory || !gameManager.isGameOver)
        {
            if (gameManager.curWave < 6)
            {
                if (time < 45) skip.gameObject.SetActive(true);
                else skip.gameObject.SetActive(false);
            }
            else skip.gameObject.SetActive(false);
        }
    }

    void UpdateTime()
    {
        if (!gameManager.isGameVictory || !gameManager.isGameOver)
        {
            time -= Time.deltaTime;

            if (time < 0)
            {
                if (gameManager.stageEnd) gameManager.StartStage();

                time = 60;
                gameManager.StartWave();
                characterSpawnerScript.CreateCharacter();
            }
        }
    }

    void FixedUpdate()
    {
        if (!gameManager.isGameVictory || !gameManager.isGameOver)
        {
            UpdateUI();
            UpdateBtn();
            UpdateTime();

            if (BossText.gameObject.activeInHierarchy)
            {
                pastTime += Time.deltaTime;
                ShowBossEmergy();
                if (gameManager.isGameVictory || gameManager.isGameOver || pastTime > bossDelay)
                    UIManager.instance.BossText.gameObject.SetActive(false);
            }
            else
            {
                pastTime = 0.0f;
            }
        }

        if (gameManager.isGameVictory)
        {
            gameClear.SetActive(true);
        }
        else if (gameManager.isGameOver)
        {
            gameOver.SetActive(true);
        }
    }

    public void ShowBossEmergy()
    {
        if (changeAlpha > 0.0f)
        {
            BossText.color = new Vector4(BossText.color.r, BossText.color.g, BossText.color.b, BossText.color.a - changeAlpha);
            if (BossText.color.a <= 0.0f)
            {
                changeAlpha = -changeAlpha;
            }
        }
        else
        {
            BossText.color = new Vector4(BossText.color.r, BossText.color.g, BossText.color.b, BossText.color.a - changeAlpha);
            if (BossText.color.a >= 1.0f)
            {
                changeAlpha = -changeAlpha;
            }
        }
    }

    public void Skip()
    {
        if (gameManager.stageEnd) gameManager.StartStage();

        time = 60;
        gameManager.StartWave();
        characterSpawnerScript.CreateCharacter();
    }

    public void UpdateInfo()
    {
        var player = selectObjectScript.target.GetComponent<CharacterInfomation>();
        var playerRank = playerInfoObj.transform.GetChild(1);

        //현재 하이어라키에 존재하는 모든 플레이어 오브젝트를 배열에 담는다.
        playerObj = GameObject.FindGameObjectsWithTag("Player");

        //List 초기화
        material2Obj.Clear();
        material3Obj.Clear();
        material4Obj.Clear();
        resIDList.Clear();
        matIDList.Clear();
        resList.Clear();
        baseList.Clear();
        mat2List.Clear();
        mat3List.Clear();
        mat4List.Clear();

        //캐릭터 정보 표시
        playerInfoObj.gameObject.SetActive(true);
        playerInfoObj.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + player.sName);
        playerInfoObj.transform.GetChild(2).GetComponent<Text>().text = "Lv. " + player.nLevel.ToString();
        playerInfoObj.transform.GetChild(3).GetComponent<Text>().text = player.sName;
        playerInfoObj.transform.GetChild(4).GetComponent<Text>().text = "공격력 : " + player.nPower.ToString();
        playerInfoObj.transform.GetChild(5).GetComponent<Text>().text = "공격속도 : " + player.fAspeed.ToString();
        playerInfoObj.transform.GetChild(6).GetComponentInChildren<Text>().text = player.nSell.ToString();

        //1성
        if (-1 < player.nID && player.nID < 8)
        {
            if (powerUp1 > 0)
            {
                playerInfoObj.transform.GetChild(7).gameObject.SetActive(true);
                playerInfoObj.transform.GetChild(7).GetComponent<Text>().text = "+ : " + powerUp1.ToString();
                playerInfoObj.transform.GetChild(8).gameObject.SetActive(false);
                playerInfoObj.transform.GetChild(9).gameObject.SetActive(false);
                playerInfoObj.transform.GetChild(10).gameObject.SetActive(false);
            }
            else
            {
                playerInfoObj.transform.GetChild(7).gameObject.SetActive(false);
                playerInfoObj.transform.GetChild(8).gameObject.SetActive(false);
                playerInfoObj.transform.GetChild(9).gameObject.SetActive(false);
                playerInfoObj.transform.GetChild(10).gameObject.SetActive(false);
            }
            playerInfoObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/1성");
            playerRank.GetChild(0).gameObject.SetActive(true);
            playerRank.GetChild(1).gameObject.SetActive(false);
            playerRank.GetChild(2).gameObject.SetActive(false);
            playerRank.GetChild(3).gameObject.SetActive(false);

            for (int i = (int)eHero.Priest; i < (int)eHero.Avenger; i++)
            {
                var combineRes = DataManager.instance.combineData[i - resNum];
                var combineBase = DataManager.instance.heroData[combineRes.nMaterial1 - matNum];
                var combineMat2 = DataManager.instance.heroData[combineRes.nMaterial2 - matNum];

                if (player.nID + 1 == combineBase.nID)
                {
                    isCombineBase = true;
                    resIDList.Add(combineRes.nID);
                    matIDList.Add(combineMat2.nID);
                    resList.Add(combineRes.sName);
                    baseList.Add(combineBase.sName);
                    mat2List.Add(combineMat2.sName);
                }
            }

            if (isCombineBase)
            {
                //캐릭터의 조합 인터페이스를 나타낸다.
                for (int j = 0; j < baseList.Count; j++)
                {
                    var combineObj = selectCombineObj.transform.GetChild(j);

                    combineObj.gameObject.SetActive(true);
                    selectCombineObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(-70, 0, 0);
                    combineObj.GetChild(4).gameObject.SetActive(false);
                    combineObj.GetChild(5).gameObject.SetActive(false);
                    combineObj.GetChild(6).gameObject.SetActive(false);
                    combineObj.GetChild(7).gameObject.SetActive(false);
                    combineObj.GetChild(10).gameObject.SetActive(false);
                    combineObj.GetChild(11).gameObject.SetActive(false);

                    combineObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/1성");
                    combineObj.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + baseList[j]);
                    combineObj.GetChild(0).GetComponentInChildren<Text>().color = new Color32(130, 70, 50, 255);
                    combineObj.GetChild(0).GetComponentInChildren<Text>().text = baseList[j];
                    combineObj.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/2성");
                    combineObj.GetChild(1).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + resList[j]);
                    combineObj.GetChild(1).GetChild(0).GetComponentInChildren<Text>().color = new Color(255, 255, 255, 255);
                    combineObj.GetChild(1).GetChild(0).GetComponentInChildren<Text>().text = resList[j];
                    combineObj.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/1성");
                    combineObj.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + mat2List[j]);
                    combineObj.GetChild(3).GetComponentInChildren<Text>().color = new Color32(130, 70, 50, 255);
                    combineObj.GetChild(3).GetComponentInChildren<Text>().text = mat2List[j];
                }

                //캐릭터의 조합에 필요한 재료를 확인한다.
                for (int i = 0; i < playerObj.Length; i++)
                {
                    var curPlayerID = playerObj[i].GetComponent<CharacterInfomation>().nID;
                    var curMatID = matIDList[0] - 1;

                    if (curPlayerID == curMatID)
                    {
                        isMaterial = true;
                    }
                }
            }
        }
        //2성
        else if (7 < player.nID && player.nID < 16)
        {
            if (powerUp2 > 0)
            {
                playerInfoObj.transform.GetChild(7).gameObject.SetActive(false);
                playerInfoObj.transform.GetChild(8).gameObject.SetActive(true);
                playerInfoObj.transform.GetChild(8).GetComponent<Text>().text = "+ : " + powerUp2.ToString();
                playerInfoObj.transform.GetChild(9).gameObject.SetActive(false);
                playerInfoObj.transform.GetChild(10).gameObject.SetActive(false);
            }
            else
            {
                playerInfoObj.transform.GetChild(7).gameObject.SetActive(false);
                playerInfoObj.transform.GetChild(8).gameObject.SetActive(false);
                playerInfoObj.transform.GetChild(9).gameObject.SetActive(false);
                playerInfoObj.transform.GetChild(10).gameObject.SetActive(false);
            }
            playerInfoObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/2성");
            playerRank.GetChild(0).gameObject.SetActive(true);
            playerRank.GetChild(1).gameObject.SetActive(true);
            playerRank.GetChild(2).gameObject.SetActive(false);
            playerRank.GetChild(3).gameObject.SetActive(false);

            for (int i = (int)eHero.Avenger; i < (int)eHero.Crusaders; i++)
            {
                var combineRes = DataManager.instance.combineData[i - resNum];
                var combineBase = DataManager.instance.heroData[combineRes.nMaterial1 - matNum];
                var combineMat2 = DataManager.instance.heroData[combineRes.nMaterial2 - matNum];
                var combineMat3 = DataManager.instance.heroData[combineRes.nMaterial3 - matNum];

                if (player.nID + 1 == combineBase.nID)
                {
                    isCombineBase = true;
                    resIDList.Add(combineRes.nID);
                    matIDList.Add(combineMat2.nID);
                    matIDList.Add(combineMat3.nID);
                    resList.Add(combineRes.sName);
                    baseList.Add(combineBase.sName);
                    mat2List.Add(combineMat2.sName);
                    mat3List.Add(combineMat3.sName);
                }
            }

            if (isCombineBase)
            {
                //캐릭터의 조합 인터페이스를 나타낸다.
                for (int j = 0; j < baseList.Count; j++)
                {
                    var combineObj = selectCombineObj.transform.GetChild(j);

                    combineObj.gameObject.SetActive(true);
                    selectCombineObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(-130, 0, 0);
                    combineObj.GetChild(4).gameObject.SetActive(true);
                    combineObj.GetChild(5).gameObject.SetActive(true);
                    combineObj.GetChild(6).gameObject.SetActive(false);
                    combineObj.GetChild(7).gameObject.SetActive(false);
                    combineObj.GetChild(10).gameObject.SetActive(true);
                    combineObj.GetChild(11).gameObject.SetActive(false);

                    combineObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/2성");
                    combineObj.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + baseList[j]);
                    combineObj.GetChild(0).GetChild(0).GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
                    combineObj.GetChild(0).GetComponentInChildren<Text>().text = baseList[j];
                    combineObj.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/3성");
                    combineObj.GetChild(1).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + resList[j]);
                    combineObj.GetChild(1).GetChild(0).GetComponentInChildren<Text>().color = new Color32(230, 230, 180, 255);
                    combineObj.GetChild(1).GetChild(0).GetComponentInChildren<Text>().text = resList[j];
                    combineObj.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/2성");
                    combineObj.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + mat2List[j]);
                    combineObj.GetChild(3).GetChild(0).GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
                    combineObj.GetChild(3).GetComponentInChildren<Text>().text = mat2List[j];
                    combineObj.GetChild(4).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/1성");
                    combineObj.GetChild(5).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + mat3List[j]);
                    combineObj.GetChild(5).GetComponentInChildren<Text>().color = new Color32(130, 70, 50, 255);
                    combineObj.GetChild(5).GetComponentInChildren<Text>().text = mat3List[j];
                }

                //캐릭터의 조합에 필요한 재료를 확인한다.
                if (player.nID != (int)eHero.Samurai - 1)
                {
                    for (int i = 0; i < playerObj.Length; i++)
                    {
                        var curPlayerID = playerObj[i].GetComponent<CharacterInfomation>().nID;
                        var curMatID = matIDList[0] - 1;
                        var curMatID_1 = matIDList[1] - 1;

                        if (curPlayerID == curMatID)
                        {
                            isSameMat = true;
                        }
                        if (curPlayerID == curMatID_1)
                        {
                            isSameMat_1 = true;
                        }
                    }

                    if (isSameMat && isSameMat_1) isMaterial = true;
                }
                else
                {
                    for (int i = 0; i < playerObj.Length; i++)
                    {
                        var curPlayerID = playerObj[i].GetComponent<CharacterInfomation>().nID;
                        var curMatID = matIDList[0] - 1;
                        var curMatID_1 = matIDList[1] - 1;
                        var curMatID_2 = matIDList[2] - 1;
                        var curMatID_3 = matIDList[3] - 1;

                        if (curPlayerID == curMatID)
                        {
                            isSameMat = true;
                        }
                        if (curPlayerID == curMatID_1)
                        {
                            isSameMat_1 = true;
                        }
                        if (curPlayerID == curMatID_2)
                        {
                            isSameMat_2 = true;
                        }
                        if (curPlayerID == curMatID_3)
                        {
                            isSameMat_3 = true;
                        }
                    }

                    if (isSameMat && isSameMat_1) isMaterial = true;
                    if (isSameMat_2 && isSameMat_3) isMaterial_1 = true;
                }
            }
        }
        //3성
        else if (15 < player.nID && player.nID < 23)
        {
            if (powerUp3 > 0)
            {
                playerInfoObj.transform.GetChild(7).gameObject.SetActive(false);
                playerInfoObj.transform.GetChild(8).gameObject.SetActive(false);
                playerInfoObj.transform.GetChild(9).gameObject.SetActive(true);
                playerInfoObj.transform.GetChild(9).GetComponent<Text>().text = "+ : " + powerUp3.ToString();
                playerInfoObj.transform.GetChild(10).gameObject.SetActive(false);
            }
            else
            {
                playerInfoObj.transform.GetChild(7).gameObject.SetActive(false);
                playerInfoObj.transform.GetChild(8).gameObject.SetActive(false);
                playerInfoObj.transform.GetChild(9).gameObject.SetActive(false);
                playerInfoObj.transform.GetChild(10).gameObject.SetActive(false);
            }
            playerInfoObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/3성");
            playerRank.GetChild(0).gameObject.SetActive(true);
            playerRank.GetChild(1).gameObject.SetActive(true);
            playerRank.GetChild(2).gameObject.SetActive(true);
            playerRank.GetChild(3).gameObject.SetActive(false);

            for (int i = (int)eHero.Crusaders; i < (int)eHero.Max; i++)
            {
                var combineRes = DataManager.instance.combineData[i - resNum];
                var combineBase = DataManager.instance.heroData[combineRes.nMaterial1 - matNum];
                var combineMat2 = DataManager.instance.heroData[combineRes.nMaterial2 - matNum];
                var combineMat3 = DataManager.instance.heroData[combineRes.nMaterial3 - matNum];
                var combineMat4 = DataManager.instance.heroData[combineRes.nMaterial4 - matNum];

                if (player.nID + 1 == combineBase.nID)
                {
                    isCombineBase = true;
                    resIDList.Add(combineRes.nID);
                    matIDList.Add(combineMat2.nID);
                    matIDList.Add(combineMat3.nID);
                    matIDList.Add(combineMat4.nID);
                    resList.Add(combineRes.sName);
                    baseList.Add(combineBase.sName);
                    mat2List.Add(combineMat2.sName);
                    mat3List.Add(combineMat3.sName);
                    mat4List.Add(combineMat4.sName);
                }
            }

            if (isCombineBase)
            {
                //캐릭터의 조합 인터페이스를 나타낸다.
                for (int j = 0; j < baseList.Count; j++)
                {
                    var combineObj = selectCombineObj.transform.GetChild(j);

                    combineObj.gameObject.SetActive(true);
                    selectCombineObj.GetComponent<RectTransform>().anchoredPosition = new Vector3(-190, 0, 0);
                    combineObj.GetChild(4).gameObject.SetActive(true);
                    combineObj.GetChild(5).gameObject.SetActive(true);
                    combineObj.GetChild(6).gameObject.SetActive(true);
                    combineObj.GetChild(7).gameObject.SetActive(true);
                    combineObj.GetChild(10).gameObject.SetActive(true);
                    combineObj.GetChild(11).gameObject.SetActive(true);

                    combineObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/3성");
                    combineObj.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + baseList[j]);
                    combineObj.GetChild(0).GetComponentInChildren<Text>().color = new Color32(230, 230, 180, 255);
                    combineObj.GetChild(0).GetComponentInChildren<Text>().text = baseList[j];
                    combineObj.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/4성");
                    combineObj.GetChild(1).GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + resList[j]);
                    combineObj.GetChild(1).GetChild(0).GetComponentInChildren<Text>().color = new Color32(100, 200, 120, 255);
                    combineObj.GetChild(1).GetChild(0).GetComponentInChildren<Text>().text = resList[j];
                    combineObj.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/3성");
                    combineObj.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + mat2List[j]);
                    combineObj.GetChild(3).GetComponentInChildren<Text>().color = new Color32(230, 230, 180, 255);
                    combineObj.GetChild(3).GetComponentInChildren<Text>().text = mat2List[j];
                    combineObj.GetChild(4).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/2성");
                    combineObj.GetChild(5).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + mat3List[j]);
                    combineObj.GetChild(5).GetChild(0).GetComponentInChildren<Text>().color = new Color32(255, 255, 255, 255);
                    combineObj.GetChild(5).GetComponentInChildren<Text>().text = mat3List[j];
                    combineObj.GetChild(6).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/1성");
                    combineObj.GetChild(7).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PlayerFace/" + mat4List[j]);
                    combineObj.GetChild(7).GetComponentInChildren<Text>().color = new Color32(130, 70, 50, 255);
                    combineObj.GetChild(7).GetComponentInChildren<Text>().text = mat4List[j];
                }

                //캐릭터의 조합에 필요한 재료를 확인한다.
                for (int i = 0; i < playerObj.Length; i++)
                {
                    var curPlayerID = playerObj[i].GetComponent<CharacterInfomation>().nID;
                    var curMatID = matIDList[0] - 1;
                    var curMatID_1 = matIDList[1] - 1;
                    var curMatID_2 = matIDList[2] - 1;

                    if (curPlayerID == curMatID)
                    {
                        isSameMat = true;
                    }
                    if (curPlayerID == curMatID_1)
                    {
                        isSameMat_1 = true;
                    }
                    if (curPlayerID == curMatID_2)
                    {
                        isSameMat_2 = true;
                    }
                }

                if (isSameMat && isSameMat_1 && isSameMat_2) isMaterial = true;
            }
        }
        //4성
        else if (23 < player.nID && player.nID < 27)
        {
            if (powerUp4 > 0)
            {
                playerInfoObj.transform.GetChild(7).gameObject.SetActive(false);
                playerInfoObj.transform.GetChild(8).gameObject.SetActive(false);
                playerInfoObj.transform.GetChild(9).gameObject.SetActive(false);
                playerInfoObj.transform.GetChild(10).gameObject.SetActive(true);
                playerInfoObj.transform.GetChild(10).GetComponent<Text>().text = "+ : " + powerUp4.ToString();
            }
            else
            {
                playerInfoObj.transform.GetChild(7).gameObject.SetActive(false);
                playerInfoObj.transform.GetChild(8).gameObject.SetActive(false);
                playerInfoObj.transform.GetChild(9).gameObject.SetActive(false);
                playerInfoObj.transform.GetChild(10).gameObject.SetActive(false);
            }
            playerInfoObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/4성");

            for (int i = 0; i < playerRank.childCount; i++)
            {
                playerRank.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public void UpdateInfoExit()
    {
        //게임 오브젝트 초기화
        playerObj = null;

        //Bool 초기화
        isSameMat = false;
        isSameMat_1 = false;
        isSameMat_2 = false;
        isSameMat_3 = false;
        isMaterial = false;
        isMaterial_1 = false;
        isCombineBase = false;
        playerInfoObj.gameObject.SetActive(false);

        for (int i = 0; i < selectCombineObj.transform.childCount; i++)
        {
            selectCombineObj.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void Sell()
    {
        var target = selectObjectScript.target;
        var player = target.GetComponent<CharacterInfomation>();

        UpdateInfoExit();

        gameManager.UseSoul(-player.nSell);
        characterSpawnerScript.curCharacterCount -= 1;
        selectObjectScript.UnSelect();
        target.gameObject.SetActive(false);
    }

    public void NonSelect()
    {
        UpdateInfoExit();
        selectObjectScript.UnSelect();
    }

    public void Combine1()
    {
        if (isMaterial)
        {
            var target = selectObjectScript.target;

            CombineButton1();
            UpdateInfoExit();

            resultID = resIDList[0] - 1;
            characterSpawnerScript.curCharacterCount -= 1;
            selectObjectScript.UnSelect();
            target.gameObject.SetActive(false);

            spawnerObj.GetComponent<CharacterSpawner>().CombineCharacter_1();
        }
    }

    public void Combine2()
    {
        if (isMaterial_1)
        {
            var target = selectObjectScript.target;

            CombineButton2();
            UpdateInfoExit();

            resultID2 = resIDList[1] - 1;
            characterSpawnerScript.curCharacterCount -= 1;
            selectObjectScript.UnSelect();
            target.gameObject.SetActive(false);

            spawnerObj.GetComponent<CharacterSpawner>().CombineCharacter_2();
        }
    }

    public void ClickCombine()
    {
        combineImage.gameObject.SetActive(true);
        isCombineTable = true;

        //2성 Tab
        rating2.GetComponent<RectTransform>().anchoredPosition = new Vector2(-210, 23);
        rating2.GetComponent<RectTransform>().sizeDelta = selectTab;
        rating3.GetComponent<RectTransform>().anchoredPosition = new Vector2(-165, 18);
        rating3.GetComponent<RectTransform>().sizeDelta = normalTab;
        rating4.GetComponent<RectTransform>().anchoredPosition = new Vector2(-120, 18);
        rating4.GetComponent<RectTransform>().sizeDelta = normalTab;

        //2성 조합표
        contentObj.transform.GetChild(4).gameObject.SetActive(true);
        contentObj.transform.GetChild(5).gameObject.SetActive(true);
        contentObj.transform.GetChild(6).gameObject.SetActive(true);
        contentObj.transform.GetChild(7).gameObject.SetActive(true);

        //이미지
        for (int i = (int)eHero.Priest; i < (int)eHero.Avenger; i++)
        {
            var combineRes = DataManager.instance.combineData[i - resNum];
            var combineBase = DataManager.instance.heroData[combineRes.nMaterial1 - matNum];
            var combineMat2 = DataManager.instance.heroData[combineRes.nMaterial2 - matNum];
            var contents = contentObj.transform.GetChild(i - rank2);

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
        isCombineTable = false;
    }

    public void ExitShop()
    {
        upgradeObj.SetActive(false);
        shopObj.SetActive(false);
    }

    public void LevelUp1()
    {
        if (gameManager.soul >= 50)
        {
            playersObj = GameObject.FindGameObjectsWithTag("Player");

            for (int i = 0; i < playersObj.Length; i++)
            {
                var id = playersObj[i].GetComponent<CharacterInfomation>().nID;

                if (0 <= id && id < 8)
                {
                    playersObj[i].GetComponent<CharacterInfomation>().nLevel += 1;
                    playersObj[i].GetComponent<CharacterInfomation>().nPower += 5;
                }
            }

            levelUp1 += 1;
            powerUp1 += 5;
            gameManager.UseSoul(50);
            upgradeObj.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "Lv. " + levelUp1.ToString();
        }
    }

    public void LevelUp2()
    {
        if (gameManager.soul >= 100)
        {
            playersObj = GameObject.FindGameObjectsWithTag("Player");

            for (int i = 0; i < playersObj.Length; i++)
            {
                var id = playersObj[i].GetComponent<CharacterInfomation>().nID;

                if (8 <= id && id < 16)
                {
                    playersObj[i].GetComponent<CharacterInfomation>().nLevel += 1;
                    playersObj[i].GetComponent<CharacterInfomation>().nPower += 10;
                }
            }

            levelUp2 += 1;
            powerUp2 += 10;
            gameManager.UseSoul(100);
            upgradeObj.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = "Lv. " + levelUp2.ToString();
        }
    }

    public void LevelUp3()
    {
        if (gameManager.soul >= 200)
        {
            playersObj = GameObject.FindGameObjectsWithTag("Player");

            for (int i = 0; i < playersObj.Length; i++)
            {
                var id = playersObj[i].GetComponent<CharacterInfomation>().nID;

                if (16 <= id && id < 23)
                {
                    playersObj[i].GetComponent<CharacterInfomation>().nLevel += 1;
                    playersObj[i].GetComponent<CharacterInfomation>().nPower += 20;
                }
            }

            levelUp3 += 1;
            powerUp3 += 20;
            gameManager.UseSoul(200);
            upgradeObj.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = "Lv. " + levelUp3.ToString();
        }
    }

    public void LevelUp4()
    {
        if (gameManager.soul >= 300)
        {
            playersObj = GameObject.FindGameObjectsWithTag("Player");

            for (int i = 0; i < playersObj.Length; i++)
            {
                var id = playersObj[i].GetComponent<CharacterInfomation>().nID;

                if (23 <= id && id < 27)
                {
                    playersObj[i].GetComponent<CharacterInfomation>().nLevel += 1;
                    playersObj[i].GetComponent<CharacterInfomation>().nPower += 50;
                }
            }

            levelUp4 += 1;
            powerUp4 += 50;
            gameManager.UseSoul(300);
            upgradeObj.transform.GetChild(3).GetChild(1).GetComponent<Text>().text = "Lv. " + levelUp4.ToString();
        }
    }

    public void BuyCharacter()
    {
        if (gameManager.soul >= 200)
        {
            characterSpawnerScript.CreateCharacter();
            gameManager.UseSoul(200);
            shopObj.SetActive(false);
        }
    }

    public void GachaSoul()
    {
        if (gameManager.soul >= 50)
        {
            int gacha = Random.Range(-100, 100);
            gameManager.UseSoul(gacha);
        }
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
        contentObj.transform.GetChild(4).gameObject.SetActive(true);
        contentObj.transform.GetChild(5).gameObject.SetActive(true);
        contentObj.transform.GetChild(6).gameObject.SetActive(true);
        contentObj.transform.GetChild(7).gameObject.SetActive(true);

        //이미지
        for (int i = (int)eHero.Priest; i < (int)eHero.Avenger; i++)
        {
            var combineRes = DataManager.instance.combineData[i - resNum];
            var combineBase = DataManager.instance.heroData[combineRes.nMaterial1 - matNum];
            var combineMat2 = DataManager.instance.heroData[combineRes.nMaterial2 - matNum];
            var contents = contentObj.transform.GetChild(i - rank2);

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
        contentObj.transform.GetChild(4).gameObject.SetActive(true);
        contentObj.transform.GetChild(5).gameObject.SetActive(true);
        contentObj.transform.GetChild(6).gameObject.SetActive(true);
        contentObj.transform.GetChild(7).gameObject.SetActive(false);

        //이미지
        for (int i = (int)eHero.Avenger; i < (int)eHero.Crusaders; i++)
        {
            var combineRes = DataManager.instance.combineData[i - resNum];
            var combineBase = DataManager.instance.heroData[combineRes.nMaterial1 - matNum];
            var combineMat2 = DataManager.instance.heroData[combineRes.nMaterial2 - matNum];
            var combineMat3 = DataManager.instance.heroData[combineRes.nMaterial3 - matNum];
            var contents = contentObj.transform.GetChild(i - rank3);

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
        contentObj.transform.GetChild(4).gameObject.SetActive(false);
        contentObj.transform.GetChild(5).gameObject.SetActive(false);
        contentObj.transform.GetChild(6).gameObject.SetActive(false);
        contentObj.transform.GetChild(7).gameObject.SetActive(false);

        //이미지
        for (int i = (int)eHero.Crusaders; i < (int)eHero.Max; i++)
        {
            var combineRes = DataManager.instance.combineData[i - resNum];
            var combineBase = DataManager.instance.heroData[combineRes.nMaterial1 - matNum];
            var combineMat2 = DataManager.instance.heroData[combineRes.nMaterial2 - matNum];
            var combineMat3 = DataManager.instance.heroData[combineRes.nMaterial3 - matNum];
            var combineMat4 = DataManager.instance.heroData[combineRes.nMaterial4 - matNum];
            var contents = contentObj.transform.GetChild(i - rank4);

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

    public void CombineButton1()
    {
        var player = selectObjectScript.target.GetComponent<CharacterInfomation>();

        //1성
        if (-1 < player.nID && player.nID < 8)
        {
            characterSpawnerScript.curCharacterCount -= 1;
            for (int i = 0; i < playerObj.Length; i++)
            {
                var curPlayerID = playerObj[i].GetComponent<CharacterInfomation>().nID;
                var curMatID = matIDList[0] - 1;

                if (curPlayerID == curMatID)
                {
                    material2Obj.Add(playerObj[i]);
                    material2Obj[0].SetActive(false);
                }
            }
        }
        //2성
        else if (7 < player.nID && player.nID < 16)
        {
            characterSpawnerScript.curCharacterCount -= 2;
            for (int i = 0; i < playerObj.Length; i++)
            {
                var curPlayerID = playerObj[i].GetComponent<CharacterInfomation>().nID;
                var curMatID = matIDList[0] - 1;
                var curMatID_1 = matIDList[1] - 1;

                if (curPlayerID == curMatID)
                {
                    material2Obj.Add(playerObj[i]);
                    material2Obj[0].SetActive(false);
                }
                if (curPlayerID == curMatID_1)
                {
                    material3Obj.Add(playerObj[i]);
                    material3Obj[0].SetActive(false);
                }
            }
        }
        //3성
        else if (15 < player.nID && player.nID < 23)
        {
            characterSpawnerScript.curCharacterCount -= 3;
            for (int i = 0; i < playerObj.Length; i++)
            {
                var curPlayerID = playerObj[i].GetComponent<CharacterInfomation>().nID;
                var curMatID = matIDList[0] - 1;
                var curMatID_1 = matIDList[1] - 1;
                var curMatID_2 = matIDList[2] - 1;

                if (curPlayerID == curMatID)
                {
                    material2Obj.Add(playerObj[i]);
                    material2Obj[0].SetActive(false);
                }
                if (curPlayerID == curMatID_1)
                {
                    material3Obj.Add(playerObj[i]);
                    material3Obj[0].SetActive(false);
                }
                if (curPlayerID == curMatID_2)
                {
                    material4Obj.Add(playerObj[i]);
                    material4Obj[0].SetActive(false);
                }
            }
        }
    }

    public void CombineButton2()
    {
        var player = selectObjectScript.target.GetComponent<CharacterInfomation>();

        //1성
        if (-1 < player.nID && player.nID < 8)
        {
            characterSpawnerScript.curCharacterCount -= 1;
            for (int i = 0; i < playerObj.Length; i++)
            {
                var curPlayerID = playerObj[i].GetComponent<CharacterInfomation>().nID;
                var curMatID = matIDList[0] - 1;

                if (curPlayerID == curMatID)
                {
                    material2Obj.Add(playerObj[i]);
                    material2Obj[0].SetActive(false);
                }
            }
        }
        //2성
        else if (7 < player.nID && player.nID < 16)
        {
            characterSpawnerScript.curCharacterCount -= 2;
            for (int i = 0; i < playerObj.Length; i++)
            {
                var curPlayerID = playerObj[i].GetComponent<CharacterInfomation>().nID;
                var curMatID = matIDList[2] - 1;
                var curMatID_1 = matIDList[3] - 1;

                if (curPlayerID == curMatID)
                {
                    material2Obj.Add(playerObj[i]);
                    material2Obj[0].SetActive(false);
                }
                if (curPlayerID == curMatID_1)
                {
                    material3Obj.Add(playerObj[i]);
                    material3Obj[0].SetActive(false);
                }
            }
        }
        //3성
        else if (15 < player.nID && player.nID < 23)
        {
            characterSpawnerScript.curCharacterCount -= 3;
            for (int i = 0; i < playerObj.Length; i++)
            {
                var curPlayerID = playerObj[i].GetComponent<CharacterInfomation>().nID;
                var curMatID = matIDList[0] - 1;
                var curMatID_1 = matIDList[1] - 1;
                var curMatID_2 = matIDList[2] - 1;

                if (curPlayerID == curMatID)
                {
                    material2Obj.Add(playerObj[i]);
                    material2Obj[0].SetActive(false);
                }
                if (curPlayerID == curMatID_1)
                {
                    material3Obj.Add(playerObj[i]);
                    material3Obj[0].SetActive(false);
                }
                if (curPlayerID == curMatID_2)
                {
                    material4Obj.Add(playerObj[i]);
                    material4Obj[0].SetActive(false);
                }
            }
        }
    }
}
