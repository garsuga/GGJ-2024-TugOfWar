using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Input")]
    private PlayerInput playerInput;


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

    private EnumInput InterpretStickAngle(InputAction.CallbackContext context)
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

    private static void EmitInput(EnumInput toEmit, object whereToSend)
    {

    }

    private Action<InputAction.CallbackContext> XInputHandler = (ctx) => EmitInput(EnumInput.X, null);
    private Action<InputAction.CallbackContext> YInputHandler = (ctx) => EmitInput(EnumInput.Y, null);
    private Action<InputAction.CallbackContext> AInputHandler = (ctx) => EmitInput(EnumInput.A, null);
    private Action<InputAction.CallbackContext> BInputHandler = (ctx) => EmitInput(EnumInput.B, null);

    private void SubscribeInputActions()
    {
        // Here we can bind our input actions to functions
        playerInput.General_Controller.LeftStick.performed += (ctx) => EmitInput(In(ctx));

        playerInput.General_Controller.X.performed += XInputHandler;
        playerInput.General_Controller.A.performed += AInputHandler;
        playerInput.General_Controller.B.performed += BInputHandler;
        playerInput.General_Controller.Y.performed += YInputHandler;


    }
    private void UnsubscribeInputActions()
    {
        // It is important to unbind and actions that we bind
        // when our object is destroyed, or this can cause issues
        playerInput.General_Controller.LeftStick.started -= RightStickMovement;
        playerInput.General_Controller.LeftStick.performed -= RightStickMovement;
        playerInput.General_Controller.LeftStick.canceled -= RightStickMovement;

        playerInput.General_Controller.X.performed -= XButtonPressed;
        playerInput.General_Controller.A.performed -= AButtonPressed;
        playerInput.General_Controller.B.performed -= BButtonPressed;
        playerInput.General_Controller.Y.performed -= YButtonPressed;


    }


}