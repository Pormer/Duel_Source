using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/Input/Online")]
public class OnlineInputSO : InputReaderSO, KeyAction.IRightInputActions, KeyAction.ILeftInputActions
{
    protected override void OnEnable()
    {
        base.OnEnable();
        _keyAction.RightInput.SetCallbacks(this);
        _keyAction.LeftInput.SetCallbacks(this);
        _keyAction.LeftInput.Enable();
        _keyAction.RightInput.Enable();
    }

    private void OnDisable()
    {
        _keyAction.LeftInput.Disable();
        _keyAction.RightInput.Disable();
    }

    void KeyAction.IRightInputActions.OnShoot(InputAction.CallbackContext context)
    {
        base.OnShoot(context);
    }

    void KeyAction.IRightInputActions.OnSkill(InputAction.CallbackContext context)
    {
        base.OnSkill(context);
    }

    void KeyAction.IRightInputActions.OnBarrier(InputAction.CallbackContext context)
    {
        base.OnBarrier(context);
    }

    void KeyAction.ILeftInputActions.OnMovementUp(InputAction.CallbackContext context)
    {
        base.OnMovementUp(context);
    }
    
    void KeyAction.ILeftInputActions.OnMovementDown(InputAction.CallbackContext context)
    {
        base.OnMovementDown(context);
    }
    
    void KeyAction.ILeftInputActions.OnMovementLeft(InputAction.CallbackContext context)
    {
        base.OnMovementLeft(context);
    }

    void KeyAction.ILeftInputActions.OnMovementRight(InputAction.CallbackContext context)
    {
        base.OnMovementRight(context);
    }
}
