using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance = null;

    [Header("Object pool")]
    [SerializeField] public GameObject deathEffectPrefab;

    public int maxPool = 60;
    public List<GameObject> deathEffectPool = new List<GameObject>();

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

        CreatePooling();
    }

    public GameObject GetDeathEffect()
    {
        for (int i = 0; i < deathEffectPool.Count; i++)
        {
            if (deathEffectPool[i].activeSelf == false)
            {
                return deathEffectPool[i];
            }
        }

        return null;
    }


    public void CreatePooling()
    {
        GameObject objectPools = new GameObject("EffectPooling");

        for (int i = 0; i < maxPool; i++)
        {
            var death = Instantiate<GameObject>(deathEffectPrefab, objectPools.transform);
            death.name = "DeathEffect";
            death.SetActive(false);
            deathEffectPool.Add(death);
        }
    }
}