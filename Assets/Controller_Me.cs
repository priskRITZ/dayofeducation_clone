using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class Controller_Me : MonoBehaviour
{
    private Vector3 MoveDirection = Vector3.zero;
    private Vector3 RotDirection = Vector3.zero;

    public float MoveSpeed = 15.0f;
    public float RotSpeed_Degree = 180.0f;

    public float JumpPower = 5;

    public AudioClip MoveAudio;

    public CancellationTokenSource cancel;
    public bool isCancel = false;
    
    // 서버에 리퀘스트 부르는 함수를 호출한다.
    IEnumerator GetRequest(string url)
    {
        // webRequest 객체를 생성한다.
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // 서버로 send한다.
            yield return webRequest.SendWebRequest();

            // 연결 실패 시 에러를 뛰운다.
            if (webRequest.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("connection error");                
            }
            else
            {
                // 서버에서 내려온 텍스트값을 로그에 찍는다.
                Debug.Log(webRequest.downloadHandler.text);
            }
        }
    }

    // 별도의 쓰레드를 돌려서 메인 쓰레드가 멈추지 않게 한뒤
    // 뒤에서 계산한다.
    void UseThreadCalculator()
    {
        // 캔슬 객체를 만들어 Task에 넣는다.
        cancel = new CancellationTokenSource();
        var myTask = Task.Run(() => NoThreadCalculator(), cancel.Token);
    }

    void NoThreadCalculator()
    {
        // 높은 연산량을 요구하거나
        // 네트워크 응답을 기다리거나
        // 로딩을 할때 Thread가 답이 될수 있다.
        for (Int64 i = 0; i < 500000100000; i++)
        {
            // 캔슬 요청이들어오면 뺀다. thread unsafe
            if (isCancel)
                break;

            // thread safe
            if (cancel.Token.IsCancellationRequested)
                break;

            Debug.Log($"{i}");
        }

        Debug.Log("Calculator finish");
    }

    async void ASyncFunction()
    {
        // 쓰레드돌리는 함수 Task.Run
        // await은 쓰레드가 끝날때까지 기다리는 키워드
        // async가 함수 선언부에 있어야 await을 쓸수 있다.

        Int64 result = await Task.Run(() => AwaitFunction());
        print(result);
    }

    Int64 AwaitFunction()
    {
        // 높은 연산량을 요구하거나
        // 네트워크 응답을 기다리거나
        // 로딩을 할때 Thread가 답이 될수 있다.

        Int64 i = 0;
        for (; i < 500000100000; i++)
        {
            // 캔슬 요청이들어오면 뺀다. thread unsafe
            if (isCancel)
                break;

            // thread safe
            if (cancel.Token.IsCancellationRequested)
                break;

            Debug.Log($"{i}");
        }

        Debug.Log("Calculator finish");
        return i;
    }

    IEnumerator Test()
    {
        // 4초뒤에 캔슬해라
        yield return new WaitForSeconds(4);

        //threadSafe
        cancel.Cancel();
        //unthreadSafe
        isCancel = true;
    }

    // Start is called before the first frame update
     void Start()
    {
        //UseThreadCalculator();

        // cancel = new CancellationTokenSource();
        //
        // ASyncFunction();
        //
        // StartCoroutine(Test());

        // 서버에 데이터를 보낸다.
    }

    // Update is called once per frame
    void Update()
    {
        // Vertical은 w일때 1에가까워지고 s일때 -1에 가까워짐
        MoveDirection = transform.forward * Input.GetAxis("Vertical");
        // Horizontal은 w일때 1에가까워지고 s일때 -1에 가까워짐
        RotDirection = transform.right * Input.GetAxis("Horizontal");

        // 애니메이터의 Speed값에 Vertical축에 대한 값을 set한다.
        GetComponent<Animator>().SetFloat("Speed", Input.GetAxis("Vertical"));

        // 스페이스 바를 누르면 점프가 된다.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(GetRequest("localhost:3000/hello?name=jungmo"));
            StartCoroutine(GetRequest("localhost:3000/data?name=jungmo"));
        }
    }

    // 애니메이션에서 호출되는 함수
    void OnFoot()
    {
        GetComponent<AudioSource>().PlayOneShot(MoveAudio);
    }

    void FixedUpdate()
    {
        // maxDistanceDelta

        //       거리
        //   -------------
        //    속력 | 시간

        
        if (MoveDirection != Vector3.zero)
        {
            Vector3 NewPosition = Vector3.MoveTowards(transform.position, transform.position + MoveDirection * 1000.0f, MoveSpeed * Time.fixedDeltaTime);

            GetComponent<Rigidbody>().MovePosition(NewPosition);
        }

        if (RotDirection != Vector3.zero)
        {
            // 벡터를 Radian으로 구하기
            Vector3 NewDirection = Vector3.RotateTowards(transform.forward, RotDirection, RotSpeed_Degree * Mathf.Deg2Rad * Time.fixedDeltaTime, 0.0f);

            GetComponent<Rigidbody>().MoveRotation(Quaternion.LookRotation(NewDirection));

            // 쿼터니언을 Degree으로 구하기
            //Quaternion NewQuaternion = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(RotDirection), RotSpeed_Degree * Time.fixedDeltaTime);

            //GetComponent<Rigidbody>().MoveRotation(NewQuaternion);
        }
    }
}
