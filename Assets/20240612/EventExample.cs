using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EventExampleDelegate();

public class EventExample : MonoBehaviour
{
    public EventExampleDelegate eventExampleDelegate;
    public event EventExampleDelegate eventExampleEvent; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        eventExampleEvent?.Invoke();
    }
}
