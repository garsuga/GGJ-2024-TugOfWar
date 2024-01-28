using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scoring : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject player1;
    public GameObject player2;

    public float howMuchToMoveWithScore = 1;
    public float scoreMoveDurationSeconds = 1f;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    public void HandleScoreChange(int scoreChange) {
        player1.transform.DOMoveX(player1.transform.position.x + scoreChange * howMuchToMoveWithScore, scoreMoveDurationSeconds).SetEase(Ease.InCubic);
        player2.transform.DOMoveX(player2.transform.position.x + scoreChange * howMuchToMoveWithScore, scoreMoveDurationSeconds).SetEase(Ease.InCubic);
        mainCamera.transform.DOMoveX(mainCamera.transform.position.x + scoreChange * howMuchToMoveWithScore, scoreMoveDurationSeconds).SetEase(Ease.InCubic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
