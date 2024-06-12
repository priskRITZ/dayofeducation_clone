using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HpBarWorld : MonoBehaviour
{
    [SerializeField] private RectTransform maskTrasform;
    [SerializeField] private RectTransform backgroundTransform;

    private float maxWidth;
    private float maxHeight;

    [SerializeField] private MyCharControllerScript _script;
    
    void Awake()
    {
        maxWidth = backgroundTransform.sizeDelta.x;
        maxHeight = backgroundTransform.sizeDelta.y;
        
        _script.HpStatusBroadCastDelegates += UpdateHpStatus;
    }

    public void UpdateHpStatus(float currentHp, float maxHp)
    {
        var lamda = new Func<int, int, int>((x, y) => x + y);

        
        // 조건문
        float factor = 1.0f;
        if (maxHp != 0.0f)
        {
            factor = currentHp / maxHp;
        }
        //
        // //삼항 연산자
        // factor = maxHp != 0.0f ? currentHp / maxHp : 1.0f;
        //
        maskTrasform.sizeDelta = new Vector2(factor * maxWidth, maxHeight);
    }
}
