using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleComponent : MonoBehaviour
{
    // 생성자라 함은
    // 클래스가 인스턴스 화 될때 초기에 제일 먼저 불리는 함수같은 역활을 하는데
    // 유니티에서는 그 생성자를 쓰면 안됩니다.
    void Awake()
    {
        Debug.Log("Awake");
    }

    // Start is called before the first frame update
    
    // 오브젝트가 Loop 매번 틱 마다 도는데
    // 처음 Update함수가 불리기 직전에 불리는 함수
    void Start()
    {
        Debug.Log("Start");
    }

    // 컴포넌트가 켜졌을 때 호출된다. 당연히 생성시에 컴포넌트가 켜져있다면
    // 이 함수도 호출이 된다.
    void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    // 컴포넌트가 꺼졌을 때 호출 된다.
    void OnDisable()
    {
        Debug.Log("OnDisable");
    }

    // 컴포넌트가 파괴될 대 호출된다.
    void OnDestroy()
    {   
        Debug.Log("OnDestroy");
    }

    // 매 틱마다 연산을 수행하는 함수이다.
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 내가 메시 렌더러를 가져오라고 시켰고
            MeshRenderer renderer = GetComponent<MeshRenderer>();

            // 1차적인 디버깅 방법
            Debug.Log(renderer);

            // 있으면 컴포넌트가 반환될 것이고 없으면 null이 반환된다.
            if (renderer != null) {

                // 전통적인 조건문 방식의 enable disable
                if (renderer.enabled)
                {
                    renderer.enabled = false;
                }
                else if (!renderer.enabled)
                // renderer.enabled 이 false일 때 !를 만나면 true로 토글 된다.
                {
                    renderer.enabled = true;
                }

                //renderer.enabled = !renderer.enabled;
            }
        }
    }

    // Update보다 느리지만 늦은 업데이트가 하고 싶을때 쓰는겁니다.
    void LateUpdate()
    {
        
    }

    // 실제 time setting에 의해 셋팅된 값마다 불린다.
    void FixedUpdate()
    {
        
    }
}
