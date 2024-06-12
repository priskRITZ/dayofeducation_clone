using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderComp : MonoBehaviour
{
    [SerializeField] private MyCharControllerScript _script;
    [SerializeField] private Slider _slider;
    
    // Start is called before the first frame update
    void Start()
    {
        //슬라이더의 게이지값이 바뀌면 OnValueChanged함수가 호출된다.
        _slider.onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(float v)
    {
        _script.JumpPower = v;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
