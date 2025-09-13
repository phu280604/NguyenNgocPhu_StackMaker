using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region --- Unity Methods ---

    void Start()
    {
        OnInit();
    }

    void Update()
    {
        DetermineDirection();

        ChangeAnimation();
        CheckBrick();

        MoveHandle(); 
    }

    #endregion

    #region --- Methods ---

    // Initialize variables
    private void OnInit()
    {
        _eDirect = EDirection.NONE;

        _isMove = false;
        _isAdd = false;
    }

    // Get the normalized direction vector from input points
    private Vector2 GetDirection()
    {

        _input.GetStartInput(ref _sPoint);
        _input.GetEndInput(ref _ePoint);

        if (_ePoint == Vector2.zero || _isMove) return Vector2.zero;

        //Debug.Log("Start Point: " + _sPoint + " End Point: " + _ePoint);

        return (_ePoint - _sPoint).normalized;
    }

    // Reset input points
    private void ResetInput()
    {
        _sPoint = Vector2.zero;
        _ePoint = Vector2.zero;
    }

    // Calculate the direction based on input
    private void CalculateDirection()
    {
        Vector2 dirNor = GetDirection();

        if (dirNor == Vector2.zero || _isMove) return;

        //Debug.Log("Direction: " + dirNor);
        ResetInput();

        float intX = Mathf.Abs(dirNor.x);
        float intY = Mathf.Abs(dirNor.y);

        if(intX <= intY)
        {
            if (Mathf.Sign(dirNor.y) > 0)
                _eDirect = EDirection.FORWARD;
            else
                _eDirect = EDirection.BACKWARD;
        }
        else if (intX > intY)
        {
            if (Mathf.Sign(dirNor.x) > 0)
                _eDirect = EDirection.RIGHT;
            else
                _eDirect = EDirection.LEFT;
        }
    }

    // Determine the movement direction based on input
    private void DetermineDirection()
    {
        if (_isMove) return;

        CalculateDirection();
        switch (_eDirect)
        {
            case EDirection.NONE:
                _moveDir = Vector3.zero;
                break;
            case EDirection.FORWARD:
                _moveDir = Vector3.forward;
                break;
            case EDirection.BACKWARD:
                _moveDir = Vector3.back;
                break;
            case EDirection.LEFT:
                _moveDir = Vector3.left;
                break;
            case EDirection.RIGHT:
                _moveDir = Vector3.right;
                break;
        }

        if(_eDirect != EDirection.NONE)
        {
            _isMove = true;
            Vector3 dir = GetRotation(_eDirect);
            _rotation.Rotation(dir);
        }
    }

    // Check for collision with blocked bricks
    private void CheckBrick()
    {
        RaycastHit hit;
        Debug.DrawLine(transform.position, transform.position + _moveDir * BLOCK_DISTANCE, Color.red);
        if (!Physics.Raycast(transform.position, _moveDir, out hit, BLOCK_DISTANCE)) return;

        switch (hit.collider.tag)
        {
            case TagName.NORMAL_BRICK:
                _isAdd = true;
                break;
            case TagName.BLOCKED_BRICK:
                _eDirect = EDirection.NONE;
                _isMove = false;
                _isAdd = false;
                break;
            case TagName.END_BRICK:
                _eDirect = EDirection.NONE;
                _isMove = false;
                _isAdd = false;
                GameManager.Instance.NextLevel();
                break;
        }
    }

    private Vector3 GetRotation(EDirection dir, int angle = 60)
    {
        switch (dir)
        {
            
            case EDirection.FORWARD:
                return Vector3.down * angle * 2;
            case EDirection.BACKWARD:
                return Vector3.up * angle;
            case EDirection.LEFT:
                return Vector3.up * angle * 2;
            case EDirection.RIGHT:
                return Vector3.down * angle;
        }

        return Vector3.zero;
    }

    private void ChangeAnimation()
    {
        if(_isAdd && !_animator.GetBool(AnimationParaName.JUMP))
            _animator.SetInteger(AnimationParaName.COLLECTING_PARA, 1);
        else if(!_isAdd && !_animator.GetBool(AnimationParaName.IDLE))
            _animator.SetInteger(AnimationParaName.COLLECTING_PARA, 0);

    }

    // Move the player in the determined direction
    private void MoveHandle()
    {
        if(!_isMove) return;

        transform.Translate(_moveDir * Time.deltaTime * _speed);
    }

    #endregion

    #region --- Fields ---

    [Header("--- Const ---")]
    [SerializeField] private const float BLOCK_DISTANCE = 0.5f;

    [Header("--- GameObject ---")]
    [SerializeField] private GameObject _goSprite;

    [Header("--- Component ---")]
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlayerRotation _rotation;
    [SerializeField] private Animator _animator;

    [Header("--- Vector ---")]
    [SerializeField] private Vector2 _sPoint;
    [SerializeField] private Vector2 _ePoint;

    [SerializeField] private Vector3 _moveDir;

    [Header("--- Enum ---")]
    private EDirection _eDirect;

    [Header("--- Bool ---")]
    [SerializeField] private bool _isMove;
    private bool _isAdd;

    [Header("--- Float ---")]
    [SerializeField] private float _speed;

    #endregion
}
