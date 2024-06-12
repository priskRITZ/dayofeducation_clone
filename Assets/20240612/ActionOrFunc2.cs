using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionOrFunc2 : MonoBehaviour
{
    // 반환값이 없다.
    private Action<int, float, bool> _action;
    
    // Func의 마지막 paramater는 반환값이다.
    private Func<int, float, string> _func;

    public void RegistFunc(Func<int, float, string> __func)
    {
        _func = __func;
        
        // 다 지원
        // _func = __func;
        // _func += __func;
        // _func -= __func;
    }
    
    public void RegistAction(Action<int, float, bool> __action)
    {
        _action = __action;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        _action?.Invoke(1, 2.0f, true);
        string printResult = _func?.Invoke(1, 2.0f);
        if (printResult != null)
            Debug.Log(printResult);
    }
}
