using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Spectator : MonoBehaviour
{
    public Sprite[] sprites;

    void Start()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }

    void Update()
    {
        
    }
}
