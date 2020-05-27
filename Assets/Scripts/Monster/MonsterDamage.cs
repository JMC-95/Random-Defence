using UnityEngine;
using UnityEngine.UI;

public class MonsterDamage : MonoBehaviour
{
    private GameManager gameManager;

    private MonsterSpawner monsterSpawnerScript;
    public SkinnedMeshRenderer skin;

    public GameObject hpBarPrefab;
    private GameObject hpBar;
    public GameObject soulPrefab;
    private GameObject soulMoney;

    public Vector3 hpBarOffset;

    private Canvas uiCanvas;
    public Image hpBarImage;

    private float colorTime;
    public float curHp;
    public float initHp;

    public bool isDie;

    public void Start()
    {
        monsterSpawnerScript = GameObject.Find("MonsterSpawner").GetComponent<MonsterSpawner>();
        skin = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    void LateUpdate()
    {
        //체력바의 위치 변경
        if (transform.localScale.x == -2)
        {
            var _hpBar = hpBar.GetComponent<MonsterHpBar>();
            _hpBar.offset = new Vector3(hpBarOffset.x, hpBarOffset.y, hpBarOffset.z);
        }
        else
        {
            var _hpBar = hpBar.GetComponent<MonsterHpBar>();
            _hpBar.offset = new Vector3(-hpBarOffset.x, hpBarOffset.y, hpBarOffset.z);
        }

        //피격시 머티리얼 색상 변경
        if (skin.material.color != Color.white)
        {
            colorTime += Time.deltaTime;

            if (colorTime > 0.3f)
            {
                colorTime = 0.0f;
                skin.material.color = Color.white;
            }
        }
    }

    public void SetHpBar(int hp)
    {
        if (!gameManager)
        {
            gameManager = GameManager.Get();
        }

        initHp = hp;
        curHp = initHp;

        uiCanvas = GameObject.Find("UI Canvas").GetComponent<Canvas>();
        hpBar = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];

        var _hpBar = hpBar.GetComponent<MonsterHpBar>();
        _hpBar.targetTr = this.gameObject.transform;
        _hpBar.offset = hpBarOffset;
    }

    public void GetSoul()
    {
        var money = soulPrefab.GetComponentInChildren<Text>();
        money.text = GetComponent<MonsterMove>().Soul.ToString();
        soulMoney = Instantiate<GameObject>(soulPrefab, uiCanvas.transform);

        var _soulMoney = soulMoney.GetComponent<MonsterSoul>();
        _soulMoney.offset = new Vector3(0.5f, 3.0f, 0.0f);
        _soulMoney.screenPos = Camera.main.WorldToScreenPoint(transform.position + _soulMoney.offset);
    }

    public void Die()
    {
        if (!isDie)
        {
            var effectManager = EffectManager.instance;
            var deathEffect = effectManager.GetEffect("DeathSoul");

            isDie = true;
            monsterSpawnerScript.curMonster -= 1;
            gameManager.UseSoul(-GetComponent<MonsterMove>().Soul);
            effectManager.SetToEffect(deathEffect, transform);
            gameObject.SetActive(false);
            Destroy(hpBar);
        }
    }
}
