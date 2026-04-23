using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "SO/Input/Right")]
public class RightInputSO : InputReaderSO, KeyAction.IRightInputActions
{
    protected override void OnEnable()
    {
        base.OnEnable();
        _keyAction.RightInput.SetCallbacks(this);
        //_keyAction.RightInput.Enable();

        SceneManager.sceneLoaded += (scene, mode) => _keyAction.RightInput.Enable();
        SceneManager.sceneUnloaded += scene => _keyAction.RightInput.Disable();
        
        IsRight = true; 
    }

    private void OnDisable()
    {
        _keyAction.RightInput.Disable();
    }
}