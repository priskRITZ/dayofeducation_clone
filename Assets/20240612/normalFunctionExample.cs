using System.Collections;
using System.Collections.Generic;
using normalFunctionExampleNS;
using UnityEngine;

namespace normalFunctionExampleNS
{
    public class TestClass
    {
        public void AddResult(int result)
        {
            Debug.Log($"AddResult Func : {result}");
        }

        public void SubstractResult(int result)
        {
            Debug.Log($"SubstractResult Func : {result}");
        }
    }

    public class Calculator
    {
        public string functionName = string.Empty;
        
        public TestClass testClass;
        
        // Add 함수 제작
        public void Add(int a, int b)
        {
            int result = a + b;
            testClass.AddResult(result);
        }

        // Subtract 함수 제작
        public void Subtract(int a, int b)
        {
            int result = a - b;
            testClass.SubstractResult(result);
        }
        
        public IEnumerator CalculatorDelayPrint()
        {
            yield return new WaitForSeconds(10.0f);

            if (functionName == "Add")
            {
                Add(3, 5);
            }
            else if (functionName == "Substract")
            {
                Subtract(3, 5);
            }
        }
    }
}


public class normalFunctionExample : MonoBehaviour
{
    private Calculator _calculator;
    private int sumResult = 0;
    
    public TestClass testClass;
    
    // Start is called before the first frame update
    void Start()
    {
        _calculator = new Calculator();
        testClass = new TestClass();

        _calculator.testClass = testClass;
        StartCoroutine(_calculator.CalculatorDelayPrint());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _calculator.functionName = "Add";
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _calculator.functionName = "Substract";
        }
    }
}
