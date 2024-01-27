using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.InputSystem.XInput;

public class ControlEvent : UnityEvent<EnumInput> {}

public struct InputHandlers 
{
    public Action<InputAction.CallbackContext> XInputHandler;
    public Action<InputAction.CallbackContext> YInputHandler;
    public Action<InputAction.CallbackContext> AInputHandler;
    public Action<InputAction.CallbackContext> BInputHandler;
    public Action<InputAction.CallbackContext> StickInputHandler;
}

public class PlayerController : MonoBehaviour
{
    [Header("Player Input")]
    public PlayerInput playerInput;

    [Header("Controls Event")]
    public ControlEvent controlEvent = new ControlEvent();

    // Awake is called before Start() when an object is created or when the level is loaded
    private void Awake()
    {
        // Set up our player actions in code
        // This class name is based on what you named your .inputactions asset
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        // Here we can subscribe functions to our
        // input actions to make code occur when
        // our input actions occur
        SubscribeInputActions();

        // We need to enable our "Player" action map so Unity will listen for our input
        playerInput.General_Controller.Enable();
    }

    private void OnDisable()
    {
        // Here we can unsubscribe our functions
        // from our input actions so our object
        // doesn't try to call functions after
        // it is destroyed
        UnsubscribeInputActions();

        // Disable our action map
        playerInput.General_Controller.Disable();
    }

    private InputHandlers inputHandlers = new InputHandlers();

    private InputHandlers CreateInputHandlers(ControlEvent cEvent) {
        return new InputHandlers{
            XInputHandler = (ctx) => PlayerController.EmitInput(EnumInput.X, cEvent),
            YInputHandler = (ctx) => PlayerController.EmitInput(EnumInput.Y, cEvent),
            AInputHandler = (ctx) => PlayerController.EmitInput(EnumInput.A, cEvent),
            BInputHandler = (ctx) => PlayerController.EmitInput(EnumInput.B, cEvent),
            StickInputHandler = (ctx) => PlayerController.EmitInput(PlayerController.InterpretStickAngle(ctx), cEvent)
        };
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
        eventParent.Invoke(toEmit);
    }

    private void SubscribeInputActions()
    {
        if(inputHandlers.XInputHandler == null) {
            inputHandlers = CreateInputHandlers(controlEvent);
        }
        playerInput.General_Controller.LeftStick.performed += inputHandlers.StickInputHandler;

        playerInput.General_Controller.X.performed += inputHandlers.XInputHandler;
        playerInput.General_Controller.A.performed += inputHandlers.AInputHandler;
        playerInput.General_Controller.B.performed += inputHandlers.BInputHandler;
        playerInput.General_Controller.Y.performed += inputHandlers.YInputHandler;


    }
    private void UnsubscribeInputActions()
    {
        if(inputHandlers.XInputHandler == null) {
            return;
        }

        playerInput.General_Controller.LeftStick.performed -= inputHandlers.StickInputHandler;

        playerInput.General_Controller.X.performed -= inputHandlers.XInputHandler;
        playerInput.General_Controller.A.performed -= inputHandlers.AInputHandler;
        playerInput.General_Controller.B.performed -= inputHandlers.BInputHandler;
        playerInput.General_Controller.Y.performed -= inputHandlers.YInputHandler;


    }


}
