using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController_Me : MonoBehaviour
{
    public float speed = 20;
    public float rotSpeed;
    public float jumpPower;

    bool isGrounded = true;

    // 인풋은 Update에서 받기 위해
    // direction과 rotDirection을 멤버 변수로 두고
    Vector3 direction = Vector3.zero;
    Vector3 rotDirection = Vector3.zero;

    public GameObject CharacterObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        // 매 업데이트 마다 direction, rotDirection을 초기화 해줘서
        // 그 다음 인풋적용되도록 한다.
        direction = Vector3.zero;
        rotDirection = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            isGrounded = false;
        }

        if (Input.GetKey(KeyCode.W))
        {
            direction += CharacterObject.transform.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction += -CharacterObject.transform.forward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rotDirection += -CharacterObject.transform.right;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rotDirection += CharacterObject.transform.right;
        }

        // 벡터의 길이를 정규화 하여 길이가 1인 벡터를 만드는 로직
        direction.Normalize();
        rotDirection.Normalize();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 이동 로직 
        // current, target까지 speed * Time.fixedDeltaTime만큼 이동한 벡터의 위치를 반환한다.
        Vector3 nextPosition = Vector3.MoveTowards(transform.position, transform.position + direction * 1000, speed * Time.fixedDeltaTime);

        // nextPosition으로 물리이동한다.
        GetComponent<Rigidbody>().MovePosition(nextPosition);
        // 이동 로직 끝

        // 회전 로직
        if (rotDirection.sqrMagnitude > 0.001f)
        {
            // newRot을 계산한다.
            // trnasform.rotation은 현재 내가 보고 있는 방향이다.
            // Quaternion.LookRotation(direction) // 내가 바라볼 방향이다.
            // Quaternion의 RotateToward는 current -> new 로 회전할 값을 정하는데
            // 그 양이 Time.fixedDeltaTime * rotSpeed 값이다.
            Quaternion newRot = Quaternion.RotateTowards(CharacterObject.transform.rotation, Quaternion.LookRotation(rotDirection), Time.fixedDeltaTime * rotSpeed);

            CharacterObject.transform.rotation = newRot;
        }
        // 회전 로직 끝


        // ProjectSetting -> Time -> fixed Timestep 0.02
        // Time.fixedDeltaTime == 0.02
    }

    // 이 컴포넌트의 gameObject의 컬리젼이 다른 물체와 충돌시 collision에 충돌 판정이 일어남
    void OnCollisionEnter(Collision collision)
    {
        // collision.tag ? == ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);

        if (other.gameObject.CompareTag("Jewelry"))
        {
            other.gameObject.GetComponent<ScoreEffectScript>().OnHit();
        }
    }
}
