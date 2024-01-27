using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{     
    public float Delay;

    private IEnumerator waitCoroutine;
    
    public UnityEvent DoTick;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnEnable()
    {
        waitCoroutine = Wait(Delay);
        StartCoroutine(waitCoroutine);
    }

    void OnDisable()
    {
        if(waitCoroutine != null) 
        {
            StopCoroutine(waitCoroutine);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    IEnumerator Wait(float delay)
    {
        while(true) {
            DoTick.Invoke();
            yield return new WaitForSeconds(delay);
        }
    }
}
