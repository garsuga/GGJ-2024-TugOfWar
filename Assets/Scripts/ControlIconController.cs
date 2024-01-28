using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

public class ControlIconController : MonoBehaviour
{
    [SerializeField]
    public PlayableAsset fadeInAnimation;
    [SerializeField]
    public PlayableAsset fadeOutAnimation;
    [SerializeField]
    public PlayableAsset failAnimation;

    [SerializeField]
    public PlayableDirector director;

    public void DoEntry() {
        director.Play(fadeInAnimation);
    }

    public void DoSucceed() {
        
    }

    public void DoFail() {
        director.Play(failAnimation);
    }

    public void DoExit() {
        director.Play(fadeOutAnimation);
    }
}
