using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    //Enum to make reading/writing easier, rather than a variable.
    public enum States
    {
        Patrol,
        Run,
    }



    public States state = States.Patrol;


    private void Start()
    {
        NextState();
    }
    //Set possible sates to the as States enum to be able to utilise in-code.
    //The function (void "NextState") can be named/declared anything.
    void NextState()
    {
        switch (state)
        //Shortcut command for making several "If" statements.
        {
            case States.Patrol:
                StartCoroutine(PatrolState());
                break;
            case States.Run:
                StartCoroutine(RunState());
                break;
            default:
                Debug.LogError("");
                break;
        }
    }
    IEnumerator PatrolState()
    {
        Debug.Log("Entering Patrol State...");
        //Remain in this loop while in the Patrol State.
        while (state == States.Patrol)
        {
            transform.rotation *= Quaternion.Euler(0,50f*Time.deltaTime,0);
            //Any code here will run once per frame; similar to void Update.
            yield return null; //Wait a single frame with blank data return before continuing the rest of the code.
        }
        Debug.Log("Exiting Patrol State...");
        NextState();
    }

    

    IEnumerator RunState()
    {
        Debug.Log("Entering Run State...");
        float startTime = Time.time;
        while (state == States.Run)
        {
            transform.position += transform.right * 1f * Time.deltaTime;
            if(Time.time - startTime > 10f)
            {
                state = States.Patrol;
            }
            yield return null;
        }
        Debug.Log("Exiting Run State...");
        NextState();
    }
}



