using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterControllerScript : MonoBehaviour
{
    // 변수 초기화 예제 1 (최신 문법)
    private Vector3 MoveDirection = Vector3.zero;
    private Vector3 RotDirection = Vector3.zero;

    public float MoveSpeed = 5.0f;
    public float RotDegreeSeconds = 180.0f;
    public float JumpPower = 10.0f;

    [SerializeField]
    private float Data = 1;

    // 외부에서 셋팅은 가능하나 값을 읽어가진 못하게 막는다.
    public float MoveSpeedFacter {  private get; set; }

    [field: SerializeField]
    public float CurrentHp { get; private  set; }

    
    [field: SerializeField]
    public float MaxHp { get; private set; }

    // 함수로 막는 방법은 아래의 코드이다.
    //public float GetMoveSpeedFactor()
    //{
    //    return MoveSpeedFacter;
    //}

    // 변수 초기화 예제 2 (생성자에 가까운 문법)
    private void Awake()
    {
        MoveDirection = Vector3.zero;
        RotDirection = Vector3.zero;

        MoveSpeedFacter = 1.0f;
    }

    // 변수 초기화 예제 3 ( 첫 Update 전에 초기화 문법 )
    void Start()
    {
        MoveDirection = Vector3.zero;
        RotDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // FixedUpdate를 위한 초기화
        MoveDirection = Vector3.zero;
        RotDirection = Vector3.zero;

        // 버티칼 축의 값을 얻어온다.
        float vertical = Input.GetAxis("Vertical");
        if (vertical != 0.0f)
        {
            // vertical 축의 값을 애니메이터의 Speed에 넣어준다.
            GetComponent<Animator>().SetFloat("Speed", vertical);
        }

        // 왜 if가 4개냐 else if는 왜 안되냐
        // 궁금증 가지실수 있는데
        // else if는 위의 if의 조건이 거짓일때만 호출되므로
        // 키를 중복으로 누를수 없다.
        if (Input.GetKey(KeyCode.W))
        {
            MoveDirection = transform.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            MoveDirection -= transform.forward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            RotDirection -= transform.right;
        }

        if (Input.GetKey(KeyCode.D))
        {
            RotDirection += transform.right;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            CurrentHp -= 10;
        }

        // 백터 정규화
        MoveDirection.Normalize();
        RotDirection.Normalize();
    }

    public void Jump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        // 매번 리지드바디의 함수를 쓰는것은 퍼포먼스 저하가 있을 수 있기에
        // 움직임에 대한 값이 있을 때만 설정한다.
        if (MoveDirection != Vector3.zero)
        {
            // MoveDirection에 1000을 곱하는 이유는 목적지가 따로 없기에
            // MoveDirection방향으로 1000m 떨어진곳을 목적지로 잡기 위함이다.
            // 이렇게 안하면 MoveSpeed가 굉장히 빨라졌을 때
            // 1이상의 값을 가지면 버그가 생긴다.
            Vector3 newPosition = Vector3.MoveTowards(transform.position, transform.forward + MoveDirection * 1000.0f, MoveSpeed * MoveSpeedFacter * Time.fixedDeltaTime);

            GetComponent<Rigidbody>().MovePosition(newPosition);
        }

        if (RotDirection != Vector3.zero)
        {
            // Quaternion.LookRotation은 Vector3를 Quaternion화 시켜준다.
            Quaternion newQuat = Quaternion.RotateTowards(Quaternion.LookRotation(transform.forward), Quaternion.LookRotation(RotDirection), RotDegreeSeconds * Time.fixedDeltaTime);

            GetComponent<Rigidbody>().MoveRotation(newQuat);
        }
    }
}
