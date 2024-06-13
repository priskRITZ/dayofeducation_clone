using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HpStatusBroadCast(float currentHp, float maxHp);

public class MyCharControllerScript : MonoBehaviour
{
    public HpStatusBroadCast HpStatusBroadCastDelegates;
    public List<Action<float, float>> funcs = new List<Action<float, float>>();

    public bool Test;
    
    public float BlendTime = 0.0f;
    public float OffsetTime = 0.0f;
    
    public float MoveSpeed = 5.0f;
    public float RotSpeed = 180.0f;
    public float JumpPower = 5;

    public int CurrentComboState = 0;
    
    Vector3 MoveDirection = Vector3.zero;
    Vector3 RotDirection = Vector3.zero;
    
    private float currentHp;
    private float maxHp;
    private bool _canCombo = false;
    private bool _canAttack = true;

    private bool _PressedButton = false;
    private int _ComboNumber = 0;

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

    void SetCanCombo(int v)
    {
        _canCombo = v == 1;
        
        Debug.Log("CanCombo " + _canCombo);
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        CurrentHp = 400;
        MaxHp = 400;
    }

    IEnumerator ComboSystem()
    { 
        // 콤보 카운트 먼저 증가
        _ComboNumber++;
        GetComponent<Animator>().CrossFade("Punching_B", BlendTime, -1, OffsetTime);

        // 일드를 한번 하는 이유는 애니를 재생하라고 요청하는거라서 1frame은 기다려야
        yield return null; 
        
        while (true)
        {
            // 1frame 기다려야 Punching_B로 바뀐다.
            AnimatorStateInfo stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            Debug.Log(stateInfo.IsName("Punching_B"));
            
            // Punching_B일때는 계속 기다리다가
            if (stateInfo.IsName("Punching_B"))
            {
                yield return null;
            }
            // 아니면 while문을 빠져 나가라
            else
            {
                break;
            }
        }

        // 콤보 카운트가 1인 상태에서 버튼이 눌렸었다면
        if (_ComboNumber == 1 && _PressedButton)
        {
            // 발차기 재생
            GetComponent<Animator>().CrossFade("Kick", 0, 0, 0.0f);
            // 발차기 재생 1프레임 걸림
            yield return null;
        }
        
        while (true)
        {
            AnimatorStateInfo stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            Debug.Log(stateInfo.IsName("Kick"));
            
            // 발차기 상태인지 체크하고
            if (stateInfo.IsName("Kick"))
            {
                yield return null;
            }
            // 발차기 상태가 아니면 while 빠져나가라
            else
            {
                break;
            }
        }
        
        Debug.Log("AnimationFinish");

        _ComboNumber = 0;
        _PressedButton = false;
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

        
        Animator animator = GetComponent<Animator>();
        int currentHashName = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
        
        
        // O 버튼을 눌렀는데
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (animator)
            {
                // 애니메이션 네임이 Punching_B가 아니고 _ComboNumber == 0일때
                if (currentHashName != Animator.StringToHash("Punching_B") && _ComboNumber == 0)
                {
                    // 펀칭을 수행해라
                    StartCoroutine(ComboSystem());
                    Debug.Log("Put");
                }
                
                _PressedButton = true;
            }

            // Debug.Log(_canAttack + " " + _canCombo);
            //
            // if (_canCombo)
            // {
            //     if (CurrentComboState == 0)                                  
            //     {
            //         GetComponent<Animator>().CrossFade("Kick", 0, -1, 0);
            //         CurrentComboState++;
            //     }
            // }
            // else if (_canAttack)
            // {
            //     CurrentComboState = 0;
            //     StartCoroutine(ComboSystem());
            //     _canAttack = false;
            //     //GetComponent<Animator>().Play("Punching_B");
            // }
        }
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            GetComponent<Animator>().SetTrigger("Punching");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
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
