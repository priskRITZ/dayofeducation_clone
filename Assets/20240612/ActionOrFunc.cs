
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PrintFunc();

public class ActionOrFunc : MonoBehaviour
{
    private PrintFunc printFunc;
    
    void Func1()
    {
        Debug.Log("Func1");
    }
    
    void Func2()
    {
        Debug.Log("Func2");
    }
    
    void Func3()
    {
        Debug.Log("Func3");
    }
    
    void Func4()
    {
        Debug.Log("Func4");
    }
    
    void Func5()
    {
        Debug.Log("Func5");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        printFunc = null;
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            printFunc = Func1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            printFunc = Func2;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            printFunc = Func3;
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            printFunc = Func4;
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            printFunc = Func5;
        }
    }

    void LateUpdate()
    {
        printFunc?.Invoke();   
    }
}
