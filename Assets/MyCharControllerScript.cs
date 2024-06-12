using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HpStatusBroadCast(float currentHp, float maxHp);

public class MyCharControllerScript : MonoBehaviour
{
    public HpStatusBroadCast HpStatusBroadCastDelegates;
    public List<Action<float, float>> funcs = new List<Action<float, float>>();

    public float BlendTime = 0.0f;
    
    public float MoveSpeed = 5.0f;
    public float RotSpeed = 180.0f;
    public float JumpPower = 5;
    
    Vector3 MoveDirection = Vector3.zero;
    Vector3 RotDirection = Vector3.zero;
    
    private float currentHp;
    private float maxHp;
    
    [field: SerializeField]
    private float CurrentHp
    {
        get => currentHp;
        set
        {
            currentHp = value;
            HpStatusBroadCastDelegates?.Invoke(CurrentHp, MaxHp);
        }
    }

    [field: SerializeField]
    private float MaxHp
    {
        get => maxHp;
        set
        {
            maxHp = value;
            HpStatusBroadCastDelegates?.Invoke(CurrentHp, MaxHp);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        CurrentHp = 400;
        MaxHp = 400;
    }

    // Update is called once per frame
    void Update()
    {
        float Vertical = Input.GetAxis("Vertical");
        float Horizontal = Input.GetAxis("Horizontal");

        MoveDirection = transform.forward * Vertical;
        RotDirection = transform.right * Horizontal;

        GetComponent<Animator>().SetFloat("Speed", Vertical);

        if (Input.GetKeyDown(KeyCode.P))
        {
            CurrentHp -= 10;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            // stateName은 애니메이션 이름
            // normalizedTransitionDuration은 CrossFade 전환 시간
            // layer 는 상하 분리 할때 쓴다. 지금은 -1(baseLayer)
            // norlaizedTimeOffset 애니메이션 길이를 0~1이라고 봤을때 몇퍼센트 쯤부터 재생할지 결정함 10%면 0.1
            GetComponent<Animator>().CrossFade("Punching", BlendTime, -1, 0);
        }
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            // stateName은 애니메이션 이름
            // normalizedTransitionDuration은 CrossFade 전환 시간
            // layer 는 상하 분리 할때 쓴다. 지금은 -1(baseLayer)
            // norlaizedTimeOffset 애니메이션 길이를 0~1이라고 봤을때 몇퍼센트 쯤부터 재생할지 결정함 10%면 0.1
            GetComponent<Animator>().SetTrigger("Punching");
        }

    }
    
    private void FixedUpdate()
    {
        if (MoveDirection != Vector3.zero)
        {
            Vector3 nextPosition = Vector3.MoveTowards(transform.position, transform.position + MoveDirection * 1000.0f, Time.fixedDeltaTime * MoveSpeed);

            // fixedDeltaTime == 0.02�� ������Ʈ ����
            // 50�� �Ҹ��ϱ�
            // fixedDeltaTime * MoveSpeed = 1tick�� �̵��� �Ÿ�
            //                �Ÿ�
            //           ---------------
            //            �ӷ�  |  �ð�

            GetComponent<Rigidbody>().MovePosition(nextPosition);
        }

        if (RotDirection != Vector3.zero)
        {
            Vector3 nextRotaton = Vector3.RotateTowards(transform.forward, RotDirection, Time.fixedDeltaTime *  RotSpeed * Mathf.Deg2Rad, 0.0f);

            GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(nextRotaton));
        }
    }

    public void Jump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
    }
}
