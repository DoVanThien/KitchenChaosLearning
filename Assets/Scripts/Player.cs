using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    //Events
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter SelectedCounter;
    }
    
    //Serialized Fields
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 30f;
    
    [SerializeField] private GameInput gameInput;
    [SerializeField] private Rigidbody rb;
    
    //Private Fields
    private bool _isWalking;
    private Vector2 _movePosition = Vector2.zero;

    private ClearCounter _selectedCounter;
    
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameInput.OnInteract += OnInteract;
    }


    private void Update()
    {
        _movePosition = gameInput.GetMovementVector();
    }

    void FixedUpdate()
    {
        HandleRotation(_movePosition);
        HandleMovement(_movePosition);
    }
    
    private void HandleRotation(Vector2 movePosition)
    {
        if (movePosition != Vector2.zero)
        {
            float angle = Mathf.Atan2(movePosition.x, movePosition.y) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(0, angle, 0);
            var targetRotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(targetRotation);
        }
    }
    
    private void HandleMovement(Vector2 movePosition)
    {
        var moveDir = new Vector3(movePosition.x, 0, movePosition.y);
        _isWalking = moveDir != Vector3.zero;
        Vector3 movement = moveDir * (moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + movement);
    }
    
    public bool IsWalking()
    {
        return _isWalking;
    }
        
    private void OnInteract(object sender, EventArgs e)
    {
        _selectedCounter?.Interact();
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.TryGetComponent(out ClearCounter clearCounter))
        {
            SetSelectedCounter(clearCounter);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.TryGetComponent(out ClearCounter clearCounter))
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(ClearCounter clearCounter)
    {
        _selectedCounter = clearCounter;
        
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            SelectedCounter = clearCounter
        });
    }
}
