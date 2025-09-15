using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region --- Unity Methods ---

    void Start()
    {
        //Time.timeScale = 0.025f;
        OnInit();
    }

    private void Update()
    {
        DetermineDirection();

        ChangeAnimation();
        MoveHandle();
    }

    private void FixedUpdate()
    {
        CheckBrick();
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
        if (!_isMove) return;

        RaycastHit hit;
        if (!Physics.Raycast(transform.position, _moveDir, out hit, BLOCK_DISTANCE) || !_isMove) return;

        if(hit.collider.tag != TagName.BLOCKED_BRICK && hit.collider.tag != TagName.END_BRICK)
        {
            _isAdd = hit.collider.tag != TagName.BRIDGE_BRICK;
            _tarPos = hit.collider.transform.position;

            if(!_isAdd)
                DecollectBrick(hit.collider.GetComponent<DecollectingBrick>());
            else
            {
                CollectingBrick colBrick = hit.collider.GetComponent<CollectingBrick>();
                _isAdd = !colBrick.IsHide;
                CollectBrick(colBrick);
            }
        }
        else if(hit.collider.tag == TagName.BLOCKED_BRICK)
        {
            _eDirect = EDirection.NONE;
            _isMove = false;
            _isAdd = false;
            _tarPos = hit.collider.transform.position - _moveDir;
        }
        else
            GameManager.Instance.NextLevel();
    }

    public void CollectBrick(CollectingBrick clBrick, bool isFirst = true)
    {
        if(clBrick.IsHide) return;

        if (isFirst)
            _goSprite.transform.position += Vector3.up * 0.2f;

        float newY = _goSprite.transform.position.y - transform.position.y;
        _listBricks.Add(clBrick.CollectBrick(newY, transform));
    }

    public void DecollectBrick(DecollectingBrick dclBrick)
    {
        if (dclBrick.IsShow) return;

        _goSprite.transform.position -= Vector3.up * 0.2f;
        dclBrick.DecollectBrick(_listBricks);

        if(_listBricks.Count <= 0)
        {
            _eDirect = EDirection.NONE;
            _isMove = false;
            return;
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
        if(!_isMove || _tarPos == Vector3.zero) return;

        _tarPos = new Vector3(_tarPos.x, transform.position.y, _tarPos.z);
        transform.position = Vector3.MoveTowards(transform.position, _tarPos, Time.deltaTime * _speed);
    }

    #endregion

    #region --- Properties ---

    public PlayerRotation RotationComp { get => _rotation; }

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
    [SerializeField] private Vector3 _tarPos;

    [Header("--- Enum ---")]
    private EDirection _eDirect;

    [Header("--- GameObjects ---")]
    [SerializeField] private List<GameObject> _listBricks;

    [Header("--- Bool ---")]
    [SerializeField] private bool _isMove;
    private bool _isAdd;

    [Header("--- Float ---")]
    [SerializeField] private float _speed;

    #endregion
}
