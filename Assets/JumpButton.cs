using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpButton : MonoBehaviour
{
    // ���� ���� ���
    //public GameObject character;
    private Button _jumpButton;

    // ���� ����� ���
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

        //�Ʒ� ����� �� ĸ��ȭ ����ȭ�� ���
        characterComp.Jump();
    }
}
