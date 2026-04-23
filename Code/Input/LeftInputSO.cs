using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "SO/Input/Left")]
public class LeftInputSO : InputReaderSO, KeyAction.ILeftInputActions
{
    protected override void OnEnable()
    {
        base.OnEnable();
        _keyAction.LeftInput.SetCallbacks(this);
        //_keyAction.LeftInput.Enable();
        
        SceneManager.sceneLoaded += (scene, mode) => _keyAction.LeftInput.Enable();
        SceneManager.sceneUnloaded += scene => _keyAction.LeftInput.Disable();
        
        IsRight = false;
    }
    
    private void OnDisable()
    {
        _keyAction.LeftInput.Disable();
    }
}
