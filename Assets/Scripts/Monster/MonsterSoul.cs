using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSoul : MonoBehaviour
{
    private Canvas canvas;
    private Camera mainCamera;
    private RectTransform rectParent;
    private RectTransform rectSoul;

    [HideInInspector] public Vector3 offset = Vector3.zero;
    [HideInInspector] public Vector3 screenPos = Vector3.zero;

    private float fTime = 0;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        mainCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectSoul = this.gameObject.GetComponent<RectTransform>();
    }

    void LateUpdate()
    {
        if (screenPos.z < 0.0f) screenPos *= -1.0f;

        var localPos = Vector2.zero;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, mainCamera, out localPos);
        rectSoul.localPosition = localPos;

        if (gameObject.activeSelf)
        {
            fTime += Time.deltaTime;
            if (fTime > 0.5f) Destroy(gameObject);
        }
        else
        {
            fTime = 0;
        }
    }
}
