using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputAction _playerInputAction;

    private void Awake()
    {
        _playerInputAction = new PlayerInputAction();
        _playerInputAction.Enable();
    }

    public Vector2 GetMovementVector()
    {
        Vector2 moveInput = _playerInputAction.Player.Move.ReadValue<Vector2>();
        return moveInput;
    }
}
