using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerInput inputManager;

    // Update is called once per frame
    void Update()
    {
    }

    private void MoveCharacter(Vector2 vector2)
    {
        transform.position = new Vector2(transform.position.x + vector2.x, transform.position.y + vector2.y); 
    }

}
