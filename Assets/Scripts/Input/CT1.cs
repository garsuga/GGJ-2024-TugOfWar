using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Input")]
    private PlayerInput playerInput;
    private Vector2 movementInput;

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

    private void SubscribeInputActions()
    {
        // Here we can bind our input actions to functions
        playerInput.General_Controller.LeftStick.started += RightStickMovement;
        playerInput.General_Controller.LeftStick.performed += RightStickMovement;
        playerInput.General_Controller.LeftStick.canceled += RightStickMovement;

        playerInput.General_Controller.X.performed += XButtonPressed;
        playerInput.General_Controller.A.performed += AButtonPressed;
        playerInput.General_Controller.B.performed += BButtonPressed;


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

    }

    private void RightStickMovement(InputAction.CallbackContext context)
    {
        // Read in the Vector2 of our player input.
        movementInput = context.ReadValue<Vector2>();

   }

    private void XButtonPressed(InputAction.CallbackContext context)
    {
    }
    private void AButtonPressed(InputAction.CallbackContext context)
    {
    }
    private void BButtonPressed(InputAction.CallbackContext context)
    {
    }
}
