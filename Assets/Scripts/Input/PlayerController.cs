using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.InputSystem.XInput;

public class ControlEvent : UnityEvent<EnumInput> {}

public class PlayerController : MonoBehaviour, PlayerInput.IGeneral_ControllerActions
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
    public void OnX(InputAction.CallbackContext ctx) {
        PlayerController.EmitInput(EnumInput.X, controlEvent);
    }

    [SerializeField]
    public void OnY(InputAction.CallbackContext ctx) {
        PlayerController.EmitInput(EnumInput.Y, controlEvent);
    }

    [SerializeField]
    public void OnA(InputAction.CallbackContext ctx) {
        PlayerController.EmitInput(EnumInput.A, controlEvent);
    }

    [SerializeField]
    public void OnB(InputAction.CallbackContext ctx) {
        PlayerController.EmitInput(EnumInput.B, controlEvent);
    }

    [SerializeField]
    public void OnLeftTrigger(InputAction.CallbackContext ctx) {
        PlayerController.EmitInput(EnumInput.Left, controlEvent);
    }

    [SerializeField]
    public void OnLeftStick(InputAction.CallbackContext ctx) {
        PlayerController.EmitInput(PlayerController.InterpretStickAngle(ctx), controlEvent);
    }

    [SerializeField]
    public void OnTest(InputAction.CallbackContext ctx) {
        print("TEST");
    }

    private static EnumInput InterpretStickAngle(InputAction.CallbackContext context)
    {
        var iVec = context.ReadValue<Vector2>();
        var angle = Vector2.Angle(Vector2.up, iVec);
        if (angle > 315 || angle <= 45)
        {
            return EnumInput.Up;
        }
        if (angle > 225)
        {
            return EnumInput.Left;
        }
        if (angle > 135)
        {
            return EnumInput.Down;
        }
        return EnumInput.Right;
    }

    private static void EmitInput(EnumInput toEmit, ControlEvent eventParent)
    {
        print(toEmit);
        eventParent.Invoke(toEmit);
    }
}
