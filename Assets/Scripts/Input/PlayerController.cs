using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.InputSystem.XInput;

public class ControlEvent : UnityEvent<EnumInput> {}

public class PlayerController : MonoBehaviour, TugOfWarControls.IGeneral_ControllerActions
{
    [Header("Controls Event")]
    [SerializeField]
    public ControlEvent controlEvent = new ControlEvent();

    

    // Awake is called before Start() when an object is created or when the level is loaded
    private void Awake()
    {
        // Set up our player actions in code
        // This class name is based on what you named your .inputactions asset
        //playerInput = new PlayerInput();

    }

    private void OnEnable()
    {
        // We need to enable our "Player" action map so Unity will listen for our input
        // playerInput.General_Controller.Enable();
    }

    private void OnDisable()
    {
        // Disable our action map
        // playerInput.General_Controller.Disable();
    }

    [SerializeField]
    public void OnUp(InputAction.CallbackContext ctx) {
        if(ctx.phase == InputActionPhase.Started)
            EmitInput(EnumInput.Up, controlEvent);
    }

    [SerializeField]
    public void OnDown(InputAction.CallbackContext ctx) {
        if(ctx.phase == InputActionPhase.Started)
            EmitInput(EnumInput.Down, controlEvent);
    }

    [SerializeField]
    public void OnLeft(InputAction.CallbackContext ctx) {
        if(ctx.phase == InputActionPhase.Started)
            EmitInput(EnumInput.Left, controlEvent);
    }

    [SerializeField]
    public void OnRight(InputAction.CallbackContext ctx) {
        if(ctx.phase == InputActionPhase.Started)
            EmitInput(EnumInput.Right, controlEvent);
    }

    [SerializeField]
    public void OnX(InputAction.CallbackContext ctx) {
        if(ctx.phase == InputActionPhase.Started)
            EmitInput(EnumInput.X, controlEvent);
    }

    [SerializeField]
    public void OnY(InputAction.CallbackContext ctx) {
        if(ctx.phase == InputActionPhase.Started)
            EmitInput(EnumInput.Y, controlEvent);
    }

    [SerializeField]
    public void OnA(InputAction.CallbackContext ctx) {
        if(ctx.phase == InputActionPhase.Started)
            EmitInput(EnumInput.A, controlEvent);
    }

    [SerializeField]
    public void OnB(InputAction.CallbackContext ctx) {
        if(ctx.phase == InputActionPhase.Started)
            EmitInput(EnumInput.B, controlEvent);
    }

    [SerializeField]
    public void OnLeftTrigger(InputAction.CallbackContext ctx) {
        if(ctx.phase == InputActionPhase.Started)
            EmitInput(EnumInput.LeftTrigger, controlEvent);
    }

    [SerializeField]
    public void OnRightTrigger(InputAction.CallbackContext ctx) {
        if(ctx.phase == InputActionPhase.Started)
            EmitInput(EnumInput.RightTrigger, controlEvent);
    }

    [SerializeField]
    public void OnRightBumper(InputAction.CallbackContext ctx) {
        if(ctx.phase == InputActionPhase.Started)
            EmitInput(EnumInput.RightBumper, controlEvent);
    }

    [SerializeField]
    public void OnLeftBumper(InputAction.CallbackContext ctx) {
        if(ctx.phase == InputActionPhase.Started)
            EmitInput(EnumInput.LeftBumper, controlEvent);
    }

    private bool stickWasFarEnoughLast = false;
    private EnumInput? lastStickAngle = null;
    [SerializeField]
    public void OnLeftStick(InputAction.CallbackContext ctx) {
        var stickVec = ctx.ReadValue<Vector2>();
        var farEnough = false;
        if(stickVec.magnitude > .55) {
            farEnough = true;
        }
        EnumInput? angle = InterpretStickAngle(stickVec);
        if(farEnough && (angle != lastStickAngle || (!stickWasFarEnoughLast))) {
            lastStickAngle = angle;
            EmitInput((EnumInput)angle, controlEvent);
        }
        stickWasFarEnoughLast = farEnough;
    }

    [SerializeField]
    public void OnTest(InputAction.CallbackContext ctx) {
        print("TEST");
    }
    private EnumInput? InterpretStickAngle(Vector2 iVec)
    {
        var angle = (Vector2.SignedAngle(iVec, Vector2.up) + 360) % 360f;
        //print(angle);
        EnumInput nextStickAngle;
        if (angle > 315 || angle <= 45)
        {
            nextStickAngle = EnumInput.Up;
        } else if (angle > 225)
        {
            nextStickAngle = EnumInput.Left;
        } else if (angle > 135)
        {
            nextStickAngle = EnumInput.Down;
        } else {
            nextStickAngle = EnumInput.Right;
        }
        return nextStickAngle;
    }

    private void EmitInput(EnumInput toEmit, ControlEvent eventParent)
    {
        eventParent.Invoke(toEmit);
    }
}
