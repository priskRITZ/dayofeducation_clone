using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

using System.Threading.Tasks;
using System;










public class MyCharacterController : MonoBehaviour
{
    // 이동하는 방향에 대한 변수
    private Vector3 MoveDirection;

    // 회전하는 방향에 대한 변수
    private Vector3 RotDirection;

    public float MoveSpeed = 5.0f;
    public float RotSpeed = 180.0f;

    private float TestDuration = 0.0f;
    private bool TestDisplayed = false;

    private int K_KeyDownCount = 0;

    IEnumerator K_KeyDownCountFunc()
    {
        yield return new WaitWhile(() =>  K_KeyDownCount < 10);

        Debug.Log("WaitWhile Finish");
    }

    async void TestRun()
    {
        await Task.Run(() => TestAsync());

        Debug.Log("AsyncFinish");
    }

    void TestAsync()
    {
        for (Int64 i = 0; i < 50000000000; i++)
        {

        }
    }

    IEnumerator TestCoroutine()
    {
        for (Int64 i = 0; i < 30000; i++)
        {
            yield return null; // FPS 한번당 null;
        }

        Debug.Log("Coroutine Finish");
    }

    float startTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.realtimeSinceStartup;

        StartCoroutine(TestCoroutine());

        Debug.Log(Time.realtimeSinceStartup - startTime);

        StartCoroutine(K_KeyDownCountFunc());
    }

    IEnumerator TestCoroutine(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);

            Debug.Log("Test Success");
        }
    }


    // Update is called once per frame
    void Update()
    {

        MoveDirection = Vector3.zero;
        RotDirection = Vector3.zero;

        float Vertical = Input.GetAxis("Vertical");
        float Horizontal = Input.GetAxis("Horizontal");

        MoveDirection = transform.forward * Vertical;
        RotDirection = transform.right * Horizontal;

        GetComponent<Animator>().SetFloat("Speed", Vertical);

        if (Input.GetKeyDown(KeyCode.K))
        {
            K_KeyDownCount++;
        }

        //if (!TestDisplayed && TestDuration >= 4.0f)
        //{
        //    Debug.Log("Test");
        //    TestDuration = 0.0f;
        //    TestDisplayed = true;
        //}

        //TestDuration += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (MoveDirection != Vector3.zero)
        {
            Vector3 nextPosition = Vector3.MoveTowards(transform.position, transform.position + MoveDirection * 1000.0f, Time.fixedDeltaTime * MoveSpeed);

            // fixedDeltaTime == 0.02초 프로젝트 설정
            // 50번 불리니까
            // fixedDeltaTime * MoveSpeed = 1tick당 이동할 거리
            //                거리
            //           ---------------
            //            속력  |  시간

            GetComponent<Rigidbody>().MovePosition(nextPosition);
        }

        if (RotDirection != Vector3.zero)
        {
            Vector3 nextRotaton = Vector3.RotateTowards(transform.forward, RotDirection, Time.fixedDeltaTime *  RotSpeed * Mathf.Deg2Rad, 0.0f);

            GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(nextRotaton));
        }
    }
}
