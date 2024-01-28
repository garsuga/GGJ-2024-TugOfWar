using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using DG.Tweening;

public enum ControlEntryMode {
    Original,
    Response
}

public struct EntryStatus {
    public ControlEntryMode mode;
    public int ownerPlayerId;
}

public class ControlIconManager : MonoBehaviour
{

    [SerializeField]
    public int numControlsInChord = 4;

    public GameObject XPrefab;
    public GameObject YPrefab;
    public GameObject APrefab;
    public GameObject BPrefab;
    public GameObject UpPrefab;
    public GameObject DownPrefab;
    public GameObject LeftPrefab;
    public GameObject RightPrefab;
    public GameObject LBPrefab;
    public GameObject RBPrefab;
    public GameObject LTPrefab;
    public GameObject RTPrefab;

    public Transform[] controlTransforms;

    public Transform leftTransform;
    public Transform rightTransform;

    public GameObject buttonLayoutParent;


    public float delaySwitchSidesSeconds = .5f;
    public float delayDestroyGameObjectsSeconds = .25f;
    public float durationMovementTweenSeconds = .5f;
    public float timeBetweenMovementUpdateSeconds = .05f;

    private Dictionary<EnumInput, GameObject> controlPrefabMapping;
    private EnumInput[] controlChord;
    private List<GameObject> controlObjects = new List<GameObject>();
    private int entryIndex = 0;

    private bool locked = false;



    private EntryStatus status;

    // Start is called before the first frame update
    void Start()
    {
        controlChord = new EnumInput[numControlsInChord];
        controlPrefabMapping = new Dictionary<EnumInput, GameObject>(){
            {EnumInput.X, XPrefab},
            {EnumInput.Y, YPrefab},
            {EnumInput.A, APrefab},
            {EnumInput.B, BPrefab},
            {EnumInput.LeftTrigger, LTPrefab},
            {EnumInput.RightTrigger, RTPrefab},
            {EnumInput.LeftBumper, LBPrefab},
            {EnumInput.RightBumper, RBPrefab},
            {EnumInput.Up, UpPrefab},
            {EnumInput.Left, LeftPrefab},
            {EnumInput.Right, RightPrefab},
            {EnumInput.Down, DownPrefab}
        };
        status = new EntryStatus() {
            mode = ControlEntryMode.Original,
            ownerPlayerId = 0
        };
        buttonLayoutParent.transform.position = leftTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DoAfter(float seconds, Action action) {
        yield return new WaitForSeconds(seconds);
        action.Invoke();
    }

    void SwitchSidesAnimation() {
        print("Switching Sides");
        var activePlayerId = status.mode == ControlEntryMode.Original ? status.ownerPlayerId : (status.ownerPlayerId + 1) % 2;
        var endTransform = activePlayerId == 0 ? leftTransform : rightTransform;
        buttonLayoutParent.transform.DOMove(endTransform.position, durationMovementTweenSeconds).SetEase(Ease.InCubic).OnComplete(() => locked = false);
        //StartCoroutine(DoMotion(buttonLayoutParent, endTransform, durationMovementTweenSeconds, timeBetweenMovementUpdateSeconds, () => {
        //    print("Done Animating");
        //    locked = false;
        //}));
    }

    public void ControlReceived(PlayerControlEntry entry) {
        if(locked) {
            return;
        }
        //if((status.mode == ControlEntryMode.Original && entry.playerId == status.ownerPlayerId) || (status.mode == ControlEntryMode.Response && entry.playerId != status.ownerPlayerId)) {
        if(true){ 
            if(status.mode == ControlEntryMode.Original) {
                controlChord[entryIndex] = entry.input;
                // create object and animate
                var go = Instantiate(controlPrefabMapping[entry.input], buttonLayoutParent.transform);
                controlObjects.Add(go);
                // go.transform.Translate(controlTransforms[entryIndex].position);
                go.transform.position = controlTransforms[entryIndex].position;
                var controller = go.GetComponent<ControlIconController>();
                controller.DoEntry();
                entryIndex += 1;
                if(entryIndex >= numControlsInChord) {
                    locked = true;
                    // complete with owner entry
                    entryIndex = 0;
                    StartCoroutine(DoAfter(delaySwitchSidesSeconds, () => {
                        status.mode = ControlEntryMode.Response;
                        SwitchSidesAnimation();
                    }));
                    
                }
            } else {
                if(controlChord[entryIndex] == entry.input) {
                    // success, animate
                    var controller = controlObjects[entryIndex].GetComponent<ControlIconController>();
                    controller.DoSucceed();
                } else {
                    // failure, animate
                    var controller = controlObjects[entryIndex].GetComponent<ControlIconController>();
                    controller.DoFail();
                }
                entryIndex += 1;
                
                if(entryIndex >= numControlsInChord) {
                    // complete with response
                    // delete all game objects
                    locked = true;
                    StartCoroutine(DoAfter(delaySwitchSidesSeconds, () => {
                        entryIndex = 0;
                        foreach(var go in controlObjects) {
                            var controller = go.GetComponent<ControlIconController>();
                            controller.DoExit();
                            StartCoroutine(DoAfter(delayDestroyGameObjectsSeconds, () => Destroy(go)));
                        }
                        controlObjects = new List<GameObject>();
                    
                        status.mode = ControlEntryMode.Original;
                        status.ownerPlayerId = (status.ownerPlayerId + 1) % 2;
                        SwitchSidesAnimation();
                    }));
                }
            }
        }
    }
}
