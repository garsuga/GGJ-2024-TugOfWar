using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ScenePulse : MonoBehaviour
{
    public GameObject bleachers;
    public GameObject stage;
    public GameObject background;
    private int backgroundIndex;
    // Start is called before the first frame update
    void Start()
    {
        backgroundIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField]
    public void BleacherPulse()
    {
        bleachers.GetComponent<PlayableDirector>().Play();
    }

    [SerializeField]
    public void StagePulse()
    {
        stage.GetComponent<PlayableDirector>().Play();
    }

    [SerializeField]
    public void BackgroundPulse()
    {
        if(backgroundIndex == 0)
        {
            background.GetComponent<PlayableDirector>().Play();
            backgroundIndex = 1;
        }
        else
        {
            backgroundIndex = 0;
        }
        
    }
}
