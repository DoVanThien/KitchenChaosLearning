using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteract;
    private PlayerInputAction _playerInputAction;

    private void Awake()
    {
        _playerInputAction = new PlayerInputAction();
        _playerInputAction.Enable();
        
        _playerInputAction.Player.Interact.performed += OnHandleInteract;
    }

    private void OnHandleInteract(InputAction.CallbackContext ctx)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }


    public Vector2 GetMovementVector()
    {
        Vector2 moveInput = _playerInputAction.Player.Move.ReadValue<Vector2>();
        return moveInput;
    }
    
    private void OnDestroy()
    {
        _playerInputAction.Player.Interact.performed -= OnHandleInteract;
        _playerInputAction.Disable();
    }
}
