using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpButton2 : MonoBehaviour
{
    public MyCharControllerScript _script;
    
    [SerializeField]
    private Button _jumpButton;

    private Button JumpButton
    {
        get => _jumpButton;
        set => _jumpButton = value;
    }

// Start is called before the first frame update
    void Start()
    {
        JumpButton.onClick.AddListener(Jump);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Jump()
    {
        // 아래코드와 동일
        // if (_script)
        //     _script.Jump();
        
        // ==============
        
        _script?.Jump();
    }
}
