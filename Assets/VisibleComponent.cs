using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleComponent : MonoBehaviour
{
    // �����ڶ� ����
    // Ŭ������ �ν��Ͻ� ȭ �ɶ� �ʱ⿡ ���� ���� �Ҹ��� �Լ����� ��Ȱ�� �ϴµ�
    // ����Ƽ������ �� �����ڸ� ���� �ȵ˴ϴ�.
    void Awake()
    {
        Debug.Log("Awake");
    }

    // Start is called before the first frame update
    
    // ������Ʈ�� Loop �Ź� ƽ ���� ���µ�
    // ó�� Update�Լ��� �Ҹ��� ������ �Ҹ��� �Լ�
    void Start()
    {
        Debug.Log("Start");
    }

    // ������Ʈ�� ������ �� ȣ��ȴ�. �翬�� �����ÿ� ������Ʈ�� �����ִٸ�
    // �� �Լ��� ȣ���� �ȴ�.
    void OnEnable()
    {
        Debug.Log("OnEnable");
    }

    // ������Ʈ�� ������ �� ȣ�� �ȴ�.
    void OnDisable()
    {
        Debug.Log("OnDisable");
    }

    // ������Ʈ�� �ı��� �� ȣ��ȴ�.
    void OnDestroy()
    {   
        Debug.Log("OnDestroy");
    }

    // �� ƽ���� ������ �����ϴ� �Լ��̴�.
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ���� �޽� �������� ��������� ���װ�
            MeshRenderer renderer = GetComponent<MeshRenderer>();

            // 1������ ����� ���
            Debug.Log(renderer);

            // ������ ������Ʈ�� ��ȯ�� ���̰� ������ null�� ��ȯ�ȴ�.
            if (renderer != null) {

                // �������� ���ǹ� ����� enable disable
                if (renderer.enabled)
                {
                    renderer.enabled = false;
                }
                else if (!renderer.enabled)
                // renderer.enabled �� false�� �� !�� ������ true�� ��� �ȴ�.
                {
                    renderer.enabled = true;
                }

                //renderer.enabled = !renderer.enabled;
            }
        }
    }

    // Update���� �������� ���� ������Ʈ�� �ϰ� ������ ���°̴ϴ�.
    void LateUpdate()
    {
        
    }

    // ���� time setting�� ���� ���õ� ������ �Ҹ���.
    void FixedUpdate()
    {
        
    }
}
