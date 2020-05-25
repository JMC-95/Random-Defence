using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance = null;

    [Header("Effect Resources")]
    [SerializeField] public GameObject[] effectPrefabs;
    public string bagicPath = "Effects/";

    [Header("Object pool")]
    private int maxPool = 50;
    public Dictionary<string, List<GameObject>> effectPools;

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

    public void SetToEffect(GameObject effectObj, Transform effectTransform)
    {
        effectObj.transform.position = effectTransform.position;
        effectObj.SetActive(true);
    }

    public GameObject GetEffect(string effectName)
    {
        var list = effectPools[effectName];

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].activeSelf == false)
            {
                return list[i];
            }
        }

        return null;
    }

    public void CreatePooling()
    {
        GameObject objectPools = new GameObject("EffectPooling");

        effectPools = new Dictionary<string, List<GameObject>>();
        LoadEffectPrefabs();

        for (int i = 0; i < Type.Effect.Max; ++i)
        {
            var effectName = Type.Effect.ToString(i);

            List<GameObject> effectPool = new List<GameObject>();

            for (int j = 0; j < maxPool; j++)
            {
                var effect = Instantiate<GameObject>(effectPrefabs[i], objectPools.transform) as GameObject;

                effectPool.Add(effect);
                effect.SetActive(false);
            }

            effectPools.Add(effectName, effectPool);
        }
    }

    public void LoadEffectPrefabs()
    {
        effectPrefabs = new GameObject[Type.Effect.Max];

        for (int i = 0; i < Type.Effect.Max; ++i)
        {
            effectPrefabs[i] = Resources.Load(bagicPath + Type.Effect.ToString(i)) as GameObject;
        }
    }
}