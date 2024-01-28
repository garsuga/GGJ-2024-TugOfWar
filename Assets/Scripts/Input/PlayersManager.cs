using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[Serializable]
public class PlayersManager : MonoBehaviour
{
    [SerializeField]
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    [SerializeField]
    public void OnPlayerJoined() {
        print("Add Player");
        //var newPlayer = Instantiate(playerPrefab, gameObject.transform);
        //var playerController = newPlayer.GetComponent<PlayerController>();
        //var input = newPlayer.GetComponent<PlayerInput>();
        //input.General_Controller.AddCallbacks(playerController);
    }

    [SerializeField]
    public void OnPlayerLeft() {
;;
    }
}
