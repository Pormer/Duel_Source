using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SettingUITrigger : MonoBehaviour, KeyAction.IUIActions
{
    private bool _isSettingUI;
    private KeyAction _keyAction;
    public bool IsPenel {  get; set; }

    private void Awake()
    {
        _keyAction = new KeyAction();
        _keyAction.UI.SetCallbacks(this);
        _keyAction.UI.Enable();
    }

    public void OnSetting(InputAction.CallbackContext context)
    {
        if (context.performed && !IsPenel)
        {
            IsPenel = true;
            _isSettingUI = !_isSettingUI;
            GameManager.Instance.OnSettingUi?.Invoke(_isSettingUI);
        }
    }
}
