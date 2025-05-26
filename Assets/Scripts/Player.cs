using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 30f;
    
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private Rigidbody _rigidbody;
    
    private bool isWalking;
    private Vector2 movePosition = Vector2.zero;

    private ClearCounter _clearCounter;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _gameInput.OnInteract += OnInteract;
    }


    private void Update()
    {
        movePosition = _gameInput.GetMovementVector();
    }

    void FixedUpdate()
    {
        HandleRotation(movePosition);
        HandleMovement(movePosition);
    }
    
    private void HandleRotation(Vector2 movePosition)
    {
        if (movePosition != Vector2.zero)
        {
            float angle = Mathf.Atan2(movePosition.x, movePosition.y) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(0, angle, 0);
            var targetRotation = Quaternion.Slerp(transform.rotation, rotation, _rotationSpeed * Time.fixedDeltaTime);
            _rigidbody.MoveRotation(targetRotation);
        }
    }
    
    private void HandleMovement(Vector2 movePosition)
    {
        var moveDir = new Vector3(movePosition.x, 0, movePosition.y);
        isWalking = moveDir != Vector3.zero;
        Vector3 movement = moveDir * (_moveSpeed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(_rigidbody.position + movement);
    }
        
    private void OnInteract(object sender, EventArgs e)
    {
        _clearCounter?.Interact();
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.TryGetComponent(out ClearCounter clearCounter))
        {
            _clearCounter = clearCounter;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.TryGetComponent(out ClearCounter clearCounter))
        {
            _clearCounter = null;
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
