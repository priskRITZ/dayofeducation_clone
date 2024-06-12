using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpButton : MonoBehaviour
{
    // 좋지 못한 방식
    //public GameObject character;
    private Button _jumpButton;

    // 좀더 깔끔한 방식
    public MyCharacterControllerScript characterComp;

    // Start is called before the first frame update
    void Start()
    {
        _jumpButton = GetComponent<Button>();
        _jumpButton.onClick.AddListener(Jump);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Jump()
    {
        //if (character != null)
        //{
        //    MyCharacterControllerScript script = character.GetComponent<MyCharacterControllerScript>();

        //    character.GetComponent<Rigidbody>().AddForce(Vector3.up * script.JumpPower, ForceMode.Impulse);
        //}

        //아래 방식이 더 캡슐화 은닉화된 방법
        characterComp.Jump();
    }
}
