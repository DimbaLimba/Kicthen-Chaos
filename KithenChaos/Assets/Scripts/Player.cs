using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKichenObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler OnPickedSomethink;
    public event EventHandler<OnSelectedCounterEventArgs> OnSelectedCounterChanger;
    public class OnSelectedCounterEventArgs : EventArgs
    {
        public BaseCounter selecterCounter;
    }
    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private LayerMask _countersLayerMask;
    [SerializeField] private Transform _kichenObjectHoldPoint;

    private float _rotationSpeed = 10f;
    private float _playerRadius = 0.7f;
    private float _playerHeigh = 2f;
    private bool _isWolking;
    private Vector3 _lastInteractDir;
    private BaseCounter _selectedCounter;
    private KichenObject _kichenObject;

    private void Awake()
    {
        Instance = this;    
    }

    private void Start()
    {
        _gameInput.OnInteractActive += _gameInput_OnInteractActive;
        _gameInput.OnInteractAlternateActive += _gameInput_OnInteractAlternateActive;
    }

    private void _gameInput_OnInteractAlternateActive(object sender, EventArgs e)
    {
        if (!KichenGameManager.Instance.IsGamePlaying()) return;

        if (_selectedCounter != null)
        {
            _selectedCounter.InteractAlternate(this);
        }
    }

    private void _gameInput_OnInteractActive(object sender, System.EventArgs e)
    {
        if (!KichenGameManager.Instance.IsGamePlaying()) return;

        if (_selectedCounter != null)
        {
            _selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = _gameInput.GetMoveningVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            _lastInteractDir = moveDir;
        }

        if (Physics.Raycast(transform.position, _lastInteractDir, out RaycastHit raycastHit, _playerHeigh, _countersLayerMask)) 
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (_selectedCounter != baseCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    public bool IsWalking()
    {
        return _isWolking;
    }

    private void HandleMovement()
    {
        Vector2 inputVector = _gameInput.GetMoveningVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        float moveDistance = _moveSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeigh, _playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeigh, _playerRadius, moveDirX, moveDistance);

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * _playerHeigh, _playerRadius, moveDirX, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }

        }

        if (canMove)
        {
            transform.position += moveDir * _moveSpeed * Time.deltaTime;
        }

        _isWolking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, _rotationSpeed * Time.deltaTime);
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        _selectedCounter = selectedCounter;

        OnSelectedCounterChanger?.Invoke(this, new OnSelectedCounterEventArgs {
            selecterCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectFolowTransform()
    {
        return _kichenObjectHoldPoint;
    }

    public void SetKichenObject(KichenObject kichenObject)
    {
        _kichenObject = kichenObject;

        if (kichenObject != null)
        {
            OnPickedSomethink?.Invoke(this, EventArgs.Empty);
        }
    }

    public KichenObject GetKichenObject()
    {
        return _kichenObject;
    }

    public void ClearKichenObject()
    {
        _kichenObject = null;
    }

    public bool HasKichenObject()
    {
        return _kichenObject != null;
    }
}
