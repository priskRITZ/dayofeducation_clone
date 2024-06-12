using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HpBar2 : MonoBehaviour
{
    //private List<Action<float, float>> _callbacks = new List<Action<float, float>>();
    
    [SerializeField]
    private TextMeshProUGUI hpText;

    [SerializeField] private RectTransform maskTrasform;
    [SerializeField] private RectTransform backgroundTransform;

    private float maxWidth;
    private float maxHeight;

    [SerializeField] private MyCharControllerScript _script;

    // public static HpBar2 operator +(HpBar2 origin, Action<float, float> action2)
    // {
    //     origin._callbacks.Add(action2);
    //     return origin;
    // }

    void Awake()
    {
        maxWidth = backgroundTransform.sizeDelta.x;
        maxHeight = backgroundTransform.sizeDelta.y;

        _script.HpStatusBroadCastDelegates += UpdateHpStatus;
        _script.HpStatusBroadCastDelegates += UpdateHpStatus;
        _script.HpStatusBroadCastDelegates += UpdateHpStatus;
        _script.HpStatusBroadCastDelegates += UpdateHpStatus;
        
        _script.funcs.Add(UpdateHpStatus);
        _script.funcs.Add(UpdateHpStatus);
        _script.funcs.Add(UpdateHpStatus);
        _script.funcs.Add(UpdateHpStatus);
        
        _script.HpStatusBroadCastDelegates?.Invoke(3, 5);
        
        foreach (var scriptFunc in _script.funcs)
        {
            scriptFunc?.Invoke(3, 5);
        }

        // Action<float, float> action = (float currentHp, float maxHp) => { UpdateHpStatus(currentHp, maxHp); };
        //
        // var hpBar2 = this;
        // hpBar2 += action;
        //
        // _callbacks.Add(action);
    }

    public void UpdateHpStatus(float currentHp, float maxHp)
    {
        hpText.text = $"{currentHp}/{maxHp}";

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
