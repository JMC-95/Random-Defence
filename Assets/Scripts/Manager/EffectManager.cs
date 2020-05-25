using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance = null;

    [Header("Object pool")]
    [SerializeField] public GameObject deathEffectPrefab;

    private int maxPool = 50;
    public List<GameObject> deathEffectPools = new List<GameObject>();

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
        for (int i = 0; i < deathEffectPools.Count; i++)
        {
            if (deathEffectPools[i].activeSelf == false)
            {
                return deathEffectPools[i];
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
            deathEffectPools.Add(death);
        }
    }
}