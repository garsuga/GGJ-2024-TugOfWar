using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject player1;
    public GameObject player2;
    public int score;
    public int pendingScore;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        pendingScore = 0;
    }

    public void HandleScoreChange(int scoreChange) {
        print(scoreChange);
    }

    // Update is called once per frame
    void Update()
    {
        if (pendingScore == 1)
        {
            var temp = mainCamera.transform.position.x;
            while(mainCamera.transform.position.x >= temp - 1)
            {
                mainCamera.transform.Translate(Vector3.left * Time.deltaTime);
                player1.transform.Translate(Vector3.left * Time.deltaTime);
                player2.transform.Translate(Vector3.left * Time.deltaTime);
            }
            pendingScore = 0;
            score++;
        }
        else if (pendingScore == -1)
        {
            mainCamera.transform.Translate(Vector3.right * Time.deltaTime);
            player1.transform.Translate(Vector3.right * Time.deltaTime);
            player2.transform.Translate(Vector3.right * Time.deltaTime);
            pendingScore = 0;
            score--;
        }
        else
        {

        }
    }
}
