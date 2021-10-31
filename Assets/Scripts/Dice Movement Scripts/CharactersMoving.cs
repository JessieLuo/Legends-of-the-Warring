using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public float movingSteps;
    public float Speed = 5f;
    public Transform character;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void move() 
    {
        Event Key = Event.current;
        switch (Key.keyCode)
        {
            case KeyCode.Alpha1:
                movingSteps = 1f;
                break;
            case KeyCode.Alpha2:
                movingSteps = 2f;
                break;
            case KeyCode.Alpha3:
                movingSteps = 3f;
                break;
            default:
                movingSteps = 0;
                break;
        }
        transform.Translate(Vector2.down * 0.2f * Speed);
    }
}
