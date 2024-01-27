using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class BPMBleachers : MonoBehaviour
{
    public GameObject Bleachers_Downbeat;
    
    public GameObject Bleachers_Upbeat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    [SerializeField]
    public void DoMotion() {
        if(Bleachers_Downbeat.activeSelf == true)
        {
            Bleachers_Downbeat.SetActive(false);
            Bleachers_Upbeat.SetActive(true);
        }
        else
        {
            Bleachers_Downbeat.SetActive(true);
            Bleachers_Upbeat.SetActive(false);
        }
    }
}
