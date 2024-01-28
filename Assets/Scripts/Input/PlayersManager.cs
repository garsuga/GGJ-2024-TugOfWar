using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public struct PlayerControlEntry {
    public int playerId;
    public EnumInput input;
}

[Serializable]
public class PlayersManager : MonoBehaviour
{
    private GameObject[] players = new GameObject[]{null, null};

    public UnityEvent<PlayerControlEntry> playerControlEntryEvent;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private object currentHaptics = null;

    IEnumerator DoHaptics(Gamepad device, float duration) {
        var id = new object();
        currentHaptics = id;
        device.ResumeHaptics();
        device.SetMotorSpeeds(0.123f, 0.234f);
        yield return new WaitForSeconds(duration);
        if(currentHaptics == id) {
            device.SetMotorSpeeds(0f, 0f);
        }
    }

    void HandleInput(EnumInput input, int ctrl, PlayerInput playerInput) {
        print("received " + input + " from " + ctrl);
        playerControlEntryEvent.Invoke(new PlayerControlEntry() {
            playerId = ctrl,
            input = input
        });
    }

    [SerializeField]
    public void OnPlayerJoined(PlayerInput input) {
        int idx = 0;
        if(players[idx] != null) {
            idx++;
        }
        players[idx] = input.gameObject;
        input.gameObject.GetComponent<PlayerController>().controlEvent.AddListener((i) => HandleInput(i, idx, input));
    }

    [SerializeField]
    public void OnPlayerLeft(PlayerInput input) {
        for(int i = 0; i < players.Length; i++) {
            if(players[i] == input.gameObject) {
                players[i] = null;
            }
        }
        input.gameObject.GetComponent<PlayerController>().controlEvent.RemoveAllListeners();
    }
}
