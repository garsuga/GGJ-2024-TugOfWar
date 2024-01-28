using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Playables;

public class Scoring : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject player1;
    public GameObject player2;
    public GameObject responseBox;
    public GameObject postProcessing;
    public PlayableAsset loseAnim;
    private int color;
    public float howMuchToMoveWithScore = 1;
    public float scoreMoveDurationSeconds = 1f;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        color = -180;
    }

    public void HandleScoreChange(int scoreChange) {
        player1.transform.DOMoveX(player1.transform.position.x + scoreChange * howMuchToMoveWithScore, scoreMoveDurationSeconds).SetEase(Ease.InCubic);
        player2.transform.DOMoveX(player2.transform.position.x + scoreChange * howMuchToMoveWithScore, scoreMoveDurationSeconds).SetEase(Ease.InCubic);
        mainCamera.transform.DOMoveX(mainCamera.transform.position.x + scoreChange * howMuchToMoveWithScore, scoreMoveDurationSeconds).SetEase(Ease.InCubic);
        responseBox.transform.DOMoveX(mainCamera.transform.position.x + scoreChange * howMuchToMoveWithScore, scoreMoveDurationSeconds).SetEase(Ease.InCubic);
        score+=scoreChange;
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

        if (score <= -10)
        {
            Win(player1);
            responseBox.SetActive(false);
            GameObject.Find("Scene Controller").SetActive(false);
            score = 0;
        }
        else if (score >= 10)
        {
            Win(player2);
            responseBox.SetActive(false);
            GameObject.Find("Scene Controller").SetActive(false);
            score = 0;
        }
    }

    public void Win(GameObject player)
    {
        if(player == player1)
        {
            print(player + " Player 1 Wins!");
            player2.GetComponentInChildren<PlayableDirector>().Play(loseAnim, DirectorWrapMode.Hold);

        }
        else
        {
            print(player + " Player 2 Wins!");
            player1.GetComponentInChildren<PlayableDirector>().Play(loseAnim, DirectorWrapMode.Hold);
        }
    }
}
