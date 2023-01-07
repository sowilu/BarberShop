using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimation : MonoBehaviour
{
    [SerializeField]
    private InputActionProperty _pinchAction;
    [SerializeField]
    private InputActionProperty _gripAction;
    
    [SerializeField] 
    private Animator _animator;
    

    
    void Update()
    {
        var triggerValue = _pinchAction.action.ReadValue<float>();
        var gripValue = _gripAction.action.ReadValue<float>();
        
        _animator.SetFloat("Trigger", triggerValue);
        _animator.SetFloat("Grip", gripValue);
    }
}
