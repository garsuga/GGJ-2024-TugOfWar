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

    public float turnTimeSeconds = 2.5f;

    public float delaySwitchSidesSeconds = .5f;
    public float delayDestroyGameObjectsSeconds = .25f;
    public float durationMovementTweenSeconds = .5f;
    public float timeBetweenMovementUpdateSeconds = .05f;

    public AudioSource audioPlayer;
    public AudioClip failSound;
    public AudioClip successSound;
    public AudioClip inputSound;
    public AudioClip switchSound;
    public AudioClip endSequenceSound;

    private Dictionary<EnumInput, GameObject> controlPrefabMapping;
    private EnumInput[] controlChord;
    private List<GameObject> controlObjects = new List<GameObject>();
    private int entryIndex = 0;

    private bool locked = false;

    public UnityEvent<int> OnScoreChange;

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

    void SwitchSidesAnimation(Action onComplete) {
        print("Switching Sides");
        var activePlayerId = status.mode == ControlEntryMode.Original ? status.ownerPlayerId : (status.ownerPlayerId + 1) % 2;
        var endTransform = activePlayerId == 0 ? leftTransform : rightTransform;
        buttonLayoutParent.transform.DOMove(endTransform.position, durationMovementTweenSeconds).SetEase(Ease.InCubic).OnComplete(() => {
            print("Side Switching Complete");
            locked = false;
            onComplete();
        });
        //StartCoroutine(DoMotion(buttonLayoutParent, endTransform, durationMovementTweenSeconds, timeBetweenMovementUpdateSeconds, () => {
        //    print("Done Animating");
        //    locked = false;
        //}));
    }

    private object latestTimerId = new object();

    // only want callback on latest in the case of overlapping timers
    public IEnumerator ProtectedTimer(float time, Action protectedCallback) {
        print("Began turn timer");
        var id = new object();
        latestTimerId = id;
        yield return new WaitForSeconds(time);
        print("Turn Failed");
        if(id == latestTimerId) {
            protectedCallback.Invoke();
        } else {
            print("Timer was not current");
        }
    }

    public void ControlReceived(PlayerControlEntry entry) {
        if(locked) {
            return;
        }

        Action? doFailure = null;
        Action? doFinishCycle = null;
        doFinishCycle = () => {
            latestTimerId = new object();
            
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
                SwitchSidesAnimation(() => {
                    print("Should start timer coroutine here");
                    StartCoroutine(ProtectedTimer(turnTimeSeconds, () => {
                        // out of time on original entry
                        foreach(var go in controlObjects) {
                            var controller = go.GetComponent<ControlIconController>();
                            controller.DoFail();
                        }
                        doFailure();
                    }));
                });
            }));
        };

        doFailure = () => {
            locked = true;
            var failedPlayer = status.mode == ControlEntryMode.Original ? status.ownerPlayerId : (status.ownerPlayerId + 1) % 2;
            var scoreChange = failedPlayer == 0 ? 1 : -1;
            OnScoreChange.Invoke(scoreChange);
            doFinishCycle();
            audioPlayer.clip = failSound;
            audioPlayer.Play();
        };

        //if((status.mode == ControlEntryMode.Original && entry.playerId == status.ownerPlayerId) || (status.mode == ControlEntryMode.Response && entry.playerId != status.ownerPlayerId)) {
        if(true){ 
            audioPlayer.clip = inputSound;
            audioPlayer.Play();
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
                    audioPlayer.clip = endSequenceSound;
                    audioPlayer.Play();
                    // complete with owner entry
                    entryIndex = 0;
                    latestTimerId = new object();
                    StartCoroutine(DoAfter(delaySwitchSidesSeconds, () => {
                        status.mode = ControlEntryMode.Response;
                        audioPlayer.clip = switchSound;
                        audioPlayer.Play();
                        SwitchSidesAnimation(() => {
                            print("Done switching sides after initial input");
                            StartCoroutine(ProtectedTimer(turnTimeSeconds, () => {
                                // out of time with response
                                for(var i = entryIndex; i < controlObjects.Count; i++) {
                                    var controller = controlObjects[i].GetComponent<ControlIconController>();
                                    controller.DoFail();
                                }
                                doFailure();
                            }));
                        });
                    }));
                    
                }
            } else {
                bool failed = false;
                if(controlChord[entryIndex] == entry.input) {
                    // success, animate
                    var controller = controlObjects[entryIndex].GetComponent<ControlIconController>();
                    controller.DoSucceed();
                    audioPlayer.clip = successSound;
                    audioPlayer.Play();
                } else {
                    // failure, animate
                    var controller = controlObjects[entryIndex].GetComponent<ControlIconController>();
                    failed = true;
                    controller.DoFail();
                    doFailure();
                }
                entryIndex += 1;
                
                if(entryIndex >= numControlsInChord) {
                    // complete with response
                    // delete all game objects
                    if(!failed){
                        audioPlayer.clip = endSequenceSound;
                        audioPlayer.Play();
                    }
                    locked = true;
                    doFinishCycle.Invoke();
                }
            }
        }
    }
}
