using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharacterControllerScript : MonoBehaviour
{
    // ���� �ʱ�ȭ ���� 1 (�ֽ� ����)
    private Vector3 MoveDirection = Vector3.zero;
    private Vector3 RotDirection = Vector3.zero;

    public float MoveSpeed = 5.0f;
    public float RotDegreeSeconds = 180.0f;
    public float JumpPower = 10.0f;

    [SerializeField]
    private float Data = 1;

    // �ܺο��� ������ �����ϳ� ���� �о�� ���ϰ� ���´�.
    public float MoveSpeedFacter {  private get; set; }

    [field: SerializeField]
    public float CurrentHp { get; private  set; }

    
    [field: SerializeField]
    public float MaxHp { get; private set; }

    // �Լ��� ���� ����� �Ʒ��� �ڵ��̴�.
    //public float GetMoveSpeedFactor()
    //{
    //    return MoveSpeedFacter;
    //}

    // ���� �ʱ�ȭ ���� 2 (�����ڿ� ����� ����)
    private void Awake()
    {
        MoveDirection = Vector3.zero;
        RotDirection = Vector3.zero;

        MoveSpeedFacter = 1.0f;
    }

    // ���� �ʱ�ȭ ���� 3 ( ù Update ���� �ʱ�ȭ ���� )
    void Start()
    {
        MoveDirection = Vector3.zero;
        RotDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // FixedUpdate�� ���� �ʱ�ȭ
        MoveDirection = Vector3.zero;
        RotDirection = Vector3.zero;

        // ��ƼĮ ���� ���� ���´�.
        float vertical = Input.GetAxis("Vertical");
        if (vertical != 0.0f)
        {
            // vertical ���� ���� �ִϸ������� Speed�� �־��ش�.
            GetComponent<Animator>().SetFloat("Speed", vertical);
        }

        // �� if�� 4���� else if�� �� �ȵǳ�
        // �ñ��� �����Ǽ� �ִµ�
        // else if�� ���� if�� ������ �����϶��� ȣ��ǹǷ�
        // Ű�� �ߺ����� ������ ����.
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

        // ���� ����ȭ
        MoveDirection.Normalize();
        RotDirection.Normalize();
    }

    public void Jump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        // �Ź� ������ٵ��� �Լ��� ���°��� �����ս� ���ϰ� ���� �� �ֱ⿡
        // �����ӿ� ���� ���� ���� ���� �����Ѵ�.
        if (MoveDirection != Vector3.zero)
        {
            // MoveDirection�� 1000�� ���ϴ� ������ �������� ���� ���⿡
            // MoveDirection�������� 1000m ���������� �������� ��� �����̴�.
            // �̷��� ���ϸ� MoveSpeed�� ������ �������� ��
            // 1�̻��� ���� ������ ���װ� �����.
            Vector3 newPosition = Vector3.MoveTowards(transform.position, transform.forward + MoveDirection * 1000.0f, MoveSpeed * MoveSpeedFacter * Time.fixedDeltaTime);

            GetComponent<Rigidbody>().MovePosition(newPosition);
        }

        if (RotDirection != Vector3.zero)
        {
            // Quaternion.LookRotation�� Vector3�� Quaternionȭ �����ش�.
            Quaternion newQuat = Quaternion.RotateTowards(Quaternion.LookRotation(transform.forward), Quaternion.LookRotation(RotDirection), RotDegreeSeconds * Time.fixedDeltaTime);

            GetComponent<Rigidbody>().MoveRotation(newQuat);
        }
    }
}
