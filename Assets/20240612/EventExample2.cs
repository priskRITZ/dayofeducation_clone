using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventExample2 : MonoBehaviour
{
    public EventExample eventObject;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void AttachFunc()
    {
        Debug.Log("????????");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            eventObject.eventExampleDelegate += AttachFunc;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            eventObject.eventExampleEvent += AttachFunc;
        }
    }
}
