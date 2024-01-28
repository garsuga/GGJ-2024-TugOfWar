using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ScenePulse : MonoBehaviour
{
    public GameObject bleachers;
    public GameObject stage;
    public GameObject background;
    public GameObject player1;
    public GameObject mainCamera;
    public PlayableAsset[] cameraAnims;
    private int cameraState;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BleacherPulse()
    {
        bleachers.GetComponent<PlayableDirector>().Play();
    }
    public void StagePulse()
    {
        stage.GetComponent<PlayableDirector>().Play();
    }
    public void BackgroundPulse()
    {
        background.GetComponent<PlayableDirector>().Play(); 
    }
    public void CameraPulse()
    {
        if(cameraState == 0)
        {
            mainCamera.GetComponent<PlayableDirector>().Play(cameraAnims[0]);
            cameraState = 1;
        }
        else
        {
           mainCamera.GetComponent<PlayableDirector>().Play(cameraAnims[1]); 
           cameraState = 0;
        }
    }
    public void Player1Pulse()
    {
        player1.GetComponent<Player>().ChangeSprite();
    }

}
