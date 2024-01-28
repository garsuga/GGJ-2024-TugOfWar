using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ScenePulse : MonoBehaviour
{
    public GameObject bleachers;
    public GameObject stage;
    public GameObject background;
    public GameObject player1;
    public GameObject player2;
    public GameObject mainCamera;
    public GameObject postProcessing;
    public PlayableAsset[] cameraAnims;
    private int color;
    private int cameraState;
    // Start is called before the first frame update
    void Start()
    {
        color = -180;
    }

    // Update is called once per frame
    void Update()
    {
        postProcessing.GetComponent<Volume>().profile.TryGet(out ColorAdjustments p);
        p.hueShift.value = color;
        color+=3;
        if(color > 180)
        {
            color = -180;
        }
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
        player1.GetComponent<Player1>().ChangeSprite();
    }
    public void Player2Pulse()
    {
        player2.GetComponent<Player2>().ChangeSprite();
    }

}
